using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.EntityFrameworkCore;
using DotNetApiSample.Domain;
using DotNetApiSample.Database;

namespace DotNetApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=DotNetApiSample;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<TransactionDbContext>(options=>options.UseSqlServer(connectionString));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
        
        /// <summary>
        /// Seed database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static void Seed(TransactionDbContext context)
        {
            if(!context.Addresses.Any())
            {
                Address wAddress = new Address();
                wAddress.Line1 = "111 Work Place Circle";
                wAddress.City = "Tucson";
                wAddress.State = State.AZ;
                wAddress.ZipCode = "85747";
                context.Addresses.Add(wAddress);

                Address hAddress = new Address();
                hAddress.Line1 = "222 Home Lane";
                hAddress.City = "Dallas";
                hAddress.State = State.TX;
                hAddress.ZipCode = "75261";
                context.Addresses.Add(hAddress);
            }

            if(!context.People.Any())
            {
                
                for(int i = 0; i < 5; i++)
                {
                    Person p = new Person();
                    p.EmailAddress = String.Format("p{0}@gmail.com", i);
                    p.FirstName = String.Format("First{0}", i);
                    p.LastName = String.Format("First{0}", i);
                    p.PhoneNumber = String.Format("({0}{0}{0}) {0}{0}{0}-{0}{0}{0}{0}", i);
                    p.HomeAddress = context.Addresses.FirstOrDefault();
                    context.People.Add(p);
                }
            }

            if(!context.Transactions.Any())
            {
                Random r = new Random();
                for(int i = 10; i < 10; i++)
                {
                    Transaction t = new Transaction();

                    t.Amount = r.NextDouble();
                    t.Payee = context.People.Where(x => x.Id == 1).FirstOrDefault();
                    t.Payer = context.People.Where(x => x.Id == 2).FirstOrDefault();
                }
            }

            context.SaveChangesAsync();
        }
    }
}

