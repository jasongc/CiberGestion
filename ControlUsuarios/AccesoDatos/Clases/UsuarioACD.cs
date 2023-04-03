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
    public class UsuarioACD : IUsuarioACD
    {
        protected readonly ControlUsuarioContext _context;
        public UsuarioACD(ControlUsuarioContext context)
        {
            _context = context;
        }
        public void CambiarContrasenia(UsuarioLoginENT usuarioLogin)
        {
            using (SqlConnection cnn = _context.Connection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CambiarContrasenia", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@piIdUsuario", usuarioLogin.iIdUsuario);
                        cmd.Parameters.AddWithValue("@pvContrasenia", usuarioLogin.sContrasenia);
                        cmd.Parameters.AddWithValue("@pvNuevaContrasenia", usuarioLogin.sNuevaContrasenia);

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
        public UsuarioENT ObtenerUsuario(string sEmail)
        {
            UsuarioENT usuarioENT = new UsuarioENT();
            using (SqlConnection cnn = _context.Connection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ObtenerUsuarioLogin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pvEmail", sEmail);

                        SqlDataReader drd = cmd.ExecuteReader();
                        while (drd.Read())
                        {
                            usuarioENT = new UsuarioENT();
                            usuarioENT.iIdUsuario = _context.reader<int>(drd, "iIdUsuario");
                            usuarioENT.iIdPerfil = _context.reader<int>(drd, "iIdPerfil");
                            usuarioENT.sPerfil = _context.reader<string>(drd, "vPerfil");
                            usuarioENT.sNombres = _context.reader<string>(drd, "vNombres");
                            usuarioENT.sApellidoMaterno = _context.reader<string>(drd, "vApellidoMaterno");
                            usuarioENT.sApellidoPaterno = _context.reader<string>(drd, "vApellidoPaterno");
                            usuarioENT.sEmail = _context.reader<string>(drd, "vEmail");
                            usuarioENT.sContrasenia = _context.reader<string>(drd, "vContrasenia");
                        }
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
    }
}
