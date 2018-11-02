using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace funcy
{
    public class Utils
    {
        public static TOut ChangeType<TIn, TOut>(TIn input)
        {
            return JsonConvert.DeserializeObject<TOut>(JsonConvert.SerializeObject(input));
        }
    }
}
