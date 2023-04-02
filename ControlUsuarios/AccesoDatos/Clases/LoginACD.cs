using AccesoDatos.Conexion;
using AccesoDatos.Interfaces;
using Entidades.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Clases
{
    public class LoginACD : ILoginACD
    {
        protected readonly ControlUsuarioContext _context;
        protected readonly IUsuarioACD _usuarioACD;
        public LoginACD(ControlUsuarioContext context, IUsuarioACD usuarioACD)
        {
            _context = context;
            _usuarioACD = usuarioACD;
        }
        public void RegistrarCierreLogin(int piIdUsuario)
        {
            using (SqlConnection cnn = _context.Connection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RegistrarCierreLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@piIdUsuario", piIdUsuario);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    cnn.Close();
                    cnn.Dispose();
                }
                catch (Exception ex)
                {

                    cnn.Close();
                    cnn.Dispose();
                    throw ex;
                }
            }
        }
        public UsuarioENT RegistrarInicioLogin(UsuarioLoginENT usuarioLogin)
        {
            UsuarioENT usuarioENT = new UsuarioENT();

            int iIdUsuario = 0;
            string sJWT = string.Empty;
            using (SqlConnection cnn = _context.Connection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RegistrarInicioLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@piIdUsuario", iIdUsuario);
                        cmd.Parameters.AddWithValue("@pvJWT", sJWT);

                        object? resultado = cmd.ExecuteScalar();

                        usuarioENT.dtFechaUltimoAcceso = DateTime.Parse(resultado.ToString());
                        cmd.Dispose();
                    }

                    cnn.Close();
                    cnn.Dispose();
                }
                catch (Exception ex)
                {

                    cnn.Close();
                    cnn.Dispose();
                    throw ex;
                }
            }

            return usuarioENT;
        }
        public void RegistrarIntentoFallidoLogin(int piIdUsuario)
        {
            using (SqlConnection cnn = _context.Connection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RegistrarIntentoFallidoLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@piIdUsuario", piIdUsuario);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    cnn.Close();
                    cnn.Dispose();
                }
                catch (Exception ex)
                {

                    cnn.Close();
                    cnn.Dispose();
                    throw ex;
                }
            }
        }
    }
}
