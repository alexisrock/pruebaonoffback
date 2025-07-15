using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Response
{
    public class AsignarTareaIdUsuarioResponse
    {
        public int Id { get; set; }
        public string NameTarea { get; set; }
        public bool IsCompleted { get; set; }
    }


    public class TareaAsignadasResponse
    {
        public int Id { get; set; }
        public string? NameTarea { get; set; }
        public string? NameUsuario { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }
    }


    public class TareasSinAsignarResponse
    {
        public int Id { get; set; }
        public string NameTarea { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }
    }

}
