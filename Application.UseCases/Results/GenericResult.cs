namespace Application.UseCases.Results;

public abstract class Result<TResult, TData> : Result
    where TResult : Result<TResult, TData>, new()
    where TData : class
{
    public TData Data { get; set; }
    
    public static TResult Ok( TData data )
    {
        return new TResult { Data = data, Error = null, };
    }

    public new static TResult Fail( Error error )
    {
        return new TResult { Data = null, Error = error, };
    }
}