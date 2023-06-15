
using MultiTenancyInDotNetCore6.API.Models;

namespace MultiTenancyInDotNetCore6.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public string TenantId { get; set; }
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(DbContextOptions options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            TenantId = _tenantService.GetCurrentTenant()?.TId;
        }

        public DbSet<Product> Products { get; set; }
        // update here because any retrive 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this for retrive all data so add global Query Filter for or in select filter by TenantId
            modelBuilder.Entity<Product>().HasQueryFilter(e => e.TenantId == TenantId);
            base.OnModelCreating(modelBuilder);
        }
        // update  here beacase any add  in system
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.TenantId = TenantId;
            } 
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService.GetConnectionString();

            if (!string.IsNullOrWhiteSpace(tenantConnectionString))
            {
                var dbProvider = _tenantService.GetDatabaseProvider();

                if (dbProvider?.ToLower() == "mssql")
                {
                    optionsBuilder.UseSqlServer(tenantConnectionString);
                }
            }
        }
    }
}
