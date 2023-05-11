using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Domain.DTO.Tournament;
using Domain.Forms.TournamentForm;

namespace Domain.Mappers
{
    public static class TournamentMapper
    {
        public static TournamentDTO ToTournamentDTO(this TournamentModel model)
        {
            return new TournamentDTO(
                model.TournamentId,
                model.TournamentName,
                model.Description,
                model.MaxPlayer,
                model.TournamentState
                );
        }
        public static IEnumerable<TournamentDTO> ToTournamentDTOList(this IEnumerable<TournamentModel> models)
        {
            foreach (var model in models)
            {
                yield return model.ToTournamentDTO();
            }
        }

        public static TournamentModel ToTournamentModel(this CreateTournamentForm createForm)
        {
            return new TournamentModel(
                createForm.TournamentName,   
                null,
                createForm.MaxPlayer,
              false
                );
        }
    }
}
