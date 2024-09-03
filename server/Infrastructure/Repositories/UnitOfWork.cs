using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _dbContext;
  public UnitOfWork(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public void Commit()
  {
    _dbContext.SaveChanges();
  }
}
