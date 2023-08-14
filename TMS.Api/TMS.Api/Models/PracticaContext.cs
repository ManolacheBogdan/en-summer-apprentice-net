using Microsoft.EntityFrameworkCore;

namespace TMS.Api.Models;

public partial class PracticaContext : DbContext
{
    public PracticaContext()
    {
    }

    public PracticaContext(DbContextOptions<PracticaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TotalNumberOfTicketsPerCategory> TotalNumberOfTicketsPerCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PPO2NO5\\SQLEXPRESS;Initial Catalog=Practica;Persist Security Info=True;User ID=Bogdan;Password=12345;TrustServerCertificate=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__B611CB9D215BD2E2");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E6164BF679A8C").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("customerName");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__event__2DC7BD6930FBAD9A");

            entity.ToTable("event");

            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("eventDescription");
            entity.Property(e => e.EventName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("eventName");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.LocationId).HasColumnName("locationID");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_eventType");

            entity.HasOne(d => d.Location).WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_location");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__eventTyp__04ACC49DBCDA044D");

            entity.ToTable("eventType");

            entity.HasIndex(e => e.EventTypeName, "UQ__eventTyp__F1C27EB11B4EE3C9").IsUnique();

            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("eventTypeName");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__location__30646B0E285DC91D");

            entity.ToTable("location");

            entity.Property(e => e.LocationId).HasColumnName("locationID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.LocationName)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("locationName");
            entity.Property(e => e.LocationType)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("locationType");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__0809337DDEDC9BB7");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.OrderedAt).HasColumnType("datetime");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticketCategoryID");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalPrice");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_customer");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__ticket_c__56F2E67A8B8977A1");

            entity.ToTable("ticket_category");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticketCategoryID");
            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TicketDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ticketDescription");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_eventID");
        });

        modelBuilder.Entity<TotalNumberOfTicketsPerCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("total_number_of_tickets_per_category");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticketCategoryID");
            entity.Property(e => e.TotalOrderAmount)
                .HasColumnType("decimal(38, 0)")
                .HasColumnName("total_order_amount");
            entity.Property(e => e.TotalSoldTickets).HasColumnName("total_sold_tickets");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
