using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class StatusPedido
    {
        [Key]
        public int StatusPedidoId { get; set; }

        [Display(Name = "Status do Pedido")]
        [Required(ErrorMessage = "O campo Status do Pedido é obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}