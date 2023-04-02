using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Conexion
{
    public class ControlUsuarioContext
    {
        IConfigurationRoot _configuration;
        public ControlUsuarioContext()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public SqlConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        bool ColumnExists(SqlDataReader dr, String ColumnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
                if (dr.GetName(i).Equals(ColumnName, StringComparison.OrdinalIgnoreCase))
                    return true;



            return false;
        }
        public T reader<T>(SqlDataReader Reader, string stColumna)
        {
            try
            {
                bool boExisteColumna = ColumnExists(Reader, stColumna);
                if (boExisteColumna)
                {
                    if (Reader[stColumna] != DBNull.Value)
                        return (T)Convert.ChangeType(Reader[stColumna], typeof(T));
                    else
                        return default;
                }
                else
                    return default;
                //throw new Exception("La columna " + stColumna + " no existe en el listado.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
