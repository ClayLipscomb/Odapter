﻿//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2019 Clay Lipscomb
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
using System.Text;
using System.IO;

namespace Odapter {
    internal sealed class Comment {

        internal static readonly String COMMENT_AUTO_GENERATED = "//------------------------------------------------------------------------------"
                                + Environment.NewLine + "// <auto-generated>"
                                + Environment.NewLine + "//     This code was auto-generated by " + Generator.APPLICATION_NAME + " " + Generator.GetAppVersion()
#if !DEBUG
                                    + " on " + DateTime.Now.ToString("R")
#endif
                                    + "."
                                + Environment.NewLine + "//     Direct edits will be lost if the code is regenerated."
                                + Environment.NewLine + "// </auto-generated>"
                                + Environment.NewLine + "//------------------------------------------------------------------------------"
                                + Environment.NewLine;

        internal static readonly String COMMENT_AUTO_GENERATED_FOR_BASE_DTO = "//------------------------------------------------------------------------------"
                                + Environment.NewLine + "// <auto-generated>"
                                + Environment.NewLine + "//     This code was auto-generated by " + Generator.APPLICATION_NAME + " " + Generator.GetAppVersion()
#if !DEBUG
                                    + " on " + DateTime.Now.ToString("R")
#endif
                                    + "."
                                + Environment.NewLine + "//     It can be edited as necessary after initial generation."
                                + Environment.NewLine + "//     To avoid overwrite, Deploy Base DTOs? must be unchecked during subsequent generation."
                                + Environment.NewLine + "// </auto-generated>"
                                + Environment.NewLine + "//------------------------------------------------------------------------------"
                                + Environment.NewLine;

        internal static readonly String COMMENT_AUTO_GENERATED_FOR_BASE_ADAPTER = "//------------------------------------------------------------------------------"
                                + Environment.NewLine + "// <auto-generated>"
                                + Environment.NewLine + "//     This code was auto-generated by " + Generator.APPLICATION_NAME + " " + Generator.GetAppVersion()
#if !DEBUG
                                    + " on " + DateTime.Now.ToString("R")
#endif
                                    + "."
                                + Environment.NewLine + "//     It can be edited as necessary after initial generation."
                                + Environment.NewLine + "//     To avoid overwrite, Deploy Base Adapter? must be unchecked during subsequent generation."
                                + Environment.NewLine + "// </auto-generated>"
                                + Environment.NewLine + "//------------------------------------------------------------------------------"
                                + Environment.NewLine;

        internal static void WriteAutoGeneratedComment(StreamWriter outFile) {
            StringBuilder headerText = new StringBuilder("");
            headerText.AppendLine(Comment.COMMENT_AUTO_GENERATED);
            outFile.Write(headerText);
        }
    }
}
