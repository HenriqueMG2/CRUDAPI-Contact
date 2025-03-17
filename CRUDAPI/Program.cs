using CRUDAPI.Context;
using CRUDAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Configuração do banco de dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do CORS para permitir acesso do frontend Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // URL do seu frontend Angular
                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddAuthorization();

// Configuração do Swagger para suportar autenticação JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD API", Version = "v1" });
});

// Registrar os serviços necessários
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Adiciona os controllers à aplicação
builder.Services.AddControllers();

var app = builder.Build();

// Configuração do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Certifique-se de usar HTTPS, autenticação e autorização na ordem correta
app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.UseAuthentication();  // Certifique-se de que a autenticação vem antes da autorização
app.UseAuthorization();   // A autorização deve vir depois da autenticação

// Mapeamento das rotas para os controllers
app.MapControllers();

// Iniciar a aplicação
app.Run();