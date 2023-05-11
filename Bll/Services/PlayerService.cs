using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Models;
using Isopoh.Cryptography.Argon2;

namespace BLL.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public int? Login(string email, string password)
        {
            PlayerModel? player = _playerRepository.GetByEmail(email);

            if (player is null)
                return null;

            if (!Argon2.Verify(player.Password, password))
                return null;

            return player.PlayerId;
        }

        public bool ChangePassword(int id, string password)
        {
            return _playerRepository.ChangePassword(id, Argon2.Hash(password));
        }

        public PlayerModel? Create(PlayerModel player)
        {
            PlayerModel playerSecure = new PlayerModel(
                player.Pseudo,
                player.Email,
                player.Birthdate,
                Argon2.Hash(player.Password)
                
            );

            return _playerRepository.Create(playerSecure);
        }

        public bool Delete(int id)
        {
            return _playerRepository.Delete(id);
        }

        public IEnumerable<PlayerModel> GetAll()
        {
            return _playerRepository.GetAll();
        }

        public PlayerModel? GetById(int id)
        {
            return _playerRepository.GetById(id);
        }

    }
}
