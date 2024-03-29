﻿//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2021 Clay Lipscomb
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace Odapter {
#region Classes Supplemental to Hydrator
    /// <summary>
    /// Attribute used by Hydrator when mapping by position
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HydratorMapAttribute : Attribute { public Int32 Position { get; set; } }

    /// <summary>
    /// An Oracle column name and type
    /// </summary>
    public class Column {
        public readonly string ColumnName;
        public readonly Type ColumnType; // Oracle type (not .NET)
        public readonly String OracleDataTypeName; // OracleDbType Enum values
        public Column(string columnName, Type columnType, String oracleDataTypeName) {
            ColumnName = columnName;
            ColumnType = columnType;
            OracleDataTypeName = oracleDataTypeName;
        }
    }

    /// <summary>
    /// A mapping from a db column to a C# class's property or field
    /// </summary>
    public class ColumnMapping {
        public readonly Column Column;
        public readonly PropertyInfo Property;  // used if column maps to a property
        public readonly FieldInfo Field;        // used if columns maps to field
        public readonly bool IsMappedToProperty;
        public readonly Type MemberUnderlyingType;
        public readonly bool IsRoundOracleDecimalToCSharpDecimal;

        /// <summary>
        /// Generically wraps the mapped member
        /// </summary>
        public MemberInfo Member { get { return IsMappedToProperty ? (MemberInfo)Property : (MemberInfo)Field; } }

        public ColumnMapping(Column column, MemberInfo member) {
            Column = column;
            Property = member.MemberType == MemberTypes.Property ? (PropertyInfo)member : null;
            Field = member.MemberType == MemberTypes.Field ? (FieldInfo)member : null;
            IsMappedToProperty = member.MemberType == MemberTypes.Property;
            MemberUnderlyingType = IsMappedToProperty
                ? Nullable.GetUnderlyingType(Property.PropertyType) ?? Property.PropertyType
                : Nullable.GetUnderlyingType(Field.FieldType) ?? Field.FieldType;
            IsRoundOracleDecimalToCSharpDecimal = MemberUnderlyingType.Equals(typeof(System.Decimal))
                && Column.ColumnType.Equals(typeof(OracleDecimal));
        }
    }

    /// <summary>
    /// Used to uniquely identify a ResultSetMapping so that it can be stored in a dictionary. The C# type name,
    /// and ordered list of each column (name and type) in a reader (result set) is required to uniquely identify 
    /// a mapping. Using the column type might be overkill but we are using it for now.
    /// </summary>
    public class ResultSetMappingIdentity : IEquatable<ResultSetMappingIdentity> {
        public readonly List<Column> ResultSetColumns;
        public readonly string CSharpTypeName;
        private readonly int hash;
        private bool _identifyResultSetWithColumnType = false;

        internal static ResultSetMappingIdentity CreateInstance<T>(OracleDataReader reader) {
            return new ResultSetMappingIdentity(reader, typeof(T).FullName);
        }

        private ResultSetMappingIdentity(OracleDataReader reader, string csharpTypeName) {
            ResultSetColumns = Hydrator.GetReaderColumnTypes(reader);
            CSharpTypeName = csharpTypeName;
            hash = BuildHash(); // do this now so we only do it once
        }

        public override int GetHashCode() {
            return hash;
        }

        private int BuildHash() {
            unchecked { // ensure overflow won't raise exception
                int hash = 17; // prime
                hash = (hash * 31) + CSharpTypeName.GetHashCode(); // hash the C# type *name*, not the type
                int colCount = ResultSetColumns.Count;
                hash = (hash * 31) + colCount; // mix in the column count
                // add name and type for each column
                foreach(Column col in ResultSetColumns) {
                    string name = col.ColumnName;
                    hash = (hash * 31) + (name == null ? 0 : name.GetHashCode());

                    if (_identifyResultSetWithColumnType) {
                        string type = col.ColumnType.Name;
                        hash = (hash * 31) + (type == null ? 0 : type.GetHashCode());
                    }
                }
                return hash;
            }
        }

#region IEquatable methods
        public override bool Equals(object obj) {
            return Equals(obj as ResultSetMappingIdentity);
        }

        /// <summary>
        /// Determine equality based on C# type, number of columns, and both the name and type 
        /// of each column
        /// </summary>
        /// <param name="itemComparand"></param>
        /// <returns></returns>
        public bool Equals(ResultSetMappingIdentity itemComparand) {
            if (    !CSharpTypeName.Equals(itemComparand.CSharpTypeName)
                 || !ResultSetColumns.Count.Equals(itemComparand.ResultSetColumns.Count)) return false;

            // compare each column name and type
            for (int c = 0; c < ResultSetColumns.Count; c++) {
                if (    !ResultSetColumns[c].ColumnName.Equals(itemComparand.ResultSetColumns[c].ColumnName)   
                     || ( _identifyResultSetWithColumnType 
                        && !ResultSetColumns[c].ColumnType.Name.Equals(itemComparand.ResultSetColumns[c].ColumnType.Name)) ) return false;
            }
            return true;
        }
        #endregion // IEquatable methods
    } // ResultSetMappingIdentity

    /// <summary>
    /// A cache-ready element for mapping all columns in a result set to a C# class
    /// </summary>
    public class ResultSetMappingCacheItem : IComparable<ResultSetMappingCacheItem> {
        public readonly List<ColumnMapping> Mappings;   // this is everything we need to map
        public readonly string CSharpTypeName;          // stored for sorting and diagnostics
        private long hitCount = 0;

        public long HitCount {
            get { return Interlocked.CompareExchange(ref hitCount, 0, 0); }
        }

        internal void RecordHit() { Interlocked.Increment(ref hitCount); }

        public int CompareTo(object obj) {
            return CompareTo(obj as ResultSetMappingCacheItem);
        }

        public int CompareTo(ResultSetMappingCacheItem item) {
            return this.CSharpTypeName.CompareTo(item.CSharpTypeName);        
        }
        
        public static ResultSetMappingCacheItem CreateInstance<T>(List<ColumnMapping> mappings) {
            return new ResultSetMappingCacheItem(mappings, typeof(T).FullName);
        }

        private ResultSetMappingCacheItem(List<ColumnMapping> mappings, string csharpTypeName)  {
            Mappings = mappings;
            CSharpTypeName = csharpTypeName;
        }
    }
#endregion // Classes Supplemental to Hydrator

#region Hydrator
    /// <summary>
    /// Hydrator is a static class that provides the following functionality:
    /// 1) build a mapping between an Oracle result set and a C# class either a) by name or b) by attribute position
    /// 2) store mappings for reuse in threadsafe cache and provides on request
    /// 3) use a mapping to hydrate a C# object list from an Oracle result set
    /// 4) dynamically create and hydrate a DataTable from an Oracle result set 
    /// </summary>
    public static class Hydrator {
#region Public properties
        public static bool CachingEnabled { get { return _cachingEnabled; } }
#endregion

        private const string NOT_AVAILABLE = "n/a";

#region Private fields
        // any string value that possibly be interpreted as a TRUE
        private static String[] _stringValuesEquivalentToBooleanTrue = new String[] { "Y", "YES", "T", "TRUE", "1" };
        private static bool _cachingEnabled = true;

        // four types of caches; thread safe without locking
        private static readonly ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> _mappingByPositionCache
            = new ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem>();
        private static readonly ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> _mappingByNameCache
            = new ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem>();
        private static readonly ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> _mappingByPositionFlexibleCache
            = new ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem>();
        private static readonly ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> _mappingByNameFlexibleCache
            = new ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem>();
#endregion

#region Cache-related methods
        /// <summary>
        /// Store a result set mapping in a cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="resultSetMapping"></param>
        private static void SetMappingCache<T>(ref ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> cache,
                                            ResultSetMappingIdentity key, List<ColumnMapping> resultSetMapping) {
            if (!_cachingEnabled) return; // short circuit if caching if disabled
            cache[key] = ResultSetMappingCacheItem.CreateInstance<T>(resultSetMapping);
        }

        /// <summary>
        /// Retrieve a result set mapping from a cache if it exists
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="resultSetMapping"></param>
        /// <returns></returns>
        private static bool TryGetMappingCache(ref ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> cache,
                                                ResultSetMappingIdentity key, out List<ColumnMapping> resultSetMapping) {
            resultSetMapping = null;
            if (!_cachingEnabled) return false; // short circuit if caching if disabled
            ResultSetMappingCacheItem cacheItem;
            if (cache.TryGetValue(key, out cacheItem)) {
                cacheItem.RecordHit();
                resultSetMapping = cacheItem.Mappings;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return a counts of all the cached queries
        /// </summary>
        /// <returns></returns>
        public static void GetCachedMappingCounts(out int mappingByPositionCacheCount, out int mappingByNameCacheCount,
                                                    out int mappingByPositionFlexibleCacheCount, out int mappingByNameFlexibleCacheCount) {

            mappingByPositionCacheCount = _mappingByPositionCache.Count;
            mappingByNameCacheCount = _mappingByNameCache.Count;
            mappingByPositionFlexibleCacheCount = _mappingByPositionFlexibleCache.Count;
            mappingByNameFlexibleCacheCount = _mappingByNameFlexibleCache.Count;
        }

        /// <summary>
        /// Return all caches
        /// </summary>
        /// <param name="mappingByPositionCache"></param>
        /// <param name="mappingByNameCache"></param>
        /// <param name="mappingByPositionFlexibleCache"></param>
        /// <param name="mappingByNameFlexibleCache"></param>
        public static void GetAllCaches(
            out ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> mappingByPositionCache,
            out ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> mappingByNameCache,
            out ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> mappingByPositionFlexibleCache,
            out ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> mappingByNameFlexibleCache
            ) {
            mappingByPositionCache = _mappingByPositionCache;
            mappingByNameCache = _mappingByNameCache;
            mappingByPositionFlexibleCache = _mappingByPositionFlexibleCache;
            mappingByNameFlexibleCache = _mappingByNameFlexibleCache;
        }

        public static void ClearAllCaches() {
            _mappingByPositionCache.Clear();
            _mappingByNameCache.Clear();
            _mappingByPositionFlexibleCache.Clear();
            _mappingByNameFlexibleCache.Clear();
        }

        public static void EnableCaching() { 
            _cachingEnabled = true; 
        }

        public static void DisableCaching() {
            _cachingEnabled = false;
            ClearAllCaches();
        }
#endregion
        // TBD
        private static T ConvertObject<T>(object obj) { 
            return (T)Convert.ChangeType(obj, typeof(T)); 
        }
#region Generics setter methods
        /// <summary>
        /// Set an object's property with a given value
        /// </summary>
        /// <param name="obj">Object with property</param>
        /// <param name="property">Property to be set on object</param>
        /// <param name="value">Value to be assigned to property</param>
        private static void SetObjectProperty(Object obj, PropertyInfo property, Object value) {
            // first, check for null value
            if (value == null) {
                property.SetValue(obj, null, null);
            // Oracle booleans cannot be returned to .NET. But we can convert some other Oracle type to a 
            //  C# boolean if the object so chooses by creating the C# boolean property. (Experimental)
            } else if (property.PropertyType == typeof(System.Boolean)) {
                property.SetValue(obj, _stringValuesEquivalentToBooleanTrue.Contains(value.ToString().Trim().ToUpper()), null);
            } else {
                Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                property.SetValue(obj, Convert.ChangeType(value, type), null);
            }
        }

        /// <summary>
        /// Set an object's field with a given value
        /// </summary>
        /// <param name="obj">Object with field</param>
        /// <param name="property">Field to be set on object</param>
        /// <param name="value">Value to be assigned to field</param>
        private static void SetObjectField(Object obj, FieldInfo field, Object value) {
            // first, check for null value
            if (value == null) {
                field.SetValue(obj, null);
            // Oracle booleans cannot be returned to .NET. But we can convert some other Oracle type to a 
            //  C# boolean if the object author so chooses by creating the C# boolean property. (Experimental)
            } else if (field.FieldType == typeof(System.Boolean)) {
                field.SetValue(obj, _stringValuesEquivalentToBooleanTrue.Contains(value.ToString().Trim().ToUpper()));
            } else {
                Type type = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
                field.SetValue(obj, Convert.ChangeType(value, type));
            }
        }

        /// <summary>
        /// Set an object's settable member (i.e., a field or property) with a given value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        private static void SetObjectMember(Object obj, MemberInfo member, Object value) {
            if (member.MemberType == MemberTypes.Field)
                SetObjectField(obj, (FieldInfo)member, value);
            else if (member.MemberType == MemberTypes.Property)
                SetObjectProperty(obj, (PropertyInfo)member, value);
        }
#endregion // Generics setter methods
        /// <summary>
        /// Create dictionary of column name and Oracle type of all columns in reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<Column> GetReaderColumnTypes(OracleDataReader reader) {
            List<Column> cols = new List<Column>();
            for (int c = 0; c < reader.FieldCount; c++) {
                cols.Add(new Column(reader.GetName(c), reader.GetProviderSpecificFieldType(c), reader.GetDataTypeName(c)));
            }
            return cols;
        }

        /// <summary>
        /// Build list of column to property/field mappings for a given C# class type and a data reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="mapByPosition"></param>
        /// <param name="allowUnmappedColumns"></param>
        /// <returns></returns>
        private static List<ColumnMapping> BuildMappings<T>(OracleDataReader reader, bool mapByPosition, bool allowUnmappedColumns) {
            // extract all settable public properties of the type (includes inherited)
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // if mapping by name, extract all settable protected and private fields (includes inherited)
            FieldInfo[] fields = null;
            if (!mapByPosition) fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // when mapping by position, filter out properties that do not have a map position set in the attribute
            if (mapByPosition) {
                properties = properties.Select(x => new {
                    Property = x,
                    Attribute = (HydratorMapAttribute)Attribute.GetCustomAttribute(x, typeof(HydratorMapAttribute))
                })
                    .Where(x => x.Attribute != null && x.Attribute.Position >= 0).OrderBy(x => x.Attribute.Position) // important to order by position
                    .Select(x => x.Property).ToArray();

                // Never allow the number of properties with the position attribute to be greater than the column count. This means the BO is looking for
                //  a column that is not there.
                if (properties.Length > reader.FieldCount) {
                    throw new Exception("Hydrator.BuildMappings<T>() - number of settable properties with a position attribute in " + typeof(T).FullName
                        + "(" + properties.Length + ") is greater than the number of columns in the reader(" + reader.FieldCount + ").");

                // If not allowed, the number of properties with the position attribute cannot be less than the column count, meaning no column should be ignored.
                } else if (!allowUnmappedColumns && properties.Length < reader.FieldCount) {
                    throw new Exception("Hydrator.BuildMappings<T>() - number of settable properties with a position attribute in " + typeof(T).FullName
                        + "(" + properties.Length + ") is less than the number of columns the reader(" + reader.FieldCount + "), meaning a column has been unmapped.");
                }
            }

            List<ColumnMapping> mappings = new List<ColumnMapping>(); // holds all column mappings for a result set
            for (int c = 0; c < reader.FieldCount; c++) { // loop reader columns
                PropertyInfo property;

                // map by position works only with properties (not fields)
                if (mapByPosition) {
                    if (c > properties.Length - 1)
                        break; // there are more columns than map properties, so our mapping is complete
                    else
                        property = properties[c]; // get the next property to be mapped

                    // Always check property position against column position at this point since the properties are sorted. If they 
                    //  don't match, something is definitely wrong in the attribute settings.
                    int propertyMapPosition = ((HydratorMapAttribute)Attribute.GetCustomAttribute(property, typeof(HydratorMapAttribute))).Position;
                    if (c != propertyMapPosition) {
                        throw new Exception("Hydrator.BuildMappings<T>() - property map position mismatch with reader columns near property position " + propertyMapPosition.ToString()
                            + " on class " + typeof(T).FullName + "." + " Check for duplicate or missing position values on properties.");
                    }

                    // valid property found, add completed mapping to our list
                    mappings.Add(new ColumnMapping(new Column(reader.GetName(c), reader.GetProviderSpecificFieldType(c), reader.GetDataTypeName(c)), property));

                // mapping by name
                } else { 
                    // look first for an _underscorePrefixedCamelCase field, and then a camelCase field (both are non-public)
                    FieldInfo field = Array.Find(fields, f => f.Name == CaseConverter.ConvertSnakeCaseToCamelCasePrefixedWithUnderscore(reader.GetName(c)))
                                   ?? Array.Find(fields, f => f.Name == CaseConverter.ConvertSnakeCaseToCamelCase(reader.GetName(c)));
                    if (field != default(FieldInfo)) {
                        // valid field found, add completed column mapping to our list
                        mappings.Add(new ColumnMapping(new Column(reader.GetName(c), reader.GetProviderSpecificFieldType(c), reader.GetDataTypeName(c)), field));
                    } else { // otherwise, look for a PascalCase property (public) since field not found
                        property = Array.Find(properties, p => p.Name == CaseConverter.ConvertSnakeCaseToPascalCase(reader.GetName(c)));
                        if (property != default(PropertyInfo)) { // this is equivalent to a "not null" compare
                            // valid property found, add completed mapping to our list
                            mappings.Add(new ColumnMapping(new Column(reader.GetName(c), reader.GetProviderSpecificFieldType(c), reader.GetDataTypeName(c)), property));
                        } else {
                            // a property does not exist, so the column has neither property nor field to map to
                            if (allowUnmappedColumns) {
                                continue; // unmapped column will be ignored - a "silent failed mapping"
                            } else {
                                throw new Exception("Hydrator.BuildMappings() - Could not find an _underscorePrefixedCamelCase non-public field, "
                                    + "a camelCase non-public field, nor a PascalCase public property on " + typeof(T).FullName + " for column " + reader.GetName(c));
                            }
                        }
                    } // if field not found
                } // mapping by name
            } // loop reader columns

            return mappings;
        }

        /// <summary>
        /// Get the mappings for a given type and data reader. Caches in appropriate cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="mapByPosition"></param>
        /// <param name="allowUnmappedColumns"></param>
        /// <returns></returns>
        private static List<ColumnMapping> GetMappings<T>(OracleDataReader reader, bool mapByPosition, bool allowUnmappedColumns) {

            // determine which of the four caches should be used 
            ConcurrentDictionary<ResultSetMappingIdentity, ResultSetMappingCacheItem> mappingCache =
                (mapByPosition
                ? (allowUnmappedColumns ? _mappingByPositionFlexibleCache : _mappingByPositionCache)
                : (allowUnmappedColumns ? _mappingByNameFlexibleCache : _mappingByNameCache));

            // a unique id can be derived from the C# type and result set alone
            ResultSetMappingIdentity mappingKey = ResultSetMappingIdentity.CreateInstance<T>(reader);

            List<ColumnMapping> mappings;
            if (!TryGetMappingCache(ref mappingCache, mappingKey, out mappings)) {
                // if they are not in cache, then build mappings now and cache them
                mappings = BuildMappings<T>(reader, mapByPosition, allowUnmappedColumns);
                SetMappingCache<T>(ref mappingCache, mappingKey, mappings);
            }
            return mappings;
        }
#region Result set readers
        /// <summary>
        /// Return a generic object fetched from a data reader that has been advanced (read) to a row
        /// </summary>
        /// <returns></returns>
        private static T ReadResultRow<T>(OracleDataReader reader, List<ColumnMapping> mappings) where T : new() {
            Object value;

            // create a DTO instance with properties/fields set from respective columns based on mappings
            T obj = new T(); // create new instance
            foreach (ColumnMapping m in mappings) {
                if (reader.IsDBNull(reader.GetOrdinal(m.Column.ColumnName)))
                    value = null;
                else if (m.IsRoundOracleDecimalToCSharpDecimal) // an OracleDecimal (sig dig 38) has to be rounded to fit in C# Decimal (sig dig 28)
                    value = (Decimal?)OracleDecimal.SetPrecision(reader.GetOracleDecimal(reader.GetOrdinal(m.Column.ColumnName)), 28);
                else if (m.MemberUnderlyingType.Namespace.Equals("Oracle.ManagedDataAccess.Types"))
                    value = reader.GetOracleValue(reader.GetOrdinal(m.Column.ColumnName));
                else if (m.MemberUnderlyingType.Equals(typeof(DateTimeOffset)))
                    value = DateTimeOffset.Parse(reader.GetValue(reader.GetOrdinal(m.Column.ColumnName)).ToString());
                else
                    value = reader.GetValue(reader.GetOrdinal(m.Column.ColumnName));
                SetObjectMember(obj, m.Member, value);
            }
            return obj;
        }

        /// <summary>
        /// Return a generic object fetched from a data reader
        /// </summary>
        /// <typeparam name="T">Type of list to create. Class type must have properties/fields that map to reader columns based on 
        ///     mapping mode.</typeparam>
        /// <param name="reader">A reader already prepared for fetching result set and positioned on the row by a Read()</param>
        /// <param name="mapByPosition">Map by position will map a column to a public property based on their shared sequential position 
        /// in the class T and reader, respectively. A property position is defined by decorating it with HydratorMapAttribute and setting
        /// the attribute's Position property. (Undecorated properties are ignored.) Otherwise, map by name (default) will attempt to
        /// map from an underscore_delimited column via translation to an _underscorePrefixedCamelCase non-public instance field, a camelCase 
        /// non-public instance field, or PascalCase public instance property (searching in this order). Both mapping modes assume the 
        /// property's C# type and column's Oracle type are compatible.</param>
        /// <param name="allowUnmappedColumns">If true with map by position, the number of reader columns can be more than the 
        /// number of positioned properties, permitting an unmapped column. If true with map by name, an appropriate field or 
        /// property is not required for each column, permitting an unmapped column. If false, an exception will occur if 
        /// all reader columns cannot be mapped to a property.</param>
        /// <returns></returns>
        public static T ReadResultRow<T>(OracleDataReader reader, bool mapByPosition = false, bool allowUnmappedColumns = false) where T : new() {
            return ReadResultRow<T>(reader, GetMappings<T>(reader, mapByPosition, allowUnmappedColumns));
        }

        /// <summary>
        /// Return a generic list of objects fetched from a data reader
        /// </summary>
        /// <typeparam name="T">Type of list to create. Class type must have properties/fields that map to reader columns based on 
        ///     mapping mode.</typeparam>
        /// <param name="reader">A reader already prepared for fetching result set</param>
        /// <param name="mapByPosition">Map by position will map a column to a public property based on their shared sequential position 
        /// in the class T and reader, respectively. A property position is defined by decorating it with HydratorMapAttribute and setting
        /// the attribute's Position property. (Undecorated properties are ignored.) Otherwise, map by name (default) will attempt to
        /// map from an underscore_delimited column via translation to an _underscorePrefixedCamelCase non-public instance field, a camelCase 
        /// non-public instance field, or PascalCase public instance property (searching in this order). Both mapping modes assume the 
        /// property's C# type and column's Oracle type are compatible.</param>
        /// <param name="allowUnmappedColumns">If true with map by position, the number of reader columns can be more than the 
        /// number of positioned properties, permitting an unmapped column. If true with map by name, an appropriate field or 
        /// property is not required for each column, permitting an unmapped column. If false, an exception will occur if 
        /// all reader columns cannot be mapped to a property.</param>
        /// <returns></returns>
        public static List<T> ReadResult<T>(OracleDataReader reader, bool mapByPosition = false, bool allowUnmappedColumns = false, 
            UInt32? optionalMaximumNumberOfRowsToRead = null) where T : new() {

            // get the mapping between this C# type and the reader's result set
            List<ColumnMapping> mappings = GetMappings<T>(reader, mapByPosition, allowUnmappedColumns);

            // iterate reader and create list of DTOs with properties/fields set from respective columns
            List<T> list = new List<T>(); // default to empty list (not null)
            if (reader != null && reader.HasRows) {
                while (reader.Read() == true) {
                    // create new instance for each row
                    T obj = ReadResultRow<T>(reader, mappings);
                    list.Add(obj);
                    if (optionalMaximumNumberOfRowsToRead != null && list.Count >= optionalMaximumNumberOfRowsToRead) break;
                }
            }

            return list;
        }

        /// <summary>
        /// Return a generic list of objects fetched from a data reader using type-specific read method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="readMethod"></param>
        /// <returns></returns>
        //public static List<T> ReadResult<T>(OracleDataReader reader, Func<OracleDataReader, T> readMethod)  {

        //    // iterate reader and create list of BOs using they type's reader method
        //    List<T> list = new List<T>();
        //    if (reader.HasRows) while (reader.Read() == true) list.Add(readMethod(reader));
        //    return list;
        //}

        /// <summary>
        /// Return a Datatable fetched from a data reader
        /// </summary>
        /// <param name="reader">Reader prepared for fetching result set</param>
        /// <returns></returns>
        public static DataTable ReadResult(OracleDataReader reader, bool convertColumnNameToTitleCaseInCaption = false, UInt32? optionalMaximumNumberOfRowsToRead = null) {

            // determine name and type of each column in result set and build empty datatable
            DataTable dt = new DataTable();
            DataColumn dc;
            List<string> colName = new List<string>();

            // column names and respective Oracle type from the reader
            List<Column> readerColumns = GetReaderColumnTypes(reader);

            // build empty datatable with column names and correct C# type
            foreach (Column col in readerColumns) {

                // add column to list 
                colName.Add(col.ColumnName);

                // create data column object based on Oracle column type, save column type to array
                if (col.ColumnType == typeof(OracleString)) {
                    dc = new DataColumn(col.ColumnName, typeof(System.String));
                } else if (col.OracleDataTypeName.Equals(OracleDbType.BinaryDouble.ToString())) {
                    dc = new DataColumn(col.ColumnName, typeof(System.Double));
                } else if (col.OracleDataTypeName.Equals(OracleDbType.BinaryFloat.ToString())) {
                    dc = new DataColumn(col.ColumnName, typeof(System.Single));
                } else if (col.ColumnType == typeof(OracleDecimal)) {
                    dc = new DataColumn(col.ColumnName, typeof(OracleDecimal));
                } else if (col.ColumnType == typeof(OracleDate) ) {
                    dc = new DataColumn(col.ColumnName, typeof(OracleDate));
                } else if (col.ColumnType == typeof(OracleTimeStamp) ) {
                    dc = new DataColumn(col.ColumnName, typeof(OracleTimeStamp));
                } else if (col.ColumnType == typeof(OracleTimeStampLTZ) ) {
                    dc = new DataColumn(col.ColumnName, typeof(OracleTimeStampLTZ));
                } else if (col.ColumnType == typeof(OracleTimeStampTZ) ) {
                    dc = new DataColumn(col.ColumnName, typeof(OracleTimeStampTZ));
                } else
                    dc = new DataColumn(col.ColumnName, typeof(Object));
                //throw new Exception("Oracle column type not recognized in 'DataTable Hydrator.ReadResult()' for database column " + col.ColumnName);

                dc.Caption = 
                    convertColumnNameToTitleCaseInCaption ? 
                    CaseConverter.ConvertSnakeCaseToLabel(col.ColumnName)
                    : col.ColumnName
                    ;
                dt.Columns.Add(dc);
            }

            // add rows to data table
            DataRow drow;
            Int32 numRowsRead = 0;
            if (reader != null && reader.HasRows) {
                while (reader.Read()) {
                    // create the row and set column data based on result set row
                    drow = dt.NewRow();
                    for (int c = 0; c < colName.Count; c++) {
                        if (reader.IsDBNull(reader.GetOrdinal(colName[c])))
                            drow[colName[c]] = DBNull.Value;
                        else if (readerColumns[c].OracleDataTypeName.Equals(OracleDbType.BinaryDouble.ToString()))
                            drow[colName[c]] = (Double?)reader.GetDouble(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].OracleDataTypeName.Equals(OracleDbType.BinaryFloat.ToString()))
                            drow[colName[c]] = (Single?)reader.GetFloat(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].ColumnType == typeof(OracleString))
                            drow[colName[c]] = (String)reader.GetOracleString(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].ColumnType == typeof(OracleDecimal))
                            drow[colName[c]] = (OracleDecimal?)OracleDecimal.SetPrecision(reader.GetOracleDecimal(reader.GetOrdinal(colName[c])), 28);
                        else if (readerColumns[c].ColumnType == typeof(OracleDate))
                            drow[colName[c]] = (OracleDate?)reader.GetOracleDate(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].ColumnType == typeof(OracleTimeStamp))
                            drow[colName[c]] = (OracleTimeStamp?)reader.GetOracleTimeStamp(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].ColumnType == typeof(OracleTimeStampLTZ))
                            drow[colName[c]] = (OracleTimeStampLTZ?)reader.GetOracleTimeStampLTZ(reader.GetOrdinal(colName[c]));
                        else if (readerColumns[c].ColumnType == typeof(OracleTimeStampTZ))
                            drow[colName[c]] = (OracleTimeStampTZ?)reader.GetOracleTimeStampTZ(reader.GetOrdinal(colName[c]));
                        else
                            drow[colName[c]] = reader.GetValue(reader.GetOrdinal(colName[c]));
                    }

                    // Add the hydrated row to the datatable
                    dt.Rows.Add(drow);

                    if (optionalMaximumNumberOfRowsToRead != null) {
                        numRowsRead++;
                        if (numRowsRead >= optionalMaximumNumberOfRowsToRead) break;
                    }
                }
            }

            // this is necessary in order for DataRowVersion status to be correct
            dt.AcceptChanges();

            return dt;
        }
#endregion
#region Miscelaneous methods
        /// <summary>
        /// Extract all values from an associative array parameter and return as comma delimited string
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetAssocArrayAsCommaDelimited(OracleParameter param) {
            if (param.CollectionType != OracleCollectionType.PLSQLAssociativeArray) return null;

            string commaDelimitedVals = "";

            List<string> vals = new List<string>();
            switch (param.OracleDbType) {
                case OracleDbType.Varchar2:
                    for (int i = 0; i < (param.Value as Array).Length; i++) {
                        if (param.Direction.ToString().EndsWith(ParameterDirection.Output.ToString())) {
                            //a = GetStructArray<OracleString>(param.Value);
                            vals.Add((param.Value as OracleString[])[i].IsNull
                                ? "NULL"
                                : "'" + (param.Value as OracleString[])[i].Value + "'"); // use single quotes to designate string
                        } else {
                            vals.Add((param.Value as String[])[i] == null
                                ? "NULL"
                                : "'" + (param.Value as String[])[i] + "'"); // use single quotes to designate string
                        }
                    }
                    commaDelimitedVals += String.Join(",", vals.ToArray());
                    break;

                case OracleDbType.Decimal:
                case OracleDbType.Int16:
                case OracleDbType.Int32:
                case OracleDbType.Int64:
                    for (int i = 0; i < (param.Value as Array).Length; i++) {
                        if (param.Direction.ToString().EndsWith(ParameterDirection.Output.ToString())) {
                            vals.Add((param.Value as OracleDecimal[])[i].IsNull
                                ? "NULL"
                                : (param.Value as OracleDecimal[])[i].Value.ToString());
                        } else {
                            //Type arrayType = param.Value.GetType().GetElementType();
                            //decimal?[] a = GetStructArray<decimal?>(param.Value);
                            //Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType
                            if (param.OracleDbType.Equals(OracleDbType.Int64)) {
                                vals.Add(!(param.Value as Int64?[])[i].HasValue
                                    ? "NULL"
                                    : Convert.ToString((param.Value as Int64?[])[i].Value));
                            } else if (param.OracleDbType.Equals(OracleDbType.Int32)) {
                                vals.Add(!(param.Value as Int32?[])[i].HasValue
                                    ? "NULL"
                                    : Convert.ToString((param.Value as Int32?[])[i].Value));
                            } else if (param.OracleDbType.Equals(OracleDbType.Int16)) {
                                vals.Add(!(param.Value as Int16?[])[i].HasValue
                                    ? "NULL"
                                    : Convert.ToString((param.Value as Int16?[])[i].Value));
                            } else if (param.OracleDbType.Equals(OracleDbType.Decimal)) {
                                vals.Add(!(param.Value as Decimal?[])[i].HasValue
                                    ? "NULL"
                                    : Convert.ToString((param.Value as Decimal?[])[i].Value));
                            } else {
                                commaDelimitedVals = NOT_AVAILABLE;
                                break;
                            }
                        }
                    }
                    commaDelimitedVals += String.Join(",", vals.ToArray());
                    break;

                case OracleDbType.Date:
                    for (int i = 0; i < (param.Value as Array).Length; i++) {
                        if (param.Direction.ToString().EndsWith(ParameterDirection.Output.ToString())) {
                            vals.Add((param.Value as OracleDate[])[i].IsNull
                                ? "NULL"
                                : (param.Value as OracleDate[])[i].Value.ToString());
                        } else {
                            vals.Add((param.Value as DateTime[])[i] == null
                                ? "NULL"
                                : (param.Value as DateTime[])[i].ToString());
                        }
                    }
                    commaDelimitedVals += String.Join(",", vals.ToArray());
                    break;
            }

            return commaDelimitedVals;
        }

#endregion
    }
#endregion // Hydrator

    /// <summary>
    /// Wrapper for an OracleCommand to be traced and timed. Should be instantiated just before command
    /// is executed.
    /// </summary>
    public class OracleCommandTrace : IDisposable {
        private readonly Stopwatch stopwatch = Stopwatch.StartNew(); // starts timing upon instantiation
        public Stopwatch Stopwatch { get { return stopwatch; } }

        private OracleCommand command;
        public OracleCommand Command { get { return command; } }

        public OracleCommandTrace(OracleCommand oracleCommand) {
            this.command = oracleCommand;
        }

        public void Dispose() {
            stopwatch.Stop();
            command = null;
        }
    }
}