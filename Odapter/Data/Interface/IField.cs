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

using System;

namespace Odapter {
    internal interface IField : IComparable<IField>, IEntityAttribute {
        string Name { get; set; }
        int MapPosition { get; set; }
        string TypeOwner { get; set; }
        /// <summary>
        /// Holds a psuedo "sub field" at next data level down that is necessary to determine 
        /// the full type of this field. For example, an associative array of a NUMBER will have
        /// have a "sub field" for the NUMBER component.
        /// </summary>
        IField SubField { get; set; }
    }
}