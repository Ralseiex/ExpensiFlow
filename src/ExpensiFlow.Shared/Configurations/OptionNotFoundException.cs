namespace ExpensiFlow.Shared.Configurations;

public class OptionsNotFoundException : Exception
{
    public OptionsNotFoundException()
    {
    }

    public OptionsNotFoundException(string message) : base(message)
    {
    }

    public OptionsNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}