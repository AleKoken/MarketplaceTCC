using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceTCC.Models
{
    public class Estados
    {
        [Key]
        [Display(Name = "Estado")]
        public int EstadosId { get; set; }

        [Display(Name = "Estado")]
        [Required (ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Estado deve ter no máximo 50 caracteres.")]
        [Index("Estado_Index", IsUnique = true)]
        public string Nome { get; set; }

        public virtual ICollection<Cidades> Cidades { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }

        public virtual ICollection<Vendedores> Vendedores { get; set; }
    }
}