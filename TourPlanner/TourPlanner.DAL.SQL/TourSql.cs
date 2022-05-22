﻿using Npgsql;
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
            Console.WriteLine(connectionString);
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

        public Tour GetTourSQL(string TourName, int TourId)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM Tour WHERE uname=@name AND id=@id", conn);

            cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, TourName);
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Varchar, TourId);

            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            else
            {
                reader.Read();

                var description = reader.GetString(reader.GetOrdinal("description"));
                var start = reader.GetString(reader.GetOrdinal("start"));
                var destination = reader.GetString(reader.GetOrdinal("destination"));
                var transportType = reader.GetString(reader.GetOrdinal("transport"));
                var distance = reader.GetInt32(reader.GetOrdinal("distance"));
                var duration = reader.GetString(reader.GetOrdinal("description"));
                var image = reader.GetString(reader.GetOrdinal("image"));

                var tourData = new Tour(TourName, start, destination, transportType, distance, description, duration, image)
                {
                    Id = TourId
                };


                return tourData;
            }
        }
    }
}
