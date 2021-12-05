using MicroservicesWithRabbitMQ.Domain.Core.Bus;
using MicroservicesWithRabbitMQ.Transfer.Domain.Events;
using MicroservicesWithRabbitMQ.Transfer.Domain.Interfaces;
using MicroservicesWithRabbitMQ.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesWithRabbitMQ.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                Amount = @event.Amount
            });

            _transferRepository.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
