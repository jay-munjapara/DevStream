using DevStream.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DevStream.API.Services;

public class DeploymentWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DeploymentWorker> _logger;

    public DeploymentWorker(IServiceScopeFactory scopeFactory, ILogger<DeploymentWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DeploymentWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // 1) QUEUED -> RUNNING
                var queued = await db.Deployments
                    .Where(d => d.Status == "QUEUED")
                    .OrderBy(d => d.CreatedAtUtc)
                    .FirstOrDefaultAsync(stoppingToken);

                if (queued != null)
                {
                    queued.Status = "RUNNING";
                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Deployment {Id} moved to RUNNING", queued.Id);
                }

                // 2) RUNNING -> SUCCESS/FAILED
                var running = await db.Deployments
                    .Where(d => d.Status == "RUNNING")
                    .OrderBy(d => d.CreatedAtUtc)
                    .FirstOrDefaultAsync(stoppingToken);

                if (running != null)
                {
                    // Simulate outcome
                    var isSuccess = Random.Shared.Next(0, 10) < 8; // 80% success
                    running.Status = isSuccess ? "SUCCESS" : "FAILED";
                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Deployment {Id} completed with {Status}", running.Id, running.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeploymentWorker error");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}