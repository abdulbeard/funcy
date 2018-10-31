using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Threading.Tasks;

namespace funcy
{
    public class Class1
    {
        public static async Task<double> Yolo()
        {
            var result = await CSharpScript.EvaluateAsync<double>("Sqrt(2 + 2)", ScriptOptions.Default.WithImports("System.Math"));
            return result;
        }
    }
}
