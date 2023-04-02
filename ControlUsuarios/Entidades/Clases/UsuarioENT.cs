using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Clases
{
    public class UsuarioENT : IUsuarioENT
	{
		public int iIdUsuario { get; set; }
		public int iIdPerfil { get; set; }
		public string sPerfil { get; set; } = string.Empty;
		public string sNombres { get; set; } = string.Empty;
		public string sApellidoMaterno { get; set; } = string.Empty;
		public string sApellidoPaterno { get; set; } = string.Empty;
		public string sEmail { get; set; } = string.Empty;
		public string sContrasenia { get; set; } = string.Empty;
		#region EXTRAS
		public DateTime dtFechaUltimoAcceso { get; set; }
		#endregion

	}
}
