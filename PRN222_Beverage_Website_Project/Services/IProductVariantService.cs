using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IProductVariantService
    {
        public ProductVariant? GetProductVariantByProductVariantId(int productVariantId);
    }
}
