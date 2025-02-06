using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Rental
{
    public int RentalId { get; set; }

    public int? CustomerId { get; set; }

    public int? CarId { get; set; }

    public DateTime? RentalDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public virtual Car? Car { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
