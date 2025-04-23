using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NiceGallery.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionando o contexto do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Adicionando os serviços de controllers
builder.Services.AddControllers();

// Adicionando o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NiceGallery API", Version = "v1" });
});

var app = builder.Build();

// Usando o Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NiceGallery API V1");
        c.RoutePrefix = string.Empty; // Isso fará com que o Swagger esteja disponível na raiz (ex: http://localhost:5000)
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
