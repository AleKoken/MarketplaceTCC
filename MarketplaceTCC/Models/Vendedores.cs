using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketplaceTCC.Models
{
    public class Vendedores
    {
        [Key]
        public int VendedoresId { get; set; }

        [Required(ErrorMessage = "O campo Razão Social é obrigatório.")]
        [Display(Name = "Vendedor")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Nome Fantasia é obrigatório.")]
        [Display(Name = "Nome Fantasia")]
        public string Nome_Fantasia { get; set; }

        [Required(ErrorMessage = "O campo E-Mail é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O campo E-Mail recebe no máximo 250 caracteres")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Index("Email_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O campo CNPJ deve ter 14 caracteres")]
        [Display(Name = "CNPJ")]
        [Index("CNPJ_Index", IsUnique = true)]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public int CidadesId { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public int EstadosId { get; set; }


        public int Comissao { get; set; }

        public int StatusId { get; set; }


        public virtual Cidades Cidade { get; set; }

        public virtual Estados Estado { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }

    }
}