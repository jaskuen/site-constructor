namespace Application.UseCases.Results;

public class Result
{
    public bool IsSuccess => Error == null;
    public Error? Error { get; set; }

    public static Result Ok()
    {
        return new Result { Error = null, };
    }

    public static Result Fail(Error error)
    {
        return new Result { Error = error, };
    }
}
