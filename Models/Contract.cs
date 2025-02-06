using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int? RentalId { get; set; }

    public string? ContractDetails { get; set; }

    public DateTime? SignedDate { get; set; }

    public virtual Rental? Rental { get; set; }
}
