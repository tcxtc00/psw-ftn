using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Models
{
    public class CancelledCheckUp
    {
        [Key]
        public int CancelledCheckUpId { get; set; }
        public int CheckUpId { get; set; }
        public CheckUp CheckUp { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public DateTime CancelationDate { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }
    }
}