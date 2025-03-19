using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public void AddProduct(Product product)
        {
            _repository.AddProduct(product);
        }

        public void AddProductVarriant(ProductVariant productVariant)
        {
            _repository.AddProductVarriant(productVariant);
        }
        public List<Product> GetProductsByShopId(int shopId)
        {
            return _repository.GetProductsByShopId(shopId);
        }

        public Product? GetProductByProductId(int productId)
        {
            return _repository.GetProductByProductId(productId);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            _repository.UpdateProduct(updatedProduct);
        }

        public bool UpdateProductVariant(ProductVariant updatedProductVariant)
        {
            return _repository.UpdateProductVariant(updatedProductVariant);
        }

        public void DeleteProductVariantByProductId(int productId)
        {
            _repository.DeleteProductVariantByProductId(productId) ;
        }
    }
}
