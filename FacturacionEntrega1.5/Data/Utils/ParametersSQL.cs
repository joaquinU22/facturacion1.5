using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data.Utils
{
    public class ParametersSQL
    {
        public ParametersSQL(string name, object value) 
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }   
    }
}
