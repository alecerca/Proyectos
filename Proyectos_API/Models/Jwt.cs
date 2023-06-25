using System.Security.Claims;

namespace Proyectos_API.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(v => v.Type == "id").Value;

                Usuario usuario = Usuario.DB().FirstOrDefault(v => v.Id.ToString() == id);

                return new
                {
                    success = true,
                    message = "exito",
                    result = usuario
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };
            }
        }

    }
}
