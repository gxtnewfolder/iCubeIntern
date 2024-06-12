using iCubeTrain.Controllers;

namespace iCubeTrain.Test;

public class HomeControllerTests
{
    [Fact]
    public void HomeController_Index_Valid()
    {

        var controller = new HomeController();
        var result = controller.Index();

        Assert.Equal("Hello from iCubeTrain", result);

    }
}