using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data.Utils
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool Delete(int id)
        {
            var parameters = new List<ParametersSQL>
            {
                 new ParametersSQL("@Id", id)
            };

            int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("sp_EliminarArticuloConDetalles", parameters);

            return rowsAffected > 0;

        }

        public List<Articulo> GetAll()
        {
            var articulos = new List<Articulo>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSpQuery("sp_TraerArticulos", null);
            foreach (DataRow row in t.Rows) 
            { 
                int id = Convert.ToInt32(row["Id"]);
                string nombre = row["Nombre"].ToString();
                double precioUnitario = Convert.ToDouble(row["PrecioUnitario"]);
                Articulo oArticulo = new Articulo()
                {
                    Id = id,
                    Nombre = nombre,
                    PrecioUnitario = precioUnitario
                };
                articulos.Add(oArticulo);
            }
            return articulos;   
        }

        public bool Update(Articulo articulo)
        {
            var parameters = new List<ParametersSQL>
            {
                new ParametersSQL("@Id", articulo.Id),
                new ParametersSQL("@Nombre", articulo.Nombre),
                new ParametersSQL("@PrecioUnitario", articulo.PrecioUnitario),
            };
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("sp_ActualizarArticulo", parameters);
            return filasAfectadas > 0;
        }
    }
}
