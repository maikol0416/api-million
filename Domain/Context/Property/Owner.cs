using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities;

public partial class Owner :BaseEntitySQLServer
{
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public byte[]? Photo { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
