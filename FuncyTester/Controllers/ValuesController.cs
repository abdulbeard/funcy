using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funcy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FuncyTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return new string[] { "value1", "value2", (await Class1.Yolo()).ToString()  };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            return (await Class1.Evaluate<double>(new List<string> {"System.Math" }, new List<string> { "System.Math"}, "System.Math.Sqrt(2 + 2)", null)).ToString();
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] FuncDef def)
        {
            return (await Class1.Evaluate<object>(def.Imports, def.References, def.Code, def.Globals));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class FuncDef
    {
        public List<string> References { get; set; }
        public List<string> Imports { get; set; }
        public string Code { get; set; }
        public Input Globals { get; set; }
    }
}
