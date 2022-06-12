using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace psw_ftn.Models.User.UserTypes
{
    public class Doctor : User
    {
        [Required]
        [MaxLength(15)]
        public string Expertise { get; set; }
        public List<CheckUp> CheckUps { get; set; }
    }
}