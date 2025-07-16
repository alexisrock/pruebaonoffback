using System.Diagnostics.CodeAnalysis;


namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]   
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string NameTarea { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }

    }
}
