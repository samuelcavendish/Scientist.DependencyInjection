using GitHub.Scientist.DependencyInjection;

namespace Scientist.DependencyInjection.Tests;

internal class ExperimentContext : IExperimentContext
{
    public ExperimentContext(IExperimentDependency experimentDependency)
    {
        ExperimentDependency = experimentDependency;
    }

    public IExperimentDependency ExperimentDependency { get; init; }
}
