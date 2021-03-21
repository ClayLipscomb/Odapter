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

namespace Odapter

open System;
open Odapter.CSharp
open Odapter.CSharp.Logic
open Odapter.Casing;
open Odapter.Translation.Api;

module CSharpTest =
    let private testFunc a = 
        let s = $"Parameter %s{nameof a} is: %s{a}!"
        let kw = USING

        let tvComposable = ComposableValue TypeValue.Int32
        let tvnComposable = ComposableValueNullable (ValueNullable TypeValue.TimeSpan)
        //let tvnComposable_iCodeable = tvnComposable :> ICodeable
        let tvnComposable2 = ComposableValueNullable TypeValue.TimeSpan.Nullable
        let trComposable = ComposableReference DataTable
        let pascalCase = PascalCase.create "TheClassName"
        //let typeClass = ClassName.ClassName .create "TheClassName"
        let className = ClassNameOfOracleIdentifier "PACKAGE.GOOD_PROC"
        //let typeInterface = TypeInterface.createOfClass typeClass
        let typeGeneric = TypeGenericName.createOfClassName className
        let typeValue = TypeValue.Int16
        let typeReference = TypeReference.String
        let typeValueNullale = TypeValue.DateTime.Nullable

        let isEqual = tvComposable.Equals(tvnComposable)
        //let isEqual1 = TypeComposable.isEquals tvComposable typeGeneric

        let typeArray = TypeArray.create (ComposableClassName className)
        let typeArray2 = TypeArray.create (ComposableValue typeValue)
        let typeArray3 = TypeArray.create (ComposableReference typeReference)
        let typeArray4 = TypeArray.create (ComposableValueNullable typeValueNullale)

        //let gc1 = TypeCollectionGeneric.createOfTypeValue(TypeCollection.ICollection, typeValue)
        //let gc2 = TypeCollectionGeneric.createOfTypeValueNullable(TypeCollection.IList, typeValueNullale)
        //let gc3 = TypeCollectionGeneric.createOfTypeReference(TypeCollection.Collection, typeReference)
        //let gc4 = TypeCollectionGeneric.createOfTypeClass(TypeCollection.IList, typeClass)

        let gc5 = TypeCollectionGeneric.create(TypeCollection.ICollection, ComposableValue typeValue)
        let gc6 = TypeCollectionGeneric.create(TypeCollection.IList, ComposableValueNullable typeValueNullale)
        let gc7 = TypeCollectionGeneric.create(TypeCollection.List, ComposableReference typeReference)
        let gc8 = TypeCollectionGeneric.create(TypeCollection.IList, ComposableClassName className)
        let gc9 = TypeCollectionGeneric.create(TypeCollection.IList, ComposableGeneric typeGeneric)
        let gc10 = TypeCollectionGeneric.create(TypeCollection.IList, ComposableArray typeArray)

        let arrayObjects : obj[] = [|kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric|] 
        let listObjects : obj list = [kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric] 
        let iTypeTargetableList : ITypeTargetable list = [typeValue; typeReference; typeValueNullale] 
        let someCode = codeSpaced [|kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric|] 
        let someCode2 = codeCommaSpaced [kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric] 
        //let someCode3 = toCodeSpaced [| typeReference ; typeValueNullale|]

        let x = ValueNullable TypeValue.Byte
        let y = x.TypeValue
        emptyString