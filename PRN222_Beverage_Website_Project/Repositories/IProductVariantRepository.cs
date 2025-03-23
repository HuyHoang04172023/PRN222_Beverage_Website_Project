using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IProductVariantRepository
    {
        public ProductVariant? GetProductVariantByProductVariantId(int productVariantId);
    }
}
