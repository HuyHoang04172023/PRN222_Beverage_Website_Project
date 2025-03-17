using System;
using System.Collections.Generic;

namespace PRN222_Beverage_Website_Project.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public string ShopAddress { get; set; } = null!;

    public string ShopImage { get; set; } = null!;

    public string ShopDescription { get; set; } = null!;

    public int StatusShopId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } 

    public int? ApprovedBy { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<ShopReview> ShopReviews { get; set; } = new List<ShopReview>();

    public virtual StatusShop StatusShop { get; set; } = null!;
}
