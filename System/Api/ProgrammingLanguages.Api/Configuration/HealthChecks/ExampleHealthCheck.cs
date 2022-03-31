using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace ProgrammingLanguages.Api.Configuration.HealthChecks
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var assembly = Assembly.Load("ProgrammingLanguages.Api");
            var versionNumber = assembly.GetName().Version;

            return HealthCheckResult.Healthy(description: $"Build {versionNumber}");
        }
    }
}
