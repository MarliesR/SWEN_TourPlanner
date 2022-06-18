using System;
using System.IO;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    public class ImportTour
    {
        public Tour CreateTour(string line)
        {
            Tour tour = new Tour();

            string[] lineValues = line.Split(':');

            switch (lineValues[0])
            {
                case "TourID:":
                    tour.Id = Convert.ToInt32(lineValues[1]);
                    break;

                case "TourName:":
                    tour.Name = lineValues[1];
                    break;

                case "Start:":
                    tour.Start = lineValues[1];
                    break;

                case "Destination:":
                    tour.Destination = lineValues[1];
                    break;

                case "TransportType:":
                    tour.TransportType = lineValues[1];
                    break;

                case "Distance:":
                    tour.Distance = Convert.ToInt32(lineValues[1]);
                    break;

                case "Description:":
                    tour.Description = lineValues[1];
                    break;

                case "Duration:":
                    tour.Duration = lineValues[1];
                    break;

                case "Image:":
                    tour.Image = lineValues[1];
                    break;
            }

            return tour;
        }

        public Tour ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            Tour tour = new Tour();

            string line;

            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                string[] lineValues = line.Split(':');

                switch (lineValues[0])
                {
                    case "TourID:":
                        tour.Id = Convert.ToInt32(lineValues[1]);
                        break;

                    case "TourName:":
                        tour.Name = lineValues[1];
                        break;

                    case "Start:":
                        tour.Start = lineValues[1];
                        break;

                    case "Destination:":
                        tour.Destination = lineValues[1];
                        break;

                    case "TransportType:":
                        tour.TransportType = lineValues[1];
                        break;

                    case "Distance:":
                        tour.Distance = Convert.ToInt32(lineValues[1]);
                        break;

                    case "Description:":
                        tour.Description = lineValues[1];
                        break;

                    case "Duration:":
                        tour.Duration = lineValues[1];
                        break;

                    case "Image:":
                        tour.Image = lineValues[1];
                        break;
                }
            }

            //close StreamReader

            return tour;
        }
    }
}
