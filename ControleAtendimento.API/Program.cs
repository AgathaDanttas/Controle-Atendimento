using ControleAtendimento.API.Data;
using ControleAtendimento.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=atendimentos.db"));

// Registro dos serviços (camada de negócio)
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IPrioridadeService, PrioridadeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();