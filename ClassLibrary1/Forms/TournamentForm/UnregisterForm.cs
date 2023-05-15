using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.TournamentForm

{
    public class UnregisterForm
    {
        [Required]
        [MinLength(2)]
        public string? Unregister { get; set; }
    }
}
