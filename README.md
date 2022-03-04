# Scientist.DependencyInjection

Scientist.DependencyInjection is a library that allows you to register an ExperimentContext that contains dependencies for use with your experiment. 

## Registering contexts

```
var container = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services
            .AddTransient<IScientist, Scientist>()
            .AddTransient<IResultPublisher, Publisher>();
            // Normal dependency
            .AddSingleton(originalDependency);
            .AddExperimentContexts(scientist =>
            {
                scientist.AddExperimentContext<ExperimentContext>(services =>          
                {
                    // Experiment dependency
                    return services.AddSingleton(experimentDependency);
                });
            });
    }).Build();
```

When adding multiple contexts, calls to AddExperimentContext can be chained. In the snippit, we're adding the ExperimentContext (which inherits IExperimentContext) with the singleton experiment dependency. Here's what the ExperimentContext looks like

```
internal class ExperimentContext : IExperimentContext
{
    public ExperimentContext(Dependency experimentDependency)
    {
        ExperimentDependency = experimentDependency;
    }

    public Dependency ExperimentDependency { get; }
}
```

The experiment context simply exposes the dependency for use in experiments

## Using the context

The context can be retrieved from DI and used anywhere dependencies can be resolved. If you want to encapsulate your experiment in a dedicated class you can do so by registering  IScientist & IResultPublisher as show above & a dedicated experiment class which can be registered as a singleton, e.g.

```
internal class Experiment
{
    private readonly IScientist _scientist;
    private readonly Dependency _experimentDependency;
    private readonly ExperimentContext _experimentContext;

    public Experiment(IScientist scientist, Dependency experimentDependency, ExperimentContext experimentContext)
    {
        _scientist = scientist;
        _experimentDependency = experimentDependency;
        _experimentContext = experimentContext;
    }

    public Guid GetValue()
    {
        return _scientist.Experiment<Guid>("Experiment Name", experiment =>
        {
            experiment.Use(() => _experimentDependency.GetValue()); // old way
            experiment.Try(() => _experimentContext.ExperimentDependency.GetValue()); // new way
        });
    }
}
```

This example scenario can be seen in the test project.
