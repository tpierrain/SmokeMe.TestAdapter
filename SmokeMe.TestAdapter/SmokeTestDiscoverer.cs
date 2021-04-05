#define LAUNCHDEBUGGER

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SmokeMe.TestAdapter
{
    [DefaultExecutorUri(Constants.ExecutorUri)]

    public class SmokeTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
#if LAUNCHDEBUGGER
            if (!Debugger.IsAttached)
                Debugger.Launch();
#endif

            PrintHeader(logger);

            var counter = 0;
            foreach (var sourceAssembly in sources)
            {
                var sourceAssemblyPath = Path.IsPathRooted(sourceAssembly) ? sourceAssembly : Path.Combine(Directory.GetCurrentDirectory(), sourceAssembly);
                logger.SendMessage(TestMessageLevel.Informational, $"Processing {sourceAssembly}");

                var fullyQualifiedTestName = $"Sample.IntegrationTests.Whatever{counter}";

                // VS expected FullyQualifiedName to be the actual class+type name,optionally with parameter types
                // in parenthesis, but they must fit the pattern of a value returned by object.GetType().
                // It should _not_ include custom name or param values (just their types).
                // However, the "fullname" from NUnit's file generation is the custom name of the test, so
                // this code must convert from one to the other.
                // Reference: https://github.com/microsoft/vstest-docs/blob/master/RFCs/0017-Managed-TestCase-Properties.md

                // Using the nUnit-provided "fullname" will cause failures at test execution time due to
                // the FilterExpressionWrapper not being able to parse the test names passed-in as filters.

                // To resolve this issue, for parameterized tests (which are the only tests that allow custom names),
                // the parent node's "fullname" value is used instead. This is the name of the actual test method
                // and will allow the filtering to work as expected.

                // Note that this also means you can no longer select a single tests of these to run.
                // When you do that, all tests within the parent node will be executed

                discoverySink.SendTestCase(new TestCase(fullyQualifiedTestName, new Uri(Constants.ExecutorUri), sourceAssemblyPath));
            }
        }

        private void PrintHeader(IMessageLogger logger)
        {
            var platform = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            var versionAttribute = typeof(SmokeTestDiscoverer).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            logger.SendMessage(TestMessageLevel.Informational, $"SmokeMe.net VSTest Adapter v{versionAttribute.InformationalVersion} ({IntPtr.Size * 8}-bit {platform})");
        }
    }
}
