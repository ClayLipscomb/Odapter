﻿//------------------------------------------------------------------------------
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

open Odapter;
open Odapter.Casing;
open Odapter.CSharp
open System
open System.Text.RegularExpressions

[<RequireQualifiedAccess>]
module internal CSharpVersion =
    let private unionCache = UtilUnion.getUnionCases<CSharpVersion> |> Seq.cache
    let ofString str = (UtilUnion.createUnionCase<CSharpVersion> unionCache str) 

[<RequireQualifiedAccess>]
module internal DtoInterfaceCategory =
    let private unionCache = UtilUnion.getUnionCases<DtoInterfaceCategory> |> Seq.cache
    let ofString str = (UtilUnion.createUnionCase<DtoInterfaceCategory> unionCache str) 

[<RequireQualifiedAccess>]
module internal Keyword = 
    let private unionCache = UtilUnion.getUnionCases<Keyword> |> Seq.cache
    /// Case-sensitive compare to (lower case) C# keyword
    let isKeyword keywordCandidate = unionCache |> Seq.exists (fun kw -> toLower kw.Name = keywordCandidate) 
    let escapeKeyword = CamelCase.map (fun str -> (if isKeyword(str) then @"@" else emptyString) + str) // prepend @ to C# keyword

[<RequireQualifiedAccess>]
module internal Namespace = 
    let ofPascalCase pascalCase = Namespace pascalCase
    let create segmentOptions = segmentOptions |> Seq.choose id |> join PERIOD |> PascalCase |> Namespace 
    let value = WrappedString.value    
    let toCodeUsing (nmspace:Namespace) = (codeSpaced [|USING;nmspace|]) + SEMICOLON

[<RequireQualifiedAccess>]
module internal ClassName = 
    let ofPascalCase pascalCase = ClassName pascalCase
    let fromCode = PascalCase.create >> ClassName
    let (|ValidCode|_|) = fromCode >> Some
    //let FromCodeOfInterfaceToCodeClass interfaceCode =
    //    interfaceCode 
    //    |> TypeInterface.createOfText 
    //    |> Option.map(fun i -> i |> TypeClass.createOfInterface) 
    //    |> Option.bind(fun c -> (c :> ICodeable).ToCode |> Some)
    //    |> Option.defaultValue emptyString
[<RequireQualifiedAccess>]
module internal PropertyName = 
    let ofPascalCase pascalCase = PropertyName pascalCase
    let private create = PascalCase.create >> PropertyName
    let fromCode propertyNameCandidate = create propertyNameCandidate
    let doubleName (PropertyName pascalCase) = PascalCase.concat [|pascalCase; pascalCase|] |> ofPascalCase
    let (|ValidCode|_|) = fromCode >> Some

[<RequireQualifiedAccess>]
module internal InterfaceName = 
    let [<Literal>] private INTERFACE_PREFIX = @"I"
    let private interfacePrefixPascalCase = PascalCase.create INTERFACE_PREFIX
    let private create baseStr = (INTERFACE_PREFIX + baseStr) |> PascalCase |> InterfaceName
    let private createOfPascalCase basePascalCase = PascalCase.concat [|interfacePrefixPascalCase; basePascalCase|] |> InterfaceName
    let ofClassName (className: ClassName) = createOfPascalCase className.Value
    let fromCode interfaceNameCandidate = 
        if (isNullOrWhiteSpace interfaceNameCandidate |> not) 
            && length interfaceNameCandidate >= 3                   // at least 3 characters long
            && isUpper (subString 0 2 interfaceNameCandidate)       // first 2 characters are uppercase
            && isLower (subString 2 1 interfaceNameCandidate)       // 3rd character is lowercase
            && (startsWith INTERFACE_PREFIX interfaceNameCandidate) then 
            interfaceNameCandidate |> PascalCase |> createOfPascalCase |> Some
        else 
            None
    let (|ValidCode|_|) = fromCode

[<RequireQualifiedAccess>]
module internal TypeGenericName = 
    let private startsWithGenericPrefix = startsWith Literal.GENERIC_PREFIX
    let private removeGenericPrefix = fun s -> (if startsWithGenericPrefix s then subStringToEnd Literal.GENERIC_PREFIX.Length s else s)
    let private create baseNamePascalCase = TypeGenericName.GenericName baseNamePascalCase
    let ofPascalCase pascalCase = create pascalCase
    let fromCode tgCandidate = 
        if tgCandidate |> startsWithGenericPrefix then 
            tgCandidate |> removeGenericPrefix |> PascalCase |> create |> Some
        else 
            None
    let (|ValidCode|_|) = fromCode
    let createOfInterfaceName (interfaceName: InterfaceName) = create interfaceName.Value
    let createOfTypeTarget (typeTarget: TypeTarget) = 
        match typeTarget with 
        | TargetClassName className -> className |> InterfaceName.ofClassName |> createOfInterfaceName 
        | TargetInterfaceName interfaceName -> interfaceName |> createOfInterfaceName 
        | TargetReference _ | TargetValue _ | TargetValueNullable _ 
        | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ 
        | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> emptyString |> PascalCase |> create 

[<RequireQualifiedAccess>]
module internal TypeGenericParameterConstraint = 
    let create (typeGenericName, interfaceNameOpt) = { GenericName = typeGenericName; InterfaceName = interfaceNameOpt; IsClass = true; HasConstructor = true }

[<RequireQualifiedAccess>]
module internal TypeGenericParameter = 
    let createTyped interfaceName = 
        let typeGenericName = TypeGenericName.createOfInterfaceName interfaceName 
        { GenericName = typeGenericName; Constraint = TypeGenericParameterConstraint.create(typeGenericName, Some interfaceName) }
    let createUntyped basePascalCase = 
        let typeGenericName = TypeGenericName.ofPascalCase basePascalCase
        { GenericName = typeGenericName; Constraint = TypeGenericParameterConstraint.create(typeGenericName, None) }
    let createOfTypeTarget typeTarget = 
        match typeTarget with 
            | TargetClassName tcn -> tcn |> InterfaceName.ofClassName |> createTyped 
            | TargetInterfaceName interfaceName -> interfaceName |> createTyped 
            | TargetReference _ | TargetValue _ | TargetValueNullable _ | TargetGenericParameter _ 
            | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> emptyString |> PascalCase |> createUntyped 
    let codeConstraints (typeGenericParameters: TypeGenericParameter seq, tabCnt) = codeTabbedLines (typeGenericParameters |> Seq.map (fun x -> x.Constraint), tabCnt)

[<RequireQualifiedAccess>]
module internal MethodName = 
    let private create = PascalCase.create >> MethodName
    let ofPascalCase pascalCase = MethodName pascalCase
    let methodNameReadResult (interfaceName: InterfaceName) = create (CodeFrag.ReadResult.Code + interfaceName.Code);
    let methodNameReadResultTypeParameter typeGenericParameter = 
        match typeGenericParameter.Constraint.InterfaceName with 
        | Some i -> create (CodeFrag.ReadResult.Code + i.Code) 
        | None -> create emptyString
    let doubleName (MethodName pascalCase) = PascalCase.concat [|pascalCase; pascalCase|] |> ofPascalCase

[<RequireQualifiedAccess>]
module internal ParameterName = 
    let private create = CamelCase.create >> ParameterName
    let ofCamelCase camelCase = ParameterName camelCase

[<RequireQualifiedAccess>]
module internal FieldNameProtected = 
    let private create = CamelCase.create >> FieldNameProtected
    let ofCamelCase camelCase = FieldNameProtected camelCase

[<RequireQualifiedAccess>]
module internal TypeValue = 
    let private unionCache = UtilUnion.getUnionCases<TypeValue> |> Seq.cache 
    let fromCode tvCandidate = UtilUnion.createUnionCase<TypeValue> unionCache tvCandidate
    let internal (|ValidCode|_|) = fromCode
[<RequireQualifiedAccess>]
module internal TypeValueNullable = 
    let create typeValue = ValueNullable typeValue 
    let endsWithNullableSuffix = endsWith NULLABLE_SUFFIX
    let fromCode tvnCandidate = 
        if tvnCandidate |> endsWithNullableSuffix then 
            match ((NULLABLE_SUFFIX |> toCharArray), tvnCandidate) ||> trimEnd |> TypeValue.fromCode with
            | Some tv -> tv |> create |> Some
            | None -> None
        else 
            None
    let (|ValidCode|_|) = fromCode
[<RequireQualifiedAccess>]
module internal TypeReference = 
    let private unionCache = UtilUnion.getUnionCases<TypeReference> |> Seq.cache 
    let fromCode trCandidate = UtilUnion.createUnionCase<TypeReference> unionCache trCandidate
    let (|ValidCode|_|) = fromCode

[<RequireQualifiedAccess>]
module internal TypeArray = 
    let create typeComposable = typeComposable |> TypeArray.TypeArray
    let private (|ValidCodeFormat|_|) arrayCode =
        let len = length arrayCode
        if arrayCode.[(len-2)..(len-1)] = BRACKETS then arrayCode.[0..(len-3)] |> Some else None
    let rec fromCode arrayCandidate = 
        match arrayCandidate with
        | ValidCodeFormat typeCandidate -> 
            match typeCandidate with
            | ValidCode t                       -> t |> ComposableArray |> create |> Some
            | TypeValueNullable.ValidCode t     -> t |> ComposableValueNullable |> create |> Some
            | TypeValue.ValidCode t             -> t |> ComposableValue |> create |> Some
            | TypeReference.ValidCode t         -> t |> ComposableReference |> create |> Some
            //| TypeCollectionGeneric.ValidCode t -> t |> ComposableReference |> create |> Some // cyclic reference
            //| TypeGenericName.ValidCode t       -> t |> ComposableGenericParameter |> create |> Some
            | InterfaceName.ValidCode t         -> t |> ComposableInterfaceName |> create |> Some
            | ClassName.ValidCode t             -> t |> ComposableClassName |> create |> Some
            | _ -> None
        | _ -> None
    and (|ValidCode|_|) = fromCode

[<RequireQualifiedAccess;AutoOpen>]
module internal TypeTarget =
    let ofITypeTargetable (iTypeTargetable: ITypeTargetable) = 
        match iTypeTargetable with 
        | :? TypeValue as t             -> TargetValue t
        | :? TypeValueNullable as t     -> TargetValueNullable t
        | :? TypeReference as t         -> TargetReference t 
        | :? TypeGenericParameter as t  -> TargetGenericParameter t
        | :? ClassName as t             -> TargetClassName t
        | :? InterfaceName as t         -> TargetInterfaceName t
        | :? TypeCollectionGeneric as t -> TargetCollectionGeneric t
        | :? TypeArray as t             -> TargetArray t
        | :? TypeNone as t              -> TargetNone t
        | _                             -> TargetNone TypeNone.NoType
    let private isOdpNetLobType typeTarget = 
        match typeTarget with
        | TargetReference t -> t.Equals(TypeReference.OracleBlob) || t.Equals(TypeReference.OracleClob) // typeTarget.ToCode |> (endsWith @"lob")
        | TargetValue _ | TargetValueNullable _| TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsOdpNetLobType|_|) typeTarget = if (typeTarget |> isOdpNetLobType) then Some true else None
    let isOdpNet typeTarget = 
        match typeTarget with
        | TargetValue _ | TargetValueNullable _ | TargetReference _ -> typeTarget.Code |> (startsWith Oracle.Code)
        | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsOdpNet|_|) typeTarget = if (typeTarget |> isOdpNet) then Some true else None
    //let asNullable typeTarget = match typeTarget with | TargetValue t -> t.Nullable :> ITypeTargetable | _ -> typeTarget.AsITypeTargetable
    let isDataTable typeTarget = 
        match typeTarget with 
        | TargetReference tr -> tr.Equals(TypeReference.DataTable) 
        | TargetGenericParameter _ | TargetValue _ | TargetValueNullable _ | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let isTypeCollectionGeneric typeTarget = match typeTarget with | TargetCollectionGeneric t -> true | _-> false
    let getSubType typeTarget = 
        match typeTarget with 
        | TargetCollectionGeneric t                                                 -> t.SubType.AsITypeComposable :> ITypeTargetable
        | TargetArray t                                                             -> t.SubType.AsITypeComposable :> ITypeTargetable
        | TargetReference _ | TargetValue _ | TargetValueNullable _ 
        | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ 
        | TargetCollectionGeneric _ | TargetArray _ | TargetNone _                  -> TypeNone.NoType :> ITypeTargetable

[<RequireQualifiedAccess>]
module internal TypeComposable =
    let private createGenericParameter t    = t |> ComposableGenericParameter
    let private createValueNullable t       = t |> ComposableValueNullable 
    let private createValue t               = t |> ComposableValue
    let private createReference t           = t |> ComposableReference
    let private createClassName t           = t |> ComposableClassName
    let private createInterfaceName t       = t |> ComposableInterfaceName
    let private createArray t               = t |> ComposableArray
    let private createCollectionGeneric t   = t |> ComposableCollectionGeneric
    let private createNone t                = t |> ComposableNone
    let internal ofITypeComposable (iTypeComposable:ITypeComposable) = 
        match iTypeComposable with
        | :? TypeGenericParameter as t  -> createGenericParameter t
        | :? TypeValueNullable as t     -> createValueNullable t
        | :? TypeValue as t             -> createValue t
        | :? TypeReference as t         -> createReference t
        | :? ClassName as t             -> createClassName t
        | :? InterfaceName as t         -> createInterfaceName t
        | :? TypeArray as t             -> createArray t
        | :? TypeCollectionGeneric as t -> createCollectionGeneric t
        | :? TypeNone
        | _                             -> createNone TypeNone.NoType
    let internal ofITypeTargetable (iTypeTargetable:ITypeTargetable) = 
        match iTypeTargetable with
        | :? TypeGenericParameter as t  -> createGenericParameter t
        | :? TypeValueNullable as t     -> createValueNullable t
        | :? TypeValue as t             -> createValue t
        | :? TypeReference as t         -> createReference t
        | :? ClassName as t             -> createClassName t
        | :? InterfaceName as t         -> createInterfaceName t
        | :? TypeArray as t             -> createArray t
        | :? TypeCollectionGeneric as t -> createCollectionGeneric t
        | :? TypeNone
        | _                             -> createNone TypeNone.NoType
    let internal ofITypeTargetableOption (iTypeTargetable: ITypeTargetable) =
        match iTypeTargetable |> TypeTarget.ofITypeTargetable with
            | TargetNone _                                                                          -> None
            | TargetValueNullable _ | TargetValue _ | TargetReference _ | TargetGenericParameter _ 
            | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ -> iTypeTargetable |> ofITypeTargetable |> Some; 
    let isEquals (typeComposable: TypeComposable) (iTypeComposable: ITypeComposable) = typeComposable.Equals(ofITypeComposable iTypeComposable)

[<RequireQualifiedAccess>]
module internal TypeCollection = 
    let private unionCache = UtilUnion.getUnionCases<TypeCollection> |> Seq.cache 
    let fromCode tcCandidate = (UtilUnion.createUnionCase<TypeCollection> unionCache tcCandidate) 
    let (|ValidCode|_|) = fromCode
[<RequireQualifiedAccess>]
module internal TypeCollectionGeneric = 
    let create (typeCollection, subType) = { TypeCollection = typeCollection; SubType = subType }
    let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ]) else None            
    let private (|ValidCodeFormat|_|) tcgCode =
        match tcgCode with
        | Regex @"(.*)<(.*)>" [typeCollectionStr; typeComposableStr] -> (typeCollectionStr, typeComposableStr) |> Some
        | _ -> None
    let rec fromCode tcgCandidate = 
        match tcgCandidate with
        | ValidCodeFormat (typeCollectionStr, typeComposableStr) -> 
            match typeCollectionStr with
            | TypeCollection.ValidCode typeCollection -> 
                match typeComposableStr with
                | TypeValueNullable.ValidCode t -> (typeCollection, ComposableValueNullable t) |> create |> Some
                | TypeValue.ValidCode t         -> (typeCollection, ComposableValue t) |> create |> Some
                | TypeReference.ValidCode t     -> (typeCollection, ComposableReference t) |> create |> Some
                | TypeArray.ValidCode t         -> (typeCollection, ComposableArray t) |> create |> Some
                | ValidCode t                   -> (typeCollection, ComposableCollectionGeneric t) |> create |> Some
                | TypeGenericName.ValidCode t   -> (typeCollection, t.Value |> TypeGenericParameter.createUntyped |> ComposableGenericParameter) |> create |> Some
                | InterfaceName.ValidCode t     -> (typeCollection, ComposableInterfaceName t) |> create |> Some
                | ClassName.ValidCode t         -> (typeCollection, ComposableClassName t) |> create |> Some
                | _ -> None
            | _ -> None
        | _ -> None
    and (|ValidCode|_|) = fromCode 
    let ofTypeTarget (typeCollection, typeTarget) = 
        match typeTarget with 
        | TargetValueNullable t     -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetValue t             -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetReference t         -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetGenericParameter t  -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetClassName t         -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetInterfaceName t     -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetCollectionGeneric t -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetArray t             -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetNone _              -> create(typeCollection, TypeReference.Void :> ITypeComposable |> TypeComposable.ofITypeComposable)
    //let internal fromCodeToCodeGenericCollectionType (tcStr, tsStr) =
    //    let tc, ts = TypeCollection.fromCode tcStr, TypeComposable.fromCode tsStr
    //    match tc, ts with
    //    | Some tc, Some ts ->  (create (tc, ts) :> ICodeable).ToCode
    //    | _ -> emptyString
    //let internal fromCodeToCodeGenericCollectionTypeSubType (tcgStr, forceValueNullableToValue) =
    //    match tcgStr with
    //    | ValidCode tcg ->
    //        match tcg.SubType with
    //        | ComposableGeneric t           -> (t :> ICodeable).ToCode
    //        | ComposableValueNullable t     -> (if forceValueNullableToValue then t.TypeValue :> ICodeable else t :> ICodeable).ToCode
    //        | ComposableValue t             -> (t :> ICodeable).ToCode
    //        | ComposableReference t         -> (t :> ICodeable).ToCode
    //        | ComposableClassName t         -> (t :> ICodeable).ToCode
    //        | ComposableArray t             -> (t :> ICodeable).ToCode
    //        | ComposableCollectionGeneric t -> (t :> ICodeable).ToCode
    //    | _ -> emptyString //failwith $"'{tcgStr}' is not valid C# code for {nameof TypeCollectionGeneric}"

[<RequireQualifiedAccess>]
module internal Property = 
    let create (propertyName, propertyType, containerType, getSet, accessModifier, backingField: FieldProtected option, isVirtual, isDataMember, isXmlElement) = 
        { PropertyName = propertyName; PropertyType = propertyType; ContainerType = containerType; GetSet = getSet; AccessModifier = accessModifier;
        IsVirtual = isVirtual; IsDataMember = isDataMember; IsXmlElement = isXmlElement; 
        BackingField = (Option.map (fun bf -> (if bf.FieldType = propertyType then Some bf else None)) backingField) |> Option.flatten }
    let createForInterface (propertyName, propertyType, containerType, getSet) = create (propertyName, propertyType, containerType, getSet, None, None, false, false, false)

[<RequireQualifiedAccess>]
module internal TypeInterface = 
    let create (accessModifier: AccessModifierInterface, interfaceName: InterfaceName, properties: Property seq, ancestorInterfaceNames : InterfaceName seq option) = 
        { AccessModifier = accessModifier; InterfaceName = interfaceName; Properties = properties; AncestorInterfaceNames = ancestorInterfaceNames }
    let dtoInterfacePropertyAccessor (dtoInterfaceCategory: DtoInterfaceCategory) = 
        match dtoInterfaceCategory with
        | DtoInterfaceCategory.MutableSet       -> PropertyAccessor.SetOnly
        | DtoInterfaceCategory.ImmutableGetInit -> PropertyAccessor.GetInit
    let private codeAncestorInterfaceNames (interfaceNamesOption : InterfaceName seq option) = 
        (match interfaceNamesOption with
        | Some interfaceNames -> 
            if Seq.isEmpty interfaceNames then emptyString 
            else codeSpaced[|COLON; (interfaceNames |> Seq.map(fun x -> x :> Object) |> codeCommaSpaced)|]
        | None -> emptyString);
    let codeFirstLine (typeInterface: TypeInterface) = 
        codeSpaced[|typeInterface.AccessModifier; INTERFACE; typeInterface.InterfaceName; codeAncestorInterfaceNames typeInterface.AncestorInterfaceNames; CURLY_OPEN|] 
    let private code typeInterface = 
        codeFirstLine typeInterface
        + if Seq.isEmpty typeInterface.Properties then emptyString else NEWLINE + codeTabbedLines (typeInterface.Properties, 1u)
        + NEWLINE + codeSpaced[|CURLY_CLOSE; @"//"; typeInterface.InterfaceName|]
    let codeTabbed tabCnt typeInterface = Coder.codeTabbed tabCnt (typeInterface |> code)

    //let toTypeClass interfaceName =
    //    let removeInterfacePrefix = replace(Ltrl_CODE_INTEFACE_PREFIX, emptyString)
    //    let startsWithInterfacePrefix str = startsWith Ltrl_CODE_INTEFACE_PREFIX str
    //    if not <| isNullOrWhiteSpace interfaceName
    //        && startsWithInterfacePrefix interfaceName
    //        && (0, 2) |> interfaceName.Substring = ((0, 2) |> interfaceName.Substring).ToUpper() then
    //        interfaceName |> removeInterfacePrefix |> TypeClass.create
    //    else
    //        TypeClass.create interfaceName

[<RequireQualifiedAccess>]
module internal OdpNetOracleDbTypeEnum =
    /// <summary>Determines OracleDbTypeEnum for a numeric C# nullable value type</summary>
    /// <returns>Valid OracleDbTypeEnum. If parameter is non-numeric, a OdpNetOracleDbTypeEnum.Byte is 
    /// returned to both avoid throwing an exception and forcing C# to handle with F# None.</returns>
    let fromTypeValueNullableNumeric (ValueNullable tv) = 
        match tv with
        | TypeValue.SByte | TypeValue.Byte              -> OdpNetOracleDbTypeEnum.Byte
        | TypeValue.Int16 | TypeValue.UInt16            -> OdpNetOracleDbTypeEnum.Int16
        | TypeValue.Int32 | TypeValue.UInt32            -> OdpNetOracleDbTypeEnum.Int32
        | TypeValue.Int64 | TypeValue.UInt64            -> OdpNetOracleDbTypeEnum.Int64
        | TypeValue.Decimal | TypeValue.OracleDecimal   -> OdpNetOracleDbTypeEnum.Decimal
        | TypeValue.Double                              -> OdpNetOracleDbTypeEnum.BinaryDouble
        | TypeValue.Single                              -> OdpNetOracleDbTypeEnum.BinaryFloat
        | _                                             -> OdpNetOracleDbTypeEnum.Byte 

[<RequireQualifiedAccess>]
module internal MethodNameReaderGetter =
    let getMethodNameSafeType cSharpOdpNetSafeType = 
        match cSharpOdpNetSafeType with 
        | TargetReference t when t.Equals(TypeReference.OracleBlob) -> MethodNameReaderGetter.GetOracleBlob
        | TargetReference t when t.Equals(TypeReference.OracleClob) -> MethodNameReaderGetter.GetOracleClob
        | TargetValueNullable _ | TargetValue _ | TargetReference _ | TargetGenericParameter _ 
        | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> MethodNameReaderGetter.GetOracleValue

[<AutoOpen>]
module internal CodeLogic =
    let isRequiresParseFromOutParameter typeTarget = 
        let typeValueMatch = TypeValue.DateTimeOffset
        match typeTarget with 
        | TargetValue t         -> t.Equals(typeValueMatch) 
        | TargetValueNullable t -> t.TypeValue.Equals(typeValueMatch) 
        | TargetReference _ | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsRequiresParseFromOutParameter|_|) typeTarget = if typeTarget |> isRequiresParseFromOutParameter then Some true else None
    let private isRequiresOracleDecimalSetPrecision typeTarget = 
        let typeValueMatch = TypeValue.Decimal
        match typeTarget with 
        | TargetValue t         -> t.Equals(typeValueMatch)
        | TargetValueNullable t -> t.TypeValue.Equals(typeValueMatch)
        | TargetReference _ | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _| TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsRequiresOracleDecimalSetPrecision|_|) typeTarget = if (typeTarget |> isRequiresOracleDecimalSetPrecision) then Some true else None 
    let isRequiresOutParmBindSize typeTarget = 
        match typeTarget with 
        | TargetReference t -> t.Equals(TypeReference.String) 
        | TargetValue _ | TargetValueNullable _ |TargetReference _ | TargetGenericParameter _ | TargetClassName _ | TargetInterfaceName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let codeReadResultAssignment(cSharpType, cSharpOdpNetSafeType, readerName:string, position:int, objectName:string, propertyName:PropertyName) =
        let assignmentTarget = codeAdjacent([|IF ; @" (!" ; readerName ; PERIOD ; $"{IsDBNull}({position})) " ; objectName ; PERIOD ; propertyName|]) 
        let assignmentValue = 
            match (cSharpType, cSharpOdpNetSafeType) with 
            | (IsRequiresOracleDecimalSetPrecision x, _) -> 
                $"({TypeValue.Decimal.Nullable}){codeDotted[|OracleDecimal;SetPrecision|]}({readerName}.{GetOracleDecimal}{inParens(position.ToString())}, 28)"
            | (IsRequiresParseFromOutParameter x, _) -> 
                codeAdjacent[cSharpType.SansNullable ; PERIOD ; Parse ; "(" ; readerName ; PERIOD ; GetValue ; inParens(position.ToString()) ; PERIOD ; $"ToString()"; ")"];
            | (IsOdpNet x, _) -> 
                codeAdjacent["(" ; cSharpType ; ")" ; readerName ; PERIOD ; (cSharpType |> MethodNameReaderGetter.getMethodNameSafeType) ; inParens(position.ToString())];
            | (_, IsOdpNetLobType x) -> 
                $"{readerName}.{(cSharpOdpNetSafeType |> MethodNameReaderGetter.getMethodNameSafeType)}{inParens(position.ToString())}.{Value}";
            | _  -> 
            codeAdjacent[CodeFrag.Convert ; PERIOD ; To ; cSharpType.SansNullable ; "(" ; readerName ; PERIOD ; GetValue ; inParens(position.ToString()) ; ")" ]; 
        codeSpaced [|assignmentTarget; EQUALS; assignmentValue|] + SEMICOLON

[<AutoOpen>]
module internal Decode =
    let fromCode typeCandidateStr = 
        match typeCandidateStr with 
        | TypeCollectionGeneric.ValidCode t     -> t :> ITypeTargetable |> Some
        | TypeArray.ValidCode t                 -> t :> ITypeTargetable |> Some
        //| TypeGenericParameter.ValidCode t      -> t :> ITypeTargetable |> Some
        | TypeReference.ValidCode t             -> t :> ITypeTargetable |> Some
        | TypeValueNullable.ValidCode t         -> t :> ITypeTargetable |> Some
        | TypeValue.ValidCode t                 -> t :> ITypeTargetable |> Some
        | InterfaceName.ValidCode t             -> t :> ITypeTargetable |> Some
        | ClassName.ValidCode t                 -> t :> ITypeTargetable |> Some
        //| TypeNone.ValidCode t                -> t :> ITypeTargetable |> Some
        | _ -> None 