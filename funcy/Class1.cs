using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace funcy
{
    public class Class1
    {
        public static async Task<double> Yolo()
        {
            //var result = await CSharpScript.EvaluateAsync<double>("Sqrt(2 + 2)", ScriptOptions.Default.WithImports("System.Math"));
            var result = await CSharpScript.EvaluateAsync<double>("System.Math.Sqrt(2 + 2)", ScriptOptions.Default.WithReferences(typeof(System.Math).Assembly));
            return result;
        }

        public static async Task<T> Evaluate<T>(List<string> imports, List<string> references, string code, Input globals)
        {
            var scriptOptions = ScriptOptions.Default.AddImports(imports);
            scriptOptions.AddReferences(references);
            var result = await CSharpScript.EvaluateAsync<T>(code, scriptOptions, globals, typeof(Input));
            return result;
        }
    }

    public class Input
    {
        public string Abdul { get; set; }
        public bool IsTrue { get; set; }
        public int Age { get; set; }
    }
}
