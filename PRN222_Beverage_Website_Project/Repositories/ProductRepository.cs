﻿using System.Reflection.Metadata.Ecma335;
using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _dao;
        public ProductRepository()
        {
            _dao = new ProductDAO();
        }
        public void AddProduct(Product product)
        {
            _dao.AddProduct(product);
        }

        public void AddProductVarriant(ProductVariant productVariant)
        {
            _dao.AddProductVarriant(productVariant);
        }

        public List<Product> GetProductsByShopId(int shopId)
        {
            return _dao.GetProductsByShopId(shopId);
        }
        public Product? GetProductByProductId(int productId)
        {
            return _dao.GetProductByProductId(productId);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            _dao.UpdateProduct(updatedProduct);
        }

        public bool UpdateProductVariant(ProductVariant updatedProductVariant)
        {
            return _dao.UpdateProductVariant(updatedProductVariant);
        }

        public void DeleteProductVariantByProductId(int productId)
        {
            _dao.DeleteProductVariantByProductId(productId);
        }

        public void DeleteProductByProductId(int productId)
        {
            _dao.DeleteProductByProductId(productId);
        }

        public List<Product>? GetProductsPending()
        {
            return _dao.GetProductsPending();
        }

        public void UpdateStatusProductByProductId(int productId, int idStatusProduct)
        {
            _dao.UpdateStatusProductByProductId(productId, idStatusProduct);
        }

        public void UpdateApprovedByOfProduct(Product updatedProduct)
        {
            _dao.UpdateApprovedByOfProduct(updatedProduct);
        }

        public List<Product> GetTopSellProducts()
        {
            return _dao.GetTopSellProducts();
        }

        public List<Product> GetLatestProducts()
        {
            return _dao.GetLatestProducts();
        }
    }
}
