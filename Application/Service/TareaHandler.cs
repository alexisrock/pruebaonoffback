using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.TransformDto;
using Application.Validations;
using AutoMapper;
using Domain.Common;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class TareaHandler : IRequestHandler<TareaRequest, BaseResponse>,
        IRequestHandler<TareaupdateRequest, BaseResponse>,
        IRequestHandler<TareaDeleteRequest, BaseResponse>,
        IRequestHandler<TareaGetRequest, List<TareaResponse>>
      




    {
        private readonly ILogger<TareaHandler> _logger;
        private readonly IValidator<TareaRequest> validator;
        private readonly IValidator<TareaupdateRequest> validatorUpdate;

        private readonly ITareaRepository _Repository;
        private readonly IMapper maper;

        public TareaHandler(ILogger<TareaHandler> logger, IValidator<TareaRequest> _validator, ITareaRepository Repository, IMapper _maper, IValidator<TareaupdateRequest> _validatorUpdate)
        {
            _logger = logger;
            validator = _validator;
            _Repository = Repository;
            maper = _maper;
            validatorUpdate = _validatorUpdate;
        }


        public async Task<BaseResponse> Handle(TareaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ApiException(result.Errors.ToString(), (int)System.Net.HttpStatusCode.BadRequest);
                }

                var tarea = maper.Map<Tarea>(request);
                await _Repository.Create(tarea);
                return new BaseResponse()
                {
                    message = "Tarea creada con exito"
                };


            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<BaseResponse> Handle(TareaupdateRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await validatorUpdate.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ApiException(result.Errors.ToString(), (int)System.Net.HttpStatusCode.BadRequest);
                }

                var tarea = await _Repository.GetId(request.IdTarea);
                tarea.MapperTareaUpdate(request);
                await _Repository.Update(tarea);
                return new BaseResponse()
                {
                    message = "Tarea actualizada con exito"
                };
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<BaseResponse> Handle(TareaDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.IdTarea == 0|| request is null)
                {
                    throw new ApiException("El id de la tarea no puede ser cero", (int)System.Net.HttpStatusCode.BadRequest);
                }

                var tarea = await _Repository.GetId(request.IdTarea);
                await _Repository.Delete(tarea);
                return new BaseResponse()
                {
                    message = "Tarea Eliminada con exito"
                };

            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<TareaResponse>> Handle(TareaGetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var listTareas = await _Repository.GetAll();
                return listTareas.MapperListTarea();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }

        }

     
    }
}
