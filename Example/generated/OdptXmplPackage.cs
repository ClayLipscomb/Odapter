//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 1.07 on Fri, 05 Feb 2021 15:40:40 GMT.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using Odapter;

namespace Schema.Odpt.Xmpl.Package {

    public sealed partial class XmplPkgExample : Schema.Odpt.Xmpl.OdptAdapter {
        private XmplPkgExample() { }
        private static readonly XmplPkgExample _instance = new XmplPkgExample();
        public static XmplPkgExample Instance { get { return _instance; } }

        public interface ITTableBigPartial {
            Int64? Id { set; }
            Int64? ColInteger { set; }
            Decimal? ColNumber { set; }
            String ColVarchar2Max { set; }
            DateTime? ColDate { set; }
            OracleTimeStamp? ColTimestamp { set; }
        } // ITTableBigPartial

        [DataContract(Namespace="http://corp.xmpl.com")][Serializable()]
        public abstract partial class TTableBigPartial : Schema.Odpt.Xmpl.OdptPackageRecord, ITTableBigPartial {
            private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
            [DataMember(Order=0, IsRequired=false)][XmlElement(Order=0, IsNullable=true)]
            public virtual Int64? Id { get; set; }
            [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
            public virtual Int64? ColInteger { get; set; }
            [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
            public virtual Decimal? ColNumber { get; set; }
            [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
            public virtual String ColVarchar2Max { get; set; }
            [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
            public virtual DateTime? ColDate { get; set; }
            [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
            public virtual OracleTimeStamp? ColTimestamp { get; set; }
        } // TTableBigPartial

        public IList<T_TTableBigPartial> ReadResultITTableBigPartial<T_TTableBigPartial>(OracleDataReader rdr, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null)
                where T_TTableBigPartial : class, ITTableBigPartial, new()   {
            IList<T_TTableBigPartial> __ret = new List<T_TTableBigPartial>();
            if (rdr != null && rdr.HasRows) {
                while (rdr.Read()) {
                    T_TTableBigPartial obj = new T_TTableBigPartial();
                    if (!rdr.IsDBNull(0)) obj.Id = Convert.ToInt64(rdr.GetValue(0));
                    if (!rdr.IsDBNull(1)) obj.ColInteger = Convert.ToInt64(rdr.GetValue(1));
                    if (!rdr.IsDBNull(2)) obj.ColNumber = (Decimal?)OracleDecimal.SetPrecision(rdr.GetOracleDecimal(2), 28);
                    if (!rdr.IsDBNull(3)) obj.ColVarchar2Max = Convert.ToString(rdr.GetValue(3));
                    if (!rdr.IsDBNull(4)) obj.ColDate = Convert.ToDateTime(rdr.GetValue(4));
                    if (!rdr.IsDBNull(5)) obj.ColTimestamp = (OracleTimeStamp?)rdr.GetOracleValue(5);
                    __ret.Add(obj);
                    if (optionalMaxNumberRowsToReadFromAnyCursor != null && __ret.Count >= optionalMaxNumberRowsToReadFromAnyCursor) break;
                }
            }
            return __ret;
        } // ReadResultITTableBigPartial

        public IList<T_TTableBigPartial> GetRowsTypedRet<T_TTableBigPartial>(Decimal? pInNumber, ref String pInOutVarchar2, ref IList<Int64?> pInOutAssocarrayInteger, out DateTime? pOutDate, 
                UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, OracleConnection optionalPreexistingOpenConnection = null)
                where T_TTableBigPartial : class, ITTableBigPartial, new() {
            IList<T_TTableBigPartial> __ret = new List<T_TTableBigPartial>(); pOutDate = null; 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_TYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_NUMBER", OracleDbType.Decimal, pInNumber, ParameterDirection.Input));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_OUT_VARCHAR2", OracleDbType.Varchar2, 32767, pInOutVarchar2, ParameterDirection.InputOutput));

                    __cmd.Parameters.Add(new OracleParameter("P_IN_OUT_ASSOCARRAY_INTEGER", OracleDbType.Int64, 50000, null, ParameterDirection.InputOutput));
                    __cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value = (pInOutAssocarrayInteger == null || pInOutAssocarrayInteger.Count == 0 ? new Int64?[]{} : pInOutAssocarrayInteger.ToArray());
                    __cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                    __cmd.Parameters.Add(new OracleParameter("P_OUT_DATE", OracleDbType.Date, null, ParameterDirection.Output));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = ReadResultITTableBigPartial<T_TTableBigPartial>(__rdr, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    pInOutVarchar2 = __cmd.Parameters["P_IN_OUT_VARCHAR2"].Status == OracleParameterStatus.NullFetched
                        ? (String)null
                        : Convert.ToString(__cmd.Parameters["P_IN_OUT_VARCHAR2"].Value.ToString());

                    pInOutAssocarrayInteger = new List<Int64?>();
                    for (int _i = 0; _i < (__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[]).Length; _i++)
                        pInOutAssocarrayInteger.Add((__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[])[_i].IsNull
                            ? (Int64?)null 
                            : Convert.ToInt64(((__cmd.Parameters["P_IN_OUT_ASSOCARRAY_INTEGER"].Value as OracleDecimal[])[_i].ToString())));

                    pOutDate = __cmd.Parameters["P_OUT_DATE"].Status == OracleParameterStatus.NullFetched
                        ? (DateTime?)null
                        : Convert.ToDateTime(__cmd.Parameters["P_OUT_DATE"].Value.ToString());
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsTypedRet

        public IList<T_returnUntyped> GetRowsUntypedRet<T_returnUntyped>(Int64? pInInteger, 
                bool mapColumnToObjectPropertyByPosition = false, bool allowUnmappedColumnsToBeExcluded = false, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, 
                OracleConnection optionalPreexistingOpenConnection = null)
                where T_returnUntyped : class, new() {
            IList<T_returnUntyped> __ret = new List<T_returnUntyped>(); 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_UNTYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_INTEGER", OracleDbType.Int64, pInInteger, ParameterDirection.Input));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = Hydrator.ReadResult<T_returnUntyped>(__rdr, mapColumnToObjectPropertyByPosition, allowUnmappedColumnsToBeExcluded, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsUntypedRet

        public DataTable GetRowsUntypedRet(Int64? pInInteger, Boolean convertColumnNameToTitleCaseInCaption = false, UInt32? optionalMaxNumberRowsToReadFromAnyCursor = null, OracleConnection optionalPreexistingOpenConnection = null) {
            DataTable __ret = null; 
            OracleConnection __conn = optionalPreexistingOpenConnection ?? GetConnection();
            try {
                using (OracleCommand __cmd = new OracleCommand("ODPT.XMPL_PKG_EXAMPLE.GET_ROWS_UNTYPED_RET", __conn)) {
                    __cmd.CommandType = CommandType.StoredProcedure;
                    __cmd.BindByName = true;
                    __cmd.Parameters.Add(new OracleParameter("!RETURN", OracleDbType.RefCursor, null, ParameterDirection.ReturnValue));
                    __cmd.Parameters.Add(new OracleParameter("P_IN_INTEGER", OracleDbType.Int64, pInInteger, ParameterDirection.Input));

                    OracleCommandTrace __cmdTrace = IsTracing(__cmd) ? new OracleCommandTrace(__cmd) : null;
                    int __rowsAffected = __cmd.ExecuteNonQuery();
                    if (!((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).IsNull)
                        using (OracleDataReader __rdr = ((OracleRefCursor)__cmd.Parameters["!RETURN"].Value).GetDataReader()) {
                            __ret = Hydrator.ReadResult(__rdr, convertColumnNameToTitleCaseInCaption, optionalMaxNumberRowsToReadFromAnyCursor);
                        } // using OracleDataReader
                    if (__cmdTrace != null) TraceCompletion(__cmdTrace, __ret.Rows.Count);
                } // using OracleCommand
            } finally {
                if (optionalPreexistingOpenConnection == null) {
                    __conn.Close();
                    __conn.Dispose();
                }
            }
            return __ret;
        } // GetRowsUntypedRet
    } // XmplPkgExample
} // Schema.Odpt.Xmpl.Package