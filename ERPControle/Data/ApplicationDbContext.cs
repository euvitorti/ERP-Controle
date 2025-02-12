using Microsoft.EntityFrameworkCore;
using Models.People;
using Models.Transactions;
using Models.Users;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired();

                entity.Property(p => p.Age)
                      .IsRequired();

                // Relação: Uma Person possui muitas Transactions com deleção em cascata
                entity.HasMany(p => p.Transactions)
                      .WithOne(t => t.Person)
                      .HasForeignKey(t => t.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Description)
                      .IsRequired();

                entity.Property(t => t.Value)
                      .IsRequired();

                entity.Property(t => t.Type)
                      .IsRequired();
            });
        }
    }
}
