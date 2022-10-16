using CurrencyConverter.Domain;
using CurrencyConverter.Repositories;

namespace CurrencyConverter.Services
{
    public sealed class ConversionService
    {
        private readonly ICurrencyRateRepository currencyRateRepository;

        public ConversionService(ICurrencyRateRepository currencyRateRepository)
        {
            this.currencyRateRepository = currencyRateRepository;
        }

        public Result<decimal> ConvertCurrency(IsoEntity from, IsoEntity to, CurrencyAmountEntity currencyAmount)
        {
            if (from.IsoValue.Equals(to.IsoValue))
            {
                return Result<decimal>.CreateFailure("ISO codes cannot be the same for conversion.");
            }


            if (from.IsoValue == currencyRateRepository.GetMoneyCurrency())
            {
                return ConvertFromMoneyCurrency(to, currencyAmount.Amount);
            }

            if (to.IsoValue == currencyRateRepository.GetMoneyCurrency())
            {
                return ConvertToMoneyCurrency(from, currencyAmount.Amount);
            }

            return ConvertOtherCurrencies(from, to, currencyAmount.Amount);
        }

        private Result<decimal> ConvertFromMoneyCurrency(IsoEntity to, decimal currencyAmount)
        {
            var currencyValueResult = currencyRateRepository.GetCurrencyValueInMoneyCurrencyNormalized(to);
            if (!currencyValueResult.IsSuccess)
            {
                return currencyValueResult;
            }

            var result = currencyAmount / currencyValueResult.Value;
            return result.ToSuccess();
        }

        private Result<decimal> ConvertToMoneyCurrency(IsoEntity from, decimal currencyAmount)
        {
            var currencyValueResult = currencyRateRepository.GetCurrencyValueInMoneyCurrencyNormalized(from);
            if (!currencyValueResult.IsSuccess)
            {
                return currencyValueResult;
            }

            var result = currencyValueResult.Value * currencyAmount;
            return result.ToSuccess();
        }

        // Used for currency conversions where neither from/to is money currency
        private Result<decimal> ConvertOtherCurrencies(IsoEntity from, IsoEntity to, decimal currencyAmount)
        {
            var currencyFromValueResult = currencyRateRepository.GetCurrencyValueInMoneyCurrency(from);
            if (!currencyFromValueResult.IsSuccess)
            {
                return currencyFromValueResult;
            }

            var currencyToValueResult = currencyRateRepository.GetCurrencyValueInMoneyCurrency(to);
            if (!currencyToValueResult.IsSuccess)
            {
                return currencyToValueResult;
            }

            var result = currencyFromValueResult.Value / currencyToValueResult.Value * currencyAmount;
            return result.ToSuccess();
        }
    }
}
