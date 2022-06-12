using System.ComponentModel.DataAnnotations;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        
        [Required]
        public Grade Grade { get; set; }

        public string Comment { get; set; }

        [Required]
        public bool IsForDisplay { get; set; } = false;

        [Required]
        public bool Incognito { get; set; } = false;

        [Required]
        public int PatientId { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}