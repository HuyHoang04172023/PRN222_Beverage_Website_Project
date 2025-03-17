using System.ComponentModel.DataAnnotations;

namespace PRN222_Beverage_Website_Project.ModelViews
{
    public class ShopViewModel
    {
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Tên cửa hàng không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên cửa hàng không được vượt quá 50 ký tự.")]
        public string ShopName { get; set; } = null!;

        [Required(ErrorMessage = "Địa chỉ cửa hàng không được để trống.")]
        [StringLength(80, ErrorMessage = "Địa chỉ cửa hàng không được vượt quá 80 ký tự.")]
        public string ShopAddress { get; set; } = null!;

        [Required(ErrorMessage = "Hình ảnh cửa hàng không được để trống.")]
        [Url(ErrorMessage = "Hình ảnh cửa hàng phải là một URL hợp lệ.")]
        public string ShopImage { get; set; } = null!;

        [Required(ErrorMessage = "Mô tả cửa hàng không được để trống.")]
        [StringLength(200, ErrorMessage = "Mô tả cửa hàng không được vượt quá 200 ký tự.")]
        public string ShopDescription { get; set; } = null!;
    }
}
