using Microsoft.EntityFrameworkCore;
using ServiceSoap.Data;
using ServiceSoap.Interface;
using ServiceSoap.Repository;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add SOAP Services
builder.Services.AddScoped<IUsuario, RUsuario>();
builder.Services.AddSoapCore();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers (REST endpoints)
app.MapControllers();

// SOAP 1.1
((IApplicationBuilder)app).UseSoapEndpoint<IUsuario>(
    "/Service.asmx",
    new SoapEncoderOptions(),
    SoapSerializer.XmlSerializer
);

app.Run();
