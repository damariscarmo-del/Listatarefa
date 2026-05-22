using Listatarefa.Data;
using Listatarefa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Listatarefa.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {
            private readonly UsuarioContext _context;

            public UsuarioController(UsuarioContext context)
            {
                _context = context;
            }



            [HttpPost("Login")]
            public IActionResult LoginUsuario(Usuario usuario)
            {
                var UsuariosDoBanco = _context.Usuarios.Where(c => c.Email.Equals(usuario.Email) && c.Senha.Equals(usuario.Senha)).ToList();
                if (UsuariosDoBanco.Count == 0)
                {
                    return NotFound("Login ou Senha incorretas");
                }
            HttpContext.Session.SetString("IdLogado", UsuariosDoBanco[0].Id.ToString());
            Response.Cookies.Append("IdLogado", UsuariosDoBanco[0].Id.ToString(),
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                });
            return Ok("Login realizado com sucesso");
            }
               

            [HttpPost]
            public IActionResult CadastraUsuario(Usuario usuario)
            {
                _context.Add(usuario);
                _context.SaveChanges();
                return Created("", usuario);
            }

        [HttpPut("{id}")]
            public IActionResult AtualizaCliente(int id, Usuario usuario)
            {
                var UsuariosDoBanco = _context.Usuarios.Find(id);
                if (UsuariosDoBanco == null)
                {
                    return NotFound("Cliente não existe no banco!");
                }
               UsuariosDoBanco.Nome = usuario.Nome;
               UsuariosDoBanco.Email = usuario.Email;
                UsuariosDoBanco.Senha = usuario.Senha;
                _context.SaveChanges();
                return Ok("Atualizado");
            }

            [HttpGet("logout")]
            public IActionResult Logout ()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Idlogado");
            Response.Cookies.Delete(" .aspNetCore.Session");
            return Ok("Deslogado");
        }
           

            [HttpDelete("{id}")]
            public IActionResult DeletaCliente(int id)
            {
                var UsuariosDoBanco = _context.Usuarios.Find(id);
                if (UsuariosDoBanco == null)
                {
                    return NotFound("Não encontrado!");
                }
                _context.Remove(UsuariosDoBanco);
                _context.SaveChanges();
                return Ok("Deletado");
            }
        

    }
}
