namespace COOPGO.Models
{
    public class Transacao
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public string tipo { get; set; }
        public decimal valor { get; set; }
        public DateTime data { get; set; }
    }
}
