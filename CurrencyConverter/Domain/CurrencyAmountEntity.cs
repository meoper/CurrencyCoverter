namespace CurrencyConverter.Domain
{
    public sealed class CurrencyAmountEntity
    {
        private CurrencyAmountEntity(decimal amount)
        {
            Amount = amount;    
        }

        public decimal Amount { get; set; }

        public static Result<CurrencyAmountEntity> Create(string? amountString)
        {
            if (string.IsNullOrWhiteSpace(amountString))
            {
                return Result<CurrencyAmountEntity>.CreateFailure("Failed to read currency.");
            }

            if (!decimal.TryParse(amountString, out var amount))
            {
                return Result<CurrencyAmountEntity>.CreateFailure("Failed to parse currency decimal.");
            }

            if (amount <= 0)
            {
                return Result<CurrencyAmountEntity>.CreateFailure("Amount to convert cannot be equal or less than 0.");
            }

            return new CurrencyAmountEntity(amount).ToSuccess();
        }
    }
}
