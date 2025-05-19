using Microsoft.EntityFrameworkCore;
using Domain.Entity;

namespace Repository.Data
{
    public partial class JujuTestContext : DbContext
    {
        public JujuTestContext()
        {
        }

        public JujuTestContext(DbContextOptions<JujuTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Post> Post { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(c => c.Posts)
                .WithOne(p => p.Customer) 
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Body).HasMaxLength(100);

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);
            });
        }
    }
}
