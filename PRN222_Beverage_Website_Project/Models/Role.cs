﻿using System;
using System.Collections.Generic;

namespace PRN222_Beverage_Website_Project.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
