using MicroservicesWithRabbitMQ.Banking.Application.Interfaces;
using MicroservicesWithRabbitMQ.Banking.Application.Models;
using MicroservicesWithRabbitMQ.Banking.Domain.Commands;
using MicroservicesWithRabbitMQ.Banking.Domain.Interfaces;
using MicroservicesWithRabbitMQ.Banking.Domain.Models;
using MicroservicesWithRabbitMQ.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesWithRabbitMQ.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
                    from: accountTransfer.FromAccount,
                    to: accountTransfer.ToAccount,
                    amount: accountTransfer.TransferAmount
                );

            _bus.SendCommand(createTransferCommand);
        }
    }
}
