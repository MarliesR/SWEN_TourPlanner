using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Library
{
    public class TourLog
    {
        public int Id { get; set; }
        public int TourId { get; set; } //long int
        public string DateTime { get; set; } //timestamp w/o time zone
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; } //w/o time zone
        public int Rating { get; set; }

        public TourLog(int tourid, string datetime, string comment, int difficulty, TimeSpan totaltime, int rating)
        {
            TourId = tourid;
            DateTime = datetime;
            Comment = comment;
            Difficulty = difficulty;
            TotalTime = totaltime;
            Rating = rating;
        }
    }
}
