# SmokeMe.TestAdapter

Test runner and adapter to transform your SmokeMe! smoke tests into integration tests.


Ce qui ne fonctionnait pas:
 - Le fait d'avoir un type "Runner" qui agrégeait les discoverer et executor
 - le fait de n'avoir pas d'attribut [DefaultExecutorUri("executor://smokeme.testadapter")] sur le Discoverer

Ce qui ne fonctionne toujours pas :
 - L'exploitation d'un package nuget qui contient les symboles (obligé de copier le pdb dans mon rép cible)

 



- Run 

      Nuget pack "C:\Dev\oss\SmokeMe.TestAdapter\SmokeMe.TestAdapter\SmokeMe.TestAdapter.csproj" -Symbols -SymbolPackageFormat snupkg

source:
"C:\\Dev\\oss\\SmokeMe.TestAdapter\\Samples\\Sample.IntegrationTests\\bin\\Debug\\netcoreapp3.1\\Sample.Integration.Tests.dll"


discovery context.RunSettings:

````
<?xml version=\"1.0\" encoding=\"utf-16\"?>
<RunSettings>
  <Python>
    <TestCases />
  </Python>
  <RunConfiguration>
    <ResultsDirectory>C:\\Dev\\oss\\SmokeMe.TestAdapter\\TestResults</ResultsDirectory>
    <SolutionDirectory>C:\\Dev\\oss\\SmokeMe.TestAdapter\\</SolutionDirectory>
    <CollectSourceInformation>False</CollectSourceInformation>
    <TargetFrameworkVersion>.NETCoreApp,Version=v3.1</TargetFrameworkVersion>
    <TargetPlatform>X64</TargetPlatform>
    <DesignMode>True</DesignMode>
  </RunConfiguration>
</RunSettings>


````
