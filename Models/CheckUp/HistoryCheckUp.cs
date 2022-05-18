using System.ComponentModel.DataAnnotations;

namespace psw_ftn.Models
{
    public class HistoryCheckUp : CheckUp
    {
        [Required]
        public Grade Grade { get; set; }
        
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}