using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class Produtos
    {
        [Key]
        public int ProdutosId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Preco é obrigatório.")]
        [Display(Name = "Preco")]
        [DataType(DataType.Currency)]
        public string Preco { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImagemFile { get; set; }

        [Required(ErrorMessage = "O campo Estoque é obrigatório.")]
        [Display(Name = "Estoque")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
        public int CategoriasId { get; set; }

        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        public int MarcasId { get; set; }


        public int VendedoresId { get; set; }

        public int StatusId { get; set; }


        public virtual Categorias Categoria { get; set; }

        public virtual Marcas Marca { get; set; }

        public virtual Vendedores Vendedor { get; set; }

        public virtual Status Status { get; set; }

    }
}