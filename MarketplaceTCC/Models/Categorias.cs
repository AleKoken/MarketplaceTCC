using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriasId { get; set; }

        [Required (ErrorMessage = "O campo Categoria é obrigatório.")]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}