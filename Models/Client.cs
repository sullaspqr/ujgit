using System;
using System.Collections.Generic;

namespace ProjektNeveBackend.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
