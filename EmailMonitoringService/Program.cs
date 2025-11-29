using EmailMonitoringService;
using EmailMonitoringService.Configuration;

#region Main
var builder = Host.CreateApplicationBuilder(args);

// Step 1: Configure environment-specific settings
ConfigureEnvironmentSettings(builder);

// Step 2: Check If service can run
if (!CanRunEmailMonitoringService(builder.Configuration))
{
    Console.WriteLine("Exiting application as the EmailMonitoringService is disabled.");
    return;
}

// Step 3: Register Hosted Service
RegisterHostedService(builder);

// Final Step: Build and Run the Host
BuildAndRunHost(builder);
#endregion

#region Methods
// Step 1: Configure Environment-Specific Settings
static void ConfigureEnvironmentSettings(HostApplicationBuilder builder)
{
    builder.Configuration
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}

// Step 2: Check if Email Monitoring Service can run
static bool CanRunEmailMonitoringService(IConfiguration configuration)
{
    IConfigurationSection section = configuration.GetSection("Service");

    // Check if the section exists or is empty
    if (!section.Exists())
    {
        Console.WriteLine("[EmailMonitoringService] Section 'Service' is missing from appsettings.json.");
        return false;
    }

    // Try binding safely
    ServiceSettings? settings;
    try
    {
        settings = section.Get<ServiceSettings>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[EmailMonitoringService] Failed to bind ServiceSettings: {ex.Message}");
        return false;
    }

    if (settings == null)
    {
        Console.WriteLine("[EmailMonitoringService] 'Service' section exists but could not be mapped.");
        return false;
    }

    // Check CanRunService key existence
    // NOTE: Nullable<bool>? used to detect omitted key
    bool? canRun = settings.CanRunService;

    if (canRun is null)
    {
        Console.WriteLine("[EmailMonitoringService] Missing key: 'CanRunService'. Defaulting to disabled.");
        return false;
    }

    return settings.CanRunService;
}

// Step 3: Register Hosted Service
static void RegisterHostedService(HostApplicationBuilder builder)
{
    builder.Services.AddHostedService<Worker>();
}

// Final Step: Build and Run the Host
static void BuildAndRunHost(HostApplicationBuilder builder)
{
    var host = builder.Build();
    host.Run();
}
#endregion