using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.LocalUser;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteConstructor.Domain.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
  public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<ISiteDataRepository, SiteDataRepository>();
    services.AddScoped<IRepository<LocalUser>, BaseRepository<LocalUser>>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
  }
}
