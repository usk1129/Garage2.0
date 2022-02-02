using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garage2._0.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage2._0.Data;
using Moq;
using Xunit;

namespace Garage2._0.Controllers.Tests
{
    [TestClass()]
    public class ParkVehiclesControllerTests
    {
        [Fact]
        public async Task TestCreate()
        {
            //Arrange
            var mockRepo = new Mock<Garage2_0Context>();

            var controller = new ParkVehiclesController(mockRepo.Object);
            controller.ModelState.AddModelError("Error", "Some Error");

            //Act

            var result = await controller.Create(parkVehicle: null);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}