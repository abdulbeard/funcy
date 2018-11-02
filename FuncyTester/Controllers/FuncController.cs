using funcy;
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
}
