namespace MadeToEngageTest.Business.Services
{
    public interface IProductService
    {
        bool GetProductBySku(int productSku, out Product product);
    }
}