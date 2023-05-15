using DAL.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly IDbConnection _Connection;

        public TournamentRepository(IDbConnection connection)
        {
            _Connection = connection;
        }

        private TournamentModel Convert(IDataRecord record)
        {
            return new TournamentModel(
                (int)record["Tournament_Id"],
                (string)record["Tournament_Name"],               
                (string)record["Description"],                
                (int)record["Max_Player"],
                (bool)record["Tournament_State"],
                (int)record["UserID_creator"]
            );
        }

        private void AddParameter(IDbCommand command, string name, object data)
        {
            IDbDataParameter cmdNom = command.CreateParameter();
            cmdNom.ParameterName = name;
            cmdNom.Value = data ?? DBNull.Value;
            command.Parameters.Add(cmdNom);
        }

        public IEnumerable<TournamentModel> GetAll()
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "SELECT * FROM [Tournament];";
            command.CommandType = CommandType.Text;

            _Connection.Open();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Convert(reader);
                }
            }

            _Connection.Close();
        }

       

        public TournamentModel? GetById(int tournamentId)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "SELECT * FROM [Tournament] WHERE [Tournament_Id] = @ArnaudOrAlex;";
            command.CommandType = CommandType.Text;

            AddParameter(command, "ArnaudOrAlex", tournamentId);

            TournamentModel? player = null;

            _Connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    player = Convert(reader);
                }
            }
            _Connection.Close();

            return player;
        }

        public TournamentModel? Create(TournamentModel tournament,int id)
        {
            string query = "INSERT INTO [Tournament] (Tournament_Name, Description, Max_Player,UserID_creator )"
                + " OUTPUT [inserted].*"
                + " VALUES(@TournamentName, @Description, @MaxPlayer,@UserID_creator)";

            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = query;
            command.CommandType= CommandType.Text;

            AddParameter(command, "TournamentName", tournament.TournamentName);
            AddParameter(command, "Description", tournament.Description);
            AddParameter(command, "Maxplayer", tournament.MaxPlayer);
            AddParameter(command, "UserID_creator", id);



            TournamentModel? tournamentCreated = null;

            try
            {
                _Connection.Open();
                using(IDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        tournamentCreated = Convert(reader);
                    }
                }
            }
            finally
            {
                _Connection.Close();
            }

            return tournamentCreated;
        }

        public bool Delete(int tournamentId)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "DELETE FROM [Tournament] WHERE [Tournament_Id] = @tournamentId";
            command.CommandType = CommandType.Text;

            AddParameter(command, "TournamentId", tournamentId);

            _Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            _Connection.Close();

            return nbRow == 1;
        }

        public bool OpenRegister(int TournamentId, int PlayerId)
        {
            
                string query = "UPDATE [Tournament]" +
                    " SET [Tournament_State] = 1 " +
                    " WHERE [Tournament_Id] = @Tournament_Id AND [UserId_creator] = @UserId_creator;";
                IDbCommand command = _Connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                AddParameter(command, "Tournament_Id", TournamentId);
                AddParameter(command, "UserId_creator", PlayerId);
                _Connection.Open();
                int nbRow = command.ExecuteNonQuery();
                _Connection.Close();
                return nbRow == 1;
            
        }
    }
}
