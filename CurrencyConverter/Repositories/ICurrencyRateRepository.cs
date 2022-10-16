using CurrencyConverter.Domain;

namespace CurrencyConverter.Repositories
{
    public interface ICurrencyRateRepository
    {
        public Result<decimal> GetCurrencyValueInMoneyCurrency(IsoEntity iso);

        public Result<decimal> GetCurrencyValueInMoneyCurrencyNormalized(IsoEntity iso);

        public string GetMoneyCurrency();
    }
}
