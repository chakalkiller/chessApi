using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PlayerModel
    {
        public PlayerModel(string pseudo, string email, DateTime birthdate, string password)
        {
            PlayerId = -1;
            Pseudo = pseudo;
            Email = email;
            Birthdate = birthdate;
            Password = password;
        }

        public PlayerModel(int playerId, string pseudo, string email, DateTime birthdate, string password)
            : this(pseudo, email, birthdate, password)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; set; }

        public string Pseudo { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public string Password { get; set; }

    }
}

