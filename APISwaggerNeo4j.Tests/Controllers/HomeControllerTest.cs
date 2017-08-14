using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APISwaggerNeo4j;
using APISwaggerNeo4j.Controllers;

namespace APISwaggerNeo4j.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
