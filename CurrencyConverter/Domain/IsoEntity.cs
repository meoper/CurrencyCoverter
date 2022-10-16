namespace CurrencyConverter.Domain
{
    public sealed class IsoEntity
    {
        private IsoEntity(string isoValue)
        {
            IsoValue = isoValue;
        }

        public string IsoValue { get; set; }

        public static Result<IsoEntity> Create(string? isoCode)
        {
            if (string.IsNullOrWhiteSpace(isoCode))
            {
                return Result<IsoEntity>.CreateFailure("Failed to read ISO code.");
            }

            isoCode = isoCode.Trim();

            if (isoCode.Length != 3)
            {
                return Result<IsoEntity>.CreateFailure("ISO code should consist of 3 symbols.");
            }

            isoCode = isoCode.ToUpperInvariant();

            return new IsoEntity(isoCode).ToSuccess();
        }
    }
}
