using Microsoft.OpenApi.Models;
using Street.Lightning.Application;
using Street.Lightning.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddAzureAppConfiguration(
    "Endpoint=https://app-street-lights-config.azconfig.io;Id=fQi9;Secret=cjpRyZR5Te32j9FXWdp8RikpR45qIT/gIPyeyXgQjBg=");
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configuration =>
{
    configuration.SwaggerDoc("v1", new OpenApiInfo { Title = "Street Lights API", Version = "v1"});
});

var app = builder.Build();



// Uncomment this if debugging locally Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
