using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace psw_ftn.Models
{
    public class HistoryCheckUp
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CheckUpId { get; set; }

        public CheckUp CheckUp { get; set; }

        [Required]
        public Grade Grade { get; set; }
        
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}