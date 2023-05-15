using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.TournamentForm
{
    public class TournamentStatusForm
    {
        [Required]
     
        public bool? ChangeStatus { get; set; }
    }
}
