using System;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models
{
    public class Player : ModelBase
    {   
        [Required(ErrorMessage = "É necessário que o campo seja informado!")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que 0")]
        public long PlayerId { get; set; }

        [Required(ErrorMessage = "É necessário que o campo seja informado!")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que 0")]
        public long GameId { get; set; }

        [Required(ErrorMessage = "É necessário que o campo seja informado!")]        
        public long Win { get; set; }
        public DateTime Timestamp { get; set; }
    }
}