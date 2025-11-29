namespace EmailMonitoringService.Configuration;

public class ServiceSettings
{
    public string Name { get; set; } = string.Empty;
    public required string Version { get; set; }
    public int CheckInterval { get; set; }
    public bool CanRunService { get; set; }
}