using Bll.Interfaces;

using DAL.Interfaces;

using Domain.Models;
using Isopoh.Cryptography.Argon2;

namespace BLL.Services
{
    public class TournamentService : ITournamentService
    {

        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }
        public TournamentModel? Create(TournamentModel tournament, int id)
        {
            TournamentModel tournamentSecure = new TournamentModel(
                tournament.TournamentName,
                tournament.Description,
                tournament.MaxPlayer,
                tournament.TournamentState,
                id

            );

            return _tournamentRepository.Create(tournamentSecure,id);
        }

        public bool Delete(int id)
        {
            return _tournamentRepository.Delete(id);
        }

        public IEnumerable<TournamentModel> GetAll()
        {
            return _tournamentRepository.GetAll();
        }

        public TournamentModel? GetById(int id)
        {
            return _tournamentRepository.GetById(id);
        }
    
    }
}
