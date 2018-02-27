using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceTCC.Models
{
    public class Cidades
    {
        [Key]
        [Display (Name = "Cidade")]
        public int CidadesId { get; set; }

        [Display(Name = "Cidade")]
        [Required (ErrorMessage = "O campo Cidade é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Cidade deve ter no máximo 50 caracteres.")]
        [Index("Cidade_Name_Index", IsUnique = true)]
        public string Nome { get; set; }

        [Display(Name = "Estado")]
        public int EstadosId { get; set; }

        public virtual Estados Estado { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }

        public virtual ICollection<Vendedores> Vendedores { get; set; }
    }
}