using System;

namespace Github.DependencyInjection.Tests;

public class Dependency
{
    public Guid Value { get; } = Guid.NewGuid();
    public virtual Guid GetValue()
    {
        return Value;
    }
}
