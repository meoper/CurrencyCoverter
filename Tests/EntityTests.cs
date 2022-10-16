using CurrencyConverter.Domain;

namespace Tests
{
    public sealed class EntityTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("aaaa", false)]
        [InlineData("AAA", true)]
        [InlineData("aaa", true)]
        public void Iso_Entity_Should_Be_Validated_Correctly(string? input, bool expected)
        {
            var validation = IsoEntity.Create(input);

            Assert.Equal(validation.IsSuccess, expected);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("0", false)]
        [InlineData("a", false)]
        [InlineData("1", true)]
        [InlineData("1.111", true)]
        [InlineData("-1.1", false)]
        public void Currency_Amount_Entity_Should_Be_Validated_Correctly(string? input, bool expected)
        {
            var validation = CurrencyAmountEntity.Create(input);

            Assert.Equal(validation.IsSuccess, expected);
        }
    }
}