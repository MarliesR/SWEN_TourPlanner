using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;

namespace TourPlanner.DAL.SQL
{
    class TourSql
    {
        //hier alle sql statements zur tour eingeben? 

        public string connectionString { get; set; }
        public TourSql()
        {
            connectionString = ConfigurationManager.AppSettings["postgreSQLConnectionStringDB"];
        }
        
        public void AddTourSQL(Tour TourData)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@$"INSERT INTO Tour
                    (
                        name,
                        description,
                        start,
                        destination,
                        transport,
                        distance,
                        duration,
                        image
                    )", conn);

            cmd.Parameters.AddWithValue("name", TourData.Name);
            cmd.Parameters.AddWithValue("description", TourData.Description);
            cmd.Parameters.AddWithValue("start", TourData.Start);
            cmd.Parameters.AddWithValue("destination", TourData.Destination);
            cmd.Parameters.AddWithValue("transport", TourData.TransportType);
            cmd.Parameters.AddWithValue("duration", TourData.Duration);
            cmd.Parameters.AddWithValue("image", TourData.Image);

            cmd.ExecuteNonQuery();
            
        }
    }
}
