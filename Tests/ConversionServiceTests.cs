using CurrencyConverter.Domain;
using CurrencyConverter.Repositories;
using CurrencyConverter.Services;

namespace Tests
{
    public sealed class ConversionServiceTests
    {
        private readonly ConversionService conversionService;

        public ConversionServiceTests()
        {
            var currencyRepository = new CurrencyRepository(TestData.CurrencyDictionary);
            conversionService = new ConversionService(currencyRepository);
        }

        [Fact]
        public void Should_Return_Error_When_Converting_To_Same()
        {
            var iso1 = IsoEntity.Create("ABC").Value;
            var iso2 = IsoEntity.Create("ABC").Value;
            var amount = CurrencyAmountEntity.Create("1").Value;

            var result = conversionService.ConvertCurrency(iso1, iso2, amount);
            
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Should_Correctly_Convert_To_Money_Currency()
        {
            var iso1 = IsoEntity.Create("EUR").Value;

            // This line kinda couples the tests to the implementation... 
            var iso2 = IsoEntity.Create("DKK").Value;
            var amount = CurrencyAmountEntity.Create("1").Value;

            var result = conversionService.ConvertCurrency(iso1, iso2, amount);
            
            Assert.True(result.IsSuccess);
            Assert.Equal(1M, result.Value);
        }

        [Fact]
        public void Should_Correctly_Convert_From_Money_Currency()
        {
            var iso1 = IsoEntity.Create("DKK").Value;
            var iso2 = IsoEntity.Create("USD").Value;
            var amount = CurrencyAmountEntity.Create("100").Value;

            var result = conversionService.ConvertCurrency(iso1, iso2, amount);

            Assert.True(result.IsSuccess);
            Assert.Equal(50M, result.Value);
        }

        [Fact]
        public void Should_Correctly_Convert_From_And_To_Other_Currencies()
        {
            var iso1 = IsoEntity.Create("EUR").Value;
            var iso2 = IsoEntity.Create("USD").Value;
            var amount = CurrencyAmountEntity.Create("100").Value;

            var result = conversionService.ConvertCurrency(iso1, iso2, amount);

            Assert.True(result.IsSuccess);
            Assert.Equal(50M, result.Value);
        }
    }
}
