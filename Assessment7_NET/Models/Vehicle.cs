using System;
using System.Collections.Generic;

namespace Assessment7_NET.Models;

public partial class Vehicle
{
    public int IdVehicle { get; set; }

    public string? Brand { get; set; }

    public string? Vin { get; set; }

    public string? Color { get; set; }

    public DateTime? Year { get; set; }

    public int? IdOwner { get; set; }

    public virtual ICollection<Claim> Claims { get; } = new List<Claim>();

    public virtual Owner? IdOwnerNavigation { get; set; }
}
