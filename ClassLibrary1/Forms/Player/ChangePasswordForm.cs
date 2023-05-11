using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.Player
{
    public class ChangePasswordForm
    {
        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*([^a-zA-Z\\d])).{8,}$")]
        public string Password { get; set; }
    }
}
