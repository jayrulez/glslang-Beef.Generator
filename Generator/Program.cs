using CppAst;
using System;
using System.IO;

namespace glslang_Beef.Generator
{
    public class Program
    {
        private static string glslangSourceDir = "../../../../../glslang";
        private static string OutputDir = "../../../../../glslang-Beef/src/Generated";
        private static string ProjectNamespace = "glslang_Beef";

        public static int Main(string[] args)
        {
            var options = new CppParserOptions
            {
                ParseMacros = true,
            };

            CppCompilation compilation = CppParser.ParseFile(Path.Combine(glslangSourceDir,"glslang","Include", "glslang_c_interface.h"), options);

            // Print diagnostic messages
            if (compilation.HasErrors)
            {
                foreach (var message in compilation.Diagnostics.Messages)
                {
                    if (message.Type == CppLogMessageType.Error)
                    {
                        var currentColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(message);
                        Console.ForegroundColor = currentColor;
                    }
                }

                return 0;
            }

            return BeefCodeGenerator.Generate(compilation, ProjectNamespace, Path.GetFullPath(OutputDir));
        }
    }
}