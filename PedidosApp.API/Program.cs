using Microsoft.EntityFrameworkCore;
using PedidosApp.API.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MediatR
builder.Services.AddMediatR(m=> m.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

//Injeção de dependência para configurar a conexão com o banco de dados do SQL Server
builder.Services.AddDbContext<SqlServerContexts>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
