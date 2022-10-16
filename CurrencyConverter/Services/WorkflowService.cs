using CurrencyConverter.Domain;

namespace CurrencyConverter.Services
{
    // Handles the flow of the application
    public sealed class WorkflowService 
    {
        private readonly ConversionService conversionService;

        public WorkflowService(ConversionService conversionService)
        {
            this.conversionService = conversionService;
        }

        public void Start()
        {
            SayHello();
            Execute();
        }

        private void Execute()
        {
            var result = RetryWrap(ReadAndConvert);
            OutputResult(result);
            GoAgainPrompt();
        }

        private void SayHello()
        {
            Console.WriteLine("Hello and welcome to demo currency converter");
            Console.WriteLine("This simple application is created by Skirmantas Navickas");
            Console.WriteLine("For the express purpose of used in the interview process");
            Console.WriteLine("DO NOT USE AS PRODUCTION CODE");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();
        }

        private void GoAgainPrompt()
        {
            Console.WriteLine("Would you like to convert another? (y/n)");
            var yes = ReadYesNo();

            if (yes)
            {
                Execute();
            }
            else
            {
                Quit();
            }
        }

        private void OutputResult(decimal result)
        {
            Console.WriteLine($"Converted amount - {result}");
        }

        private bool ReadYesNo()
        {
            var key = char.ToLowerInvariant(Console.ReadKey().KeyChar);

            Console.WriteLine();

            if (key == 'y')
            {
                return true;
            }

            return false;
        }

        private Result<decimal> ReadAndConvert()
        {
            var isoFrom = RetryWrap(ReadIso, "Input currency ISO FROM which we are converting: ");

            var isoTo = RetryWrap(ReadIso, "Input currency ISO TO which we are converting: ");

            var amount = RetryWrap(ReadAmount, "Input currency ISO TO which we are converting: ");

            return ConvertCurrencies(isoFrom, isoTo, amount);
        }

        private Result<decimal> ConvertCurrencies(IsoEntity from, IsoEntity to, CurrencyAmountEntity amount)
        {
            return conversionService.ConvertCurrency(from, to, amount);
        }

        private T RetryWrap<T>(Func<string, Result<T>> command, string message)
        {
            while (true)
            {
                // A lil bit not DRY, but oh well
                var result = command(message);
                if (!result.IsSuccess)
                {
                    Console.WriteLine($"Action failed, reason - {result.Message}");
                    Console.WriteLine("Would you like to try again? (y/n)");
                    var yes = ReadYesNo();

                    if (!yes)
                    {
                        Quit();
                    }

                    continue;
                }

                return result.Value;
            }
        }

        private T RetryWrap<T>(Func<Result<T>> command)
        {
            while (true)
            {
                var result = command();
                if (!result.IsSuccess)
                {
                    Console.WriteLine($"Action failed, reason - {result.Message}");
                    Console.WriteLine("Would you like to try again? (y/n)");
                    var yes = ReadYesNo();

                    if (!yes)
                    {
                        Quit();
                    }

                    continue;
                }

                return result.Value;
            }
        }

        private Result<IsoEntity> ReadIso(string message)
        {
            Console.WriteLine(message);
            var iso = Console.ReadLine();

            return IsoEntity.Create(iso);
        }

        private Result<CurrencyAmountEntity> ReadAmount(string message)
        {
            Console.WriteLine(message);
            var amount = Console.ReadLine();

            return CurrencyAmountEntity.Create(amount);
        }

        private void Quit()
        {
            Console.WriteLine("The application will finish its processes...");
            Environment.Exit(0);
        }
    }
}
