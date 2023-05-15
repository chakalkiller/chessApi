using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Tournament
{
    public class TournamentDTO
    {
        public TournamentDTO(int tournamentId, string tournamentName , string? description, int maxPlayer , bool tournamentState, int userIdCreator)
        {
            TournamentId = tournamentId;
            TournamentName = tournamentName;
            Description = description;
            MaxPlayer = maxPlayer;
            TournamentState = tournamentState;
            UserIdCreator = userIdCreator;
        }

        public int TournamentId { get; set; }

        public string TournamentName { get; set; }


        public string? Description { get; set; }

        public int MaxPlayer { get; set; }
        public bool TournamentState { get; set; }
        public int UserIdCreator { get; set;}
    }
}

