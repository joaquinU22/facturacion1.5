using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Data.Utils
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool Add(Factura factura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction transaction = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                transaction = cnn.BeginTransaction();

                var parametros = new List<ParametersSQL>
                {
            new ParametersSQL("@Fecha", factura.Fecha),
            new ParametersSQL("@IdFormaPago", factura.IdFormaPago),
            new ParametersSQL("@ClienteId", factura.ClienteId),
                 };
                int rowsAffected = DataHelper.GetInstance().ExecuteSPDMLTransaction("sp_AgregarNuevaFactura", parametros, transaction);
                if (rowsAffected > 0)
                {
                    var idFacturaParam = new List<ParametersSQL>
                    {
                new ParametersSQL("@Fecha", factura.Fecha),
                new ParametersSQL("@IdFormaPago", factura.IdFormaPago),
                new ParametersSQL("@ClienteId", factura.ClienteId)
                    };
                    var resultado = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerUltimaFacturaId", idFacturaParam);
                    if (resultado != null && resultado.Rows.Count > 0)
                    {
                        int facturaId = Convert.ToInt32(resultado.Rows[0]["IdFactura"]);
                        factura.Id = facturaId;
                        foreach (var detalle in factura.Detalles)
                        {
                            var parametrosDetalle = new List<ParametersSQL>
                            {
                        new ParametersSQL("@IdFactura", factura.Id),
                        new ParametersSQL("@ArticuloId", detalle.Articulo.Id),
                        new ParametersSQL("@Cantidad", detalle.Cantidad),
                        new ParametersSQL("@Precio", detalle.Precio)
                            };
                            DataHelper.GetInstance().ExecuteSPDMLTransaction("sp_AgregarNuevoDetalleFactura", parametrosDetalle, transaction);
                        }
                        transaction.Commit();
                    }
                    else
                    {
                        result = false;
                        transaction.Rollback();
                    }
                }
                
            }
            catch (SqlException ex)
            {
                // Log the exception details here for debugging purposes
                if (transaction != null) transaction.Rollback();
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public List<Factura> GetAll()
        {
            var facturas = new List<Factura>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSpQuery("sp_TraerFacturas", null);

            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["IdFactura"]);
                DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                int idFormaPago = Convert.ToInt32(row["IdFormaPago"]);
                int idCliente = Convert.ToInt32(row["ClienteId"]);
                Factura factura = new Factura
                {
                    Id = id,
                    Fecha = fecha,
                    IdFormaPago = idFormaPago,
                    ClienteId = idCliente,
                };
                facturas.Add(factura);
            }
            return facturas;
        }
    }
}
