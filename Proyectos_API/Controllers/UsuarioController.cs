using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Proyectos_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyectos_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string user = data.usuario.ToString();
            string pass = data.password.ToString();

            Usuario usuario = Usuario.DB().Where(x => x.Nombre == user && x.Password == pass).FirstOrDefault();
            
            if(usuario == null)
            {
                return new
                {
                    success = true,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.Id.ToString()),
                new Claim("nombre", usuario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims, 
                    expires: DateTime.Now.AddDays(1), //Totalmente opcional lo puse para saber como se hacia
                    signingCredentials: SignIn
                );
            return new
            {
                success = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
