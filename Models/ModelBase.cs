using System;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models
{
    public class ModelBase
    {
        [Key]
        public Guid Id { get; set; }         
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public bool Ativo { get; set; } = true;

    }
}