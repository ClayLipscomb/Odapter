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

/// C# domain types
namespace Odapter.CSharp

open System
open Odapter.Casing;
open Odapter.CSharp.Logic;

    type Undefined = exn
       
    /// A C# type that can be a translation target
    type ITypeTargetable = 
        /// Type can be turned to code
        abstract member Code : string
        /// Ensures type is not an explicitly nullable type (removes any suffix "?" in code)
        abstract member SansNullable : ITypeTargetable
    /// A C# type that can be used to compose a more complex C# type
    type ITypeComposable = 
        inherit ITypeTargetable
    
    [<Struct>]
    type CSharpVersion = | FourZero | NineZero with
        member this.ToString = this |> UtilUnion.fromDuCaseToString

    [<Struct>]
    type CodeFrag =
        | ReadResult | CommandType | StoredProcedure | BindByName | Rows | Count | CollectionType | Get | Set 
        | GetOracle  | OracleCollectionType | PLSQLAssociativeArray | OracleDbType | ArrayBindSize | Value | Convert | To | Oracle
        member this.Code = this |> UtilUnion.fromDuCaseToString
        override this.ToString() = this.Code
    [<Struct>]
    type CodeMethod =
        | ExecuteNonQuery | Close | Dispose | Read | ToArray | GetDataReader | GetValue | GetOracleValue | SetPrecision | Parse | IsDBNull
        member this.Code = this |> UtilUnion.fromDuCaseToString
        override this.ToString() = this.Code
    //[<Struct>] ?? using a struct causes a "CLR detected an invalid program" error
    type Keyword =
        | ABSTRACT | EVENT | NEW | STRUCT | AS | EXPLICIT | NULL | SWITCH | BASE | EXTERN | OBJECT | THIS | BOOL | FALSE | OPERATOR | THROW | BREAK | FINALLY | OUT | TRUE 
        | BYTE | FIXED | OVERRIDE | TRY | CASE | FLOAT | PARAMS | TYPEOF | CATCH | FOR | PRIVATE | UINT | CHAR | FOREACH | PROTECTED | ULONG | CHECKED | GOTO | PUBLIC 
        | UNCHECKED | CLASS | IF | READONLY | UNSAFE | CONST | IMPLICIT | REF | USHORT | CONTINUE | IN | RETURN | USING | DECIMAL | INT | SBYTE | VIRTUAL | DEFAULT | INTERFACE 
        | SEALED | VOLATILE | DELEGATE | INTERNAL | SHORT | VOID | DO | IS | SIZEOF | WHILE | DOUBLE | LOCK | STACKALLOC | ELSE | LONG | STATIC | ENUM | NAMESPACE | STRING 
        | DYNAMIC | GET | LET | PARTIAL | SET | VALUE | VAR | WHERE | INIT // a few contextual keywords
        member this.Code = this |> UtilUnion.fromDuCaseToString |> toLower
        override this.ToString() = this.Code
    [<Struct>]
    type AccessModifier = | INTERNAL | PUBLIC | PROTECTED | PRIVATE  with
        member this.Code = this |> UtilUnion.fromDuCaseToString |> toLower
        override this.ToString() = this.Code

    [<Struct>]
    type Namespace = internal Namespace of PascalCase with
        member this.Code = let (Namespace pascalCase) = this in (pascalCase :> IWrappedString).Value
        override this.ToString() = this.Code
    [<Struct>]
    type TypeGenericName = internal GenericName of PascalCase with 
        member this.Code = let (GenericName pascalCase) = this in Literal.GENERIC_PREFIX + (pascalCase :> IWrappedString).Value
        override this.ToString() = this.Code
        member this.Value = let (GenericName pascalCase) = this in pascalCase
    [<Struct>]
    type PropertyName = internal PropertyName of PascalCase with
        member this.Code = let (PropertyName pascalCase) = this in (pascalCase :> IWrappedString).Value
        member this.Value = let (PropertyName pascalCase) = this in pascalCase
        override this.ToString() = this.Code
    [<Struct>]
    type MethodName = internal MethodName of PascalCase with
        member this.Code = let (MethodName pascalCase) = this in (pascalCase :> IWrappedString).Value
        override this.ToString() = this.Code
    [<Struct>]
    type ParameterName = internal ParameterName of CamelCase with
        member this.Code = let (ParameterName camelCase) = this in (camelCase :> IWrappedString).Value
        member this.Value = let (ParameterName camelCase) = this in camelCase
        override this.ToString() = this.Code
    [<Struct>]
    type FieldNameProtected = internal FieldNameProtected of CamelCase with
        member this.Code = let (FieldNameProtected camelCase) = this in (camelCase :> IWrappedString).Value
        override this.ToString() = this.Code
    [<Struct>]
    type InterfaceName = internal InterfaceName of PascalCase with
        member this.Code = let (InterfaceName pascalCase) = this in (pascalCase :> IWrappedString).Value
        member this.Value = let (InterfaceName pascalCase) = this in pascalCase
        override this.ToString() = this.Code
    [<Struct>]
    type ClassName = internal ClassName of PascalCase with
        member this.Code = let (ClassName pascalCase) = this in (pascalCase :> IWrappedString).Value
        member this.Value = let (ClassName pascalCase) = this in pascalCase
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable

    [<Struct>]
    type TypeGenericParameterConstraint = { GenericName: TypeGenericName; InterfaceName: InterfaceName option; IsClass: bool; HasConstructor: bool } with
        member this.Code = // Ex: where TypeITTableNumberDec : class, ITTableNumberDec, new()
            codeSpaced [ WHERE ; this.GenericName ; COLON ] + SPACE
            + codeCommaSpaced [ (if this.IsClass then CLASS.Code else emptyString)
                                ; (match this.InterfaceName with | Some i -> i.Code | None -> emptyString)
                                ; (if this.HasConstructor then NEW.Code + "()" else emptyString) ]
        override this.ToString() = this.Code

    [<Struct>]
    type TypeGenericParameter = { GenericName: TypeGenericName; Constraint: TypeGenericParameterConstraint } with
        member this.Code = this.GenericName.Code 
        [<Obsolete>] member this.CodeInterface = match this.Constraint.InterfaceName with | Some i -> i.Code | None -> emptyString 
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable

    [<Struct>]
    type MethodNameReaderGetter = | GetValue | GetOracleValue | GetOracleDecimal | GetOracleBlob | GetOracleClob with
        member this.Code = this |> UtilUnion.fromDuCaseToString 
        override this.ToString() = this.Code

    [<Struct>]
    type OdpNetOracleDbTypeEnum =
        | Byte | Int16 | Int32 | Int64 | Decimal | BinaryDouble | BinaryFloat | Date | TimeStamp | TimeStampTZ | TimeStampLTZ | IntervalDS | IntervalYM
        | Varchar2 | NChar | NVarchar2 | Char | BFile | Blob | Clob | NClob | Long | RefCursor | Raw | LongRaw 
        // Not Available in ODP.NET, Managed Driver: Array, Boolean, Object, Ref, XmlType -> https://docs.oracle.com/database/121/ODPNT/OracleDbTypeEnumerationType.htm#ODPNT2286
        | Boolean | XmlType // | Array | | Object | Ref  // Boolean and XmlType do compile
        member this.Code = (UtilUnion.fromDuCaseToString CodeFrag.OracleDbType) + PERIOD + (this |> UtilUnion.fromDuCaseToString)
        override this.ToString() = this.Code

    [<Struct>]
    type TypeValue =
        | SByte | Byte | Int16 | UInt16 | Int32 | UInt32 | Int64 | UInt64  | DateTime | DateTimeOffset | TimeSpan | Double | Single | Float | Decimal | BigInteger
        | OracleDecimal | OracleDate | OracleString | OracleBinary | OracleRef | OracleTimeStamp | OracleTimeStampLTZ | OracleTimeStampTZ | OracleIntervalDS | OracleIntervalYM 
        member this.Code = this |> UtilUnion.fromDuCaseToString
        override this.ToString() = this.Code
        member this.Nullable with get() = ValueNullable this
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable
    and [<Struct>] TypeValueNullable = internal ValueNullable of TypeValue with 
        member this.Code = let (TypeValueNullable.ValueNullable tv) = this in tv.Code + NULLABLE_SUFFIX
        override this.ToString() = this.Code
        member this.TypeValue with get() = let (ValueNullable typeValue) = this in typeValue
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this.TypeValue :> ITypeTargetable
        interface ITypeComposable
    [<Struct>]
    type TypeReference =
        | Boolean |  Object | XmlDocument | String | DataTable | Dynamic | Void
        | OracleRefCursor | OracleBFile | OracleBlob | OracleClob | OracleRef | OracleXmlType 
        member this.Code = if this = Void then Keyword.VOID.Code else this |> UtilUnion.fromDuCaseToString 
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code 
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable

    [<Struct>]
    type TypeNone = | NoType with
        member _.Code = emptyString
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable

    [<Struct>]
    type TypeCollection = | List | IList | ICollection with
        member this.Code = this |> UtilUnion.fromDuCaseToString
        override this.ToString() = this.Code

    //[<NoEquality;NoComparison>]
    [<Struct>]
    type TypeCollectionGeneric = internal { TypeCollection: TypeCollection; SubType: TypeComposable } with
        member this.Code = 
            let {TypeCollection = typeCollection; SubType = subType} = this
            UtilUnion.fromDuCaseToString typeCollection + LT + subType.Code + GT
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable
        member this.GetTypeCollection with get() = this.TypeCollection 
        member this.GetSubType with get() = 
            match this.SubType with
            | ComposableGenericParameter t  -> t :> ITypeTargetable
            | ComposableValueNullable t     -> t :> ITypeTargetable
            | ComposableValue t             -> t :> ITypeTargetable
            | ComposableReference t         -> t :> ITypeTargetable
            | ComposableClassName t         -> t :> ITypeTargetable
            | ComposableArray t             -> t :> ITypeTargetable
            | ComposableCollectionGeneric t -> t :> ITypeTargetable
    and [<Struct>] TypeArray = internal TypeArray of TypeComposable with 
        member this.SubType with get() = let (TypeArray typeComposable) = this in typeComposable
        member this.Code = let (TypeArray.TypeArray typeComposable) = this in typeComposable.Code + BRACKETS
        override this.ToString() = this.Code
        interface ITypeTargetable with
            member this.Code = this.Code
            member this.SansNullable = this :> ITypeTargetable
        interface ITypeComposable
    /// Type used to compose other types
    and TypeComposable = 
        internal 
        | ComposableGenericParameter of TypeGenericParameter:TypeGenericParameter
        | ComposableReference of TypeReference:TypeReference
        | ComposableValue of TypeValue:TypeValue
        | ComposableValueNullable of TypeValueNullable:TypeValueNullable
        | ComposableClassName of ClassName:ClassName
        | ComposableArray of TypeArray:TypeArray
        | ComposableCollectionGeneric of TypeCollectionGeneric:TypeCollectionGeneric
        member this.Code = 
            match this with
            | ComposableGenericParameter t  -> t.Code
            | ComposableValueNullable t     -> t.Code
            | ComposableValue t             -> t.Code
            | ComposableReference t         -> t.Code
            | ComposableClassName t         -> t.Code
            | ComposableArray t             -> t.Code
            | ComposableCollectionGeneric t -> t.Code
        override this.ToString() = this.Code
        member this.AsITypeComposable =
            match this with
            | ComposableGenericParameter t  -> t :> ITypeComposable
            ///| ComposableGeneric t           -> t :> ITypeComposable
            | ComposableValueNullable t     -> t :> ITypeComposable
            | ComposableValue t             -> t :> ITypeComposable
            | ComposableReference t         -> t :> ITypeComposable
            | ComposableClassName t         -> t :> ITypeComposable
            | ComposableArray t             -> t :> ITypeComposable
            | ComposableCollectionGeneric t -> t :> ITypeComposable
        member this.ValueNullableToValue = 
            match this with
            | ComposableValueNullable t -> ComposableValue t.TypeValue
            | ComposableGenericParameter _ | ComposableValue _ | ComposableReference _ | ComposableClassName _ | ComposableArray _ | ComposableCollectionGeneric _ -> this

    [<Struct>]
    type TypeTarget = 
        internal
        | TargetValueNullable of TypeValueNullable:TypeValueNullable
        | TargetValue of TypeValue:TypeValue
        | TargetReference of TypeReference:TypeReference
        | TargetGenericParameter of TypeGenericParameter:TypeGenericParameter
        | TargetClassName of ClassName:ClassName
        | TargetCollectionGeneric of TypeCollectionGeneric:TypeCollectionGeneric
        | TargetArray of TypeArray:TypeArray
        | TargetNone of TypeNone:TypeNone with
        member this.AsITypeTargetable =
            match this with
            | TargetValueNullable t     -> t :> ITypeTargetable
            | TargetValue t             -> t :> ITypeTargetable
            | TargetReference t         -> t :> ITypeTargetable
            | TargetGenericParameter t  -> t :> ITypeTargetable
            | TargetClassName t         -> t :> ITypeTargetable
            | TargetCollectionGeneric t -> t :> ITypeTargetable
            | TargetArray t             -> t :> ITypeTargetable
            | TargetNone t              -> t :> ITypeTargetable
        member this.Code =
            match this with
            | TargetValueNullable t     -> t.Code
            | TargetValue t             -> t.Code
            | TargetReference t         -> t.Code
            | TargetGenericParameter t  -> t.Code
            | TargetClassName t         -> t.Code
            | TargetCollectionGeneric t -> t.Code
            | TargetArray t             -> t.Code
            | TargetNone t              -> t.Code
        override this.ToString() = this.Code
        member this.SansNullable =
            match this with
            | TargetValueNullable t -> t.TypeValue |> TargetValue
            | TargetGenericParameter _ | TargetValue _ | TargetReference _ | TargetClassName _ | TargetCollectionGeneric _ | TargetArray _ | TargetNone _ -> this 

    [<Struct>]
    type FieldProtected = { FieldName: FieldNameProtected; FieldType: TypeComposable } with
        member this.Code = codeSpaced[|PROTECTED; this.FieldType; this.FieldName|] + SEMICOLON
        override this.ToString() = this.Code

    [<Struct>]
    type PropertyAccessor = | GetOnly | GetInit | SetOnly | GetSet with
        member this.Code = 
            match this with 
            | GetSet _ -> codeSpaced[|GET.Code + SEMICOLON; SET.Code + SEMICOLON|]
            | GetInit _ -> codeSpaced[|GET.Code + SEMICOLON; INIT.Code + SEMICOLON|]
            | GetOnly _ -> GET.Code + SEMICOLON
            | SetOnly _ -> SET.Code + SEMICOLON
        override this.ToString() = this.Code
    [<Struct>]
    type Property = { PropertyName: PropertyName; PropertyType: TypeComposable; ContainerType: TypeComposable option; 
                        AccessModifier: AccessModifier option; GetSet: PropertyAccessor; BackingField: FieldProtected option;
                        IsVirtual: bool; IsDataMember: bool; IsXmlElement: bool } with
        member this.Code = codeSpaced [|
            (match this.AccessModifier with | Some am -> am.Code + SPACE | None _ -> emptyString);
            (match this.ContainerType with | Some ct -> ct.Code + PERIOD | None _ -> emptyString) + this.PropertyType.ToString();
            this.PropertyName; 
            CURLY_OPEN; this.GetSet; CURLY_CLOSE|]
        override this.ToString() = this.Code

    [<Struct>]
    type DtoInterfaceCategory =
        | MutableSet | ImmutableGetInit

    [<Struct>]
    type AccessModifierInterface = | PUBLIC | INTERNAL with
        member this.Code = this |> UtilUnion.fromDuCaseToString |> toLower
        override this.ToString() = this.Code
    [<Struct>]
    type TypeInterface = { AccessModifier: AccessModifierInterface; InterfaceName: InterfaceName; Properties: Property seq }

    [<Struct>]
    type internal TypeClassDto = { 
        AccessModifier: AccessModifier; ClassName: ClassName; 
        Properties: Property seq; IsAutoImplementedProperties: bool; Fields: FieldProtected seq option;
        IsPartial: bool; IsDataContract: bool; IsSerializable: bool
        ExtendedClassName: ClassName; ExtendedClassNameNamespace: Namespace; ImplementedInterfaceName: InterfaceName
    }