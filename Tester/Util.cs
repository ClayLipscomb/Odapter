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
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Odapter.Tester {
    internal static class Util {
        /// <summary>
        /// A make shift compare for two double values
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        internal static bool IsEqual(Double? v1, Double? v2) {
            if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Double.NaN) && v2.Equals(Double.NaN)) ||
                (v1.Equals(Double.PositiveInfinity) && v2.Equals(Double.PositiveInfinity)) || v1.Equals(Double.NegativeInfinity) && v2.Equals(Double.NegativeInfinity)) return true;
            double allowedDelta = Math.Abs(v1.Value * 0.00001);
            double delta = Math.Abs(v1.Value - v2.Value);
            return delta <= allowedDelta;
        }
        internal static bool IsEqual(Single? v1, Single? v2) {
            if ((v1 == null && v2 == null) || (v1 == 0.0 && v2 == 0.0) || (v1.Equals(Single.NaN) && v2.Equals(Single.NaN)) ||
                (v1.Equals(Single.PositiveInfinity) && v2.Equals(Single.PositiveInfinity)) || v1.Equals(Single.NegativeInfinity) && v2.Equals(Single.NegativeInfinity)) return true;
            double allowedDelta = Math.Abs(v1.Value * 0.00001);
            double delta = Math.Abs(v1.Value - v2.Value);
            return delta <= allowedDelta;
        }
        internal static bool IsEqual(DateTime? val1, DateTime? val2) {
            if (val1.HasValue != val2.HasValue)
                return false;
            else if (!val1.HasValue && !val2.HasValue)
                return true;
            else
                return (val1 - val2 < TimeSpan.FromSeconds(1));
        }
        internal static bool IsEqual(DateTimeOffset? val1, DateTimeOffset? val2) {
            if (val1.HasValue != val2.HasValue)
                return false;
            else if (!val1.HasValue && !val2.HasValue)
                return true;
            else
                return (val1 - val2 < TimeSpan.FromSeconds(1));
        }
        private static readonly int OracleTimeStampPrecisionCompare = 5;
        internal static bool IsEqual(OracleTimeStamp? val1, OracleTimeStamp? val2) =>
            val1.Value.IsNull != val2.Value.IsNull
                ? false
                : (val1.Value.IsNull && val2.Value.IsNull)
                    || OracleTimeStamp.SetPrecision(val1.Value, OracleTimeStampPrecisionCompare).Equals(OracleTimeStamp.SetPrecision(val2.Value, OracleTimeStampPrecisionCompare));
        internal static bool IsEqual(OracleTimeStampTZ? val1, OracleTimeStampTZ? val2) =>
            val1.Value.IsNull != val2.Value.IsNull
                ? false
                : (val1.Value.IsNull && val2.Value.IsNull)
                    || OracleTimeStampTZ.SetPrecision(val1.Value, OracleTimeStampPrecisionCompare).Equals(OracleTimeStampTZ.SetPrecision(val2.Value, OracleTimeStampPrecisionCompare));
        internal static bool IsEqual(OracleTimeStampLTZ? val1, OracleTimeStampLTZ? val2) =>
            val1.Value.IsNull != val2.Value.IsNull
                ? false
                : (val1.Value.IsNull && val2.Value.IsNull)
                    || OracleTimeStampLTZ.SetPrecision(val1.Value, OracleTimeStampPrecisionCompare).Equals(OracleTimeStampLTZ.SetPrecision(val2.Value, OracleTimeStampPrecisionCompare));
        internal static bool IsEqual(OracleDate? val1, OracleDate? val2) =>
            val1.Value.IsNull != val2.Value.IsNull
                ? false
                : (val1.Value.IsNull && val2.Value.IsNull) || val1.Equals(val2);
    }
}