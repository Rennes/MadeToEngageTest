namespace MadeToEngageTest.Business.Services
{
    public interface IOrganisationService
    {
        bool GetOgranisationalDiscountForUser(int userId, out decimal discount);
    }
}