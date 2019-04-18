namespace MadeToEngageTest.Business.Services
{
    public interface IPricingService
    {
        bool GetCustomerPriceForUser(int userId, int productSku, out decimal customerPrice);
    }
}