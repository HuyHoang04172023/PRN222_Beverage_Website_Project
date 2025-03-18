using System;
using System.Collections.Generic;

namespace PRN222_Beverage_Website_Project.Models;

public partial class ProductSize
{
    public int ProductSizeId { get; set; }

    public string? ProductSizeName { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
