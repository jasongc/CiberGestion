using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Clases
{
    public class UsuarioENT : IUsuarioENT
	{
		[JsonPropertyName("id_usuario")]
		public int iIdUsuario { get; set; }
		[JsonIgnore]
		public int iIdPerfil { get; set; }
		[JsonPropertyName("perfil")]
		public string sPerfil { get; set; } = string.Empty;
		[JsonPropertyName("nombres")]
		public string sNombres { get; set; } = string.Empty;
		[JsonPropertyName("apellido_paterno")]
		public string sApellidoMaterno { get; set; } = string.Empty;
		[JsonPropertyName("apellido_materno")]
		public string sApellidoPaterno { get; set; } = string.Empty;
		[JsonPropertyName("usuario")]
		public string sEmail { get; set; } = string.Empty;
		[JsonIgnore]
		public string sContrasenia { get; set; } = string.Empty;
		[JsonPropertyName("json_web_token")]
		public string sJsonWebToken { get; set; } = string.Empty;
		#region EXTRAS
		[JsonIgnore]
		public DateTime dtFechaUltimoAcceso { get; set; }
		[JsonPropertyName("fecha_ultimo_acceso")]
		public string sFechaUltimoAcceso { get => dtFechaUltimoAcceso.ToString("dd-MM-yyyy HH:mm:ss"); }
		#endregion

	}
}
