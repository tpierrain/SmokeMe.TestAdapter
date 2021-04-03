using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SmokeMe.TestAdapter
{
    /// <summary>
    /// Runner for SmokeMe smoke tests.
    /// </summary>
    public class SmokeTestsVsRunner : ITestDiscoverer, ITestExecutor
    {
        private readonly SmokeTestDiscoverer _smokeTestDiscoverer;
        private readonly SmokeTestExecutor _smokeTestExecutor;

        public SmokeTestsVsRunner() : this(new SmokeTestDiscoverer(), new SmokeTestExecutor())
        {
        }

        public SmokeTestsVsRunner(SmokeTestDiscoverer smokeTestDiscoverer, SmokeTestExecutor smokeTestExecutor)
        {
            _smokeTestDiscoverer = smokeTestDiscoverer;
            _smokeTestExecutor = smokeTestExecutor;
        }

        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            _smokeTestDiscoverer.DiscoverTests(sources, discoveryContext, logger, discoverySink);
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            _smokeTestExecutor.RunTests(tests, runContext, frameworkHandle);
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            _smokeTestExecutor.RunTests(sources, runContext, frameworkHandle);
        }

        public void Cancel()
        {
            _smokeTestExecutor.Cancel();
        }
    }
}
