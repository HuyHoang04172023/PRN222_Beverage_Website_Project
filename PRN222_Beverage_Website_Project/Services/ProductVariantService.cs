using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _repository;
        public ProductVariantService()
        {
            _repository = new ProductVariantRepository();
        }

        public ProductVariant? GetProductVariantByProductVariantId(int productVariantId)
        {
            return _repository.GetProductVariantByProductVariantId(productVariantId);
        }
    }
}
