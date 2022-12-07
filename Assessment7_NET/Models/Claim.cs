using System;
using System.Collections.Generic;

namespace Assessment7_NET.Models;

public partial class Claim
{
    public int IdClaim { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? Date { get; set; }

    public int? IdVehicle { get; set; }

    public virtual Vehicle? IdVehicleNavigation { get; set; }
}
