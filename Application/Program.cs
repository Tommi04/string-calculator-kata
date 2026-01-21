using Microsoft.Extensions.Logging.Abstractions;
using string_calculator_kata.Application.Services;

namespace string_calculator_kata.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AddService addService = new(new NullLoggerFactory().CreateLogger(""), new WebService());

            string? stringNumbers;
            if (args.Length > 0)
            {
                stringNumbers = args[0];
            }
            else
            {
                Console.WriteLine("Give me some number separated by ','");
                stringNumbers = Console.ReadLine();
            }

            while (!string.IsNullOrEmpty(stringNumbers))
            {
                Console.WriteLine("The sum is: " + addService.Add(stringNumbers));
                Console.WriteLine("Another input please!");
                stringNumbers = Console.ReadLine();
            }
        }
    }
}