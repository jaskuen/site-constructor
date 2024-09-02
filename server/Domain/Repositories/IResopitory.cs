using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Domain.Repositories;

public interface IRepository<T> where T : Entity
{
  IQueryable<T> Table { get; }
  T GetById(int id);
  List<T> GetAll();
  void Add(T entity);
  void Add(IEnumerable<T> entities);
  void Remove(T entity);
  void Remove(int id);
  void Remove(IEnumerable<T> entities);
}
