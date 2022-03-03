using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitHub.Scientist.DependencyInjection;

public class ExperimentConfiguration
{
    public IServiceCollection Services { get; init; } = null!;
    public ExperimentConfiguration AddExperimentContext<T>(Func<IServiceCollection, IServiceCollection> experimentConfiguration)
        where T : class, IExperimentContext
    {
        using var scientistHost = Host.CreateDefaultBuilder()
            .ConfigureServices(scientistServices =>
            {
                scientistServices.AddSingleton<T>();
                experimentConfiguration(scientistServices);
            }).Build();
        Services.AddSingleton<T>(scientistHost.Services.GetRequiredService<T>());
        return this;
    }
}
