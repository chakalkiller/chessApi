using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.Player
{
    public class CreatePlayerForm
    {

        [Required]
        [MinLength(2)]
        public string Pseudo { get; set; }

      
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*([^a-zA-Z\\d])).{8,}$")]
        public string Password { get; set; }

   

    }
}
