using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IProductService
    {
        public void AddProduct(Product product);
        public void AddProductVarriant(ProductVariant productVariant);
        public List<Product> GetProductsByShopId(int shopId);
        public Product? GetProductByProductId(int productId);
        public void UpdateProduct(Product updatedProduct);
        public bool UpdateProductVariant(ProductVariant updatedProductVariant);
        public void DeleteProductVariantByProductId(int productId);
        public void DeleteProductByProductId(int productId);
    }
}
