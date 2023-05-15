using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InscriptionModel
    {
        public InscriptionModel(int insriptionId, int playerId, int tournamentId)
        {
            InsriptionId = insriptionId;
            PlayerId = playerId;
            TournamentId = tournamentId;
        }
        public int InsriptionId { get; set; }
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
    }
}
