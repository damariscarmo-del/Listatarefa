using Listatarefa.Models;
using Microsoft.EntityFrameworkCore;

namespace Listatarefa.Data
{
    public class UsuarioContext : DbContext
    {
            public DbSet<Usuario> Usuarios { get; set; }

            public DbSet<Tarefas> Tarefas { get; set; }

            public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }

        }
    }
