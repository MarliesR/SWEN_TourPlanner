using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;

namespace TourPlanner.DAL.SQL
{
    public class Database
    {
        private string connectionString;
        public Database()
        {
            //get connections string form app.config file
            string name = "postgreSQLConnectionString";

            connectionString = null;

            //read from app.config where name = postgreSQL..
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
            {
                connectionString = settings.ConnectionString;
                Console.WriteLine(connectionString);
                ConnectDBServer();
                Console.WriteLine("success!");
            }
            else
            {
                Console.WriteLine("failed to get connString!");
            }
        }


        private void ConnectDBServer()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            if (!DatabaseExists())
            {
                CreateDB();

            }
            connectionString += "Database=tourplanner;";
            CreateTourTable();
            CreateTourLogTable();

        }


        private bool DatabaseExists()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            using var cmdChek = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname='tourplanner'", conn);
            var dbExists = cmdChek.ExecuteScalar() != null;
            if (dbExists)
            {
                return true;
            }
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

            Console.WriteLine("created Database tourplanner!");

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
                        distance integer,
                        duration character varying(40),
                        image character varying,
                        PRIMARY KEY (id)
                    )", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateTourLogTable()
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(@$"CREATE TABLE IF NOT EXISTS public.tourlog
                    (
                        id serial NOT NULL,
                        tourid bigint NOT NULL,
                        datetime timestamp without time zone[],
                        comment text,
                        difficulty integer,
                        totaltime time without time zone[],
                        rating integer,
                        PRIMARY KEY (id)
                    )", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }


    
}
}
