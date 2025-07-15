using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.TransformDto;
using AutoMapper;
using Domain.Common;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Service
{
    public class AsignarTareaHandler : IRequestHandler<AsignarTareaRequest, BaseResponse>,
        IRequestHandler<AsignarTareaDeleteRequest, BaseResponse>,
        IRequestHandler<AsignarTareaIdUserRequest, List<AsignarTareaIdUsuarioResponse>>,
        IRequestHandler<TareaAsignadasrequest, List<TareaAsignadasResponse>>,
        IRequestHandler<TareasSinAsignadasrequest, List<TareasSinAsignarResponse>>
    {


        private readonly IAsignacionTareaRepository _signacionTareaRepository;
        private readonly ITareaRepository tareaRepository;
        private readonly IMapper mapper;
        private readonly IStoreProcedureRepository storeProcedureRepository;

        public AsignarTareaHandler(IAsignacionTareaRepository signacionTareaRepository, ITareaRepository tareaRepository, IMapper mapper, IStoreProcedureRepository storeProcedureRepository)
        {
            _signacionTareaRepository = signacionTareaRepository;
            this.mapper = mapper;
            this.tareaRepository = tareaRepository;
            this.storeProcedureRepository = storeProcedureRepository;
        }

        public async Task<BaseResponse> Handle(AsignarTareaRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            try
            {
                if (!await ValildarTareaAsignada(request.IdTarea))
                {
                    throw new ApiException($"la tarea ya a sido asignada", (int)System.Net.HttpStatusCode.BadRequest);
                }
                var asignarTarea =  mapper.Map<AsignarTarea>(request);
                await _signacionTareaRepository.Insert(asignarTarea);
                response.SetDataResponse(HttpStatusCode.OK, $"Tarea asignada con exito");
                return response;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }

        }
        private async Task<bool> ValildarTareaAsignada(int idTarea)
        {
            var result = _signacionTareaRepository.GetByParam(x => x.IdTarea == idTarea);
            return await result == null;
        }

        public async Task<BaseResponse> Handle(AsignarTareaDeleteRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            try
            {
                var asignarTarea = await _signacionTareaRepository.GetById(request.Id);

                if (asignarTarea is null || await ValidarTareaFinalizada(asignarTarea.IdTarea))
                {
                    throw new ApiException($"la tarea ya a sido finalizada ", (int)System.Net.HttpStatusCode.BadRequest);
                }
                await _signacionTareaRepository.Delete(asignarTarea);
                response.SetDataResponse(HttpStatusCode.OK, $"Tarea fue eliminada con exito");
                return response;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }
        private async Task<bool> ValidarTareaFinalizada(int idTarea)
        {

            var tarea = await tareaRepository.GetId(idTarea);
            return tarea.IsCompleted;
        }

        public async Task<List<AsignarTareaIdUsuarioResponse>> Handle(AsignarTareaIdUserRequest request, CancellationToken cancellationToken)
        {
            var list = new List<AsignarTareaIdUsuarioResponse>();
            try
            {

                var listTareasAsignadasUsuario = await storeProcedureRepository.GetAsignarTareaIdUsuario(request.IdUsuario);
                list.MapperAsignarTareaIdUsuarioResponse(listTareasAsignadasUsuario);
                return list;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<TareaAsignadasResponse>> Handle(TareaAsignadasrequest request, CancellationToken cancellationToken)
        {
            var list = new List<TareaAsignadasResponse>();
            try
            {

                var listTareasAsignadas = await storeProcedureRepository.GetTareasAsignadas();
                list.MapperAsignarTareaResponse(listTareasAsignadas);
                return list;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<TareasSinAsignarResponse>> Handle(TareasSinAsignadasrequest request, CancellationToken cancellationToken)
        {
            var list = new List<TareasSinAsignarResponse>();
            try
            {

                var listTareasAsignadas = await storeProcedureRepository.GetTareassinAsignadas();
                list.MapperTareasSinAsignarResponse(listTareasAsignadas);
                return list;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
            throw new NotImplementedException();
        }
    }
}
