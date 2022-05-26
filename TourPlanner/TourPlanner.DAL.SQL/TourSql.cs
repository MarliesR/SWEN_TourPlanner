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
    public class TourSql
    {
        //hier alle sql statements zur tour eingeben? 

        public string connectionString { get; set; }
        public TourSql()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["postgreSQLConnectionStringDB"];
            connectionString = settings.ConnectionString;
        }
        
        public void AddTourSQL(Tour TourData)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@$"INSERT INTO Tour (name, description, start, destination, transport, distance, duration, image) 
                                                VALUES(@name, @description, @start, @destination, @transport, @distance, @duration, @image)", conn);

                cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, TourData.Name);
                cmd.Parameters.AddWithValue("description", NpgsqlDbType.Text, TourData.Description);
                cmd.Parameters.AddWithValue("start", NpgsqlDbType.Varchar, TourData.Start);
                cmd.Parameters.AddWithValue("distance", NpgsqlDbType.Integer, TourData.Distance);
                cmd.Parameters.AddWithValue("destination", NpgsqlDbType.Varchar, TourData.Destination);
                cmd.Parameters.AddWithValue("transport", NpgsqlDbType.Varchar, TourData.TransportType);
                cmd.Parameters.AddWithValue("duration", NpgsqlDbType.Varchar, TourData.Duration);
                cmd.Parameters.AddWithValue("image", NpgsqlDbType.Varchar, TourData.Image);

                cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Tour GetTourSQL(int TourId)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM Tour WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Varchar, TourId);

            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            else
            {
                reader.Read();

                var name = reader.GetString(reader.GetOrdinal("name"));
                var description = reader.GetString(reader.GetOrdinal("description"));
                var start = reader.GetString(reader.GetOrdinal("start"));
                var destination = reader.GetString(reader.GetOrdinal("destination"));
                var transportType = reader.GetString(reader.GetOrdinal("transport"));
                var distance = reader.GetInt32(reader.GetOrdinal("distance"));
                var duration = reader.GetString(reader.GetOrdinal("description"));
                var image = reader.GetString(reader.GetOrdinal("image"));

                var tourData = new Tour(name, start, destination, transportType, distance, description, duration, image)
                {
                    Id = TourId
                };


                return tourData;
            }
        }

        public void DeleteTourSQL(int TourId)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@$"DELETE FROM CustomersTour WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Varchar, TourId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Tour> GetToursSQL()
        {
            
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            List<Tour> tourlist = new();

            using var cmd = new NpgsqlCommand("SELECT * FROM Tour", conn);


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
                    var name = reader.GetString(reader.GetOrdinal("name"));
                    var description = reader.GetString(reader.GetOrdinal("description"));
                    var start = reader.GetString(reader.GetOrdinal("start"));
                    var destination = reader.GetString(reader.GetOrdinal("destination"));
                    var transportType = reader.GetString(reader.GetOrdinal("transport"));
                    var distance = reader.GetInt32(reader.GetOrdinal("distance"));
                    var duration = reader.GetString(reader.GetOrdinal("duration"));
                    var image = reader.GetString(reader.GetOrdinal("image"));

                    Tour tourData = new Tour(name, start, destination, transportType, distance, description, duration, image);
                    tourData.Id = id;
                    tourlist.Add(tourData);
                }
            }
            conn.Close();
            return tourlist;
        }
    }
}
