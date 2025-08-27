using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities;

public partial class Property: BaseEntitySQLServer
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? CodeInternal { get; set; }

    public int? Year { get; set; }

    public int IdOwner { get; set; }
    public virtual Owner IdOwnerNavigation { get; set; } = null!;

    public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    public virtual ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
}
