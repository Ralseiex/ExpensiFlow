namespace ExpensiFlow.Shared.Messaging;

public record MessageBusOptions(
    string Host = "localhost", 
    string Username = "guest", 
    string Password = "guest");