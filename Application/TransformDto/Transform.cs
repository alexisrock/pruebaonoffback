using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;

namespace Application.TransformDto
{
    [ExcludeFromCodeCoverage]
    internal static class Transform
    {




        internal static Tarea MapperTareaUpdate(this Tarea tarea, TareaupdateRequest request)
        {
            tarea.IsCompleted = request.IsCompleted;
            tarea.NameTarea = request.NameTarea;
            tarea.DescriptionTarea = request.DescriptionTarea;
            return tarea;
        }
        internal static List<TareaResponse> MapperListTarea(this IEnumerable<Tarea> listTarea)
        {
            List<TareaResponse> tareaResponses = new List<TareaResponse>();
            if (listTarea.Any())
            {
                listTarea.ToList().ForEach(response =>
                {
                    var tareaResponse = new TareaResponse();
                    tareaResponse.IsCompleted = response.IsCompleted;
                    tareaResponse.NameTarea = response.NameTarea;
                    tareaResponse.DescriptionTarea = response.DescriptionTarea;
                    tareaResponse.IdTarea = response.IdTarea;
                    tareaResponses.Add(tareaResponse);
                });
            }
           


            return tareaResponses;

        }
        internal static BaseResponse SetDataResponse(this BaseResponse objeto, HttpStatusCode StatusCode, string? Message)
        {

            objeto.message = Message;
            return objeto;
        }
        internal static Usuario SetUsuarioUpdate(this Usuario usuario, UsuarioUpdateRequest request)
        {
            usuario.Idrol = request.Idrol;
            usuario.NameUsuario = request.NameUsuario;
            usuario.Email = request.Email;
            return usuario;
        }
        internal static List<UserResponse> MapperListUserResponse(this List<UserResponse> list, List<Usuario> listusuario)
        {
            if (listusuario.Any())
            {
                listusuario.ForEach(usuario =>
                {
                    var userResponse = new UserResponse();
                    userResponse.Idrol = usuario.Idrol;
                    userResponse.Email = usuario.Email;
                    userResponse.Id = usuario.Id;
                    userResponse.NameUsuario = usuario.NameUsuario;
                    list.Add(userResponse);
                });
            }


            return list;
        }
        internal static List<AsignarTareaIdUsuarioResponse> MapperAsignarTareaIdUsuarioResponse(this List<AsignarTareaIdUsuarioResponse> list, List<AsignarTareaIdUsuario> listAsignarTareaIdUsuario)
        {
            if (listAsignarTareaIdUsuario.Any())
            {
                listAsignarTareaIdUsuario.ForEach(item =>
                {
                    var response = new AsignarTareaIdUsuarioResponse();
                    response.Id = item.Id;
                    response.NameTarea = item.NameTarea;
                    response.IsCompleted = item.IsCompleted;
                    list.Add(response);
                });


            }
            return list;
        }

        internal static List<TareaAsignadasResponse> MapperAsignarTareaResponse(this List<TareaAsignadasResponse> list, List<TareaAsignadas> listAsignarTarea)
        {
            if (listAsignarTarea.Any())
            {
                listAsignarTarea.ForEach(item =>
                {
                    var response = new TareaAsignadasResponse();
                    response.Id = item.Id;
                    response.NameTarea = item.NameTarea;
                    response.NameUsuario = item.NameUsuario;
                    response.DescriptionTarea = item.DescriptionTarea;
                    response.IsCompleted = item.IsCompleted;
                    list.Add(response);
                });

            }
            return list;
        }


        internal static List<TareasSinAsignarResponse> MapperTareasSinAsignarResponse(this List<TareasSinAsignarResponse> list, List<TareasSinAsignar> listTareaSinAsignar)
        {
            if (listTareaSinAsignar.Any())
            {
                listTareaSinAsignar.ForEach(item =>
                {
                    var response = new TareasSinAsignarResponse();
                    response.Id = item.IdTarea;
                    response.NameTarea = item.NameTarea; 
                    response.DescriptionTarea = item.DescriptionTarea;
                    response.IsCompleted = item.IsCompleted;
                    list.Add(response);
                });

            }
            return list;
        }

    }
}
