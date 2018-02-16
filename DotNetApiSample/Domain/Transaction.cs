using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetApiSample.Domain
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }

        public DateTime Timestamp { get; private set; }

        public Person Payee { get; set; }

        public Person Payer { get; set; }

        public Transaction()
        {
            Timestamp = DateTime.Now;
        }
    }
}
