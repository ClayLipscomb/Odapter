//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2019 Clay Lipscomb
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

using System.Collections.Generic;
using System.Linq;

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public static class TranslaterFactoryEntity {

        private static IList<ITranslaterEntity> OracleEntityTranslaters;
        private static void InitEntityTranslaters() { OracleEntityTranslaters = new List<ITranslaterEntity>(); }
        internal static void Initialize() { InitEntityTranslaters(); }

        internal static ITranslaterEntity GetTranslater(IEntity entity) {
            var dataTypeFull = entity.OrclEntity.BuildEntityTypeFullName(entity);

            if (!OracleEntityTranslaters.Any(t => t.DataTypeFull.Equals(dataTypeFull))) {
                switch (entity.EntityType) { 
                    case Orcl.TABLE:
                        OracleEntityTranslaters.Add(new TranslaterTable(dataTypeFull));
                        break;
                    case Orcl.VIEW:
                        OracleEntityTranslaters.Add(new TranslaterView(dataTypeFull));
                        break;
                    case Orcl.OBJECT:
                        OracleEntityTranslaters.Add(new TranslaterObjectEntity(dataTypeFull));
                        break;
                    case Orcl.RECORD:
                        OracleEntityTranslaters.Add(new TranslaterRecordEntity(dataTypeFull));
                        break;
                    default:
                        OracleEntityTranslaters.Add(new TranslaterUndefinedEntity(dataTypeFull));
                        break;
                }
            }

            return OracleEntityTranslaters.SingleOrDefault(t => t.DataTypeFull.Equals(dataTypeFull));
        }
    }
}