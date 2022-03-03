using GitHub;
using GitHub.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Github.DependencyInjection.Tests;

internal class ExperimentContext : IExperimentContext
{
    public ExperimentContext(Dependency experimentDependency)
    {
        ExperimentDependency = experimentDependency;
    }

    public Dependency ExperimentDependency { get; }
}
