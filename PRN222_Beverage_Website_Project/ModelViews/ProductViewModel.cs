using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PRN222_Beverage_Website_Project.ModelViews
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Tên cửa hàng không được để trống.")]
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductImage { get; set; } = null!;

        public List<ProductVariantViewModel> ProductVariants { get; set; } = new List<ProductVariantViewModel>();
        public List<SelectListItem> ProductSizes { get; set; } = new List<SelectListItem>();
    }

    public class ProductVariantViewModel
    {
        public decimal ProductVariantPrice { get; set; }
        public int ProductSizeId { get; set; }
    }
}
