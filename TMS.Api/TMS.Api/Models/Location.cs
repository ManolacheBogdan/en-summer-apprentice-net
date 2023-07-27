using System;
using System.Collections.Generic;

namespace TMS.Api.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string LocationType { get; set; } = null!;

    public int? Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public override string ToString()
    {
        return LocationName;

    }
}
