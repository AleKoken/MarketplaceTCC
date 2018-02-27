using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class ItensPedido
    {
        [Key]
        public int ItensPedidoId { get; set; }

        [Display(Name = "Produto")]
        public int ProdutosId { get; set; }

        [Display(Name = "Pedido")]
        public int Numero { get; set; }

        public int Quantidade { get; set; }
    }
}