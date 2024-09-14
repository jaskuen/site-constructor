using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SiteConstructor.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DependencyInjection.AddDependencies(builder.Services, builder.Configuration);
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
