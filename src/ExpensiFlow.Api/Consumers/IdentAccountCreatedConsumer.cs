using ExpensiFlow.Domain;
using ExpensiFlow.Ident.Events;
using ExpensiFlow.Infrastructure.Database;
using MassTransit;

namespace ExpensiFlow.Api.Consumers;

public class AccountCreatedConsumer(ExpensiFlowContext db, ILogger<AccountCreatedConsumer> logger)
    : IConsumer<AccountCreated>
{
    private readonly ExpensiFlowContext _db = db;
    private readonly ILogger<AccountCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<AccountCreated> context)
    {
        var message = context.Message;
        if (_db.Users.Any(user => user.Id == message.Id))
        {
            _logger.LogError("User already exists");
            return;
        }

        _db.Add(new User { Id = message.Id, Name = message.Name, Email = message.Email });
        await _db.SaveChangesAsync(context.CancellationToken);
    }

    public class Definition : ConsumerDefinition<AccountCreatedConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<AccountCreatedConsumer> consumerConfigurator,
            IRegistrationContext context)
        {
            Console.WriteLine();
            base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
        }
    }
}