//------------------------------------------------------------------------------
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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    /// <summary>
    /// Package record type as type of Entity
    /// </summary>
    internal sealed class PackageRecord : EntityBase, IPackageRecord {
        public IOrclEntity OrclEntity { get => new OrclRecordEntity(); }
        public string EntityType { get => OrclEntity.EntityType; }

        public string EntityName { get { return TypeSubName; } set { TypeSubName = value; } }   // sub_name is underlying sys view column
        public bool IsInstantiable { get => false; }  // a translated package record should not be instantiable

        // IEntityNameable specific
        public string ContainerType { get => TypeName ?? PackageName; }
        public bool IsDefinedExternally { get => TypeName != null && PackageName != null && TypeName != PackageName; } // defined external to the package using this record as a parameter

        public ITranslaterEntity Translater { get; set; }

        // IPackageRecord specific
        public string PackageName { get; set; }     // package where record type is used with proc argument
        public string TypeName { get; set; }        // package containing record definition
        public string TypeSubName { get; set; }     // record name
        public int CompareTo(IPackageRecord r) { return this.ToString().CompareTo(r.ToString()); }
        public IArgument RecordArgument { get; set; }

        public override string ToString() { return (PackageName ?? "null") + "." + (TypeName ?? "null") + "." + (TypeSubName ?? "null"); }

        internal static string BuildRecordFullName(IEntityNameable rec) {
            string recordName;
            if (Parameter.Instance.IsUsingSchemaFilter && !rec.ContainerType.StartsWith(Parameter.Instance.Filter) && rec.IsDefinedExternally)
                // if filtering and record is defined in another package, prefix with owning package name only to *prevent naming conflict*
                recordName = rec.ContainerType + Orcl.PERIOD + rec.EntityName;
            else
                recordName = rec.EntityName;

            return recordName;
        }

        internal static string BuildRecordFullName(ITypeNameable rec) {
            string recordName;
            // Type and subtype can be null (e.g., a bug in the view when a record type based on a table). In this case, 
            //      use proc name (which is what subtype usually is anyway) and some extra special text. We need a 
            //      better algorithm to guarantee uniqueness in the C# namespace.
            if (String.IsNullOrEmpty(rec.DataTypeProperName)) 
                recordName = rec.NamingHelpValue + Orcl.PERIOD + (rec.DataTypeLabel ?? "SOME") + Orcl.UNDERSCORE + "ROW_TYPE";
            else if (Parameter.Instance.IsUsingSchemaFilter && !rec.ContainerType.StartsWith(Parameter.Instance.Filter) && rec.IsDefinedExternally)
                // if filtering and record is defined in another package, prefix with owning package name only to *prevent naming conflict*
                recordName = rec.ContainerType + Orcl.PERIOD + rec.DataTypeProperName;
            else 
                recordName = rec.DataTypeProperName;
            
            return recordName;
        }
    }
}