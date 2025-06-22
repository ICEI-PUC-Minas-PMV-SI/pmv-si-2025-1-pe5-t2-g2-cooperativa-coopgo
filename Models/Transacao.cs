using CadastroUsuarios.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOPGO.Models
{
    public class Transacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int usuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo { get; set; } // "Saque" ou "Depósito"

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal valor { get; set; }

        [Required]
        public DateTime data { get; set; }

        // Navigation property
        [ForeignKey("usuarioId")]
        public virtual Usuarios? Usuario { get; set; }
    }
}