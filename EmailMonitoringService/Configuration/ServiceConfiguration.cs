namespace EmailMonitoringService.Configuration;

public class ServiceSettings
{
    public string Name { get; set; } = string.Empty;
    public required string Version { get; set; }
    public int CheckIntervalInMs { get; set; } = 3000; // Default to 3000 ms
    public bool CanRunService { get; set; }
}