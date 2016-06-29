using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeZoneManager.API;
using TimeZoneManager.API.Controllers;

namespace TimeZoneManager.API.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            IHttpActionResult result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(2, ()result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        //[TestMethod]
        //public void GetById()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    string result = controller.Get(5);

        //    // Assert
        //    Assert.AreEqual("value", result);
        //}

        //[TestMethod]
        //public void Post()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Post("value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Put()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Put(5, "value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
