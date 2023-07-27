using System;
using System.Collections.Generic;

namespace TMS.Api.Models;

public partial class TotalNumberOfTicketsPerCategory
{
    public int? TicketCategoryId { get; set; }

    public int? TotalSoldTickets { get; set; }

    public decimal? TotalOrderAmount { get; set; }
}
