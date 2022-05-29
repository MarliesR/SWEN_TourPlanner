using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;

namespace TourPlanner.DAL.SQL
{
    public class LogSql
    {
        public string connectionString { get; set; }
        public LogSql()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["postgreSQLConnectionStringDB"];
            connectionString = settings.ConnectionString;
        }

        public List<TourLog> GetLogsSQL(int tourId)
        {

            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            List<TourLog> loglist = new();

            using var cmd = new NpgsqlCommand("SELECT * FROM tourlog WHERE tourid=@id", conn);
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, tourId);


            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(reader.GetOrdinal("id"));
                    var tourid = reader.GetInt32(reader.GetOrdinal("tourid"));
                    var datetime = reader.GetString(reader.GetOrdinal("datetime"));
                    var comment = reader.GetString(reader.GetOrdinal("comment"));
                    var difficulty = reader.GetInt32(reader.GetOrdinal("difficulty"));
                    var totaltime = reader.GetString(reader.GetOrdinal("totaltime"));
                    var rating = reader.GetInt32(reader.GetOrdinal("rating"));
            

                    TourLog logData = new TourLog(tourid, datetime.ToString(), comment, difficulty, totaltime.ToString(), rating);
                    logData.Id = id;
                    loglist.Add(logData);
                }
            }
            conn.Close();
            return loglist;
        }

        public void AddLogSQL(TourLog log)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@$"INSERT INTO Tourlog (tourid, datetime, comment, difficulty, totaltime, rating) 
                                                VALUES(@tourid, @datetime, @comment, @difficulty, @totaltime, @rating)", conn);

            cmd.Parameters.AddWithValue("tourid", NpgsqlDbType.Integer, log.TourId);
            cmd.Parameters.AddWithValue("datetime", NpgsqlDbType.Varchar, log.DateTime);
            cmd.Parameters.AddWithValue("comment", NpgsqlDbType.Varchar, log.Comment);
            cmd.Parameters.AddWithValue("difficulty", NpgsqlDbType.Integer, log.Difficulty);
            cmd.Parameters.AddWithValue("totaltime", NpgsqlDbType.Varchar, log.TotalTime);
            cmd.Parameters.AddWithValue("rating", NpgsqlDbType.Integer, log.Rating);
           

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public bool DeleteLogSQL(int logId)
        {
            int rowsAffected;
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@$"DELETE FROM Tourlog WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, logId);

            rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
