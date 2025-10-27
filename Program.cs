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
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion,
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        }));

// Add SOAP Services
builder.Services.AddScoped<IUsuario, RUsuario>();
builder.Services.AddSoapCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
