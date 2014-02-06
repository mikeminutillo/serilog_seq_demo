using Serilog;
using Serilog.Formatting.Raw;
using Serilog.Sinks.IOFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakGlass
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                .WriteTo.ColoredConsole()
.WriteTo.Sink(new FileSink(@"C:\Demo\Serilog\Dump.Log", new RawFormatter(), null))
                .Enrich.WithProperty("SERVER", Environment.MachineName)
                .Enrich.WithThreadId()

                    
.CreateLogger();

            var user = new
            {
                Logon = "minutilm",
                ScreenName = "Mike"
            };


            Log.Information("Payment Accepted for Cart {cartId} from User {user}", 
                12345, user);

            Log.Information("Hello, World!");

        }
    }
}
