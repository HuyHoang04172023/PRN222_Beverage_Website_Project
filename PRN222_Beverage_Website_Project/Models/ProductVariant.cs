using System;
using System.Collections.Generic;

namespace PRN222_Beverage_Website_Project.Models;

public partial class ProductVariant
{
    public int ProductVariantId { get; set; }

    public decimal ProductVariantPrice { get; set; }

    public int ProductSizeId { get; set; }

    public int ProductId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ProductSize ProductSize { get; set; } = null!;
}
