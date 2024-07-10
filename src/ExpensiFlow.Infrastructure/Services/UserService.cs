using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Infrastructure.Services;

public sealed class UserService(ExpensiFlowContext db)
{
    private readonly ExpensiFlowContext _db = db;

    public async Task<bool> IsUserExists(AccountId accountId, CancellationToken cancellationToken = default)
        => await _db.Users.AnyAsync(user => user.Id == accountId, cancellationToken);
}