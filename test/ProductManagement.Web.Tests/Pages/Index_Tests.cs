using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ProductManagement.Pages;

[Collection(ProductManagementTestConsts.CollectionDefinitionName)]
public class Index_Tests : ProductManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
