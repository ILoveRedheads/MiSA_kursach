using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Tp_CourseWork.Controllers;
using Tp_CourseWork.DB;
using Tp_CourseWork.Repositories;
using Xunit;

namespace TP_CourseWork.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public HomeControllerTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        [Fact]
        public async Task Get_ViewResult_WithIndex()
        {
            var controller = new HomeController();

            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Get_PartialViewResult_WithDetails()
        {
            var controller = new HomeController();

            var result = await controller.Details(1);

            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public async Task Get_ViewResult_WithDetailsNotFound()
        {
            var controller = new HomeController();

            var result = await controller.Details(20);

            Assert.IsType<ViewResult>(result);
        }
    }
}
