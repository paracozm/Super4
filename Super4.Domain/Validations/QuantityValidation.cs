namespace Super4.Domain.Validations
{
    public static class QuantityValidation
    {
        public static async Task<bool> IsQuantityValid(int quantity)
        {
            return quantity >= 0;
        }
        
        
    }
}
