using Listatarefa.Data;
using Listatarefa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Listatarefa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly UsuarioContext _context;
        public TarefaController(UsuarioContext context)
        {
            _context = context;

        }
        [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("atualizar/{id}")]

        public IActionResult AtualizarTarefas(int id, Tarefas tarefa)
        {
            var TarefaDoBanco = _context.Tarefas.Find(id);

            if (TarefaDoBanco == null)
                return NotFound("Tarefa não encontrada");
            TarefaDoBanco.Descricao = tarefa.Descricao;
            TarefaDoBanco.Status = tarefa.Status;

            _context.SaveChanges();

            return Ok("Atualizado");

        }


           [HttpPost("Cadastrar")]
        public IActionResult CriarTarefas(Tarefas tarefa)
        {
            var idUsuario = HttpContext.Session.GetString("IdLogado");
            if (idUsuario == null) return Unauthorized("não autorizado");

            var sessao = Request.Cookies["IdLogado"];

            if (sessao != null)
            {

                tarefa.idUsuarios = int.Parse(sessao);

            }
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("Teste", tarefa);
        }
        [HttpGet("tarefaUsuario")]
        public IActionResult TarefaUsuario()

        {
            var idUsuario = HttpContext.Session.GetString("IdLogado");
            if (idUsuario == null) return Unauthorized("Faça login antes! ");

            var sessao = Request.Cookies["IdLogado"];

            var resultado = from u in _context.Usuarios
                            join t in _context.Tarefas
                            on u.Id equals t.idUsuarios
                            where u.Id == int.Parse(idUsuario)
                            select new
                            {
                                Usuario = u.Nome,
                                u.Email,
                                Tarefas = t.Id,t.Status,
                                t.Descricao

                            };
            return Ok(resultado.ToList());
        }
    }

    
}
