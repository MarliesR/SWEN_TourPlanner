using System;
using Xunit;
using TourPlanner.Library;
using TourPlanner.DAL.Mapquest;

namespace TourPlanner.UnitTests
{
    public class MapQuestTest
    {
        string start = "Vienna";
        string destination = "Paris";

        [Fact]
        public void TourClassTest()
        {
            string name = "abc";
            string start = "hier";
            string destination = "dort";
            string transport = "fastest";
            string description = "abc123";
            double distance = 123;
            string duration = "123:123:123";
            string image = "abc.png";

            Tour testTour = new Tour(name, start, destination, transport, distance, description, duration, image);

            Assert.Equal(name, testTour.Name);
            Assert.Equal(start, testTour.Start);
            Assert.Equal(destination, testTour.Destination);
            Assert.Equal(transport, testTour.TransportType);
            Assert.Equal(distance, testTour.Distance);
            Assert.Equal(description, testTour.Description);
            Assert.Equal(duration, testTour.Duration);
            Assert.Equal(image, testTour.Image);
        }

        [Fact]
        public void MapQuestDistanceFastTest()
        {
            string transporttype = "fastest";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            double distance = mapquest.GetDistance();

            Assert.Equal(1237.4, distance);
        }

        [Fact]
        public void MapQuestDistanceShortTest()
        {
            string transporttype = "shortest";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            double distance = mapquest.GetDistance();

            Assert.Equal(1189.29, distance);
        }


        [Fact]
        public void MapQuestDistancePedestrianTest()
        {
            string startP = "Dortmund";
            string destinationP = "Cologne";
            string transporttype = "pedestrian";

            Mapquest mapquest = new Mapquest(startP, destinationP, transporttype);
            double distance = mapquest.GetDistance();
            Assert.Equal( 89.4, distance);
        }


        [Fact]
        public void MapQuestDistanceBicycleTest()
        {
            string transporttype = "bicycle";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            double distance = mapquest.GetDistance();

            Assert.Equal(1376.58, distance);
        }

        [Fact]
        public void MapQuestTimeFastTest()
        {
            string transporttype = "fastest";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            string time = mapquest.GetTime();

            Assert.Equal("11:15:53", time);
        }

        [Fact]
        public void MapQuestTimeShortTest()
        {
            string transporttype = "shortest";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            string time = mapquest.GetTime();

            Assert.Equal("12:43:28", time);
        }

        [Fact]
        public void MapQuestTimePedestrianTest()
        {
            string startP = "Dortmund";
            string destinationP = "Cologne";
            string transporttype = "pedestrian";

            Mapquest mapquest = new Mapquest(startP, destinationP, transporttype);
            string time = mapquest.GetTime();

            Assert.Equal("22:13:20", time);

        }

        [Fact]
        public void MapQuestTimeBicycleTest()
        {
            string transporttype = "bicycle";

            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            string time = mapquest.GetTime();

            Assert.Equal("60:57:53", time);

        }
    }
}
