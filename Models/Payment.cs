using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? RentalId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual Rental? Rental { get; set; }
}
