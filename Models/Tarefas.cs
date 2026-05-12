namespace Listatarefa.Models
{
    public class Tarefas
    {
        internal object idUsuario;

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Status {  get; set; }
        public int idUsuarios { get; set; }
    }
}
