using GitHub;
using GitHub.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Shouldly;
using Xunit;

namespace Github.DependencyInjection.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void Should_ResolveDifferentVersionWithinContext()
    {
        var container = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // Normal dependency
                services.AddSingleton(new Dependency());
                services.AddExperimentContexts(scientist =>
                {
                    scientist.AddExperimentContext<ExperimentContext>(services =>
                    {
                        // Experiment dependency
                        return services.AddSingleton(new Dependency());
                    });
                });
            }).Build();


        var mainContextItem = container.Services.GetRequiredService<Dependency>();
        var scienceContextItem = container.Services.GetRequiredService<ExperimentContext>().ExperimentDependency;
        mainContextItem.GetValue().ShouldBe(mainContextItem.GetValue());
        mainContextItem.GetValue().ShouldNotBe(scienceContextItem.GetValue());
    }

    [Fact]
    public void Should_RunExperiment()
    {
        var originalDependency = new Mock<Dependency>();
        var experimentDependency = new Mock<Dependency>();
        var container = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IScientist, Scientist>();
                services.AddTransient<IResultPublisher, Publisher>();
                // Normal dependency
                services.AddSingleton(originalDependency.Object);
                services.AddSingleton<Experiment>();
                services.AddExperimentContexts(scientist =>
                {
                    scientist.AddExperimentContext<ExperimentContext>(services =>
                    {
                        // Experiment dependency
                        return services.AddSingleton(experimentDependency.Object);
                    });
                });
            }).Build();

        var experiment = container.Services.GetRequiredService<Experiment>();
        _ = experiment.GetValue();

        originalDependency.Verify(x => x.GetValue(), Times.Once());
        experimentDependency.Verify(x => x.GetValue(), Times.Once());
    }
}
