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

open Odapter;
open Odapter.Casing;
open Odapter.CSharp
open System
open System.Text.RegularExpressions

[<AutoOpen>]
module internal CoderGeneric =
    // Code a series of tabs
    let rec internal codeTab n = if n = 0u then String.Empty else replicate 4 SPACE + codeTab (n - 1u)

    //let toCodeTabbed : ToCodeTabbed = fun(codeable:ICodeable, tabCnt) -> 
    //    let splitNewLine str = (split NEWLINE str) |> Array.toSeq
    //    let joinNewLine strings = join NEWLINE strings
    //    codeable.Code |> splitNewLine |> Seq.map(fun s -> (if isNullOrWhiteSpace s then emptyString else codeTab tabCnt) + s) |> joinNewLine
    let toCodeTabbed (object:Object, tabCnt) =
        let splitNewLine str = (split NEWLINE str) |> Array.toSeq
        let joinNewLine strings = join NEWLINE strings
        object.ToString() |> splitNewLine |> Seq.map(fun s -> (if isNullOrWhiteSpace s then emptyString else codeTab tabCnt) + s) |> joinNewLine
        // Before: abc NL def NL ghi NL ""
        // Split: abc def ghi ""
        // After: TABBEDabc NL TABBEDdef NL TABBEDghi NL ""
    let toCodeTabbed1 object = toCodeTabbed (object, 1u)
    let toCodeTabbed2 object = toCodeTabbed (object, 2u)
    let toCodeTabbed3 object = toCodeTabbed (object, 3u)
    let toCodeTabbed4 object = toCodeTabbed (object, 4u)
    let toCodeTabbed5 object = toCodeTabbed (object, 5u)
    let toCodeTabbed6 object = toCodeTabbed (object, 6u)

    let private toCodeDelimited delimiter (objects: Object seq) = join delimiter (objects |> Seq.map(fun x -> x.ToString()))
    /// Code a collection of codeables as space delimited
    let toCodeAdjacent (objects: Object seq) = toCodeDelimited emptyString objects
    /// Code a collection of codeables as space delimited
    let toCodeSpaced (objects: Object seq) = toCodeDelimited SPACE objects
    /// Code a collection of codeables delimited by a comma and space
    let toCodeCommaSpaced (objects: Object seq) = toCodeDelimited ", " objects
    /// Code a collection of codeables as dot delimited
    let toCodeDotted (objects: Object seq) = toCodeDelimited PERIOD objects

[<AutoOpen>]
module internal CodeBuilder =
    let inParens code = $"({code})"
    let inAngles code = $"<{code}>"
    let inBraces code = @"{" + $"{code}" + @"}"
    let inBrackets code = $"[{code}]"

[<RequireQualifiedAccess>]
module internal CSharpVersion =
    let private unionCache = UtilUnion.getUnionCases<CSharpVersion> |> Seq.cache
    let ofString str = (UtilUnion.createUnionCase<CSharpVersion> unionCache str) 

[<RequireQualifiedAccess>]
module internal Keyword = 
    let private unionCache = UtilUnion.getUnionCases<Keyword> |> Seq.cache
    let isKeyword keywordCandidate = unionCache |> Seq.exists (fun kw -> toUpper kw.Name = toUpper keywordCandidate)

[<RequireQualifiedAccess>]
module internal Namespace = 
    let ofPascalCase pascalCase = Namespace pascalCase
    let create segments = (join PERIOD segments) |> PascalCase |> Namespace 
    let value = WrappedString.value    
    let toCodeUsing (nmspace:Namespace) = (toCodeSpaced [|USING;nmspace|]) + SEMICOLON

[<RequireQualifiedAccess>]
module internal PropertyName = 
    let ofPascalCase pascalCase = PropertyName pascalCase
    let private create = PascalCase.create >> PropertyName
    let fromCode propertyNameCandidate = create propertyNameCandidate
    let (|ValidCode|_|) typeStr = typeStr |> fromCode |> Some

[<RequireQualifiedAccess>]
module internal ClassName = 
    let ofPascalCase pascalCase = ClassName pascalCase
    let fromCode = PascalCase.create >> ClassName
    let (|ValidCode|_|) typeStr = typeStr |> fromCode |> Some
    //let FromCodeOfInterfaceToCodeClass interfaceCode =
    //    interfaceCode 
    //    |> TypeInterface.createOfText 
    //    |> Option.map(fun i -> i |> TypeClass.createOfInterface) 
    //    |> Option.bind(fun c -> (c :> ICodeable).ToCode |> Some)
    //    |> Option.defaultValue emptyString

[<RequireQualifiedAccess>]
module internal InterfaceName = 
    let [<Literal>] private INTEFACE_PREFIX = @"I"
    let private prefixPascalCase = PascalCase.create INTEFACE_PREFIX
    let private startsWithInterfacePrefix str = startsWith INTEFACE_PREFIX str
    //let private (|ValidCodeFormat|_|) str =
    //    match startsWithInterfacePrefix str with
    //    | true -> Some str
    //    | false -> Some str
    let create baseStr = (INTEFACE_PREFIX + baseStr) |> PascalCase |> InterfaceName
    let private createOfPascalCase basePascalCase = PascalCase.concat [|prefixPascalCase; basePascalCase|] |> InterfaceName
    let private ofTypeGenericName (typeGenericName: TypeGenericName) = typeGenericName.Value |> create 
    let ofTypeTarget typeTarget = match typeTarget with | TargetGenericName tgn -> ofTypeGenericName tgn | _ -> create emptyString 
    let ofClassName (className: ClassName) = createOfPascalCase className.Value

[<RequireQualifiedAccess>]
module internal TypeGenericName = 
    let private startsWithGenericPrefix = startsWith Literal.GENERIC_PREFIX
    let private removeGenericPrefix = fun s -> (if startsWithGenericPrefix s then subStringToEnd Literal.GENERIC_PREFIX.Length s else s)
    let private create baseName = WrappedString.create (*canonicalize:*)singleLineTrimmed TypeGenericName.GenericName baseName
    let ofCamelCase (CamelCase camelCaseStr) = create camelCaseStr
    let private ofPascalCase (PascalCase pascalCaseStr) = create pascalCaseStr
    let fromCode tgCandidate = 
        if tgCandidate |> startsWithGenericPrefix then 
            tgCandidate |> removeGenericPrefix |> create |> Some
        else 
            None
    let (|ValidCode|_|) typeStr = typeStr |> fromCode
    let createOfClassName (className: ClassName) = ofPascalCase className.Value
    let createOfTypeTarget (typeTarget: TypeTarget) = match typeTarget with | TargetClassName className -> createOfClassName className | _ -> create emptyString

[<RequireQualifiedAccess>]
module internal MethodName = 
    let private create = PascalCase.create >> MethodName
    let ofPascalCase pascalCase = MethodName pascalCase
    let methodNameReadResult (interfaceName:InterfaceName) = create (CodeFrag.ReadResult.Code + interfaceName.Code);

[<RequireQualifiedAccess>]
module internal TypeValue = 
    let private unionCache = UtilUnion.getUnionCases<TypeValue> |> Seq.cache 
    let fromCode tvCandidate = UtilUnion.createUnionCase<TypeValue> unionCache tvCandidate
    let internal (|ValidCode|_|) typeStr = typeStr |> fromCode
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
    let (|ValidCode|_|) typeStr = typeStr |> fromCode
[<RequireQualifiedAccess>]
module internal TypeReference = 
    let private unionCache = UtilUnion.getUnionCases<TypeReference> |> Seq.cache 
    let fromCode trCandidate = UtilUnion.createUnionCase<TypeReference> unionCache trCandidate
    let (|ValidCode|_|) typeStr = typeStr |> fromCode

[<RequireQualifiedAccess>]
module internal TypeArray = 
    let create typeComposable = typeComposable |> TypeArray.TypeArray
    //let [<Literal>] private BRACKETS = @"[]"
    let private (|ValidCodeFormat|_|) arrayCode =
        let len = length arrayCode
        if arrayCode.[(len-2)..(len-1)] = BRACKETS then arrayCode.[0..(len-3)] |> Some else None
    let rec fromCode arrayCandidate = 
        match arrayCandidate with
        | ValidCodeFormat typeCandidate -> 
            match typeCandidate with
            | ValidCode t                   -> t |> ComposableArray |> create |> Some
            | TypeGenericName.ValidCode t   -> t |> ComposableGeneric |> create |> Some
            | TypeValueNullable.ValidCode t -> t |> ComposableValueNullable |> create |> Some
            | TypeValue.ValidCode t         -> t |> ComposableValue |> create |> Some
            | TypeReference.ValidCode t     -> t |> ComposableReference |> create |> Some
            | ClassName.ValidCode t         -> t |> ComposableClassName |> create |> Some
            | _ -> None
        | _ -> None
    and (|ValidCode|_|) typeStr = fromCode typeStr

[<RequireQualifiedAccess>]
module internal TypeComposable =
    let private createGeneric t             = t |> ComposableGeneric
    let private createValueNullable t       = t |> ComposableValueNullable 
    let private createValue t               = t |> ComposableValue
    let private createReference t           = t |> ComposableReference
    let private createClassName t           = t |> ComposableClassName
    let private createArray t               = t |> ComposableArray
    let private createCollectionGeneric t   = t |> ComposableCollectionGeneric
    let internal ofITypeComposable (iTypeComposable:ITypeComposable) = 
        match iTypeComposable with
        | :? TypeGenericName as t       -> createGeneric t
        | :? TypeValueNullable as t     -> createValueNullable t
        | :? TypeValue as t             -> createValue t
        | :? TypeReference as t         -> createReference t
        | :? ClassName as t             -> createClassName t
        | :? TypeArray as t             -> createArray t
        | :? TypeCollectionGeneric as t -> createCollectionGeneric t
        | _ -> failwith $"Type '{iTypeComposable.Code}' not recognized as valid {nameof TypeComposable}"
    //let fromCode typeCandidate =
    //    match typeCandidate with
    //    | TypeArray.ValidCode t         -> t |> ComposableArray |> Some
    //    | TypeGenericName.ValidCode t   -> t |> ComposableGeneric |> Some
    //    | TypeValueNullable.ValidCode t -> t |> ComposableValueNullable |> Some
    //    | TypeValue.ValidCode t         -> t |> ComposableValue |> Some
    //    | TypeReference.ValidCode t     -> t |> ComposableReference |> Some
    //    | ClassName.ValidCode t         -> t |> ComposableClassName |> Some
    //    //| TypeCollectionGeneric.ValidCode     cyclic reference
    //    | _                             -> None 
    //let fromCodeToCodeAsNullable typeCandidate =
    //    match fromCode typeCandidate with
    //    | Some (ComposableValue tv) -> (tv.Nullable :> ICodeable).ToCode 
    //    | Some typeComposable       -> (typeComposable :> ICodeable).ToCode
    //    | None                      -> emptyString
    let isEquals (typeComposable:TypeComposable) (iTypeComposable:ITypeComposable) = typeComposable.Equals(ofITypeComposable iTypeComposable)

[<RequireQualifiedAccess>]
module internal TypeCollection = 
    let private unionCache = UtilUnion.getUnionCases<TypeCollection> |> Seq.cache 
    let fromCode tcCandidate = (UtilUnion.createUnionCase<TypeCollection> unionCache tcCandidate) 
    let (|ValidCode|_|) str = fromCode str
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
                | TypeGenericName.ValidCode t   -> (typeCollection, ComposableGeneric t) |> create |> Some
                | TypeValueNullable.ValidCode t -> (typeCollection, ComposableValueNullable t) |> create |> Some
                | TypeValue.ValidCode t         -> (typeCollection, ComposableValue t) |> create |> Some
                | TypeReference.ValidCode t     -> (typeCollection, ComposableReference t) |> create |> Some
                | TypeArray.ValidCode t         -> (typeCollection, ComposableArray t) |> create |> Some
                | ClassName.ValidCode t         -> (typeCollection, ComposableClassName t) |> create |> Some
                | ValidCode t                   -> (typeCollection, ComposableCollectionGeneric t) |> create |> Some
                | _ -> None
            | _ -> None
        | _ -> None
    and (|ValidCode|_|) tcgCandidate = fromCode tcgCandidate
    let ofTypeTarget (typeCollection, typeTarget) = 
        match typeTarget with 
        | TargetValueNullable t     -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetValue t             -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetReference t         -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetGenericName t       -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
        | TargetClassName t         -> create(typeCollection, t :> ITypeComposable |> TypeComposable.ofITypeComposable)
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
    let create (propertyName, propertyType) = { PropertyName = propertyName; PropertyType = propertyType }
[<RequireQualifiedAccess>]
module internal TypeInterface = 
    let create (interfaceName, properties) = { InterfaceName = InterfaceName.create interfaceName; Properties = properties }
    let toCode typeInterface = 
        $"{toCodeSpaced[AccessModifier.PUBLIC; INTERFACE; typeInterface.InterfaceName]} {CURLY_OPEN}" + NEWLINE 
        // property code goes here...
        + CURLY_CLOSE
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
    /// <summary>Determines OracleDbTypeEnum for numeric C# type value nullable</summary>
    /// <param name="t">Type value</param>
    let fromTypeValueNullableNumeric (ValueNullable tv) = 
        match tv with
        | TypeValue.SByte | TypeValue.Byte              -> OdpNetOracleDbTypeEnum.Byte
        | TypeValue.Int16 | TypeValue.UInt16            -> OdpNetOracleDbTypeEnum.Int16
        | TypeValue.Int32 | TypeValue.UInt32            -> OdpNetOracleDbTypeEnum.Int32
        | TypeValue.Int64 | TypeValue.UInt64            -> OdpNetOracleDbTypeEnum.Int64
        | TypeValue.Decimal | TypeValue.OracleDecimal   -> OdpNetOracleDbTypeEnum.Decimal
        | TypeValue.Double                              -> OdpNetOracleDbTypeEnum.BinaryDouble
        | TypeValue.Single                              -> OdpNetOracleDbTypeEnum.BinaryFloat
        | _                                             -> OdpNetOracleDbTypeEnum.Byte //failwith $"{tv.ToString} not defined as *numeric* {nameof TypeValue} in fromTypeValueNumeric" 

[<RequireQualifiedAccess;AutoOpen>]
module internal TypeTarget =
    let ofITypeTargetable (iTypeTargetable: ITypeTargetable) = 
        match iTypeTargetable with 
        | :? TypeValue as t             -> TargetValue t
        | :? TypeValueNullable as t     -> TargetValueNullable t
        | :? TypeReference as t         -> TargetReference t 
        | :? TypeGenericName as t       -> TargetGenericName t
        | :? ClassName as t             -> TargetClassName t
        | :? TypeCollectionGeneric as t -> TargetCollectionGeneric t
        | :? TypeArray as t             -> TargetArray t
        | _                             -> TargetNone TypeNone.NoType
    let private isOdpNetLobType typeTarget = 
        match typeTarget with
        | TargetReference t -> t.Equals(TypeReference.OracleBlob) || t.Equals(TypeReference.OracleClob) // typeTarget.ToCode |> (endsWith @"lob")
        | TargetValue _ | TargetValueNullable _| TargetGenericName _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsOdpNetLobType|_|) typeTarget = if (typeTarget |> isOdpNetLobType) then Some true else None
    let isOdpNet typeTarget = 
        match typeTarget with
        | TargetValue _ | TargetValueNullable _ | TargetReference _ -> typeTarget.Code |> (startsWith @"Oracle")
        | TargetGenericName _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsOdpNet|_|) typeTarget = if (typeTarget |> isOdpNet) then Some true else None
    //let asNullable typeTarget = match typeTarget with | TargetValue t -> t.Nullable :> ITypeTargetable | _ -> typeTarget.AsITypeTargetable
    let isDataTable typeTarget = 
        match typeTarget with 
        | TargetReference tr -> tr.Equals(TypeReference.DataTable) 
        | TargetGenericName _ | TargetValue _ | TargetValueNullable _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let isTypeCollectionGeneric typeTarget = match typeTarget with | TargetCollectionGeneric t -> true | _-> false
    let getSubType typeTarget = 
        match typeTarget with 
        | TargetCollectionGeneric t -> t.SubType.AsITypeComposable :> ITypeTargetable
        | TargetArray t             -> t.SubType.AsITypeComposable :> ITypeTargetable
        | _                         -> TypeNone.NoType :> ITypeTargetable

[<RequireQualifiedAccess>]
module internal MethodNameReaderGetter =
    let getMethodNameSafeType cSharpOdpNetSafeType = 
        match cSharpOdpNetSafeType with 
        | TargetReference t when t.Equals(TypeReference.OracleBlob) -> MethodNameReaderGetter.GetOracleBlob
        | TargetReference t when t.Equals(TypeReference.OracleClob) -> MethodNameReaderGetter.GetOracleClob
        | TargetValueNullable _ | TargetValue _ | TargetReference _ | TargetGenericName _ 
        | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> MethodNameReaderGetter.GetOracleValue

[<AutoOpen>]
module internal CodeLogic =
    let isRequiresParseFromOutParameter typeTarget = 
        let typeValueMatch = TypeValue.DateTimeOffset
        match typeTarget with 
        | TargetValue t         -> t.Equals(typeValueMatch) 
        | TargetValueNullable t -> t.TypeValue.Equals(typeValueMatch) 
        | TargetReference _ | TargetGenericName _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsRequiresParseFromOutParameter|_|) typeTarget = if typeTarget |> isRequiresParseFromOutParameter then Some true else None
    let private isRequiresOracleDecimalSetPrecision typeTarget = 
        let typeValueMatch = TypeValue.Decimal
        match typeTarget with 
        | TargetValue t         -> t.Equals(typeValueMatch)
        | TargetValueNullable t -> t.TypeValue.Equals(typeValueMatch)
        | TargetReference _ | TargetGenericName _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let (|IsRequiresOracleDecimalSetPrecision|_|) typeTarget = if (typeTarget |> isRequiresOracleDecimalSetPrecision) then Some true else None 
    let isRequiresOutParmBindSize typeTarget = 
        match typeTarget with 
        | TargetReference t -> t.Equals(TypeReference.String) 
        | TargetValue _ | TargetValueNullable _ |TargetReference _ | TargetGenericName _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> false
    let codeReadResultAssignmentValue(cSharpType, cSharpOdpNetSafeType, paramReaderName, position) =
        let decimalTypeMaxPrecision = 28;
        match (cSharpType, cSharpOdpNetSafeType) with 
        | (IsRequiresOracleDecimalSetPrecision x, _) -> 
            $"({TypeValue.Decimal.Nullable}){toCodeDotted[|OracleDecimal;SetPrecision|]}({paramReaderName}.{GetOracleDecimal}{inParens(position.ToString())}, {decimalTypeMaxPrecision})"
        | (IsRequiresParseFromOutParameter x, _) -> 
            toCodeAdjacent[cSharpType.SansNullable; "." ; Parse ; "(" ; paramReaderName ; "." ; GetValue ; inParens(position.ToString()) ; ".ToString())"];
        | (IsOdpNet x, _) -> 
            toCodeAdjacent["(" ; cSharpType ; ")" ; paramReaderName ; "." ; (cSharpType |> MethodNameReaderGetter.getMethodNameSafeType) ; inParens(position.ToString())];
        | (_, IsOdpNetLobType x) -> 
            $"{paramReaderName}.{(cSharpOdpNetSafeType |> MethodNameReaderGetter.getMethodNameSafeType)}{inParens(position.ToString())}.Value";
        | _  -> 
            toCodeAdjacent["Convert.To" ; cSharpType.SansNullable; "(" ; paramReaderName; "." ; GetValue ; inParens(position.ToString()) ; ")" ]; 

[<AutoOpen>]
module internal Decode =
    let fromCode typeCandidateStr = 
        match typeCandidateStr with 
        | TypeCollectionGeneric.ValidCode t     -> t :> ITypeTargetable |> Some
        | TypeArray.ValidCode t                 -> t :> ITypeTargetable |> Some
        | TypeGenericName.ValidCode t           -> t :> ITypeTargetable |> Some
        | TypeReference.ValidCode t             -> t :> ITypeTargetable |> Some
        | TypeValueNullable.ValidCode t         -> t :> ITypeTargetable |> Some
        | TypeValue.ValidCode t                 -> t :> ITypeTargetable |> Some
        | ClassName.ValidCode t                 -> t :> ITypeTargetable |> Some
        //| TypeNone.ValidCode t                -> t :> ITypeTargetable |> Some
        | _ -> None 

[<AutoOpen>]
module private UtilApi =
    let nullableToOption = function | null -> None | x -> Some x
    let optionToNullable = function | Some x -> x | None -> null
    let ifNoneThrowElseValue (optionType, errorMsg) = if optionType |> Option.isNone then failwith errorMsg else optionType.Value 
    let failwithNullOrEmpty desc = failwith $"{desc} cannot be null or empty" 

/// API for consumption by C#
module Api =
    // coding validation
    let IsTypeCollectionGeneric (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isTypeCollectionGeneric// match iTypeTargetable with | :? TypeCollectionGeneric -> true | _ -> false
    let IsOdpNet (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isOdpNet
    let IsDataTable (iTypeTargetable: ITypeTargetable) =  iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.isDataTable
    let IsRequiresParseFromOutParameter (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> CodeLogic.isRequiresParseFromOutParameter 
    let IsRequiresOutParmBindSize (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> CodeLogic.isRequiresOutParmBindSize 

    // decoding (transitional/temporary)
    //let FromCodeTypeValue str               = ifNoneThrowElseValue(TypeValue.fromCode str, $"'{str}' is not valid C# code for {nameof TypeValue}")
    //let FromCodeTypeCollection str          = ifNoneThrowElseValue(TypeCollection.fromCode str, $"'{str}' is not valid C# code for {nameof TypeCollection}")
    //let FromCodeTypeCollectionGeneric str   = ifNoneThrowElseValue(TypeCollectionGeneric.fromCode str, $"'{str}' is not valid C# code for {nameof TypeCollectionGeneric}")
    //let FromCodeTypeComposable str          = ifNoneThrowElseValue(TypeComposable.fromCode str, $"'{str}' is not valid C# code for {nameof TypeComposable}")
    //let FromCodeTypeTargetable str          = ifNoneThrowElseValue(fromCode str,  $"'{str}' is not valid C# code for {nameof ITypeTargetable}")
    //let IsCodeValidGenericCollectionType typeCandidate = typeCandidate |> (TypeCollectionGeneric.fromCode >> Option.isSome)
    //let IsCodeKeyword kw = Keyword.isKeyword kw    
    //let IsCodeOdpNet typeStr = typeStr |> Decode.fromCode |> Option.map(fun t -> TypeUtil.isOdpNet t) |> (Option.defaultValue false)

    // decoding for types persisted in config file
    let FromCodeTypeCollectionWithDefault str typeCollectionDefault = str |> TypeCollection.fromCode |> (Option.defaultValue typeCollectionDefault)
    let FromCodeTypeTargetableWithDefault str typeTargetableDefault = str |> fromCode |> (Option.defaultValue typeTargetableDefault)
    let FromCodeTypeValueWithDefault str typeValueDefault = str |> TypeValue.fromCode |> (Option.defaultValue typeValueDefault)
    let FromCodeTypeReferenceWithDefault str typeReferenceDefault = str |> TypeReference.fromCode |> (Option.defaultValue typeReferenceDefault)
    let CSharpVersionOfStringWithDefault str cSharpVersionDefault = str |> CSharpVersion.ofString |> (Option.defaultValue cSharpVersionDefault)

    // coding
    let CodeTab n = codeTab n
    let CodeAdjacent (objects: Object seq) = toCodeAdjacent objects
    let CodeSpaced (objects: Object seq) = toCodeSpaced objects
    let CodeCommaSpaced (objects: Object seq) = toCodeCommaSpaced objects
    let CodeDotted (objects: Object seq) = toCodeDotted objects
    let CodeUsing(nmspace: Namespace) = Namespace.toCodeUsing nmspace
    let CodeReadResultAssignmentValue (cSharpType: ITypeTargetable, cSharpOdpNetSafeType: ITypeTargetable, paramReaderName: string, position: int) =
        CodeLogic.codeReadResultAssignmentValue (cSharpType |> TypeTarget.ofITypeTargetable, cSharpOdpNetSafeType |> TypeTarget.ofITypeTargetable, paramReaderName, position)

    // constructor wrappers
    let MethodNameReadResult (interfaceName:InterfaceName) = MethodName.methodNameReadResult interfaceName
    let InterfaceNameOfClassName (className: ClassName) = InterfaceName.ofClassName className
    let InterfaceNameOfTypeGenericName (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> InterfaceName.ofTypeTarget
    let NumericOdpNetOracleDbTypeEnum tvn = OdpNetOracleDbTypeEnum.fromTypeValueNullableNumeric tvn
    let TypeNone = TypeNone.NoType
    let Namespace (segments: string seq) = if segments |> Seq.exists(fun s -> isNullOrWhiteSpace s) then failwithNullOrEmpty $"{nameof Namespace} segments" else Namespace.create segments
    let TypeGenericNameOfClassName (className: ClassName) = TypeGenericName.createOfClassName className
    let TypeGenericNameOf (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeGenericName.createOfTypeTarget
    let TypeArrayOf (iTypeComposable: ITypeComposable) = iTypeComposable |> TypeComposable.ofITypeComposable |> TypeArray.create
    let TypeCollectionGeneric (typeCollection, iTypeTargetable: ITypeTargetable) = TypeCollectionGeneric.ofTypeTarget(typeCollection, iTypeTargetable |> TypeTarget.ofITypeTargetable)
    let GetSubType (iTypeTargetable: ITypeTargetable) = iTypeTargetable |> TypeTarget.ofITypeTargetable |> TypeTarget.getSubType