using GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.DependencyInjection.Tests
{
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
}
