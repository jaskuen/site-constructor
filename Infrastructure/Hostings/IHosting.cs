namespace Infrastructure.Hostings;

public interface IHosting<TParameters, TResult>
{
    public Task<TResult> HostAsync(TParameters paramaters);
}
