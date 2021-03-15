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

module CSharpTest =
    let private testFunc a = 
        let s = $"Parameter %s{nameof a} is: %s{a}!"
        let kw = USING

        let tvComposable = ComposableValue TypeValue.Int32
        let tvnComposable = ComposableValueNullable (ValueNullable TypeValue.TimeSpan)
        let tvnComposable_iCodeable = tvnComposable :> ICodeable
        let tvnComposable2 = ComposableValueNullable TypeValue.TimeSpan.AsNullable
        let trComposable = ComposableReference DataTable
        //let typeClass = TypeClass.create "TheClassName"
        let className = ClassName.create "MyClassName"
        //let typeInterface = TypeInterface.createOfClass typeClass
        let typeGeneric = TypeGeneric.create "T_MyGeneric"
        let typeValue = TypeValue.Int16
        let typeReference = TypeReference.String
        let typeValueNullale = TypeValue.DateTime.AsNullable

        let isEqual = tvComposable.Equals(tvnComposable)
        let isEqual1 = TypeComposable.isEquals tvComposable typeGeneric

        let typeArray = TypeArray.create className
        let typeArray2 = TypeArray.create typeValue
        let typeArray3 = TypeArray.create typeReference
        let typeArray4 = TypeArray.create typeValueNullale

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

        let arrayCodeables : ICodeable[] = [|kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric|] 
        let listCodeables : ICodeable list = [kw ; typeValue; typeReference; typeValueNullale; className; typeGeneric] 
        let someCode = toCodeSpaced arrayCodeables
        let someCode2 = toCodeCommaSpaced listCodeables
        let someCode3 = toCodeSpaced [|className ; typeReference ; typeValueNullale|]

        let x = ValueNullable TypeValue.Byte
        let y = x.TypeValue
        ""