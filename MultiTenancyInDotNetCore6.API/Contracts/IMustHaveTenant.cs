namespace MultiTenancyInDotNetCore6.API.Contracts
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
