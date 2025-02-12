using Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        // public DbSet<Motel> Motels { get; set; }
        // public DbSet<Suite> SuiteTypes { get; set; }
        // public DbSet<Reservation> Reservations { get; set; }

        // // Relacionamentos e configurações adicionais no banco
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // Relacionamento entre User e Reservation
        //     modelBuilder.Entity<User>()
        //         .HasMany(u => u.Reservations)
        //         .WithOne(r => r.User)
        //         .HasForeignKey(r => r.UserId);

        //     // Relacionamento entre Motel e Reservation
        //     modelBuilder.Entity<Motel>()
        //         .HasMany(m => m.Reservations)
        //         .WithOne(r => r.Motel)
        //         .HasForeignKey(r => r.MotelId);

        //     // Relacionamento entre Motel e SuiteType
        //     modelBuilder.Entity<Motel>()
        //         .HasMany(m => m.SuiteTypes)
        //         .WithOne(s => s.Motel)
        //         .HasForeignKey(s => s.MotelId);

        //     // Relacionamento entre SuiteType e Reservation
        //     modelBuilder.Entity<Suite>()
        //         .HasMany(s => s.Reservations)
        //         .WithOne(r => r.SuiteType)
        //         .HasForeignKey(r => r.SuiteTypeId);

        //     // Índice único para Email na tabela User
        //     modelBuilder.Entity<User>()
        //         .HasIndex(u => u.Email)
        //         .IsUnique();

        //     // Criação de índices para otimizar as consultas
        //     modelBuilder.Entity<Reservation>()
        //         .HasIndex(r => new { r.StartDate, r.EndDate, r.MotelId, r.SuiteTypeId });

        //     modelBuilder.Entity<Reservation>()
        //         .HasIndex(r => r.StartDate);
        //     modelBuilder.Entity<Reservation>()
        //         .HasIndex(r => r.EndDate);
        // }
    }
}