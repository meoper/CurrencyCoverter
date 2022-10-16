using CurrencyConverter.Domain;

namespace CurrencyConverter.Repositories
{
    public sealed class CurrencyRepository : ICurrencyRateRepository
    {
        private const string MoneyCurrency = "DKK";

        private readonly Dictionary<string, decimal> _currencyDict;

        public CurrencyRepository(Dictionary<string, decimal> currencyDict)
        {
            _currencyDict = currencyDict;
        }

        public Result<decimal> GetCurrencyValueInMoneyCurrency(IsoEntity iso)
        {
            if (!_currencyDict.ContainsKey(iso.IsoValue))
            {
                return Result<decimal>.CreateFailure($"Failed to find the specified currency - {iso}");
            }

            return _currencyDict[iso.IsoValue].ToSuccess();
        }

        public Result<decimal> GetCurrencyValueInMoneyCurrencyNormalized(IsoEntity iso)
        {
            var result = GetCurrencyValueInMoneyCurrency(iso);
            if (!result.IsSuccess)
            {
                return result;
            }

            return (result.Value / 100).ToSuccess();
        }

        public string GetMoneyCurrency()
        {
            return MoneyCurrency;
        }
    }
}
