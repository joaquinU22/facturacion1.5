using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace Facturacion1._5.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _istancia;
        private readonly string _connection;
        private DataHelper()
        {
            _connection = "Data Source=GWTN156-4\\JOAQUINUTRERA;Initial Catalog=FacturacionDb;Integrated Security=True";
        }
        public static DataHelper GetInstance()
        {
            if(_istancia == null )
                _istancia = new DataHelper();
            return _istancia;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connection);
        }
        public DataTable ExecuteSpQuery(string sp, List<ParametersSQL>? parameters)
        {
            DataTable t = new DataTable();
            using (var connection = GetConnection())
            {
                using (var cmd = new SqlCommand(sp, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }

                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        t.Load(reader);
                    }
                }
            }
            return t;
        }
        public int ExecuteSPDML(string sp, List<ParametersSQL>?parameters)
        {
            int rows;
            using (var connection = GetConnection())
            {
                using (var cmd = new SqlCommand(sp, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }

                    connection.Open();
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return rows;
        }
        public int ExecuteSPDMLTransaction(string sp, List<ParametersSQL>?parameters, SqlTransaction transaction)
        {
            int rowsAffected = 0;

            using (var command = new SqlCommand(sp, transaction.Connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }

                rowsAffected = command.ExecuteNonQuery(); // Devuelve el número de filas afectadas
            }

            return rowsAffected;
        }
    }
}
