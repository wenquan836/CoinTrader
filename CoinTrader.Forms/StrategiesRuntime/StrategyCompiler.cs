
using CoinTrader.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoinTrader.Strategies
{
    internal class StrategyCompiler
    {
        private static string[] codeExtentions = new string[] { ".cs", ".csx" };
        public static Assembly Compile(string filePath, out string err)
        {
            err = string.Empty;

            Debug.Assert( !string.IsNullOrEmpty( filePath));

            if(!Directory.Exists(filePath))
            {
                return null;
            }
 
            var files = from file in Directory.EnumerateFiles(filePath,"*.*", SearchOption.AllDirectories) 
                        where codeExtentions.Contains( Path.GetExtension(file).ToLower())
                        select file ;

            if (files.Count() == 0)
                return null;

            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            cplist.ReferencedAssemblies.Add("CoinTrader.Common.dll");
            cplist.ReferencedAssemblies.Add("CoinTrader.OKXCore.dll");
            cplist.ReferencedAssemblies.Add("CoinTrader.Strategies.dll");

            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
 
            CompilerResults cr = codeDomProvider.CompileAssemblyFromFile(cplist, files.ToArray());
            if (cr.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.AppendLine(ce.ToString());
                }

                err = sb.ToString();
                ///编译错误
                Logger.Instance.LogError(err);
                return null;
            }

            return cr.CompiledAssembly;
        }
    }
}
