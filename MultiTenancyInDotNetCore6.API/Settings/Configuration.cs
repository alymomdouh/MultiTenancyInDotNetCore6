namespace MultiTenancyInDotNetCore6.API.Settings
{
    public class Configuration
    {
        public string DBProvider { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}
