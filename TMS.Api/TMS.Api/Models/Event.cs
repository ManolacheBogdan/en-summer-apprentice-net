﻿namespace TMS.Api.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int? LocationId { get; set; }

    public int? EventTypeId { get; set; }

    public string EventDescription { get; set; } = null!;

    public string EventName { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual EventType? EventType { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
}
