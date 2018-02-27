using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }

        public virtual ICollection<Vendedores> Vendedores { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }
}