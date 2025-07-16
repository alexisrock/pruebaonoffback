using System.Diagnostics.CodeAnalysis;


namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]  
    public class Usuario
    {

        public int Id { get; set; }
        public string? NameUsuario { get; set; } 
        public string? Email { get; set; }
        public string? Password { get; set; }       
        public int Idrol { get; set; }       
        public Rol? Rol { get; set; }



    }
}
