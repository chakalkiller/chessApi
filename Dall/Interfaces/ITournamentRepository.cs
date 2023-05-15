using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITournamentRepository
    {

        public IEnumerable<TournamentModel> GetAll();

        public TournamentModel? GetById(int tournamentId);

   

        public TournamentModel? Create(TournamentModel tournament,int id);

        public bool Delete(int tournamentId);
        public bool OpenRegister(int TournamentId, int PlayerId);



    }
}
