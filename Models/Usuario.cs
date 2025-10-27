using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using ServiceSoap.Dto;

namespace ServiceSoap.Models
{
    [Table("USERS")]
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Usuario
    {
        [Column("ID_USER")]
        [Key]
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [Column("NAME_USER")]
        [StringLength(120)]
        [DataMember(Order = 2)]
        public string Name { get; set; } = string.Empty;

        [Column("SURNAME_USER")]
        [StringLength(120)]
        [DataMember(Order = 3)]
        public string Surname { get; set; } = string.Empty;

        [Column("LOGIN_USER")]
        [StringLength(60)]
        [DataMember(Order = 4)]
        public string Login { get; set; } = string.Empty;

        [Column("EMAIL_USER")]
        [StringLength(200)]
        [DataMember(Order = 5)]
        public string Email { get; set; } = string.Empty;

        [Column("PASSWORD_USER")]
        [StringLength(12)]
        [DataMember(Order = 6)]
        public string Password { get; set; } = string.Empty;

        public UsuarioDto GetUsuarioDto()
        {
            UsuarioDto result = new()
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email
            };

            return result;
        }
    }
}
