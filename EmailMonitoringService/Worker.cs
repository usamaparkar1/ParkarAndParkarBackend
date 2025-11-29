using Microsoft.Extensions.Options;
using EmailMonitoringService.Configuration;

namespace EmailMonitoringService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceSettings _serviceSettings;

    public Worker(
        ILogger<Worker> logger,
        IOptions<ServiceSettings> serviceSettings
    )
    {
        _logger = logger;
        _serviceSettings = serviceSettings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(_serviceSettings.CheckIntervalInMs, stoppingToken);
        }
    }
}