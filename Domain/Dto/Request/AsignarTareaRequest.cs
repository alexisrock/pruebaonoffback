using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Dto.Response;
using Domain.Entities;
using MediatR;

namespace Domain.Dto.Request
{
    public class AsignarTareaRequest : IRequest<BaseResponse>
    {
        public int IdTarea { get; set; }
        public int IdUsuario { get; set; }
    }


    public class AsignarTareaDeleteRequest : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class AsignarTareaIdUserRequest: IRequest<List<AsignarTareaIdUsuarioResponse>>
    {
        public int IdUsuario { get; set; }
    }


    public class  TareaAsignadasrequest : IRequest<List<TareaAsignadasResponse>>
    {
    }


    public class TareasSinAsignadasrequest : IRequest<List<TareasSinAsignarResponse>>
    {
    }

}
