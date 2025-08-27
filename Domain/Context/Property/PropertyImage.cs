using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities;

public partial class PropertyImage: BaseEntitySQLServer
{
    public int IdProperty { get; set; }

    public byte[] FileContent { get; set; } = null!;

    public bool? Enabled { get; set; }

    public virtual Property IdPropertyNavigation { get; set; } = null!;
}
