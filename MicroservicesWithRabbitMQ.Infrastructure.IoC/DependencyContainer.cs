using MediatR;
using MicroservicesWithRabbitMQ.Banking.Application.Interfaces;
using MicroservicesWithRabbitMQ.Banking.Application.Services;
using MicroservicesWithRabbitMQ.Banking.Data.Context;
using MicroservicesWithRabbitMQ.Banking.Data.Repository;
using MicroservicesWithRabbitMQ.Banking.Domain.CommandHandlers;
using MicroservicesWithRabbitMQ.Banking.Domain.Commands;
using MicroservicesWithRabbitMQ.Banking.Domain.Interfaces;
using MicroservicesWithRabbitMQ.Domain.Core.Bus;
using MicroservicesWithRabbitMQ.Infrastructure.Bus;
using MicroservicesWithRabbitMQ.Transfer.Application.Interfaces;
using MicroservicesWithRabbitMQ.Transfer.Application.Services;
using MicroservicesWithRabbitMQ.Transfer.Data.Context;
using MicroservicesWithRabbitMQ.Transfer.Data.Repository;
using MicroservicesWithRabbitMQ.Transfer.Domain.EventHandlers;
using MicroservicesWithRabbitMQ.Transfer.Domain.Events;
using MicroservicesWithRabbitMQ.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesWithRabbitMQ.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Bus
            services.AddTransient<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            // Subscriptions
            services.AddTransient<TransferEventHandler>();

            // Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            // Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            // Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            // Data
            services.AddTransient<BankingDbContext>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<TransferDbContext>();
            services.AddTransient<ITransferRepository, TransferRepository>();


            return services;
        }
    }
}
