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

#define SAMPLE
#if SAMPLE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Schema.Odpt.Package;

namespace Odapter.Sample {
    public class Sample {
        public void Test() {
            uint?       rowLimit = 10;
            Decimal?    pInDecimal = 10.0M;
            String      pInOutString = "Hello";
            DateTime?   pOutDate;

            List<OdptPkgSample.TTableBigPartial> retTableBigPartialList = OdptPkgSample.Instance.GetRowsTypedRet<OdptPkgSample.TTableBigPartial>(pInDecimal, ref pInOutString, out pOutDate, rowLimit, null);
            Debug.Assert(retTableBigPartialList.Count == rowLimit);
            Debug.Assert(pInOutString.Equals("Goodbye"));
            Debug.Assert(pOutDate.Equals(new DateTime (1999, 12, 31)));
        }
    }
}
#endif
