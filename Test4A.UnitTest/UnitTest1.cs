using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace Test4A.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var loggerMock = new Mock<ILogger<Test4A.Controllers.HomeController>>();
            var dbMock = new Mock<IVoteDb>();
            var apiMock = new Mock<ISWAPI>();
            apiMock.Setup(i => i.List()).ReturnsAsync(new List<Test4A.Models.FilmModel>
            {
                new Test4A.Models.FilmModel { index = 1, episode_id = 1, title = "test 1" },
                new Test4A.Models.FilmModel { index = 2, episode_id = 2, title = "test 2"},
            });

            Test4A.Controllers.HomeController controller = new Controllers.HomeController(loggerMock.Object, apiMock.Object, dbMock.Object);

            var task= controller.Index(null, null);
            task.Wait();
            var viewResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ViewResult>(task.Result);
            var model = Assert.IsAssignableFrom<dynamic>(viewResult.ViewData.Model);
            Assert.NotNull(model.list);
            Assert.Equal(2, model.list.Count);

        }
    }
}
