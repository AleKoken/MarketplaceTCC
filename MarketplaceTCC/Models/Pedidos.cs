using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class Pedidos
    {
        [Key]
        public int PedidosId { get; set; }

        [Display(Name = "Numero do Pedido")]
      //  [Index("Numero_Index", IsUnique = true)]
        public string Numero { get; set; }

        [Display(Name = "Data do Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPedido { get; set; }

        [Display(Name = "Valor Total")]
        public string ValorTotal { get; set; }

        public int ClientesId { get; set; }

        public int StatusPedidoId { get; set; }

        public virtual Clientes Cliente { get; set; }

        public virtual StatusPedido StatusPedido { get; set; }

    }
}