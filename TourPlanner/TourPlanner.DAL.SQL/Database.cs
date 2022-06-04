using System;
using System.Collections.Generic;
using Npgsql;
using TourPlanner.Library;
using NpgsqlTypes;
using Microsoft.Extensions.Configuration;


namespace TourPlanner.DAL.SQL
{
    public class Database : IDataAccesss
    {
        private string connectionString;
        public Database()
        {
           
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", false, true).Build();
            var constring = config["Database:ConnectionString"];
            Console.WriteLine(constring);

            if (constring != null)
            {
                connectionString = constring;
                if (!DatabaseExists())
                {
                    CreateDB();
                }
                connectionString = config["Database:ConnectionStringDB"];
                CreateTourTable();
                CreateTourLogTable();
            }
            else
            {
                Console.WriteLine("failed to get connString!");
            }
        }


        private bool DatabaseExists()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            using var cmdChek = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname='tourplanner'", conn);
            var dbExists = cmdChek.ExecuteScalar() != null;
            if (dbExists)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        private void CreateDB()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(@$"CREATE DATABASE tourplanner ENCODING = 'UTF8'", conn))
            {
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        private void CreateTourTable()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(@$"CREATE TABLE IF NOT EXISTS public.tour
                    (
                        id serial NOT NULL,
                        name character varying(100) NOT NULL,
                        description text,
                        start character varying(40),
                        destination character varying(40),
                        transport character varying(40),
                        distance double precision,
                        duration character varying(40),
                        image character varying,
                        PRIMARY KEY (id)
                    )", conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        private void CreateTourLogTable()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(@$"CREATE TABLE IF NOT EXISTS public.tourlog
                    (
                        id serial NOT NULL,
                        tourid bigint NOT NULL,
                        datetime character varying,
                        comment text,
                        difficulty integer,
                        totaltime interval,
                        rating integer,
                        PRIMARY KEY (id)
                    )", conn))
            {
                cmd.ExecuteNonQuery();
            }
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
                    var distance = reader.GetDouble(reader.GetOrdinal("distance"));
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

        public TimeSpan GetTimeTotalAverage(int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            TimeSpan avg = TimeSpan.Parse("00:00");
            using var cmd = new NpgsqlCommand("SELECT AVG(totaltime) FROM tourlog WHERE tourid = @tourid;", conn);
            cmd.Parameters.AddWithValue("tourid", NpgsqlDbType.Integer, id);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["avg"]))
                    {
                        avg = reader.GetTimeSpan(reader.GetOrdinal("avg"));
                    }
                }
            }
            conn.Close();
            return avg;
        }

        public double GetDifficultyAverage(int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            double avg = 0;
            using var cmd = new NpgsqlCommand("SELECT AVG(difficulty) FROM tourlog WHERE tourid = @tourid;", conn);
            cmd.Parameters.AddWithValue("tourid", NpgsqlDbType.Integer, id);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["avg"]))
                    {
                        avg = reader.GetInt32(reader.GetOrdinal("avg"));
                    }
                }
            }
            conn.Close();
            return avg;
        }

        public int GetTourPoularitySQL(int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            int popularityRate = 0;
            using var cmd = new NpgsqlCommand("SELECT tourid, RANK() OVER(Order by COUNT(id) DESC) popularity FROM tourlog GROUP BY tourid; ", conn);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var tourid = reader.GetInt32(reader.GetOrdinal("tourid"));
                    var popularity = reader.GetInt32(reader.GetOrdinal("popularity"));
                    if (tourid.Equals(id))
                    {
                        popularityRate = popularity;
                    }
                }
            }

            conn.Close();
            return popularityRate;
        }

        public void UpdateTourSQL(string tourname, string tourdescription, int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(@$"UPDATE Tour SET name=@name, description=@description WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, tourname);
            cmd.Parameters.AddWithValue("description", NpgsqlDbType.Varchar, tourdescription);
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteTourSQL(int TourId)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(@$"DELETE FROM Tour WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, TourId);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public Tour GetTourSQL(int TourId)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM Tour WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, TourId);

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
                var distance = reader.GetDouble(reader.GetOrdinal("distance"));
                var duration = reader.GetString(reader.GetOrdinal("description"));
                var image = reader.GetString(reader.GetOrdinal("image"));

                var tourData = new Tour(name, start, destination, transportType, distance, description, duration, image)
                {
                    Id = TourId
                };


                return tourData;
            }
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
            cmd.Parameters.AddWithValue("distance", NpgsqlDbType.Double, TourData.Distance);
            cmd.Parameters.AddWithValue("destination", NpgsqlDbType.Varchar, TourData.Destination);
            cmd.Parameters.AddWithValue("transport", NpgsqlDbType.Varchar, TourData.TransportType);
            cmd.Parameters.AddWithValue("duration", NpgsqlDbType.Varchar, TourData.Duration);
            cmd.Parameters.AddWithValue("image", NpgsqlDbType.Varchar, TourData.Image);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void UpdateLogSQL(TourLog log)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(@$"UPDATE Tourlog SET datetime=@datetime, comment=@comment, difficulty=@difficulty, totaltime=@totaltime, rating=@rating WHERE id=@id", conn);


            cmd.Parameters.AddWithValue("datetime", NpgsqlDbType.Varchar, log.DateTime);
            cmd.Parameters.AddWithValue("comment", NpgsqlDbType.Varchar, log.Comment);
            cmd.Parameters.AddWithValue("difficulty", NpgsqlDbType.Integer, log.Difficulty);
            cmd.Parameters.AddWithValue("totaltime", NpgsqlDbType.Interval, log.TotalTime);
            cmd.Parameters.AddWithValue("rating", NpgsqlDbType.Integer, log.Rating);
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, log.Id);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void DeleteAllLogsOfTourSQL(int tourid)
        {
            int rowsAffected;
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@$"DELETE FROM Tourlog WHERE tourid=@id", conn);

            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, tourid);

            rowsAffected = cmd.ExecuteNonQuery();

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
            cmd.Parameters.AddWithValue("totaltime", NpgsqlDbType.Interval, log.TotalTime);
            cmd.Parameters.AddWithValue("rating", NpgsqlDbType.Integer, log.Rating);


            cmd.ExecuteNonQuery();

            conn.Close();
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
                    var totaltime = reader.GetTimeSpan(reader.GetOrdinal("totaltime"));
                    var rating = reader.GetInt32(reader.GetOrdinal("rating"));


                    TourLog logData = new TourLog(tourid, datetime.ToString(), comment, difficulty, totaltime, rating);
                    logData.Id = id;
                    loglist.Add(logData);
                }
            }
            conn.Close();
            return loglist;
        }
    }
}
