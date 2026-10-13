using NetArchTest.Rules;

using Pos.SharedKernel;
using Pos.Catalog;


namespace Pos.ArchitectureTests;

public class CatalogLayerTests
{
    private const string Domain = "Pos.Catalog.Domain";
    private const string Application = "Pos.Catalog.Application";
    private const string Infrastructure = "Pos.Catalog.Infrastructure";
    private const string Catalog = "Pos.Catalog";

    [Fact]
    public void DomainLayer_ShouldNotDependOnApplicationOrInfrastructure()
    {
        var result = Types.InAssembly(CatalogAssembly.Assembly)
            .That().ResideInNamespace(Domain)
            .ShouldNot().HaveDependencyOnAny(Application, Infrastructure)
            .GetResult();

        Assert.True(result.IsSuccessful, Failures(result));

    }

    [Fact]
    public void ApplicationLayer_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(CatalogAssembly.Assembly)
            .That().ResideInNamespace(Application)
            .ShouldNot().HaveDependencyOn(Infrastructure)
            .GetResult();
        Assert.True(result.IsSuccessful, Failures(result));
    }

    [Fact]
    public void SharedKernel_ShouldNotDependOnCatalog()
    {


        var result = Types.InAssembly(SharedKernelAssembly.Assembly)
            .ShouldNot().HaveDependencyOn(Catalog)
            .GetResult();
          
        Assert.True(result.IsSuccessful, Failures(result));
    }
    private static string Failures(TestResult result) =>
        "Violations: " + string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>());
}
