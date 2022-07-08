using System.Reflection;
using DbUp;
using System;
using Microsoft.Extensions.Configuration;

namespace DbMigration
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                string connectionString = String.Empty;

                connectionString = string.Join(" ", args);

                Console.WriteLine($@"Connecting to : { connectionString } !");
                var upgrader = DeployChanges.To
                                    .SqlDatabase(connectionString)
                                    .JournalToSqlTable("dbo", "SchemaVersions")
                                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                    .LogToConsole()
                                    .WithExecutionTimeout(TimeSpan.FromSeconds(360))
                                    .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
                    Console.ReadLine();
                    return -1;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"Error-StackTrace : {ex.StackTrace}");
                Console.WriteLine($@"Error-Message : {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
                return -1;
            }
        }
    }
}
