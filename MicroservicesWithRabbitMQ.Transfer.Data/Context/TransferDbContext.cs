using MicroservicesWithRabbitMQ.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesWithRabbitMQ.Transfer.Data.Context
{
    public class TransferDbContext: DbContext
    {
        public DbSet<TransferLog> TransferLogs { get; set; }

        public TransferDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
