using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]  
    public class AsignarTarea
    {
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public Tarea Tarea { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }



    }
}
