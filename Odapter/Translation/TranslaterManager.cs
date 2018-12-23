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

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public static class TranslaterManager {
        internal static void Initialize(IParameterTranslation param) {
            TranslaterFactoryType.Initialize(param);
            TranslaterFactoryEntity.Initialize();
        }

        internal static void AssignTranslaters(Loader loader) {
            if (loader.ArgumentsPackaged != null) {
                foreach (IArgument arg in loader.ArgumentsPackaged) arg.Translater = TranslaterFactoryType.GetTranslater(arg);
            }

            if (loader.PackageRecordTypes != null) {
                foreach (IPackageRecord rec in loader.PackageRecordTypes) {
                    rec.Translater = TranslaterFactoryEntity.GetTranslater(rec);
                    foreach (IEntityAttribute attrib in rec.Attributes) attrib.Translater = TranslaterFactoryType.GetTranslater(attrib);
                }
            }

            foreach (List<IEntity> entityList in new List<List<IEntity>>() { loader.Tables, loader.Views, loader.ObjectTypes }) {
                if (entityList == null) continue;
                foreach (IEntity entity in entityList) {
                    entity.Translater = TranslaterFactoryEntity.GetTranslater(entity);
                    foreach (IEntityAttribute attrib in entity.Attributes) attrib.Translater = TranslaterFactoryType.GetTranslater(attrib);
                }
            }
        }

        #region Properties for Advanced Options
        internal static bool UseDatatableForUntypedCursor { get; set; } = false;
        #endregion
    }
}