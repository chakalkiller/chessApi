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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _Connection;

        public PlayerRepository(IDbConnection connection)
        {
            _Connection = connection;
        }

        private PlayerModel Convert(IDataRecord record)
        {
            return new PlayerModel(
                (int)record["Player_Id"],
                (string)record["Pseudo"],               
                (string)record["Email"],                
                (DateTime)record["Birthdate"],
                (string)record["Password_Hash"]
            );
        }

        private void AddParameter(IDbCommand command, string name, object data)
        {
            IDbDataParameter cmdEmail = command.CreateParameter();
            cmdEmail.ParameterName = name;
            cmdEmail.Value = data ?? DBNull.Value;
            command.Parameters.Add(cmdEmail);
        }

        public IEnumerable<PlayerModel> GetAll()
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "SELECT * FROM [PlayerApp];";
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

        public PlayerModel? GetByEmail(string email)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "SELECT * FROM [PlayerApp] WHERE [Email] = @Email;";
            command.CommandType = CommandType.Text;

            //IDbDataParameter cmdEmail = command.CreateParameter();
            //cmdEmail.ParameterName = "Email";
            //cmdEmail.Value = email;
            //command.Parameters.Add(cmdEmail);

            AddParameter(command, "Email", email);

            PlayerModel? player = null;

            _Connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if(reader.Read())
                {
                    player = Convert(reader);
                }
            }
            _Connection.Close();

            return player;
        }

        public PlayerModel? GetById(int id)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "SELECT * FROM [PlayerApp] WHERE [Player_Id] = @ArnaudOrAlex;";
            command.CommandType = CommandType.Text;

            AddParameter(command, "ArnaudOrAlex", id);

            PlayerModel? player = null;

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

        public PlayerModel? Create(PlayerModel player)
        {
            string query = "INSERT INTO [PlayerApp] (Pseudo, Email, Birthdate , Password_Hash)"
                + " OUTPUT [inserted].*"
                + " VALUES(@Pseudo, @Email, @Birthdate,  @Password)";

            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = query;
            command.CommandType= CommandType.Text;

            AddParameter(command, "Pseudo", player.Pseudo);
            AddParameter(command, "Email", player.Email);
            AddParameter(command, "Birthdate", player.Birthdate);
            AddParameter(command, "Password", player.Password);

            PlayerModel? playerCreated = null;

            try
            {
                _Connection.Open();
                using(IDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        playerCreated = Convert(reader);
                    }
                }
            }
            finally
            {
                _Connection.Close();
            }

            return playerCreated;
        }

        public bool ChangePassword(int playerId, string password)
        {
            string query = "UPDATE [PlayerApp]" +
                " SET [Password_Hash] = @Password" +
                " WHERE [Player_Id] = @PlayerId";

            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = query;
            command.CommandType= CommandType.Text;

            AddParameter(command, "PlayerId", playerId);
            AddParameter(command, "Password", password);

            _Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            _Connection.Close();

            return nbRow == 1;
        }

        public bool Delete(int playerId)
        {
            IDbCommand command = _Connection.CreateCommand();
            command.CommandText = "DELETE FROM [UserApp] WHERE [Player_Id] = @Id";
            command.CommandType = CommandType.Text;

            AddParameter(command, "PlayerIdId", playerId);

            _Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            _Connection.Close();

            return nbRow == 1;
        }
    }
}
