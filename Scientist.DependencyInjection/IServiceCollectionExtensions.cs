using Microsoft.Extensions.DependencyInjection;

namespace GitHub.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddExperimentContexts(this IServiceCollection services, Action<ExperimentConfiguration> experimentConfiguration)
    {
        ArgumentNullException.ThrowIfNull(experimentConfiguration);

        experimentConfiguration(new ExperimentConfiguration(services));
        return services;
    }
}
