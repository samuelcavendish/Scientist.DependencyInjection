using GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.DependencyInjection.Tests
{
    public class Publisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
            return Task.CompletedTask;
        }
    }
}
