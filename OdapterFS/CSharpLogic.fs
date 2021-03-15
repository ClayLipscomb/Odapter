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

namespace OdapterFS.CSharp.Logic

open OdapterFS.CSharp
open System
open System.Text
open System.Text.RegularExpressions

//module Blue = let f x = x + 2

[<AutoOpen>]
module internal Coder =
    // Code a series of tabs
    let rec internal codeTab n = if n = 0uy then String.Empty else replicate 4 SPACE + codeTab (n - 1uy)

    let toCode : ToCode = fun(codeable:ICodeable) -> codeable.ToCode
    let toCodeTabbed : ToCodeTabbed = fun(codeable:ICodeable, tabCnt:byte) -> 
        let splitNewLine str = split (NEWLINE, str) |> Array.toSeq
        let joinNewLine strings = join NEWLINE strings
        codeable.ToCode |> splitNewLine |> Seq.map(fun s -> (if isNullOrWhiteSpace s then emptyString else codeTab tabCnt) + s) |> joinNewLine
        // Before: abc NL def NL ghi NL ""
        // Split: abc def ghi ""
        // After: TABBEDabc NL TABBEDdef NL TABBEDghi NL ""
    let toCodeTabbed1 codeable = toCodeTabbed (codeable, 1uy)
    let toCodeTabbed2 codeable = toCodeTabbed (codeable, 2uy)
    let toCodeTabbed3 codeable = toCodeTabbed (codeable, 3uy)
    let toCodeTabbed4 codeable = toCodeTabbed (codeable, 4uy)
    let toCodeTabbed5 codeable = toCodeTabbed (codeable, 5uy)
    let toCodeTabbed6 codeable = toCodeTabbed (codeable, 6uy)

    let private toCodeDelimited delimiter (codeables : ICodeable seq) = concat delimiter (codeables |> Seq.map(fun x -> toCode x))
    /// Code a collection of codeables as space delimited
    let toCodeAdjacent codeables = toCodeDelimited emptyString codeables
    /// Code a collection of codeables as space delimited
    let toCodeSpaced codeables = toCodeDelimited " " codeables
    /// Code a collection of codeables delimited by a comma and space
    let toCodeCommaSpaced codeables = toCodeDelimited ", " codeables
    /// Code a collection of codeables as dot delimited
    let toCodeDotted codeables = toCodeDelimited "." codeables

[<RequireQualifiedAccess>]
module internal Keyword = 
    let private unionCache = UtilUnion.getUnionCases<Keyword> |> Seq.cache
    let isKeyword keywordCandidate = unionCache |> Seq.exists (fun kw -> toUpper kw.Name = toUpper keywordCandidate)

[<RequireQualifiedAccess>]
module internal Namespace = 
    let create segments = WrappedString.create (*canonicalize:*)singleLineTrimmed Namespace (concat PERIOD segments)
    let value = WrappedString.value    
[<RequireQualifiedAccess>]
module internal TypeGeneric = 
    let [<Literal>] private Ltrl_GENERIC_PREFIX = @"T_"
    let private startsWithGenericPrefix = startsWith Ltrl_GENERIC_PREFIX
    let private removeGenericPrefix = fun s -> (if startsWithGenericPrefix s then subStringToEnd Ltrl_GENERIC_PREFIX.Length s else s)
    let create baseName = 
        let ctor str = Ltrl_GENERIC_PREFIX + str |> TypeGeneric.GenericName
        WrappedString.create (*canonicalize:*)singleLineTrimmed ctor baseName
    let fromCode tgCandidate = 
        if tgCandidate |> startsWithGenericPrefix then 
            tgCandidate |> removeGenericPrefix |> create |> Some
        else 
            None
    let (|ValidCode|_|) typeStr = typeStr |> fromCode
[<RequireQualifiedAccess>]
module internal PropertyName = 
    let create input = WrappedString.create (*canonicalize:*)singleLineTrimmed PropertyName input
    let value = WrappedString.value
    let fromCode propertyNameCandidate = create propertyNameCandidate
    let (|ValidCode|_|) typeStr = typeStr |> fromCode |> Some
[<RequireQualifiedAccess>]
module internal InterfaceName = 
    let private startsWithInterfacePrefix str = startsWith Ltrl_CODE_INTEFACE_PREFIX str
    let private (|ValidCodeFormat|_|) str =
        match startsWithInterfacePrefix str with
        | true -> Some str
        | false -> Some str
    let create baseNameStr = WrappedString.create (*canonicalize:*)(fun str -> Ltrl_CODE_INTEFACE_PREFIX + (WrappedString.singleLineTrimmed str)) InterfaceName baseNameStr
[<RequireQualifiedAccess>]
module internal ClassName = 
    let create input = WrappedString.create (*canonicalize:*)singleLineTrimmed ClassName input
    let value = WrappedString.value
    let fromCode classCandidate = create classCandidate
    let (|ValidCode|_|) typeStr = typeStr |> fromCode |> Some

[<RequireQualifiedAccess>]
module internal TypeValue = 
    let private unionCache = UtilUnion.getUnionCases<TypeValue> |> Seq.cache 
    let fromCode tvCandidate = UtilUnion.createUnionCase<TypeValue> unionCache tvCandidate
    let internal (|ValidCode|_|) typeStr = typeStr |> fromCode
[<RequireQualifiedAccess>]
module internal TypeValueNullable = 
    let create typeValue = ValueNullable typeValue 
    let endsWithNullableSuffix = endsWith Ltrl_CODE_NULLABLE_SUFFIX
    let fromCode tvnCandidate = 
        if tvnCandidate |> endsWithNullableSuffix then 
            match ((Ltrl_CODE_NULLABLE_SUFFIX |> toCharArray), tvnCandidate) ||> trimEnd |> TypeValue.fromCode with
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
    let create (subType:ITypeComposable) = subType |> TypeArray.TypeArray
    //let create (typeScalar:ISubTypeArray) =
    //    match typeScalar with
    //    | :? TypeValue as t         -> t :> ISubTypeArray |> TypeArray.TypeArray 
    //    //| :? TypeValueNullable as t -> t |> ScalarValueNullable |> TypeArray.TypeArray 
    //    //| :? TypeReference as t     -> t |> ScalarReference |> TypeArray.TypeArray 
    //    //| :? TypeClass as t         -> t |> ScalarClass |> TypeArray.TypeArray  
    let fromCode arrayCandidate = 
        let (|ValidCodeFormat|_|) arrayCode =
            let len = length arrayCode
            if arrayCode.[(len-2)..(len-1)] = Ltrl_BRACKETS then arrayCode.[0 .. (len-3)] |> Some else None
        match arrayCandidate with
        | ValidCodeFormat typeCandidate -> 
            match typeCandidate with
            | TypeValue.ValidCode t -> t |> create |> Some
            | _ -> None
        | _ -> None
    let (|ValidCode|_|) typeStr = fromCode typeStr
[<RequireQualifiedAccess>]
module internal TypeComposable =
    let internal createGeneric t        = t |> ComposableGeneric
    let internal createValueNullable t  = t |> ComposableValueNullable 
    let internal createValue t          = t |> ComposableValue
    let internal createReference t      = t |> ComposableReference
    let internal createClassName t      = t |> ComposableClassName
    let internal createArray t          = t |> ComposableArray
    let fromCode typeCandidate =
        match typeCandidate with
        | TypeGeneric.ValidCode t       -> t |> ComposableGeneric |> Some
        | TypeValueNullable.ValidCode t -> t |> ComposableValueNullable |> Some
        | TypeValue.ValidCode t         -> t |> ComposableValue |> Some
        | TypeReference.ValidCode t     -> t |> ComposableReference |> Some
        | ClassName.ValidCode t         -> t |> ComposableClassName |> Some
        | TypeArray.ValidCode t         -> t |> ComposableArray |> Some
        | _                             -> None //failwith $"'{typeCandidate}' is not valid C# code for a {nameof TypeScalar}"
    let fromCodeToCodeAsNullable typeCandidate =
        match fromCode typeCandidate with
        | Some (ComposableValue tv) -> (tv.AsNullable :> ICodeable).ToCode 
        | Some typeComposable       -> (typeComposable :> ICodeable).ToCode
        | None                      -> emptyString
    let (|ValidCode|) typeStr = fromCode typeStr
    let isEquals (typeComposable:TypeComposable) (iTypeComposable:ITypeComposable) = 
        let ts =
            match iTypeComposable with
            | :? TypeGeneric as t       -> ComposableGeneric t
            | :? TypeValueNullable as t -> ComposableValueNullable t
            | :? TypeValue as t         -> ComposableValue t
            | :? TypeReference as t     -> ComposableReference t
            | :? ClassName as t         -> ComposableClassName t
            | :? TypeArray as t         -> ComposableArray t
            | _ -> failwith $"Type '{typeComposable}' not recognized as valid {nameof TypeComposable}"
        typeComposable.Equals(ts)

[<RequireQualifiedAccess>]
module internal TypeCollection = 
    let [<Literal>] private Ltrl_CODE_GENERIC_OF_T = @"<T>"
    let private unionCache = UtilUnion.getUnionCases<TypeCollection> |> Seq.cache 
    let fromCode tcCandidate = (UtilUnion.createUnionCase<TypeCollection> unionCache tcCandidate) 
    let toCodeOfT (typeCollection:TypeCollection) = UtilUnion.fromDuCaseToString(typeCollection) + Ltrl_CODE_GENERIC_OF_T
    let (|ValidCode|_|) str = fromCode str
[<RequireQualifiedAccess>]
module internal TypeCollectionGeneric = 
    let create (typeCollection, subType)                            = { typeCollection = typeCollection; subType = subType }
    let createOfGeneric (typeCollection, typeGeneric)               = create(typeCollection, ComposableGeneric typeGeneric)
    let createOfValueNullable (typeCollection, typeValueNullable)   = create(typeCollection, ComposableValueNullable typeValueNullable)
    let createOfValue (typeCollection, typeValue)                   = create(typeCollection, ComposableValue typeValue)
    let createOfReference (typeCollection, typeReference)           = create(typeCollection, ComposableReference typeReference)
    let createOfClassName (typeCollection, className)               = create(typeCollection, ComposableClassName className)
    let createOfArray (typeCollection, typeArray)                   = create(typeCollection, ComposableArray typeArray)
    //let create (typeCollection, typeScalar:ITypeScalar) =
    //    match typeScalar with
    //    | :? TypeValueNullable as t -> { typeCollection = typeCollection; typeScalar = (TypeScalar.create t) }
    //    | :? TypeValue as t         -> { typeCollection = typeCollection; typeScalar = (TypeScalar.create t) }
    //    | :? TypeReference as t     -> { typeCollection = typeCollection; typeScalar = (TypeScalar.create t) }
    //    | :? TypeClass as t         -> { typeCollection = typeCollection; typeScalar = (TypeScalar.create t) }
    //let create (typeCollection, typeScalar) = 
    //    match typeScalar with
    //        | ScalarReference t -> { typeCollection = typeCollection; typeScalar = ScalarReference t }
    //        | ScalarValue t -> { typeCollection = typeCollection; typeScalar = ScalarValue t }
    //        | ScalarValueNullable t -> { typeCollection = typeCollection; typeScalar = ScalarValueNullable t }
    //        | ScalarClass t -> { typeCollection = typeCollection; typeScalar = ScalarClass t }
    //let internal toCode {typeCollection = tc; typeScalar = ts} = UtilUnion.fromDuCaseToString tc + Ltrl_LT + TypeScalar.toCode ts + Ltrl_GT
    let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ]) else None            
    let private (|ValidCodeFormat|_|) tcgCode =
        match tcgCode with
        | Regex @"(.*)<(.*)>" [typeCollectionStr; typeComposableStr] -> (typeCollectionStr, typeComposableStr) |> Some
        | _ -> None
    let fromCode tcgCandidate = 
        match tcgCandidate with
        | ValidCodeFormat (typeCollectionStr, typeComposableStr) -> 
            match typeCollectionStr with
            | TypeCollection.ValidCode typeCollection -> 
                match typeComposableStr with
                | TypeGeneric.ValidCode t       -> (typeCollection, ComposableGeneric t) |> create |> Some
                | TypeValueNullable.ValidCode t -> (typeCollection, ComposableValueNullable t) |> create |> Some
                | TypeValue.ValidCode t         -> (typeCollection, ComposableValue t) |> create |> Some
                | TypeReference.ValidCode t     -> (typeCollection, ComposableReference t) |> create |> Some
                | ClassName.ValidCode t         -> (typeCollection, ComposableClassName t) |> create |> Some
                | TypeArray.ValidCode t         -> (typeCollection, ComposableArray t) |> create |> Some
                | _ -> None
            | _ -> None
        | _ -> None
    let internal fromCodeToCodeGenericCollectionType (tcStr, tsStr) =
        let tc, ts = TypeCollection.fromCode tcStr, TypeComposable.fromCode tsStr
        match tc, ts with
        | Some tc, Some ts ->  (create (tc, ts) :> ICodeable).ToCode
        | _ -> emptyString
    let internal (|ValidCode|_|) tcgCandidate = fromCode tcgCandidate
    let internal fromCodeToCodeGenericCollectionTypeSubType (tcgStr, forceValueNullableToValue) =
        match tcgStr with
        | ValidCode tcg ->
            match tcg.SubType with
            | ComposableGeneric t           -> (t :> ITypeTargetable).ToCode
            | ComposableValueNullable t     -> (if forceValueNullableToValue then t.AsNonNullable :> ITypeTargetable else t :> ITypeTargetable).ToCode
            | ComposableValue t             -> (t :> ITypeTargetable).ToCode
            | ComposableReference t         -> (t :> ITypeTargetable).ToCode
            | ComposableClassName t         -> (t :> ITypeTargetable).ToCode
            | ComposableArray t             -> (t :> ITypeTargetable).ToCode
        | _ -> failwith $"'{tcgStr}' is not valid C# code for {nameof TypeCollectionGeneric}"
[<RequireQualifiedAccess>]
module internal Property = 
    let create (propertyName, propertyType) = { propertyName = propertyName; propertyType = propertyType }
[<RequireQualifiedAccess>]
module internal TypeInterface = 
    let create (interfaceName, properties) = { interfaceName = InterfaceName.create interfaceName; properties = properties }
    let toCode typeInterface = 
        $"{toCodeSpaced[PUBLIC; INTERFACE; typeInterface.interfaceName]} {Ltrl_CURLY_OPEN}" + NEWLINE 
        // properties go here
        + Ltrl_CURLY_CLOSE

[<RequireQualifiedAccess>]
module internal OdpNetOracleDbTypeEnum =
    //let internal toCode (enum : OdpNetOracleDbTypeEnum) = (UtilUnion.fromDuCaseToString CodeFragment.OracleDbType) + period + (UtilUnion.fromDuCaseToString enum)
    /// <summary>handles OracleDbTypeEnum coding from numeric C# types *only*</summary>
    /// <param name="t">Type value</param>
    let toCodeFromTypeValueNumeric tv = 
        match tv with
        | TypeValue.SByte | TypeValue.Byte              -> (OdpNetOracleDbTypeEnum.Byte :> ICodeable).ToCode
        | TypeValue.Int16 | TypeValue.UInt16            -> (OdpNetOracleDbTypeEnum.Int16 :> ICodeable).ToCode
        | TypeValue.Int32 | TypeValue.UInt32            -> (OdpNetOracleDbTypeEnum.Int32 :> ICodeable).ToCode
        | TypeValue.Int64 | TypeValue.UInt64            -> (OdpNetOracleDbTypeEnum.Int64 :> ICodeable).ToCode
        | TypeValue.Decimal | TypeValue.OracleDecimal   -> (OdpNetOracleDbTypeEnum.Decimal :> ICodeable).ToCode
        | TypeValue.Double                              -> (OdpNetOracleDbTypeEnum.BinaryDouble :> ICodeable).ToCode
        | TypeValue.Single                              -> (OdpNetOracleDbTypeEnum.BinaryFloat :> ICodeable).ToCode
        | _                                             -> "C#type_" + nameof tv + "_UndefinedIn_toCodeNumericOracleDbTypeEnum" 
    let fromCodeToCodeNumericOracleDbTypeEnum typeValueStr = 
        match typeValueStr with
        | TypeValue.ValidCode tv -> toCodeFromTypeValueNumeric tv
        | _ -> failwith $"'{typeValueStr}' is not valid C# code for {nameof TypeValue}"

[<AutoOpen>]
module internal Decode =
    let fromCode typeCandidateStr = 
        match typeCandidateStr with 
        //| TypeArray.Valid ta                -> ta :> ITypeTargetable |> Some
        | ClassName.ValidCode t                 -> t :> ITypeTargetable |> Some
        | TypeReference.ValidCode t             -> t :> ITypeTargetable |> Some
        | TypeValue.ValidCode t                 -> t :> ITypeTargetable |> Some
        | TypeValueNullable.ValidCode t         -> t :> ITypeTargetable |> Some
        | TypeCollectionGeneric.ValidCode t     -> t :> ITypeTargetable |> Some
        | TypeArray.ValidCode t                 -> t :> ITypeTargetable |> Some
        | _ -> None //failwith $"'{typeCandidateStr}' is not valid C# code for a type"

    let isOdpNet (typeTargetable:ITypeTargetable) = 
        match typeTargetable with
        | :? TypeValue | :? TypeValueNullable | :? TypeReference    -> (typeTargetable:>ICodeable).ToCode |> (startsWith Ltrl_ORACLE)
        | _                                                         -> false

    /// Codes anything that is a codeable 
    //let internal toCode : ToCode = fun (codeable:ICodeable) ->
    //    match codeable with
    //    | :? TypeScalar as ts                       -> (ts :> ICodeable2).ToCode // TypeScalar.toCode ts
    //    | :? TypeValueNullable as tvn               -> (tvn :> ICodeable2).ToCode //TypeValueNullable.toCode tvn
    //    | :? TypeValue as tv                        -> (tv :> ICodeable2).ToCode //TypeValue.toCode tv
    //    | :? TypeReference as tr                    -> (tr :> ICodeable2).ToCode //TypeReference.toCode tr
    //    | :? TypeClass as tc                        -> (tc :> ICodeable2).ToCode //TypeClass.toCode tc
    //    | :? TypeGenericParameter as tgp            -> (tgp :> ICodeable2).ToCode //TypeGenericParameter.toCode tgp 
    //    | :? TypeCollectionGeneric as tcg           -> (tcg :> ICodeable2).ToCode //TypeCollectionGeneric.toCode tcg
    //    //| :? TypeArray as ta                        -> TypeArray.toCode ta
    //    | :? TypeArray as ta                        -> (ta :> ICodeable2).ToCode //TypeArray.toCode ta

    //    | :? Keyword as keyword                     -> (keyword :> ICodeable2).ToCode //Keyword.toCode keyword
    //    | :? Namespace as nmspace                   -> (nmspace :> ICodeable2).ToCode // Namespace.toCode nmspace
    //    | :? InterfaceName as interfaceName         -> (interfaceName :> ICodeable2).ToCode //InterfaceName.toCode interfaceName
    //    | :? CodeFragment as codeFragment           -> (codeFragment :> ICodeable2).ToCode //CodeFragment.toCode codeFragment
    //    | :? CodeMethod as codeMethod               -> (codeMethod :> ICodeable2).ToCode //CodeMethod.toCode codeMethod
    //    | :? OdpNetOracleDbTypeEnum as enum         -> (enum :> ICodeable2).ToCode //OdpNetOracleDbTypeEnum.toCode enum
    //    | _ -> String.Empty

    //let toTypeClass interfaceName =
    //    let removeInterfacePrefix = replace(Ltrl_CODE_INTEFACE_PREFIX, emptyString)
    //    let startsWithInterfacePrefix str = startsWith Ltrl_CODE_INTEFACE_PREFIX str
    //    if not <| isNullOrWhiteSpace interfaceName
    //        && startsWithInterfacePrefix interfaceName
    //        && (0, 2) |> interfaceName.Substring = ((0, 2) |> interfaceName.Substring).ToUpper() then
    //        interfaceName |> removeInterfacePrefix |> TypeClass.create
    //    else
    //        TypeClass.create interfaceName

[<AutoOpen>]
module private UtilApi =
    let nullableToOption = function
        | null -> None
        | x -> Some x
    let optionToNullable = function
        | Some x -> x
        | None -> null
    let ifNoneThrowElseValue (optionType, errorMsg) = if optionType |> Option.isNone then failwith errorMsg else optionType.Value 
    let failwithNullOrEmpty desc = failwith $"{desc} cannot be null or empty" 

/// API of coding functions
module ApiCode =
    let IsValidGenericCollectionType typeCandidate = typeCandidate |> TypeCollectionGeneric.fromCode |> Option.isSome   
    let IsKeyword kw = Keyword.isKeyword kw    
    //let IsOdpNet typeStr = typeStr |> Decode.fromCode |> Decode.isOdpNet
    let IsOdpNet typeStr = typeStr |> Decode.fromCode |> Option.map(fun t -> Decode.isOdpNet t) |> (Option.defaultValue false)
    let IsEqualTypeComposable typeComposable iTypeComposable = TypeComposable.isEquals typeComposable iTypeComposable

    // decoding (temporary)
    let FromTypeValue str               = ifNoneThrowElseValue(TypeValue.fromCode str, $"'{str}' is not valid C# code for {nameof TypeValue}")
    let FromTypeCollection str          = ifNoneThrowElseValue(TypeCollection.fromCode str, $"'{str}' is not valid C# code for {nameof TypeCollection}")
    let FromTypeCollectionGeneric str   = ifNoneThrowElseValue(TypeCollectionGeneric.fromCode str, $"'{str}' is not valid C# code for {nameof TypeCollectionGeneric}")
    let FromTypeComposable str          = ifNoneThrowElseValue(TypeComposable.fromCode str, $"'{str}' is not valid C# code for {nameof TypeComposable}")
    let FromTypeTargetable str          = ifNoneThrowElseValue(fromCode str,  $"'{str}' is not valid C# code for {nameof ITypeTargetable}")
    // decoding for types persisted in config file
    let FromTypeCollectionWithDefault str typeCollectionDefault = str |> TypeCollection.fromCode |> (Option.defaultValue typeCollectionDefault)
    let FromTypeComposableWithDefault str typeComposableDefault = str |> TypeComposable.fromCode |> (Option.defaultValue typeComposableDefault)
    let FromTypeTargetableWithDefault str typeTargetableDefault = str |> fromCode |> (Option.defaultValue typeTargetableDefault)

    // coding
    let Tab n = codeTab n
    let To codeable = toCode codeable
    let ToAdjacent codeables = toCodeAdjacent codeables
    let ToSpaced codeables = toCodeSpaced codeables
    let ToCommaSpaced codeables = toCodeCommaSpaced codeables
    let ToDotted codeables = toCodeDotted codeables
    let ToNumericOracleDbTypeEnum tvStr = OdpNetOracleDbTypeEnum.toCodeFromTypeValueNumeric tvStr
    let ToUsing(nmspace:Namespace) = (ToSpaced [|USING;nmspace|]) + SEMICOLON
    // decoding to coding (temporary)
    let ToFromGenericCollectionType (tcStr, tsStr) = TypeCollectionGeneric.fromCodeToCodeGenericCollectionType (tcStr, tsStr)
    let ToFromGenericCollectionTypeSubType (tcgStr, forceValueNullableToValue) = TypeCollectionGeneric.fromCodeToCodeGenericCollectionTypeSubType(tcgStr, forceValueNullableToValue)
    let ToFromNumericOracleDbTypeEnum typeValueStr = OdpNetOracleDbTypeEnum.fromCodeToCodeNumericOracleDbTypeEnum typeValueStr
    let ToFromAsNullable typeStr = TypeComposable.fromCodeToCodeAsNullable typeStr

//let FromCodeOfInterfaceToCodeClass interfaceCode =
//    interfaceCode 
//    |> TypeInterface.createOfText 
//    |> Option.map(fun i -> i |> TypeClass.createOfInterface) 
//    |> Option.bind(fun c -> (c :> ICodeable).ToCode |> Some)
//    |> Option.defaultValue emptyString

/// API of contructors
module ApiConstructor =
    // constructor
    let Namespace segments = if segments |> Seq.exists(fun s -> isNullOrWhiteSpace s) then failwithNullOrEmpty $"{nameof Namespace} segments" else Namespace.create segments
    let TypeGeneric str = str |> TypeGeneric.create
    let InterfaceName baseNameStr = InterfaceName.create baseNameStr
    let ClassName input = ClassName.create input
    let TypeArray ts = TypeArray.create ts  
    let TypeCollectionGenericOfValueNullable (typeCollection, typeValueNullable) = TypeCollectionGeneric.createOfValueNullable(typeCollection, typeValueNullable)
    let TypeCollectionGenericOfValue (typeCollection, typeValue) = TypeCollectionGeneric.createOfValue(typeCollection, typeValue)
    let TypeCollectionGenericOfReference (typeCollection, typeReference) = TypeCollectionGeneric.createOfReference(typeCollection, typeReference)
    let TypeCollectionGenericOfClassName (typeCollection, className) = TypeCollectionGeneric.createOfClassName(typeCollection, className)
    let TypeCollectionGenericOfGeneric (typeCollection, typeGeneric) = TypeCollectionGeneric.createOfGeneric(typeCollection, typeGeneric)
    let TypeCollectionGeneric (typeCollection, typeComposable:TypeComposable) = TypeCollectionGeneric.create(typeCollection, typeComposable)
    let TypeComposable(iTypeComposable:ITypeComposable) = 
        match iTypeComposable with
        | :? TypeGeneric as t       -> TypeComposable.createGeneric t
        | :? TypeValueNullable as t -> TypeComposable.createValueNullable t
        | :? TypeValue as t         -> TypeComposable.createValue t
        | :? TypeReference as t     -> TypeComposable.createReference t
        | :? ClassName as t         -> TypeComposable.createClassName t
        | :? TypeArray as t         -> TypeComposable.createArray t
        | _ -> failwith $"Type '{iTypeComposable}' not recognized as valid {nameof TypeComposable}"