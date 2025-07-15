using System.Net;
using Api.Controllers;
using Domain.Common;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.Api
{
    public class TareaControllerTest
    {

        private Mock<ISender> _sender;
        private TareaController _controller;


        [SetUp]
        public void Setup()
        {
            _sender = new Mock<ISender>();
            _controller = new TareaController(_sender.Object);

        }

        [Test]
        public async Task Create_ReturnsOkResult_WhenTareaCreated()
        {
            // Arrange
            // Populate with test data
            var baseResponse = new BaseResponse { message= "Tarea creada con exito" };
            var request = new TareaRequest() { NameTarea="realizar labores casa", DescriptionTarea= "realizar labores casa", IsCompleted=false };
            _sender.Setup(x => x.Send(request, It.IsAny <CancellationToken>())).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Create(request);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var okResult = (ObjectResult)result;
            Assert.That(okResult, Is.EqualTo(result));
            Assert.IsNotNull(result);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task Create_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {

            // Arrange
            var request = new TareaRequest() { NameTarea= "realizar labores casa", DescriptionTarea="", IsCompleted=false };
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("La descripcion de la tarea es obligatorio", 404));
            // Act

            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("La descripcion de la tarea es obligatorio"));
            Assert.That(ex.StatusCode, Is.EqualTo(404));

        }


        [Test]
        public async Task Create_ReturnsProblem__WhenExceptionThrown()
        {
            // Arrange
            var request = new TareaRequest() {  };
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("Ocurrió un error inesperado", 500));
            

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Create(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Ocurrió un error inesperado"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }



        [Test]
        public async Task Update_ReturnsOkResult_WhenTareaUpdated()
        {
            // Arrange
            // Populate with test data
            var baseResponse = new BaseResponse { message = "Tarea creada con exito" };
            var request = new TareaupdateRequest { IdTarea = 1, IsCompleted=true };
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Update(request);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var okResult = (ObjectResult)result;
            Assert.That(okResult, Is.EqualTo(result));
            Assert.IsNotNull(result);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task Update_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {

            // Arrange
            var request = new TareaupdateRequest { IdTarea = 0, IsCompleted = true };
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("El id de la tarea debe ser mayor que cero.", 404));
            // Act

            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("El id de la tarea debe ser mayor que cero."));
            Assert.That(ex.StatusCode, Is.EqualTo(404));

        }


        [Test]
        public async Task Update_ReturnsProblem__WhenExceptionThrown()
        {
            // Arrange
            var request = new TareaupdateRequest() { };
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("Ocurrió un error inesperado", 500));


            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Update(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Ocurrió un error inesperado"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }



        [Test]
        public async Task Delete_ReturnsOkResult_WhenTareaDeleted()
        {
            // Arrange
            // Populate with test data
            var baseResponse = new BaseResponse { message = "Tarea Eliminada con exito" };
            var request = 1 ;
            _sender.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(baseResponse);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var okResult = (ObjectResult)result;
            Assert.That(okResult, Is.EqualTo(result));
            Assert.IsNotNull(result);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task Delete_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {

            try
            {
                // Arrange
                var request = 1;
                _sender.Setup(x => x.Send(It.IsAny<TareaDeleteRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("El id de la tarea no puede ser cero.", 404));
                // Act

                var ex = Assert.ThrowsAsync<ApiException>(async () =>
                {
                    await _controller.Delete(request);
                });


                // Assert
                Assert.That(ex.Message, Is.EqualTo("El id de la tarea no puede ser cero."));
                Assert.That(ex.StatusCode, Is.EqualTo(404));

            }
            catch (Exception ex)
            {

                throw;
            }
          

        }


        [Test]
        public async Task Delete_ReturnsProblem__WhenExceptionThrown()
        {
            // Arrange
            var request = 0;
            _sender.Setup(x => x.Send(It.IsAny<TareaDeleteRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("Ocurrió un error inesperado", 500));


            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.Delete(request);
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Ocurrió un error inesperado"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }


        [Test]
        public async Task GetAll_ReturnsOkResult_WithTareas()
        {
            // Arrange
            var tareas = new List<TareaResponse> { new TareaResponse(), new TareaResponse() };
            var request = new TareaGetRequest { };
            _sender.Setup(x => x.Send(It.IsAny<TareaGetRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(tareas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        }

        [Test]
        public async Task GetAll_ReturnsProblem_WhenExceptionThrown()
        {
            // Arrange
            _sender.Setup(x => x.Send(It.IsAny<TareaGetRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ApiException("error"));

            // Act
            var ex = Assert.ThrowsAsync<ApiException>(async () =>
            {
                await _controller.GetAll();
            });


            // Assert
            Assert.That(ex.Message, Is.EqualTo("error"));
            Assert.That(ex.StatusCode, Is.EqualTo(500));
        }


    }
}
