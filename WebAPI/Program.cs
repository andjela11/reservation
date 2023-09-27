using System.Reflection;
using Application;
using Persistence;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddSwaggerGen(c =>
{
    var callingAssembly = Assembly.GetEntryAssembly();
    if (callingAssembly is null)
    {
        throw new Exception("Calling assembly not found");
    }
    var callingAssemblyName = callingAssembly.GetName().Name;

    var xmlFile = $"{callingAssemblyName}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
