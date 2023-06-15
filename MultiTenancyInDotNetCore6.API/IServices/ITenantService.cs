namespace MultiTenancyInDotNetCore6.API.IServices
{
    public interface ITenantService
    {
        string GetDatabaseProvider();
        string GetConnectionString();
        Tenant? GetCurrentTenant();
    }
}
