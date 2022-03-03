using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitHub.DependencyInjection;

public class ExperimentConfiguration
{
    private readonly IServiceCollection _services;

    public ExperimentConfiguration(IServiceCollection services)
    {
        _services = services;
    }

    public ExperimentConfiguration AddExperimentContext<T>(Func<IServiceCollection, IServiceCollection> experimentConfiguration)
        where T : class, IExperimentContext
    {
        using var scientistHost = Host.CreateDefaultBuilder()
            .ConfigureServices(scientistServices =>
            {
                scientistServices.AddSingleton<T>();
                experimentConfiguration(scientistServices);
            }).Build();
        _services.AddSingleton(scientistHost.Services.GetRequiredService<T>());
        return this;
    }
}
