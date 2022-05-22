using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel 
    {
        public Tour Tour { get; }

        public AddTourViewModel(Tour TourData)
        {
            Tour = TourData;
            // objekt mit tour daten

            // Check if data valid 
            // maybe transform data
        }
        
    }

}
