using System;
using System.Collections.Generic;

namespace PRN222_Beverage_Website_Project.Models;

public partial class StatusShop
{
    public int StatusShopId { get; set; }

    public string StatusShopName { get; set; } = null!;

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
