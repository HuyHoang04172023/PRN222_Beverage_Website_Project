using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public void AddProductVarriant(ProductVariant productVariant);

        public List<Product> GetProductsByShopId(int shopId);
        public Product? GetProductByProductId(int productId);
        public void UpdateProduct(Product updatedProduct);
        public bool UpdateProductVariant(ProductVariant updatedProductVariant);
        public void DeleteProductVariantByProductId(int productId);
        public void DeleteProductByProductId(int productId);
        public List<Product>? GetProductsPending();
        public void UpdateStatusProductByProductId(int productId, int idStatusProduct);
        public void UpdateApprovedByOfProduct(Product updatedProduct);
    }
}
