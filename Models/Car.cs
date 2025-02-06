using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Car
{
    public int Id { get; set; }

    public string? LicensePlate { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public decimal? RentalPricePerDay { get; set; }

    public string? Status { get; set; }

    public int? Mileage { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
