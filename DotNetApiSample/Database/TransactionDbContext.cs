using Microsoft.EntityFrameworkCore;
using DotNetApiSample.Domain;

namespace DotNetApiSample.Database
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }


        public TransactionDbContext() : base()
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
