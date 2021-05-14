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

using CS = Odapter.CSharp;

namespace Odapter {
    public interface IParameterTranslation {
        CS.TypeCollection TypeTargetForOracleAssociativeArray { get; set; }
        string CSharpTypeUsedForOracleBfile { get; set; }
        CS.ITypeTargetable TypeTargetForOracleBlob { get; set; }
        CS.TypeReference TypeTargetForOracleClob { get; set; }
        CS.TypeValue TypeTargetForOracleDate { get; set; }
        CS.TypeValue TypeTargetForOracleInteger { get; set; }
        bool IsConvertOracleNumberToIntegerIfColumnNameIsId { get; set; }
        string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        CS.TypeValue TypeTargetForOracleNumber { get; set; }
        CS.TypeCollection TypeTargetForOracleRefCursor { get; set; }
        CS.TypeValue TypeTargetForOracleTimestamp { get; set; }
        CS.TypeValue TypeTargetForOracleTimestampTZ { get; set; }
        CS.TypeValue TypeTargetForOracleTimestampLTZ { get; set; }
    }
}