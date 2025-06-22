using COOPGO.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroUsuarios.Models
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "A senha deve ter no mínimo 4 caracteres")]
        public string senha { get; set; }

        // Navigation property para as transações
        public virtual ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}