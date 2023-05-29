using APIGrupoFycas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var rulesCors = "RulesCors";

builder.Services.AddCors(option =>
    option.AddPolicy(name: rulesCors,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        })
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<grupo_fycasContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionStrings"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.20-mariadb"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(rulesCors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
