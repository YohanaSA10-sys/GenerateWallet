using Microsoft.WindowsAzure.Storage;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = "Server=q0uvl1phe4.database.windows.net;Database=LSPROD;User Id=THD001_SQLUSR;Password=phQHGL#*;";
builder.Services.AddSingleton(new SqlConnection(connectionString)); 
builder.Services.AddTransient<DatabaseOperations>();
builder.Services.AddHttpClient();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
