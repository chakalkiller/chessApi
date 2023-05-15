using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.TournamentForm
{
    public class RegisterForm
    {
        [Required]
        [MinLength(2)]
        public string? RegisterChoise { get; set; }
    }
}
