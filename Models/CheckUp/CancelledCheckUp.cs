using System;
using System.ComponentModel.DataAnnotations;

namespace psw_ftn.Models
{
    public class CancelledCheckUp : CheckUp
    {
        [Required]
        public DateTime CancelationDate { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }
    }
}