using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPlayerService
    {

        public IEnumerable<PlayerModel> GetAll();

        public PlayerModel? GetById(int playerId);

        public PlayerModel? Create(PlayerModel player);

        public bool Delete(int playerId);

        public bool ChangePassword(int playerId, string password);

        public int? Login(string email,  string password);
    }
}
