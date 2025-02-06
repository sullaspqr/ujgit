using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int? CarId { get; set; }

    public string? Description { get; set; }

    public DateTime? MaintenanceDate { get; set; }

    public decimal? Cost { get; set; }

    public virtual Car? Car { get; set; }
}
