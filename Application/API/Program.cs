using Application.Authentication;
using Application.UseCases;
using Application.UseCases.Authentication;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.AddDependencies( builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureUseCases.AddServices( builder.Services );
AuthConfigurator.AddAuthenticationServices(
    builder.Services,
    configuration.GetSection( "Authentication" ).Get<AuthConfiguration>()  );
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.MigrationsAssembly( "Application" )));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostPorts", builder =>
    {
        builder.WithOrigins("https://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhostPorts");
AuthConfigurator.AddAuthentication( app );

app.MapControllers();

app.Run();
