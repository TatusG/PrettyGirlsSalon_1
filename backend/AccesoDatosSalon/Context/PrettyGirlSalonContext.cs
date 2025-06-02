using AccesoDatosSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatosSalon.Context;

public partial class PrettyGirlSalonContext : DbContext
{
    public PrettyGirlSalonContext()
    {
    }

    public PrettyGirlSalonContext(DbContextOptions<PrettyGirlSalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }

    public virtual DbSet<Stylist> Stylists { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O76F4T4\\SQLEXPRESS;Database=PrettyGirl_Salon;Trust Server Certificate=true;User Id=Tatiana;Password=123456;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83F7579F4A2");

            entity.ToTable("appointment");

            entity.HasIndex(e => e.AppointmentDateTime, "idx_appointment_datetime");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentDateTime)
                .HasColumnType("datetime")
                .HasColumnName("appointmentDateTime");
            entity.Property(e => e.AppointmentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("appointmentStatus");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.StylistUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stylistUser");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__clien__4316F928");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__servi__440B1D61");

            entity.HasOne(d => d.StylistUserNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StylistUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__styli__44FF419A");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__client__3213E83F9C767A5E");

            entity.ToTable("client");

            entity.HasIndex(e => e.Dni, "UQ__client__D87608A743C395D0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registrationDate");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__review__3213E83F9E9FDF2D");

            entity.ToTable("review");

            entity.HasIndex(e => e.ReviewDate, "idx_review_date");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");
            entity.Property(e => e.RatingValue).HasColumnName("ratingValue");
            entity.Property(e => e.Response)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("response");
            entity.Property(e => e.ReviewComment)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("reviewComment");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("reviewDate");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__review__appointm__48CFD27E");
        });

        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__serviceR__3213E83FB067CABE");

            entity.ToTable("serviceRequest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DurationMinutes).HasColumnName("durationMinutes");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("isAvailable");
            entity.Property(e => e.ServiceDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("serviceDescription");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("serviceName");
            entity.Property(e => e.ServicePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("servicePrice");
        });

        modelBuilder.Entity<Stylist>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__stylist__66DCF95DBF5B2AEE");

            entity.ToTable("stylist");

            entity.HasIndex(e => e.Email, "UQ__stylist__AB6E6164A761E98D").IsUnique();

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("specialty");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
