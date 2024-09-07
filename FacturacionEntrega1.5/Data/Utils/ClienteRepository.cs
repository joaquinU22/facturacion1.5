using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data.Utils
{
    public class ClienteRepository : IClienteRepository
    {
        public List<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();
            DataTable table = DataHelper.GetInstance().ExecuteSpQuery("sp_TraerCliente", null);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    
                    Cliente cliente = new Cliente
                    {
                        Id = Convert.ToInt32(row["IdCliente"]),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Dni = row["Dni"].ToString()
                    };
                    clientes.Add(cliente);
                }
            }
            return clientes;
        }
    }
}
