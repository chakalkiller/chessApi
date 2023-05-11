using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPlayerRepository
    {

        public IEnumerable<PlayerModel> GetAll();

        public PlayerModel? GetById(int playerId);

        public PlayerModel? GetByEmail(string email);

        public PlayerModel? Create(PlayerModel player);

        public bool Delete(int playerIdid);

        public bool ChangePassword(int id, string password);

    }
}
