
using master_workshop_api.repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ← AGREGAR: Registrar controllers
builder.Services.AddControllers();

// AGREGAR: Registro de silgletons

builder.Services.AddSingleton<ProductsRepository>();

// ← AGREGAR: Registrar el repositorio para inyección de dependencias
// builder.Services.AddScoped<ProductsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ← AGREGAR: Mapear los controllers
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () => "Hello World!");

app.Run();

