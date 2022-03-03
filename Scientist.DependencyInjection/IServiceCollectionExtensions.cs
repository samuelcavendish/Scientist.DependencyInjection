using Microsoft.Extensions.DependencyInjection;

namespace GitHub.Scientist.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddExperimentContexts(this IServiceCollection services, Action<ExperimentConfiguration> experimentConfiguration)
    {
        ArgumentNullException.ThrowIfNull(experimentConfiguration);

        experimentConfiguration(new ExperimentConfiguration { Services = services });
        return services;
    }
}
