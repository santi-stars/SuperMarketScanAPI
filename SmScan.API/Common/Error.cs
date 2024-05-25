namespace SmScan.API.Common;
public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null Value was provided");

    public static implicit operator Result(Error error) => Result.Failure(error);
}
