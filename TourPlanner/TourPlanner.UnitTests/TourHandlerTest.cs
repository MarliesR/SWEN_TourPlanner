using System;
using Xunit;
using TourPlanner.Library;
using TourPlanner.BL;

namespace TourPlanner.UnitTests
{
    public class TourHandlerTest
    {
        ITourPlannerFactory tourPlannerFactory = TourPlannerFactory.GetInstance();

        [Fact]
        public void AddTourTest()
        {
            // problem mit db, schreibt in db soll aber nicht


            //tourPlannerFactory.AddTour()


            //string name = "abc";
            //string start = "hier";
            //string destination = "dort";
            //string transport = "fastest";
            //string description = "abc123";

            //TourTest.AddTour(name, start, destination, transport, description);
        }

        public void DeleteTourTest()
        {
            // problem mit db, schreibt in db soll aber nicht

            //string name = "abc";
            //string start = "hier";
            //string destination = "dort";
            //string transport = "fastest";
            //string description = "abc123";

            //TourTest.AddTour(name, start, destination, transport, description);
        }

        public void EditTourTest()
        {
            // problem mit db, schreibt in db soll aber nicht

            //string name = "abc";
            //string start = "hier";
            //string destination = "dort";
            //string transport = "fastest";
            //string description = "abc123";

            //TourTest.AddTour(name, start, destination, transport, description);
        }
    }
}