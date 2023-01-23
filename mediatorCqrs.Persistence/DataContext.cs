using mediatorCqrs.Domain;
using mediatorCqrs.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace mediatorCqrs.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) 
        {
            
        }
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        //    {
        //        entry.Entity.DateTime = DateTime.Now;
        //        if(entry.State == EntityState.Added) 
        //        {
        //            entry.Entity.DateTime = DateTime.Now;
        //        }
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        public DbSet<User> User { get; set; }

    }
}
