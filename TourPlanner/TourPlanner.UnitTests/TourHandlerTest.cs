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

            Assert.True(false);
        }

        [Fact]
        public void GenerateSummarizeReport()
        {
            // problem mit db, schreibt in db soll aber nicht

            //string name = "abc";
            //string start = "hier";
            //string destination = "dort";
            //string transport = "fastest";
            //string description = "abc123";

            //TourTest.GenerateSummarizeReport(name, start, destination, transport, description);

            Assert.True(false);
        }

        [Fact]
        public void EditTourTest()
        {
            // problem mit db, schreibt in db soll aber nicht

            //string name = "abc";
            //string start = "hier";
            //string destination = "dort";
            //string transport = "fastest";
            //string description = "abc123";

            //TourTest.AddTour(name, start, destination, transport, description);

            Assert.True(false);
        }

        [Fact]
        public void GetTourChildFriendlynessTest()
        {
            //public string GetTourChildFriendlyness(Tour tour)
            //{
            //    string childFriendly = "Child friendly Route!";
            //    string notChildFriendly = "Not a Child friendly Route!";
            //    double difficultyAVG = tourPlannerDAO.GetDifficultyAverage(tour.Id);
            //    TimeSpan totalTimeAVG = tourPlannerDAO.GetTimeTotalAverage(tour.Id); //default 00:00

            //    int limitDifficulty = 2;
            //    TimeSpan limitTime = TimeSpan.Parse("03:00"); //3 stunden ist grenze von der zeit
            //    int limitDistance = 300; //300km grenze

            //    if (difficultyAVG > limitDifficulty) { return notChildFriendly; };
            //    if (totalTimeAVG > limitTime) { return notChildFriendly; };
            //    if (tour.Distance > limitDistance) { return notChildFriendly; }

            //    return childFriendly;
            //}

            Assert.True(false);
        }
    }
}