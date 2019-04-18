namespace MadeToEngageTest.Business.Services
{
    public interface IAuthService
    {
        bool IsAuthorized(int userId);
    }
}