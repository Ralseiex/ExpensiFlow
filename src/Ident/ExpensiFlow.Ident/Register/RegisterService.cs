using Ardalis.Result;
using ExpensiFlow.Ident.Events;
using ExpensiFlow.Ident.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace ExpensiFlow.Ident.Register;

internal class RegisterService(IPasswordHasher<User> hasher, UserManager<User> userManager, IBus bus) : IRegisterService
{
    private readonly IPasswordHasher<User> _hasher = hasher;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IBus _bus = bus;

    public async Task<Result> Register(string userName, string password)
    {
        var user = new User { UserName = userName };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded == false)
            return Result.Error(new ErrorList(result.Errors.Select(error => error.Description)));

        await _bus.Publish(new AccountCreated(user.Id, user.UserName, user.Email ?? string.Empty));
        return Result.Success();
    }
}