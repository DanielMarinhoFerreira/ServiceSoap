using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ServiceSoap.Dto;

public class UsuarioDto
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

        [Column("EMAIL_USER")]
        [StringLength(200)]
        [DataMember(Order = 5)]
        public string Email { get; set; } = string.Empty;
}
