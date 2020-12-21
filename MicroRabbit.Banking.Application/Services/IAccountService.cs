using System.Collections.Generic;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Application.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
    }
}