using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceTCC.Models
{
    public class Marcas
    {
        [Key]
        public int MarcasId { get; set; }

        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        [Display(Name = "Marca")]
        public string Nome { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}