using Application.Features.CarFeatures.Commands.CreateCar;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace UnitTest
{
    public class CarsControllerUnitTest
    {
        [Fact]
        public async Task Create_ReturnsOkResult_WhenRequestIsValid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new(
                "Toyota","Corolla","5000");

            MessageResponse messageResponse = new("Araç başarıyla kaydedildi");
            CancellationToken cancellationToken = new();

            mediatorMock.Setup(x => x.Send(createCarCommand, cancellationToken)).ReturnsAsync(messageResponse);

            CarsController carsController = new(mediatorMock.Object);

            // Act
            var result = await carsController.Create(createCarCommand, cancellationToken);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(messageResponse, returnValue);
            mediatorMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
        }
    }
}
