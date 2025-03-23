using System.ComponentModel.DataAnnotations;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.ModelViews
{
    public class ItemCart
    {
        public int ProductVariantId { get; set; }  // Chỉ lưu ID thay vì cả đối tượng ProductVariant
        public string ProductName { get; set; }    // Lấy từ Product
        public string ProductImage { get; set; }   // Lấy từ Product
        public decimal ProductVariantPrice { get; set; }  // Lấy từ ProductVariant
        public string ProductSizeName { get; set; } // Lấy từ ProductSize
        public int Quantity { get; set; }          // Số lượng sản phẩm trong giỏ
    }
}
