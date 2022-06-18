using System;
using System.IO;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    public class ImportTour
    {
        public string SplitString(string line)
        {
            string[] lineValues = line.Split(':');

            return lineValues[1];
        }

        public Tour ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            Tour readTour = new Tour();

            string line;
            string result;
            int count = 1;

            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                result = SplitString(line);

                if (count == 0) { readTour.Id = Convert.ToInt32(result); }
                if (count == 1) { readTour.Name = result; }
                if (count == 2) { readTour.Start = result; }
                if (count == 3) { readTour.Destination = result; }
                if (count == 4) { readTour.TransportType = result; }
                if (count == 5) { readTour.Distance = Convert.ToInt32(result); }
                if (count == 6) { readTour.Description = result; }
                if (count == 7) { readTour.Duration = result; }
                if (count == 8) { readTour.Image = result; }

                count++;
            }

            //close StreamReader

            return readTour;
        }
    }
}
