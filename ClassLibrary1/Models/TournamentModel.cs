using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TournamentModel
    {
        public TournamentModel(string tournamentName, string? description, int maxPlayer ,bool tournamentState ,int userIdCreator)
        {
            TournamentId = -1;
            TournamentName = tournamentName;
            Description = description;
            MaxPlayer = maxPlayer;
            TournamentState = tournamentState;
            UserIdCreator= userIdCreator;
        }
        public TournamentModel(int tournamentId, string tournamentName, string? description, int maxPlayer, bool tournamentState, int userIdCreator)
        : this(tournamentName, description, maxPlayer, tournamentState,  userIdCreator)
        {
            TournamentId = tournamentId;
        }

        public int TournamentId { get; set; }

        public string TournamentName { get; set; }

        public string? Description { get; set; }

        public int MaxPlayer { get; set; }

        public bool TournamentState { get; set; }

        public int UserIdCreator { get; set; }
    }
}
