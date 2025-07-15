using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Service;
using AutoMapper;
using Domain.Dto.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Aplication
{
    public class TareaHandlerTest
    {


        private Mock<ILogger<TareaHandler>> _loggerMock;
        private Mock<IValidator<TareaRequest>> _validatorMock;
        private Mock<IValidator<TareaupdateRequest>> _validatorUpdateMock;
        private Mock<ITareaRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private TareaHandler _handler;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<TareaHandler>>();
            _validatorMock = new Mock<IValidator<TareaRequest>>();
            _validatorUpdateMock = new Mock<IValidator<TareaupdateRequest>>();
            _repositoryMock = new Mock<ITareaRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new TareaHandler(_loggerMock.Object, _validatorMock.Object, _repositoryMock.Object, _mapperMock.Object, _validatorUpdateMock.Object);
        }




        [Test]
        public async Task Handle_CreateTarea_Success()
        {
            var request = new TareaRequest() { NameTarea = "realizar labores casa", DescriptionTarea = "realizar labores casa", IsCompleted = false };
            var tarea = new Tarea();
            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<Tarea>(request)).Returns(tarea);
            _repositoryMock.Setup(r => r.Create(It.IsAny<Tarea>())).Returns(Task.CompletedTask);

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.IsNotNull(response);
            Assert.That(response.message, Is.EqualTo("Tarea creada con exito"));
        }

        [Test]
        public void Handle_CreateTarea_ValidationFails()
        {

            var request = new TareaRequest() { NameTarea = "realizar labores casa", DescriptionTarea = "", IsCompleted = false };

            var validationResult = new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Propiedad", "Error de validación")
            });
            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            var ex = Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task Handle_UpdateTarea_Success()
        {
            var request = new TareaupdateRequest { IdTarea = 1 };
            var tarea = new Tarea();
            _validatorUpdateMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _repositoryMock.Setup(r => r.GetId(request.IdTarea)).ReturnsAsync(tarea);
            _repositoryMock.Setup(r => r.Update(tarea)).Returns(Task.CompletedTask);

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.That(response.message, Is.EqualTo("Tarea actualizada con exito"));
        }

        [Test]
        public void Handle_UpdateTarea_NotFound()
        {
            var request = new TareaupdateRequest { IdTarea = 0 };           
        

            var validationResult = new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Propiedad", "Error de validación")
            });
            _validatorUpdateMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            var ex = Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.StatusCode, Is.EqualTo(400));

        }

        [Test]
        public async Task Handle_DeleteTarea_Success()
        {
            var request = new TareaDeleteRequest { IdTarea = 1 };
            var tarea = new Tarea();
            _repositoryMock.Setup(r => r.GetId(request.IdTarea)).ReturnsAsync(tarea);
            _repositoryMock.Setup(r => r.Delete(tarea)).Returns(Task.CompletedTask);

            var response = await _handler.Handle(request, CancellationToken.None);

            Assert.That(response.message, Is.EqualTo("Tarea Eliminada con exito"));
        }

        [Test]
        public void Handle_DeleteTarea_IdZero_ThrowsException()
        {
            var request = new TareaDeleteRequest { IdTarea = 0 };
            var ex = Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.StatusCode, Is.EqualTo(400));
            Assert.That(ex.Message, Is.EqualTo("El id de la tarea no puede ser cero"));
        }



    }
}
