namespace Proyectos_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public static List<Usuario> DB()
        {
            var List = new List<Usuario>()
            {
                new Usuario
                {
                    Id = 1,
                    Nombre = "Federico",
                    Password = "password",
                    Email = "email",
                    Rol = "Usuario"
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Messi",
                    Password = "1234",
                    Email = "email",
                    Rol = "Usuario"
                }
            };
            return List;
        }
    }
}
