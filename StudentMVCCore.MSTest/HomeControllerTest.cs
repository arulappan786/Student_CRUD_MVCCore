using Microsoft.Extensions.Logging;
using StudentMVCCoreWb.Controllers;

namespace StudentMVCCore.MSTest;

[TestClass]
public class HomeControllerTest
{
    private readonly HomeController _controller;

    public HomeControllerTest()
    {
        var logger = new LoggerFactory().CreateLogger<HomeController>();
        _controller = new HomeController(logger);
    }

    [TestMethod]
    public void Index_ReturnsNotNull()
    {
        var result = _controller.Index();
        Assert.IsNotNull(result);
    }
}
