using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PRN222_Beverage_Website_Project.ModelViews
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên sản phẩm phải từ 3 đến 100 ký tự.")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống.")]
        [StringLength(255, ErrorMessage = "Mô tả sản phẩm không được vượt quá 255 ký tự.")]
        public string ProductDescription { get; set; } = null!;

        [Required(ErrorMessage = "Hình ảnh sản phẩm không được để trống.")]
        public string ProductImage { get; set; } = null!;

        public List<ProductVariantViewModel> ProductVariants { get; set; } = new List<ProductVariantViewModel>();
        public List<SelectListItem> ProductSizes { get; set; } = new List<SelectListItem>();
    }

    public class ProductVariantViewModel
    {
        public int ProductVariantId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
        public decimal ProductVariantPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn kích thước sản phẩm.")]
        public int ProductSizeId { get; set; }
    }
}
