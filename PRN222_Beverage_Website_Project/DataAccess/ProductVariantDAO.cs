using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ProductVariantDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ProductVariantDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public ProductVariant? GetProductVariantByProductVariantId(int productVariantId)
        {
            ProductVariant? productVariant = _context.ProductVariants
                .Include(pd => pd.Product)
                .Include(pd => pd.ProductSize)
                .FirstOrDefault(pd => pd.ProductVariantId == productVariantId);
            return productVariant;
        }
    }
}
