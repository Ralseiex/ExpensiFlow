using ExpensiFlow.Domain.AccountIdAggregate;
using Microsoft.AspNetCore.Identity;

namespace ExpensiFlow.Ident.Models;

public class User : IdentityUser<AccountId>;