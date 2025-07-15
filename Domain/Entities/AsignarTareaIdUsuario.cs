using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AsignarTareaIdUsuario
    {
        public int Id { get; set; }
        public string NameTarea { get; set; }
        public bool IsCompleted { get; set; }
    }



    public class TareaAsignadas
    {
        public int Id { get; set; }
        public string NameTarea { get; set; }
        public string NameUsuario { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }
    }


    public class TareasSinAsignar
    {
        public int IdTarea { get; set; }
        public string NameTarea { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }
    }
}
