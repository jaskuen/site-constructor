namespace Application.UseCases.Results;

public class Error
{
    public string Reason { get; set; }

    public Error( string reason )
    {
        Reason = reason;
    }
}