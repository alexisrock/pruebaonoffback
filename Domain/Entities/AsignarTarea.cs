using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("AsignarTarea")]
    public class AsignarTarea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Tarea")]
        public int IdTarea { get; set; }
        [ForeignKey("IdTarea")]
        public Tarea Tarea { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }



    }
}
