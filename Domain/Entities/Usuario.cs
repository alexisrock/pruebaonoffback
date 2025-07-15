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
    [Table("Usuario")]
    public class Usuario
    {

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NameUsuario { get; set; } 
        public string? Email { get; set; }
        public string? Password { get; set; }
        [ForeignKey("Rol")]
        public int Idrol { get; set; }
        [ForeignKey("Idrol"), Required]
        public Rol? Rol { get; set; }



    }
}
