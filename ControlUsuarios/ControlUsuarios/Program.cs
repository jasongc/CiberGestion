using AccesoDatos.Clases;
using AccesoDatos.Conexion;
using AccesoDatos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Middleware;
using Negocios.Clases;
using Negocios.Interfaces;
using Negocios.Utilitarios;
using System.Text;

var builder = WebApplication.CreateBuilder(args); ;
var CORSName = "CORSName";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSName,
                        policy =>
                        {
                            policy.WithOrigins().AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                        });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    //options.Authority = "https://localhost:5001";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtKey").ToString())),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddTransient<ControlUsuarioContext>();
builder.Services.AddTransient<ILoginNEG, LoginNEG>();
builder.Services.AddTransient<ILoginACD, LoginACD>();
builder.Services.AddTransient<IUsuarioNEG, UsuarioNEG>();
builder.Services.AddTransient<IUsuarioACD, UsuarioACD>();
builder.Services.AddTransient<IJsonWebTokenNEG, JsonWebTokenNEG>();

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

app.UseCors(CORSName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
