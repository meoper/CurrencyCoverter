using CurrencyConverter.Domain;
using CurrencyConverter.Repositories;

namespace Tests
{
    public sealed class CurrencyRepositoryTests
    {
        private readonly CurrencyRepository currencyRepository;

        public CurrencyRepositoryTests()
        {
            currencyRepository = new CurrencyRepository(TestData.CurrencyDictionary);
        }


        [Fact]
        public void Should_Give_Error_When_Iso_Not_Found()
        {
            var result = currencyRepository.GetCurrencyValueInMoneyCurrency(IsoEntity.Create("ABC").Value);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Should_Get_Correct_Currency_Rate()
        {
            var result = currencyRepository.GetCurrencyValueInMoneyCurrency(IsoEntity.Create("USD").Value);

            Assert.True(result.IsSuccess);
            Assert.Equal(200M, result.Value);
        }

        [Fact]
        public void Should_Get_Correct_Currency_Rate_Normalized()
        {
            var result = currencyRepository.GetCurrencyValueInMoneyCurrencyNormalized(IsoEntity.Create("USD").Value);

            Assert.True(result.IsSuccess);
            Assert.Equal(2M, result.Value);
        }
    }
}