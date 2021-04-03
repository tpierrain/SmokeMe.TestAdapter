using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SmokeMe.TestAdapter
{
    public class SmokeTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            PrintHeader(logger);

            discoverySink.SendTestCase(new TestCase("fullyQualifiedName", new Uri("executorUri"), "source"));
        }

        private void PrintHeader(IMessageLogger logger)
        {
            var platform = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            var versionAttribute = typeof(SmokeTestsVsRunner).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            logger.SendMessage(TestMessageLevel.Informational, $"SmokeMe.net VSTest Adapter v{versionAttribute.InformationalVersion} ({IntPtr.Size * 8}-bit {platform})");
        }
    }
}
