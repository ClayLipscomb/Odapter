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
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

namespace Odapter.CSharp.Logic

open Odapter.Casing;
open Odapter.CSharp
open System

[<AutoOpen>]
module private ApiHelper =
    let nullableToOption = function | null -> None | x -> Some x
    let optionToNullable = function | Some x -> x | None -> null
    let ifNoneThrowElseValue (optionType, errorMsg) = if optionType |> Option.isNone then failwith errorMsg else optionType.Value 
    //let failwithNullOrEmpty desc = failwith $"{desc} cannot be null or empty" 

/// API for consumption by C#
module Api =
    // coding validation
    let IsTypeCollectionGeneric (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isTypeCollectionGeneric
    let IsOdpNet (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isOdpNet
    let IsDataTable (iTypeTargetable: ITypeTargetable) =  iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isDataTable
    let IsRequiresParseFromOutParameter (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> CodeLogic.isRequiresParseFromOutParameter 
    let IsRequiresOutParmBindSize (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> CodeLogic.isRequiresOutParmBindSize 

    // decoding for types persisted in config file
    let FromCodeTypeCollectionWithDefault str typeCollectionDefault = str |> TypeCollection.fromCode |> (Option.defaultValue typeCollectionDefault)
    let FromCodeTypeTargetableWithDefault str typeTargetableDefault = str |> fromCode |> (Option.defaultValue typeTargetableDefault)
    let FromCodeTypeValueWithDefault str typeValueDefault = str |> TypeValue.fromCode |> (Option.defaultValue typeValueDefault)
    let FromCodeTypeReferenceWithDefault str typeReferenceDefault = str |> TypeReference.fromCode |> (Option.defaultValue typeReferenceDefault)
    let CSharpVersionOfStringWithDefault str cSharpVersionDefault = str |> CSharpVersion.ofString |> (Option.defaultValue cSharpVersionDefault)
    let DtoInterfaceCategoryOfStringWithDefault str dtoInterfaceCategoryDefault = str |> DtoInterfaceCategory.ofString |> (Option.defaultValue dtoInterfaceCategoryDefault)

    // coding
    let CodeTab n = codeTab n
    let CodeTabbed (tabCnt: uint32, object: Object) = codeTabbed tabCnt object
    let CodeAdjacent (objects: Object seq) = codeAdjacent objects
    let CodeSpaced (objects: Object seq) = codeSpaced objects
    let CodeCommaSpaced (objects: Object seq) = codeCommaSpaced objects
    let CodeDotted (objects: Object seq) = codeDotted objects
    let CodeUsing (nmspace: Namespace) = Namespace.toCodeUsing nmspace
    let CodeReadResultAssignment (cSharpType:ITypeTargetable, cSharpOdpNetSafeType: ITypeTargetable, readerName: string, position: int, objectName: string, propertyName: PropertyName) =
        codeReadResultAssignment (cSharpType |> TypeTarget.ofITypeTargetable, cSharpOdpNetSafeType |> TypeTarget.ofITypeTargetable, readerName, position, objectName, propertyName)

    let CodeInterface (tabCnt: uint32, typeInterface: TypeInterface) = TypeInterface.codeTabbed tabCnt typeInterface
    let CodeInterfaceFirstLine (typeInterface: TypeInterface) = TypeInterface.codeFirstLine typeInterface
    let CodeTypeGenericConstraints (typeGenericParameters: TypeGenericParameter seq, tabCnt: uint32) = TypeGenericParameter.codeConstraints (typeGenericParameters, tabCnt)

    // constructor wrappers
    let MethodNameReadResult (interfaceName: InterfaceName) = MethodName.methodNameReadResult interfaceName
    let MethodNameReadResultTypeParameter (typeGenericParameter: TypeGenericParameter) = MethodName.methodNameReadResultTypeParameter typeGenericParameter

    let InterfaceNameOfClassName (className: ClassName) = InterfaceName.ofClassName className
    let NumericOdpNetOracleDbTypeEnum tvn = OdpNetOracleDbTypeEnum.fromTypeValueNullableNumeric tvn
    let TypeNone = TypeNone.NoType
    let Namespace (segments: string seq) = segments |> Seq.map (fun s -> if isNullOrWhiteSpace s then None else Some s) |> Namespace.create 

    let TypeGenericParameterOfInterface (interfaceName: InterfaceName) = TypeGenericParameter.createTyped interfaceName
    let TypeGenericParameterUntyped (pascalCase: PascalCase) = TypeGenericParameter.createUntyped pascalCase
    let TypeGenericParameterOf (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeGenericParameter.createOfTypeTarget

    let TypeGenericNameOfInterfaceName (interfaceName: InterfaceName) = TypeGenericName.createOfInterfaceName interfaceName
    let TypeGenericNameOf (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeGenericName.createOfTypeTarget

    let TypeArrayOf (iTypeComposable: ITypeComposable) = iTypeComposable |> TypeComposable.ofITypeComposable |> TypeArray.create
    let TypeCollectionGeneric (typeCollection: TypeCollection, iTypeTargetable: ITypeTargetable) = TypeCollectionGeneric.ofTypeTarget(typeCollection, iTypeTargetable |> TypeTarget.ofITypeTargetable)
    let GetSubType (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.getSubType

    let DtoInterfacePropertyAccessor (dtoInterfaceCategory: DtoInterfaceCategory) = TypeInterface.dtoInterfacePropertyAccessor  dtoInterfaceCategory
    let TypeInterface (accessModifier: AccessModifierInterface, interfaceName: InterfaceName, properties: Property seq, ancestorIntefaceNames: InterfaceName seq) = 
        TypeInterface.create (accessModifier, interfaceName, properties, if Seq.isEmpty ancestorIntefaceNames then None else Some ancestorIntefaceNames)

    let PropertyClass (propertyName: PropertyName, propertyType: ITypeTargetable, containerType: ITypeTargetable, accessor: PropertyAccessor, accessModifier: AccessModifier, isVirtual: bool, isDataMember: bool, isXmlElement: bool) = 
        Property.create (propertyName, propertyType |> TypeComposable.ofITypeTargetable, containerType |> TypeComposable.ofITypeTargetableOption, accessor, Some accessModifier, None, isVirtual, isDataMember, isXmlElement)
    let PropertyInterface (propertyName: PropertyName, propertyType: ITypeTargetable, containerType: ITypeTargetable, accessor: PropertyAccessor) =        
        Property.createForInterface (propertyName, propertyType |> TypeComposable.ofITypeTargetable, containerType |> TypeComposable.ofITypeTargetableOption, accessor)