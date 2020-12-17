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
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Odapter {
    public class OracleHome {
        public String Value { get; set; }
        public String Description { get; set; }
        public override string ToString() {  return Description; }

        public OracleHome(String value, String description) {
            Value = value;
            Description = description;
        }
    }

    public class TnsNamesReader {
        public List<OracleHome> GetOracleHomesTest() {
            List<OracleHome> oracleHomes = new List<OracleHome>();
            RegistryKey rgkLM = Registry.LocalMachine;
            string[] subKeys = rgkLM.GetSubKeyNames();
            int count = subKeys.Length;

            RegistryKey rgkTest;

            rgkLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            rgkTest = rgkLM.OpenSubKey(@"SOFTWARE");
            rgkTest = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");

            RegistryKey rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");
            if (rgkAllHome != null)
                foreach (string subkey in rgkAllHome.GetSubKeyNames())
                    if (subkey.StartsWith("KEY_")) oracleHomes.Add(new OracleHome(subkey, subkey.Substring(4).ToUpper()));
            return oracleHomes;
        }

        public List<OracleHome> GetOracleHomes() {
            RegistryKey rgkAllHome = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ORACLE");

            if (rgkAllHome == null) {
                // retry by specifying the 64 bit version of key
                RegistryKey rgkLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");
            }

            List<OracleHome> oracleHomes = new List<OracleHome>();
            if (rgkAllHome != null) 
                foreach (string subkey in rgkAllHome.GetSubKeyNames()) 
                    if (subkey.StartsWith("KEY_")) oracleHomes.Add(new OracleHome(subkey, subkey.Substring(4).ToUpper()));
            return oracleHomes;
        }

        private string GetOracleHomePath(String oracleHomeRegistryKey) {
            if (String.IsNullOrWhiteSpace(oracleHomeRegistryKey)) return "";
            RegistryKey rgkOracleHome = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ORACLE\" + oracleHomeRegistryKey);

            if (rgkOracleHome == null) {
                // retry by specifying the 64 bit version of key
                RegistryKey rgkLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                rgkOracleHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE\" + oracleHomeRegistryKey);
            }

            if (rgkOracleHome != null) return rgkOracleHome.GetValue("ORACLE_HOME").ToString();
            return "";
        }

        private string GetTnsNamesOraFilePath(String OracleHomeRegistryKey) {
            string oracleHomePath = this.GetOracleHomePath(OracleHomeRegistryKey);
            string tnsNamesOraFilePath = "";
            if (!String.IsNullOrWhiteSpace(oracleHomePath)) {
                tnsNamesOraFilePath = oracleHomePath + @"\NETWORK\ADMIN\TNSNAMES.ORA";
                if (!(System.IO.File.Exists(tnsNamesOraFilePath))) 
                    tnsNamesOraFilePath = oracleHomePath + @"\NET80\ADMIN\TNSNAMES.ORA";                
            }
            return tnsNamesOraFilePath;
        }

        public List<string> LoadTnsNames(string OracleHomeRegistryKey) {
            List<string> DBNamesCollection = new List<string>();
            string RegExPattern = @"[\n][\s]*[^\(][a-zA-Z0-9_.]+[\s]*=[\s]*\(";
            string strTnsNamesOraFilePath = GetTnsNamesOraFilePath(OracleHomeRegistryKey);

            if (!strTnsNamesOraFilePath.Equals("")) {
                // check out that file does physically exists
                System.IO.FileInfo fiTNS = new System.IO.FileInfo(strTnsNamesOraFilePath);
                if (fiTNS.Exists) {
                    if (fiTNS.Length > 0) {
                        // read tnsnames.ora file
                        int iCount;
                        for (iCount = 0; iCount < Regex.Matches(System.IO.File.ReadAllText(fiTNS.FullName), RegExPattern).Count; iCount++) {
                            DBNamesCollection.Add(Regex.Matches(System.IO.File.ReadAllText(fiTNS.FullName), RegExPattern)[iCount].Value.Trim().ToUpper()
                                .Substring(0, Regex.Matches(System.IO.File.ReadAllText(fiTNS.FullName), RegExPattern)[iCount].Value.Trim().ToUpper().IndexOf("=")).Trim());
                        }
                    }
                }
            }

            DBNamesCollection.Sort();
            return DBNamesCollection;
        }
    }
}

