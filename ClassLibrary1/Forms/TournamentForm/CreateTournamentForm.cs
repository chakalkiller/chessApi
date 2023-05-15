using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.TournamentForm
{
    public class CreateTournamentForm
    {

        [Required]
        [MinLength(2)]
        public string TournamentName { get; set; }

      

        [Required]
        public int MaxPlayer { get; set; }

 
        public string TournamentDescription { get; set; } = string.Empty;
   

    }
}
