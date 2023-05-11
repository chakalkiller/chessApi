using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Player
{
    public class PlayerDTO
    {
        public PlayerDTO(int playerId, string pseudo, string email, DateTime birthdate)
        {
            PlayerId = playerId;
            Pseudo = pseudo;           
            Email = email;
            Birthdate = birthdate;
        }

        public int PlayerId { get; set; }

        public string Pseudo { get; set; }
       

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }
    }
}

