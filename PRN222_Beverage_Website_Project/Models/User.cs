using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222_Beverage_Website_Project.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Tên người dùng là bắt buộc.")]
    [StringLength(20, ErrorMessage = "Tên người dùng không được vượt quá 20 ký tự.")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email là bắt buộc.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Shop> ShopApprovedByNavigations { get; set; } = new List<Shop>();

    public virtual ICollection<Shop> ShopCreatedByNavigations { get; set; } = new List<Shop>();

    public virtual ICollection<ShopReview> ShopReviews { get; set; } = new List<ShopReview>();
}
