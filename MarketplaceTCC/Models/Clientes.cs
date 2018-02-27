using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceTCC.Models
{
    public class Clientes
    {
        [Key]
        public int ClientesId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

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

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [MaxLength(11, ErrorMessage = "O campo CPF deve ter 11 caracteres")]
        [Display(Name = "CPF")]
        [Index("CPF_Index", IsUnique = true)]
        public string CPF { get; set; }

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

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public int EstadosId { get; set; }


        public int StatusId { get; set; }


        public virtual Cidades Cidade { get; set; }

        public virtual Estados Estado { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}