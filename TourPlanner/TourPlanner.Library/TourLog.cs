using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Library
{
    class TourLog
    {
        public int Id { get; set; }
        public int TourId { get; set; } //long int
        public string DateTime { get; set; } //timestamp w/o time zone
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public string TotalTime { get; set; } //w/o time zone
        public int Rating { get; set; }
    }
}
