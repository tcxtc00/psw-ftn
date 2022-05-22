using System;
using System.ComponentModel.DataAnnotations;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Models
{
    public class CheckUp
    {
        [Required]
        [Key]
        public int CheckUpId { get; set; }

        public Patient Patient { get; set; }
        
        [Required]
        public Doctor Doctor { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }
        
        [Required]
        public DateTime CancellationTime { get; set; }

        public HistoryCheckUp HistoryCheckUp { get; set; }
    }
}