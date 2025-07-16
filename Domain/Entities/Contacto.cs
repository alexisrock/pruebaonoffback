
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Contacto
    {

        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }

    }
}
