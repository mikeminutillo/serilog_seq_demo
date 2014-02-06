using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seq;
using System.Threading;

namespace CustomerSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.Seq("http://localhost:5341")
                            .CreateLogger();

            var customer = new Customer(args[0]);
            customer.Go();
        }
    }

    class Product
    {
        public string Sku { get; set; }
        public string Name { get; set; }

        public static Product[] Products = new[]
        {
            new Product { Sku = "PROVS10", Name = "Professional Visual Studio 2010" }, 
            new Product { Sku = "LEGACY", Name = "Working Effectively with Legacy Code" }, 
            new Product { Sku = "NUTSHELL", Name = "C# 5 in a Nutshell" }, 
            new Product { Sku = "DDD", Name = "Domain Driven Design" }, 
            new Product { Sku = "HEADFIRST", Name = "Head First Design Patterns" }, 
            new Product { Sku = "COMPLETE", Name = "Code Complete"}, 
            new Product { Sku = "REFACTORING", Name = "Refactoring"}, 
            new Product { Sku = "NOTHINK", Name = "Don't Make Me Think" }, 
            new Product { Sku = "AGILEART", Name = "The Art of Agile Development" }, 
            new Product { Sku = "PRAGMATIC", Name = "The Pragmatic Programmer"}, 
            new Product { Sku = "SHIPIT", Name = "Ship It!"}, 
            new Product { Sku = "CODE4FUN", Name = "Coding 4 Fun"}, 
            new Product { Sku = "TESTART", Name = "The Art of Unit Testing"}

        };
    }

    class Customer
    {
        private readonly string _name;

        public Customer(string name)
	    {
            _name = name;
	    }

        public void Go()
        {
            var random = new Random();
            while(true)
            {
                do
                {
                    Thread.Sleep(random.Next(1000, 2000));
                    if (random.NextDouble() > 0.5)
                    {
                        Log.Information("{customer} updated cart - added {@product}", _name, Product.Products.OrderBy(_ => Guid.NewGuid()).First());
                    }
                    else
                    {
                        Log.Information("{customer} updated cart - removed {@product}", _name, Product.Products.OrderBy(_ => Guid.NewGuid()).First());
                    }
                } while (random.NextDouble() > 0.1);
                if(random.NextDouble() < 0.1)
                {
                    Log.Error("Payment Gateway error when checking out for {customer}", _name);
                }
                else
                {
                    Log.Information("Payment Accepted during checkout for {customer}", _name);
                }
            }
        }
    }
}
