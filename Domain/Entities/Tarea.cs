using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("Tarea")]
    public class Tarea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarea { get; set; }
        public string NameTarea { get; set; }
        public string DescriptionTarea { get; set; }
        public bool IsCompleted { get; set; }

    }
}
