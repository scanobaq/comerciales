using comerciales.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureCors();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.JwtConfiguration(builder.Configuration);
builder.Services.AddAplicacionServices(builder.Configuration);
builder.Services.AddControllers(); // ← Agregar esto para registrar controladores
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();          // expone /swagger/v1/swagger.json
    app.UseSwaggerUI();        // UI en /swagger
}

app.UseHttpsRedirection();

app.MapControllers(); // ← Agregar esto para mapear los controladores

app.Run();


