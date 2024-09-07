using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data.Utils
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPago> GetAll()
        {
            var forma = new List<FormaPago>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSpQuery("sp_TraerFormasPago", null);
            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["IdFormaPago"]);
                string nombre = row["Nombre"].ToString();
                FormaPago oForma = new FormaPago()
                {
                    Id = id,
                    Nombre = nombre
                };
                forma.Add(oForma);
            }
            return forma;
        }
    }
}
