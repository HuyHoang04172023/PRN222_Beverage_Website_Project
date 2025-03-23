using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly ProductVariantDAO _dao;
        public ProductVariantRepository()
        {
            _dao = new ProductVariantDAO();
        }

        public ProductVariant? GetProductVariantByProductVariantId(int productVariantId)
        {
            return _dao.GetProductVariantByProductVariantId(productVariantId);
        }
    }
}
