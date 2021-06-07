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

using System.Collections.Generic;
using CS = Odapter.CSharp;

namespace Odapter {
    public interface IParameter : IParameterLoad, IParameterTranslation {
        IList<string> ConfigFileNames { get; }

        string OutputPath { get; set; }

        bool IsSavePassword { get; set; }

        string AncestorClassNamePackage { get; set; }

        CS.DtoInterfaceCategory TargetDtoInterfaceCategoryRecord { get; set; }
        CS.DtoInterfaceCategory TargetDtoInterfaceCategoryObject { get; set; }
        CS.DtoInterfaceCategory TargetDtoInterfaceCategoryTable { get; set; }
        CS.DtoInterfaceCategory TargetDtoInterfaceCategoryView { get; set; }
        CS.CSharpVersion TargetCSharpVersion { get; set; }
        bool IsCSharp90 { get; }

        bool IsDeployResources { get; set; }

        bool IsPartialPackage { get; set; }


        string NamespaceBase { get; set; }
        string NamespaceObjectType { get; set; }
        string NamespacePackage { get; set; }
        string NamespaceTable { get; set; }
        string NamespaceView { get; set; }
        string NamespaceBaseAdapter { get; set; }

        string FileNamePackage { get; set; }
        string FileNameObject { get; set; }
        string FileNameTable { get; set; }
        string FileNameView { get; set; }
        string FileNameBaseAdapter { get; set; }
        string FileNameBaseEntity { get; set; }

        bool IsDuplicatePackageRecordOriginatingOutsideFilterAndSchema { get; set; }
        bool IsGenerateDynamicMappingMethodForTypedCursor { get; set; }
        bool IsIncludeFilterPrefixInNaming { get; set; }
        string LocalVariableNameSuffix { get; set; }
        int MaxAssocArraySize { get; set; }
        short MaxReturnAndOutArgStringSize { get; set; }
    }
}