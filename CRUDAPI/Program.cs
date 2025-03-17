using CRUDAPI.Context;
using CRUDAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Configura��o do banco de dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o do CORS para permitir acesso do frontend Angular
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

// Configura��o do Swagger para suportar autentica��o JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD API", Version = "v1" });
});

// Registrar os servi�os necess�rios
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Adiciona os controllers � aplica��o
builder.Services.AddControllers();

var app = builder.Build();

// Configura��o do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Certifique-se de usar HTTPS, autentica��o e autoriza��o na ordem correta
app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.UseAuthentication();  // Certifique-se de que a autentica��o vem antes da autoriza��o
app.UseAuthorization();   // A autoriza��o deve vir depois da autentica��o

// Mapeamento das rotas para os controllers
app.MapControllers();

// Iniciar a aplica��o
app.Run();