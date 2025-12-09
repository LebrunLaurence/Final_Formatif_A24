using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestMethod]
    public void ReserveSeat()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controllerMock = new Mock<SeatsController>(serviceMock.Object) { CallBase = true};

        controllerMock.Setup(c => c.UserId).Returns("1");

        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(),It.IsAny<int>())).Returns(new Models.Seat() { Id = 1,Number = 1 });

        var act = controllerMock.Object.ReserveSeat(1);

        var result = act.Result as OkObjectResult;

        Assert.IsNotNull(act);
    }

    [TestMethod]
    public void ReserveSeatTaken()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controllerMock = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        controllerMock.Setup(c => c.UserId).Returns("1");

        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatAlreadyTakenException());

        var act = controllerMock.Object.ReserveSeat(1);

        var result = act.Result as UnauthorizedResult;

        Assert.IsNotNull(act);
    }

    [TestMethod]
    public void ReserveTooHigh()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controllerMock = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        controllerMock.Setup(c => c.UserId).Returns("1");

        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatOutOfBoundsException());

        var act = controllerMock.Object.ReserveSeat(1);

        var result = act.Result as NotFoundResult;

        Assert.IsNotNull(act);
    }

    [TestMethod]
    public void ReserveAlreadyReserved()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controllerMock = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        controllerMock.Setup(c => c.UserId).Returns("1");

        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new UserAlreadySeatedException());

        var act = controllerMock.Object.ReserveSeat(1);

        var result = act.Result as BadRequestResult;

        Assert.IsNotNull(act);
    }
}
