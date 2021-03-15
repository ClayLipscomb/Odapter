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

namespace OdapterFS.CSharp

    //type Blue = Aqua | Magenta

    // C# domain types

    type Undefined = exn
    
    /// *Atomic* type that can be turned to code
    type ICodeable = 
        abstract member ToCode : string
    /// C# code type that can be a translation target
    type ITypeTargetable = inherit ICodeable
    /// Type that can be used to compose more complex type
    type ITypeComposable = inherit ITypeTargetable
    
    type ToCode = ICodeable -> string
    type ToCodeTabbed = ICodeable * byte -> string

    [<Struct>]
    type CodeFragment =
        | OracleDbType | GetOracle | ReadResult | T_ | CommandType | StoredProcedure | BindByName | Rows | Count
        interface ICodeable with
            member this.ToCode = this |> UtilUnion.fromDuCaseToString
    [<Struct>]
    type CodeMethod =
        | ExecuteNonQuery | Close | Dispose | Read | ToArray | ToString | GetDataReader | GetValue | GetOracleValue | SetPrecision
        interface ICodeable with
            member this.ToCode = this |> UtilUnion.fromDuCaseToString
    //[<Struct>] ?? using a struct causes a "CLR detected an invalid program" error
    type Keyword =
        | ABSTRACT | EVENT | NEW | STRUCT | AS | EXPLICIT | NULL | SWITCH | BASE | EXTERN | OBJECT | THIS | BOOL | FALSE | OPERATOR | THROW | BREAK | FINALLY | OUT | TRUE 
        | BYTE | FIXED | OVERRIDE | TRY | CASE | FLOAT | PARAMS | TYPEOF | CATCH | FOR | PRIVATE | UINT | CHAR | FOREACH | PROTECTED | ULONG | CHECKED | GOTO | PUBLIC 
        | UNCHECKED | CLASS | IF | READONLY | UNSAFE | CONST | IMPLICIT | REF | USHORT | CONTINUE | IN | RETURN | USING | DECIMAL | INT | SBYTE | VIRTUAL | DEFAULT | INTERFACE 
        | SEALED | VOLATILE | DELEGATE | INTERNAL | SHORT | VOID | DO | IS | SIZEOF | WHILE | DOUBLE | LOCK | STACKALLOC | ELSE | LONG | STATIC | ENUM | NAMESPACE | STRING 
        | DYNAMIC | GET | LET | PARTIAL | SET | VALUE | VAR | WHERE  // a few contextual keywords
        interface ICodeable with
            member this.ToCode = this |> UtilUnion.fromDuCaseToString |> toLower

    [<Struct>]
    type Namespace = Namespace of string with
        interface IWrappedString with
            member this.Value = let (Namespace s) = this in s
        interface ICodeable with
            member this.ToCode = (this :> IWrappedString).Value
    [<Struct>]
    type TypeGeneric = GenericName of string with
        interface IWrappedString with
            member this.Value = let (GenericName s) = this in s
        interface ITypeTargetable with
            member this.ToCode = (this :> IWrappedString).Value
        interface ITypeComposable
    [<Struct>]
    type PropertyName = PropertyName of string with
        interface IWrappedString with
            member this.Value = let (PropertyName s) = this in s
        interface ICodeable with
            member this.ToCode = (this :> IWrappedString).Value
    [<Struct>]
    type InterfaceName = InterfaceName of string with
        interface IWrappedString with
            member this.Value = let (InterfaceName interfaceName) = this in interfaceName
        interface ICodeable with
            member this.ToCode = (this :> IWrappedString).Value
    [<Struct>]
    type ClassName = ClassName of string with
        interface IWrappedString with
            member this.Value = let (ClassName s) = this in s
        interface ICodeable with
            member this.ToCode = (this :> IWrappedString).Value
        interface ITypeComposable

    [<Struct>]
    type OdpNetOracleDbTypeEnum =
        | Byte | Int16 | Int32 | Int64 | Decimal | BinaryDouble | BinaryFloat | DateTime | TimeStamp | TimeStampTZ | TimeStampLTZ | IntervalDS | IntervalYM
        | Varchar2 | NChar | NVarchar2 | Char | BFile | Blob | Clob | NClob | Long | XmlType | RefCursor | Raw | LongRaw
        // Not Available in ODP.NET, Managed Driver: | Array | Object | Ref
        interface ICodeable with
            member this.ToCode = (UtilUnion.fromDuCaseToString CodeFragment.OracleDbType) + PERIOD + (this |> UtilUnion.fromDuCaseToString)

    [<Struct>]
    type TypeValue =
        | SByte | Byte | Int16 | UInt16 | Int32 | UInt32 | Int64 | UInt64  | DateTime | DateTimeOffset | TimeSpan | Double | Single | Float | Decimal 
        | OracleDecimal | OracleDate | OracleString | OracleBinary | OracleRef | OracleXmlType | OracleTimestamp | OracleTimestampLTZ | OracleTimestampTZ | OracleIntervalDS | OracleIntervalYM 
        member this.ToCode = this |> UtilUnion.fromDuCaseToString
        member this.AsNullable = ValueNullable this
        interface ITypeTargetable with
            member this.ToCode = this.ToCode
        interface ITypeComposable
    and TypeValueNullable = internal ValueNullable of TypeValue with 
        interface ITypeTargetable with
            member this.ToCode = 
                let (TypeValueNullable.ValueNullable tv) = this
                tv.ToCode + Ltrl_CODE_NULLABLE_SUFFIX
        interface ITypeComposable
        member this.TypeValue with get() = let (ValueNullable typeValue) = this in typeValue
        member this.AsNonNullable = this.TypeValue
    [<Struct>]
    type TypeReference =
        | Boolean |  Object | XmlDocument | String | BigInteger | BigRational | DataTable | Dyanmic | Void
        | OracleRefCusror | OracleBFile | OracleBlob | OracleClob 
        interface ITypeTargetable with
            member this.ToCode = if this = Void then (Keyword.VOID :> ICodeable).ToCode else this |> UtilUnion.fromDuCaseToString end
        interface ITypeComposable

    [<Struct>]
    type TypeCollection = | List | IList | ICollection 
    //[<NoEquality;NoComparison>]
    [<Struct>]
    type TypeCollectionGeneric = { typeCollection: TypeCollection; subType: TypeComposable } with
        // Value, ValueNullable, Reference, Class, GenericParameter, Interface
        interface ITypeTargetable with
            member this.ToCode = 
                let {typeCollection = tColl; subType = tComp} = this
                UtilUnion.fromDuCaseToString tColl + Ltrl_LT + (tComp :> ICodeable).ToCode + Ltrl_GT
        member this.TypeCollection with get() = this.typeCollection
        member this.SubType with get() = this.subType
    /// Single dimensional array
    and TypeArray = internal TypeArray of ITypeComposable with 
        interface ITypeTargetable with
            member this.ToCode = 
                let (TypeArray.TypeArray subType) = this
                subType.ToCode + Ltrl_BRACKETS
        interface ITypeComposable
    /// Type used to compose other types
    and TypeComposable = 
        internal 
        | ComposableGeneric of TypeGeneric
        | ComposableReference of TypeReference:TypeReference
        | ComposableValue of TypeValue:TypeValue
        | ComposableValueNullable of TypeValueNullable:TypeValueNullable
        | ComposableClassName of ClassName:ClassName
        | ComposableArray of TypeArray:TypeArray
        interface ITypeTargetable with
            member this.ToCode = 
                match this with
                | ComposableGeneric t       -> (t :> ICodeable).ToCode
                | ComposableValueNullable t -> (t :> ICodeable).ToCode
                | ComposableValue t         -> (t :> ICodeable).ToCode
                | ComposableReference t     -> (t :> ICodeable).ToCode
                | ComposableClassName t     -> (t :> ICodeable).ToCode
                | ComposableArray t         -> (t :> ICodeable).ToCode
        member this.ValueNullableToValue = 
            match this with
            | ComposableValueNullable t    -> ComposableValue t.AsNonNullable
            | ComposableGeneric _ | ComposableValue _ | ComposableReference _ | ComposableClassName _ | ComposableArray _ -> this

    [<Struct>]
    type Property = { propertyName : PropertyName; propertyType : TypeComposable }
    [<Struct>]
    type TypeInterface = { interfaceName : InterfaceName; properties : Property seq }
    [<Struct>]
    type TypeClassDto = { 
        className : ClassName; 
        properties : seq<Property>
        useDataMemberAttribute : bool; 
        useXmlElementAttribute : bool
        extendedClassName : ClassName; 
        extendedClassNameNamespace : Namespace; 
        implementedInterfaceName : InterfaceName
}