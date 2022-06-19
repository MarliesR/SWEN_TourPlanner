using System;
using System.IO;
using TourPlanner.Library;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.BL
{
    public class ExportTourLogic
    {
        public string GetDirectory(string path)
        {
            // See if folder exists
            if (Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");
                return path;
            }

            // create folder
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            // add folder to path
            string newPath = di.FullName;

            // FOR TESTING 
            Console.WriteLine(newPath);

            return newPath;
        }

        public string GetFullFilePath(string dirPath, string tourName)
        {
            string pathPart1 = GetDirectory(dirPath);
            string fullPath = pathPart1 + @"\" + tourName + ".txt";

            // FOR TESTING
            Console.WriteLine(fullPath);

            return fullPath;
        }

        public void WriteTourIntoFile(Tour tour)
        {
            // get path
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsetting.json", false, true).Build();
            string folderPath = config["Folderpath:TourFiles"];
            string path = GetFullFilePath(folderPath, tour.Name);

            // create file 
            StreamWriter outputFile = new StreamWriter(path);

            outputFile.WriteLine($"TourID={tour.Id}");
            outputFile.WriteLine($"TourName={tour.Name}");
            outputFile.WriteLine($"Start={tour.Start}");
            outputFile.WriteLine($"Destination={tour.Destination}");
            outputFile.WriteLine($"TransportType={tour.TransportType}");
            outputFile.WriteLine($"Distance={tour.Distance}");
            outputFile.WriteLine($"Description={tour.Description}");
            outputFile.WriteLine($"Duration={tour.Duration}");
            outputFile.WriteLine($"Image={tour.Image}");

            // close StreamWriter
            outputFile.Close();
        }

        public void DeleteDirectory(string path)
        {
            //DirectoryInfo dir = Directory.SetCurrentDirectory(path);
        }
    }
}
