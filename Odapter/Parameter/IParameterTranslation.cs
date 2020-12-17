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

namespace Odapter {
    public interface IParameterTranslation {
        string CSharpTypeUsedForOracleAssociativeArray { get; set; }
        string CSharpTypeUsedForOracleBFile { get; set; }
        string CSharpTypeUsedForOracleBlob { get; set; }
        string CSharpTypeUsedForOracleClob { get; set; }
        string CSharpTypeUsedForOracleDate { get; set; }
        string CSharpTypeUsedForOracleInteger { get; set; }
        bool IsConvertOracleNumberToIntegerIfColumnNameIsId { get; set; }
        string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        string CSharpTypeUsedForOracleNumber { get; set; }
        string CSharpTypeUsedForOracleRefCursor { get; set; }
        string CSharpTypeUsedForOracleTimeStamp { get; set; }
    }
}