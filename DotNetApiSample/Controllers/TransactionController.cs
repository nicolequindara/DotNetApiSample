using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetApiSample.Database;
using DotNetApiSample.Domain;

namespace DotNetApiSample.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;

            if (!_context.Transactions.Any())
            {
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    Transaction t = new Transaction();
                    t.Amount = r.NextDouble();
                    Address a = new Address { Line1 = String.Format("{0}{0}{0} Home Lame", r.Next(10), r.Next(10), r.Next(10)), City = "Dallas", State = State.TX, ZipCode = "7526" + r.Next(10) };
                    t.Payee = new Person { FirstName = "Payer" + i, LastName = "McPayerson", EmailAddress = "pay_me@gmail.com", PhoneNumber = "(111) 111-1111", HomeAddress = a };
                    t.Payer = new Person { FirstName = "Apayee" + i, LastName = "Payeeson", EmailAddress = "pay_you@gmail.com", PhoneNumber = "(222) 151-1213", HomeAddress = a };

                    _context.Addresses.Add(a);
                    _context.People.Add(t.Payee);
                    _context.People.Add(t.Payer);
                    _context.Transactions.Add(t);
                }
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _context.Transactions.ToList();
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        public Transaction Get(int Id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == Id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Transaction transaction)
        {
            if(transaction == null)
            {
                return BadRequest();
            }

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return CreatedAtRoute("Transaction", new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Transaction transaction)
        {
            if(transaction == null || transaction.Id != id)
            {
                return BadRequest();
            }

            Transaction t = _context.Transactions.FirstOrDefault(x => x.Id == id);
            if(t == null)
            {
                return NotFound();
            }

            t.Amount = transaction.Amount;
            _context.Transactions.Update(t);
            _context.SaveChanges();
            return new NoContentResult();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Transaction transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if(transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}