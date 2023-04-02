using AccesoDatos.Clases;
using AccesoDatos.Conexion;
using AccesoDatos.Interfaces;
using Middleware;
using Negocios.Clases;
using Negocios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ControlUsuarioContext>();
builder.Services.AddTransient<ILoginNEG, LoginNEG>();
builder.Services.AddTransient<ILoginACD, LoginACD>();
builder.Services.AddTransient<IUsuarioNEG, UsuarioNEG>();
builder.Services.AddTransient<IUsuarioACD, UsuarioACD>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TiempoRespuestaMDW>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
