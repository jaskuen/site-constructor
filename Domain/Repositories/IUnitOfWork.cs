namespace Domain.Repositories;

public interface IUnitOfWork
{
    public void Commit();
}
