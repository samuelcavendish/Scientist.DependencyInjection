using GitHub.Scientist.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Shouldly;
using Xunit;

namespace Scientist.DependencyInjection.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void Should_ResolveDifferentVersionWithinContext()
    {
        var container = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // Normal dependency
                services.AddSingleton(Mock.Of<IExperimentDependency>());
                services.AddExperimentContexts(scientist =>
                {
                    scientist.AddExperimentContext<ExperimentContext>(services =>
                    {
                        // Experiment dependency
                        return services.AddSingleton(Mock.Of<IExperimentDependency>());
                    });
                });
            }).Build();

        var mainContextItem = container.Services.GetRequiredService<IExperimentDependency>();
        var scienceContextItem = container.Services.GetRequiredService<ExperimentContext>().ExperimentDependency;
        scienceContextItem.ShouldNotBeNull();
        mainContextItem.ShouldNotBe(scienceContextItem);
    }
}
