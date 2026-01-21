using Microsoft.Extensions.Logging;
using string_calculator_kata.Application.Interfaces;

namespace string_calculator_kata.Application.Services
{
    public class AddService(ILogger logger, IWebservice webservice)
    {
        private readonly ILogger _logger = logger;
        private readonly IWebservice _webservice = webservice;

        public int? Add(string numbers)
        {
            try
            {
                if (string.IsNullOrEmpty(numbers)) return 0;

                char[] delimiters = [',', '\n'];
                char delimiter = GetDelimiter(numbers);
                if (delimiter != '\0')
                {
                    delimiters = [delimiter];

                    string[] stringsNumbers = GetStringNumbersBySeparator('\n', numbers);
                    if (stringsNumbers.Length > 1) { numbers = stringsNumbers[1]; }
                }

                List<int> intNumbers = numbers.Split(delimiters)
                                                .Select(sn => int.Parse(sn))
                                                .Where(n => n <= 1000)
                                                .ToList();
                if (intNumbers.Any(n => n < 0))
                {
                    throw new Exception("negatives not allowed: " + intNumbers.Where(n => n < 0).Select(n => n.ToString()).Aggregate((a, b) => a + ", " + b));
                }

                int sum = intNumbers.Sum();
                _logger.LogInformation(sum.ToString());
                return sum;
            }
            catch (Exception ex)
            {
                _webservice.NotifyLoggingFailure(ex.Message);
                return null;
            }
        }

        public static char GetDelimiter(string numbers)
        {
            if (numbers.Length > 1 && numbers.Substring(0, 2) == "//")
            {
                return numbers.Substring(2, 1).Cast<char>().Single();
            }
            return '\0';
        }

        public static string[] GetStringNumbersBySeparator(char separator, string numbers)
        {
            string[] stringNumbersArray = numbers.Split(separator);
            return stringNumbersArray;
        }
    }
}