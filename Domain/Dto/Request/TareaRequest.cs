using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Domain.Dto.Response;
using Domain.Entities;
using MediatR;

namespace Domain.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public class TareaRequest : IRequest<BaseResponse>
    {
        public string NameTarea { get; set; } = string.Empty;
        public string DescriptionTarea { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }

    [ExcludeFromCodeCoverage]
    public class TareaupdateRequest : IRequest<BaseResponse>
    {
        public int IdTarea { get; set; }
        public string NameTarea { get; set; } = string.Empty;
        public string DescriptionTarea { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

    }
    [ExcludeFromCodeCoverage]
    public class TareaDeleteRequest : IRequest<BaseResponse>
    {
        public int IdTarea { get; set; }       

    }

    [ExcludeFromCodeCoverage]
    public class TareaGetRequest : IRequest<List<TareaResponse>>
    {   
    }  

}
