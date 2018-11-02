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

        public static async Task<object> EvaluateFromFuncDef(FuncDefOut input, object globals)
        {
            if (input.Name == FuncDefs.SalesTaxCalculator.Name)
            {
                var globalsInterpreted = Utils.ChangeType<object, CalculateTaxesInput>(globals);
                var scriptOptions = ScriptOptions.Default.AddImports(input.Imports);
                scriptOptions = scriptOptions.AddReferences(input.References);
                var result = await CSharpScript.EvaluateAsync<CalculateTaxesOutput>(input.Code, scriptOptions, globalsInterpreted, typeof(CalculateTaxesInput));
                return result;
            }
            return null;
        }
    }

    public class Input
    {
        public string Abdul { get; set; }
        public bool IsTrue { get; set; }
        public int Age { get; set; }
    }

    public class FuncDefs
    {
        public static List<FuncDefOut> Default => new List<FuncDefOut> { FuncDefOut.FromFuncDef(SalesTaxCalculator) };
        public static FuncDef<CalculateTaxesInput, CalculateTaxesOutput> SalesTaxCalculator => new FuncDef<CalculateTaxesInput, CalculateTaxesOutput>
        {
            //Code = "return Subtotal * (1 + (TaxPercentage / 100))",
            //Code = "public class CalculateTaxesOutput{public double Result {get;set;}} var x = Subtotal; Subtotal = 34545.00;return new CalculateTaxesOutput{ Result = x * (1 + (TaxPercentage / 100))};",
            Code = "var x = Subtotal; Subtotal = 34545.00;return new funcy.CalculateTaxesOutput{ Result = x * (1 + (TaxPercentage / 100))};",
            Imports = new List<string>() { },
            InputClassDefinition = new CalculateTaxesInput
            {
                Subtotal = 45.454545,
                TaxBracket = "SuperRich",
                TaxPercentage = 5.45657
            },
            OutputClassDefinition = new CalculateTaxesOutput
            {
                Result = 4.454545
            },
            Name = "CTT_Ver_1.0",
            References = new List<string>() { "funcy" }
        };
        //"if(System.String.IsNullOrWhiteSpace(Abdul)){switch(IsTrue){case true:{return 445;} case false: {return 110;}}} else {if(Age < 34){return 500;} else {return (System.Math.Sqrt(2+2)) + Age;}}"
    }

    public class CalculateTaxesOutput
    {
        public double Result { get; set; }
    }

    public class CalculateTaxesInput
    {
        public double Subtotal { get; set; }
        public double TaxPercentage { get; set; }
        public string TaxBracket { get; set; }
    }

    public class FuncDefOut
    {
        public static FuncDefOut FromFuncDef<TIn, TOut>(FuncDef<TIn, TOut> input)
        {
            return new FuncDefOut
            {
                Name = input.Name,
                Code = input.Code,
                Imports = input.Imports,
                InputClassDefinition = input.InputClassDefinition,
                OutputClassDefinition = input.OutputClassDefinition,
                References = input.References
            };
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public object InputClassDefinition { get; set; }
        public object OutputClassDefinition { get; set; }
        public List<string> References { get; set; }
        public List<string> Imports { get; set; }
    }

    public class FuncDef<TIn, TOut>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public TIn InputClassDefinition { get; set; }
        public TOut OutputClassDefinition { get; set; }
        public List<string> References { get; set; }
        public List<string> Imports { get; set; }
    }
}
