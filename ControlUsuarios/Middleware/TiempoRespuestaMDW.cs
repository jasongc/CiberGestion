using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Middleware
{
    public class TiempoRespuestaMDW
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TiempoRespuestaMDW> _logger;
        private readonly IConfiguration _configuration;

        public TiempoRespuestaMDW(RequestDelegate next, ILogger<TiempoRespuestaMDW> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                string rutaCarpetaLogs = _configuration.GetSection("Logging:File:Path").Value;
                rutaCarpetaLogs = string.IsNullOrEmpty(rutaCarpetaLogs) ? "file_logs/logs" : rutaCarpetaLogs;
                DirectoryInfo dir = new DirectoryInfo(rutaCarpetaLogs);

                string rutaArchivo = string.Format("{0}\\{1}.txt", rutaCarpetaLogs, DateTime.Now.ToString("dd-MM-yyyy"));
                FileInfo file = new FileInfo(rutaArchivo);

                if (!dir.Exists)
                    dir.Create();
                if (!file.Exists)
                    file.Create().Dispose();

                long responseTime = stopwatch.ElapsedMilliseconds;
                string message = string.Format("[{0}] {1} {2} => {3} ms", DateTime.Now, context.Request.Method, context.Request.Path, responseTime);

                // escribir el tiempo de respuesta en un archivo de registro
                using var streamWriter = new StreamWriter(rutaArchivo, append: true);
                await streamWriter.WriteLineAsync(message);

                // registrar el tiempo de respuesta en el registro de la aplicación
                _logger.LogInformation(message);
                streamWriter.Dispose();
            }
        }
    }
}