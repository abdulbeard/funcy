using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuncyTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncController : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public async Task<List<FuncDefOut>> Get()
        {
            return new List<FuncDefOut> { FuncDefOut.FromFuncDef(FuncDefs.SalesTaxCalculator) };
        }
    }

    public class FuncDefs
    {
        public static FuncDef<CalculateTaxesInput, CalculateTaxesOutput> SalesTaxCalculator => new FuncDef<CalculateTaxesInput, CalculateTaxesOutput>
        {
            Code = "return Subtotal * (1 + (TaxPercentage / 100))",
            Imports = new List<string>(),
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
            References = new List<string>()
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
