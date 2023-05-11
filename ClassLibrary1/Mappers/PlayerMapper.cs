using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO.Player;
using Domain.Forms.Player;
using Domain.DTO.Tournament;

namespace Domain.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerDTO ToPlayerDTO(this PlayerModel model)
        {
            return new PlayerDTO(
                model.PlayerId,
                model.Pseudo,
                model.Email,
                model.Birthdate
                );
        }
        public static IEnumerable<PlayerDTO> ToPlayerDTOList(this IEnumerable<PlayerModel> models)
        {
            foreach (var model in models)
            {
                yield return model.ToPlayerDTO();
            }
        }

        public static PlayerModel ToPlayerModel(this CreatePlayerForm createForm)
        {
            return new PlayerModel(
                createForm.Pseudo,
                createForm.Email,                
                createForm.Birthdate,
                createForm.Password
                );
        }
    }
}
