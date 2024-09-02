using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : Entity
{
  protected readonly DbSet<T> Repository;
  public IQueryable<T> Table
  {
    get => Repository.AsQueryable();
  }

  protected BaseRepository(DbSet<T> entities)
  {
    Repository = entities;
  }

  public BaseRepository(ApplicationDbContext dbContext) : this( dbContext.Set<T>() )
  {
  }

  public virtual void Add(T entity)
  {
    Repository.Add(entity);
  }

  public virtual void Add(IEnumerable<T> entities)
  {
    Repository.AddRange(entities);
  }

  public virtual List<T> GetAll()
  {
    return Repository.ToList();
  }

  public virtual T? GetById(int id)
  {
    return Repository.FirstOrDefault(entity => entity.Id == id);
  }

  public virtual void Remove(T entity)
  {
    Repository.Remove(entity);
  }

  public virtual void Remove(int id)
  {
    T? entityToDelete = Repository.FirstOrDefault(entity => entity.Id == id);
    if ( entityToDelete == null )
    {
      throw new ArgumentOutOfRangeException( $"No entity with id: {id} exists" );
    }
    Repository.Remove(entityToDelete);
  }
  public virtual void Remove(IEnumerable<T> entities)
  {
    Repository.RemoveRange(entities);
  }
}
