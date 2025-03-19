﻿using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ProductDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ProductDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void AddProductVarriant(ProductVariant productVariant)
        {
            _context.ProductVariants.Add(productVariant);
            _context.SaveChanges();
        }

        public List<Product> GetProductsByShopId(int shopId)
        {
            return _context.Products
                .Where(p => p.ShopId == shopId)
                .OrderByDescending(p => p.ProductId)
                .Include(p => p.StatusProduct)
                .ToList();
        }

        public Product? GetProductByProductId(int productId)
        {
            return _context.Products
                .Include(p => p.StatusProduct)
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.ProductId == productId);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _context.Products.Find(updatedProduct.ProductId);

            if (existingProduct == null)
            {
                throw new Exception("Sản phẩm không tồn tại.");
            }

            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.ProductDescription = updatedProduct.ProductDescription;
            existingProduct.ProductImage = updatedProduct.ProductImage;

            _context.SaveChanges();
        }

        public bool UpdateProductVariant(ProductVariant updatedProductVariant)
        {
            var existingProductVariant = _context.ProductVariants.Find(updatedProductVariant.ProductVariantId);

            if (existingProductVariant == null)
            {
                return false; // Trả về false nếu không tìm thấy
            }

            existingProductVariant.ProductVariantPrice = updatedProductVariant.ProductVariantPrice;
            existingProductVariant.ProductSizeId = updatedProductVariant.ProductSizeId;

            _context.SaveChanges();
            return true; // Trả về true nếu cập nhật thành công
        }

        public void DeleteProductVariantByProductId(int productId)
        {
            var productVariants = _context.ProductVariants.Where(v => v.ProductId == productId).ToList();

            if (!productVariants.Any())
            {
                return;
            }

            _context.ProductVariants.RemoveRange(productVariants);

            _context.SaveChanges();
        }


    }
}
