//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2018 Clay Lipscomb
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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Odapter {
    #region Oracle objects (package, proc, etc.)
    public class Package  {
        public string PackageName { get { return objectName; } set { objectName = value; } } private string objectName { get; set; }
        public string Owner { get; set; }
        public List<Procedure> Procedures { get; set; }
    }

    // a proc/function (packaged or not)
    public class Procedure {
        public string PackageName { get { return objectName; } set { objectName = value; } } private string objectName { get; set; }
        public string ProcedureName { get; set; }
        public string Overload { get; set; }
        public List<Argument> Arguments { get; set; }

        /// <summary>
        /// if a function, gets the return Oracle type
        /// </summary>
        public string ReturnOracleDataType {
            get { return (Arguments.Count == 0 || Arguments[0].DataLevel != 0 || Arguments[0].Position != 0 ? null : Arguments[0].DataType); }
        }

        /// <summary>
        /// Returns whether this stored proc is a function
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsFunction() {
            return (Arguments.Count == 0 ? false : (Arguments[0].DataLevel == 0 && Arguments[0].Position == 0 && Arguments[0].InOut == Orcl.OUT));
        }

        /// <summary>
        /// Returns whether procedure has at least one OUT (not IN OUT) param, excluding the return 
        /// </summary>
        /// <returns></returns>
        public Boolean HasOutArgument() {
            foreach (Argument arg in Arguments) if ((arg.DataLevel != 0 || arg.Position != 0) && arg.InOut.Equals(Orcl.OUT)) return true; // explicit OUT
            return false;
        }

        public Boolean IsIgnoredDueToOracleArgumentTypes(out String reasonMsg) {
            reasonMsg = "";
            String unimplemntedType = "";

            if (HasArgumentOfOracleTypeAssocArrayOfUnimplementedType(out unimplemntedType)) {
                reasonMsg = ".NET cannot send/receive an associative array of a " + unimplemntedType;
                return true;
            } else if (HasInArgumentOfOracleTypeRefCursor()) {
                reasonMsg = ".NET cannot send/receive a " + Orcl.REF_CURSOR;
                return true;
            } else {
                foreach (String oraType in Translater.OracleTypesIgnored) 
                    if (UsesOracleType(oraType, !oraType.Equals(Orcl.RECORD))) {
                        Translater.IsOracleTypeIgnored(oraType, out reasonMsg); // get reason
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determine if a given Oracle type is found in any of the arguments/return
        /// </summary>
        /// <param name="oracleType"></param>
        /// <param name="checkNestedArgs"></param>
        /// <returns></returns>
        private Boolean UsesOracleType(String oracleType, bool checkNestedArgs) {
            if (Arguments == null) return false;
            // only search nested arguments if specified
            return Arguments.FindIndex(a => (a.DataType == oracleType || a.TypeName == oracleType 
                //|| a.PlsType == oracleType
                ) && (checkNestedArgs || a.DataLevel == 0)) != -1;
        }

        /// <summary>
        /// Determine if a given Oracle type is found in any of the arguments/return including nested arguments
        /// </summary>
        /// <param name="oracleType"></param>
        /// <param name="checkNestedArgs"></param>
        /// <returns></returns>
        //public Boolean UsesOracleType(String oracleType) {
        //    return UsesOracleType(oracleType, true);
        //}

        /// <summary>
        /// Determine if procedure has an argument/return of a given Oracle type. Nested levels of argument
        ///     are not considered, only the main argument type.
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        public Boolean HasArgumentOfOracleType(String oracleType) {
            return UsesOracleType(oracleType, false);
        }

        /// <summary>
        /// Does procedure have at least one weakly typed cursor as return or argument?
        /// </summary>
        /// <returns></returns>
        public Boolean UsesWeaklyTypedCursor() {
            foreach (Argument arg in Arguments) {
                // when we reach last arg, we must return here with a simple check: a cursor at the end is weakly typed
                if (Arguments.IndexOf(arg) == Arguments.Count - 1) return (arg.DataType == Orcl.REF_CURSOR ? true : false); 

                // check if argument is cursor and its subsequent argument is on the same data level - this reveals a weakly typed cursro
                if (arg.DataType == Orcl.REF_CURSOR && Arguments[Arguments.IndexOf(arg) + 1].DataLevel == arg.DataLevel) return true;
            }
            return false; // we found weakly typed cursors
        }

        /// <summary>
        /// Does procedure have at least one associative array of an unimplemented type as return or argument?
        /// </summary>
        /// <returns></returns>
        public Boolean HasArgumentOfOracleTypeAssocArrayOfUnimplementedType(out String unimplementedType) {
            unimplementedType = null;
            foreach (Argument arg in Arguments) {
                if (Arguments.IndexOf(arg) == Arguments.Count - 1) return false; // reached end of arg list since assoc array uses "2 args"
                // check type of argument and its subsequent argument
                if (    arg.DataType == Orcl.ASSOCIATITVE_ARRAY
                    &&  !Translater.TypesImplementedForAssociativeArrays.Contains(Arguments[Arguments.IndexOf(arg) + 1].DataType)) { 
                    //Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.RECORD
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.BLOB
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.CLOB
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.NCLOB
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.TIMESTAMP
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.PLS_INTEGER
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.BINARY_INTEGER
                    //    ||  Arguments[Arguments.IndexOf(arg) + 1].DataType == Orcl.PLSQL_BOOLEAN)) {
                    unimplementedType = Arguments[Arguments.IndexOf(arg) + 1].DataType;
                    return true;
                }
            }
            return false;
        }

        public Boolean HasInArgumentOfOracleTypeRefCursor() {
            //reasonMsg = "";
            foreach (Argument arg in Arguments) if (arg.DataType.Equals(Orcl.REF_CURSOR) && arg.InOut.StartsWith(Orcl.IN)) return true; // IN or IN OUT
            return false;
        }
    }

    // an argument to a funtion/proc
    public class Argument {
        public Argument NextArgument { get; set; }
        public string Owner { get; set; }
        public string PackageName { get; set; }
        public string ProcedureName { get { return objectName; } set { objectName = value; } } private string objectName { get; set; }
        public string Overload { get; set; }
        public int DataLevel { get; set; }
        public string ArgumentName { get; set; }
        public int Position { get; set; }
        public int Sequence { get; set; }
        public string DataType { get; set; }
        public string InOut { get; set; }
        public int? DataLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
        public string PlsType { get; set; }
        public string TypeOwner { get; set; }
        public string TypeName { get; set; }
        public string TypeSubname { get; set; }
        public string TypeLink { get; set; }
        public int? CharLength { get; set; }
        public bool Defaulted { get { return (defaulted == "Y" ? true : false); } } private string defaulted { get; set; }
        public bool IsReturnArgument { get { return (Position == 0 && DataLevel == 0 && ArgumentName == null && InOut == Orcl.OUT); } }
    }

    // a field of a PL/SQL record
    public class Field : IComparable<Field>, IEntityAttribute {
        // standard entity properties
        public string EntityName { get { return Name; } set { Name = value; } }
        public string AttrName { get { return Name; } set { Name = value; } }
        public string AttrType { get { return DataType; } set { DataType = value; } }
        public string AttrTypeOwner { get { return TypeOwner; } set { TypeOwner = value; } }
        public string AttrTypeMod { get; set; }
        public int? Length { get { return DataLength; } set { DataLength = value; } } 
        public int? Precision { get { return DataPrecision; } set { DataPrecision = value; } } 
        public int? Scale { get { return DataScale; } set { DataScale = value; } } 
        public int Position { get { return MapPosition; } set { MapPosition = value; } }  
        public String CSharpType { get; set; }
        public bool Nullable { get { return true; } }
        public String ContainerClassName { get; set; } // Container class if C# type is nested class

        // field specific
        public String Name { get; set; }
        public int MapPosition { get; set; }
        public int CompareTo(Field f) { return Name.CompareTo(f.Name); }
        public String DataType { get; set; }
        public String TypeOwner { get; set; }
        public int? DataLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
    }

    // a PL/SQL package record type
    public class PackageRecord : Entity, IEntity, IComparable<PackageRecord> {
        // standard entity properties
        public string EntityName { get { return CSharpType; } set { CSharpType = value; } } 
        public string AncestorTypeName { get; set; }
        public bool Instantiable { get { return true; } }
        public String CSharpType { get; set; }

        // record specific

        /// <summary>
        /// Package containing argument from which record is derived
        /// </summary>
        public string PackageName { get; set; } 

        /// <summary>
        /// Package name of record definition; could be different from package with argument using it
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Name of record
        /// </summary>
        public String SubName { get; set; }

        /// <summary>
        /// Schema of record defintion; could be different from schema with argument using it
        /// </summary>
        public String Owner { get; set; }

        //public List<IEntityAttribute> Fields { get { return Attributes; } } // alias for Attributes
        public int CompareTo(PackageRecord r) { return CSharpType.CompareTo(r.CSharpType); }
    }

    /// <summary>
    /// Interface of an entity attribute. Should be implemented mapping to underlying sys view column if naming is different.
    /// </summary>
    public interface IEntityAttribute {
        string EntityName { get; set; }
        string AttrName { get; set; }
        string AttrType { get; set; }
        string AttrTypeOwner { get; set; }
        string AttrTypeMod { get; set; }
        int? Length { get; set; }
        int? Precision { get; set; }
        int? Scale { get; set; }
        int Position { get; set; }
        bool Nullable { get; }
        string CSharpType { get; set; } // optionally set during load of data (e.g., package record type fields)
        String ContainerClassName { get; set; } // Container class if C# type is nested class
    }

    /// <summary>
    /// Attribute for an object type
    /// </summary>
    public class ObjectTypeAttribute : IEntityAttribute {
        // standard attribute properties
        public string EntityName { get { return typeName; } set { typeName = value; } } private string typeName { get; set; }
        public string AttrName { get; set; }
        public string AttrType { get { return attrTypeName; } set { attrTypeName = value; } } private string attrTypeName { get; set; }
        public string AttrTypeOwner { get; set; } 
        public string AttrTypeMod { get; set; } 
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public int Position { get { return attrNo; } set { attrNo = value; } } private int attrNo { get; set; }
        public bool Nullable { get { return true; } }
        public string CSharpType { get; set; }
        public String ContainerClassName { get; set; } // Container class if C# type is nested class
    }

    /// <summary>
    /// Column for a table or view
    /// </summary>
    public class Column : IEntityAttribute {
        // standard attribute properties
        public string EntityName { get { return tableName; } set { tableName = value; } } private string tableName { get; set; }
        public string AttrName { get { return columnName; } set { columnName = value; } } private string columnName { get; set; }
        public string AttrType { get { return dataType; } set { dataType = value; } } private string dataType { get; set; }
        public string AttrTypeOwner { get { return dataTypeOwner; } set { dataTypeOwner = value; } } private string dataTypeOwner { get; set; }
        public string AttrTypeMod { get { return dataTypeMod; } set { dataTypeMod = value; } } private string dataTypeMod { get; set; }
        public int? Length { get { return dataLength; } set { dataLength = value; } } private int? dataLength { get; set; }
        public int? Precision { get { return dataPrecision; } set { dataPrecision = value; } } private int? dataPrecision { get; set; }
        public int? Scale { get { return dataScale; } set { dataScale = value; } } private int? dataScale { get; set; }
        public int Position { get { return columnId; } set { columnId = value; } } private int columnId { get; set; }
        public bool Nullable { get { return (nullable == "Y" ? true : false); } } private string nullable { get; set; }
        public string CSharpType { get; set; }
        public String ContainerClassName { get; set; } // Container class if C# type is nested class

        // column specific properties
        public int CharLength { get; set; }
    }

    /// <summary>
    /// Field for a package record type - TO BE USED WHEN GenerateEntityClass() HANDLES RECORD TYPES
    /// </summary>
    //public class RecordTypeField : EntityAttribute, IComparable<RecordTypeField> {
    //    public override string EntityName { get; set; }
    //    public override string AttrName { get; set; }
    //    public override string AttrType { get; set; }
    //    public override int Length { get; set; }
    //    public override int Precision { get; set; }
    //    public override int Scale { get; set; }
    //    public override int Position { get; set; }

    //    public String CSharpType { get; set; }
    //    public Int32 MapPosition { get; set; }
    //    public int CompareTo(RecordTypeField f) { return AttrName.CompareTo(f.AttrName); }
    //}

    /// <summary>
    /// Interface of an entity. Should be implemented mapping to underlying sys view column if naming is different.
    /// </summary>
    public interface IEntity {
        string EntityName { get; set; }
        string Owner { get; set; }
        List<IEntityAttribute> Attributes { get; set; }
        string AncestorTypeName { get; set; }
        bool Instantiable { get; }
        String CSharpType { get; set; }
    }

    /// <summary>
    /// Base abstract class Entity with list of attribute
    /// </summary>
    public abstract class Entity {
        public List<IEntityAttribute> Attributes { get; set; }

        /// <summary>
        /// Determine if a given Oracle type is found in any of the attributes
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        public Boolean UsesOracleType(String oracleType) {
            if (Attributes == null) return false;
            return Attributes.FindIndex(a => a.AttrType.Equals(oracleType)) != -1;
        }

        public Boolean IsIgnoredDueToOracleTypes(out String reasonMsg) {
            reasonMsg = "";

            foreach (String oraType in Translater.OracleTypesIgnored)
                if (UsesOracleType(oraType)) {
                    Translater.IsOracleTypeIgnored(oraType, out reasonMsg, Attributes[0].GetType().Name.ToLower()); // get reason
                    return true;
                }

            return false;
        }
    }

    /// <summary>
    /// Object Type as type of Entity
    /// </summary>
    public class ObjectType : Entity, IEntity {
        public string Owner { get; set; }
        public string EntityName { get { return typeName; } set { typeName = value; } } private string typeName { get; set; }
        public string AncestorTypeName { get { return supertypeName; } set { supertypeName = value; } } private string supertypeName { get; set; }
        public bool Instantiable { get { return (instantiable == Orcl.YES ? true : false); } } private string instantiable { get; set; }
        public String CSharpType { get; set; }
    }

    /// <summary>
    /// Table as type of Entity
    /// </summary>
    public class Table : Entity, IEntity {
        public string Owner { get; set; }
        public string EntityName { get { return tableName; } set { tableName = value; } } private string tableName { get; set; }
        public string AncestorTypeName { get; set; }
        public bool Instantiable { get { return true; } }
        public String CSharpType { get; set; }
    }

    /// <summary>
    /// View as type of Entity
    /// </summary>
    public class View : Entity, IEntity {
        public string Owner { get; set; }
        public string EntityName { get { return viewName; } set { viewName = value; } } private string viewName { get; set; }
        public string AncestorTypeName { get; set; }
        public bool Instantiable { get { return true; } }
        public String CSharpType { get; set; }
    }

    /// <summary>
    /// Record Type as type of Entity - TO BE USED WHEN GenerateEntityClass() HANDLES RECORD TYPES
    /// </summary>
    //public class RecordType : Entity<RecordTypeField>, IComparable<RecordType> {
    //    public override string EntityName { get; set; }

    //    public string PackageName { get; set; }
    //    public String SubName { get; set; }
    //    public String Owner { get; set; }
    //    public String CSharpType { get; set; }
    //    public int CompareTo(RecordType r) { return CSharpType.CompareTo(r.CSharpType); }
    //}
    #endregion
}