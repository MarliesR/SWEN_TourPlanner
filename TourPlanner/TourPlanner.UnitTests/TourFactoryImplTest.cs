using System;
using Xunit;
using TourPlanner.Library;
using TourPlanner.BL;

namespace TourPlanner.UnitTests
{
    public class TourFactoryImplTest
    {
        TourPlannerFactoryImpl handler = new TourPlannerFactoryImpl();

        Tour testTour = new Tour { 
            Name = "TestTour1", 
            Start = "Vienna", 
            Destination = "Dortmund", 
            TransportType = "fastest", 
            Distance = 123, 
            Description = "This is my first tour", 
            Duration = "10:10:10", 
            Image = "image.png" 
        };
        TourLog testTourLog = new TourLog(1, "10:10", "This is my first log comment", 3, TimeSpan.Parse("10:10"), 3);

        [Fact]
        public void ValidateNameTrueTest()
        {
            Assert.True(handler.ValidateStringInput(testTour.Name));
            Assert.True(handler.ValidateStringInput(testTour.Start));
            Assert.True(handler.ValidateStringInput(testTour.Destination));
            Assert.True(handler.ValidateStringInput(testTour.Description));
            Assert.True(handler.ValidateStringInput(testTourLog.Comment));
        }

        [Fact]
        public void ValidateNameFalseTest()
        {
            Assert.False(handler.ValidateStringInput("§/SELECThalloINSTERT@//bDELETE@/destination/%/aUPDATE/vienna/#//bCREATE"));
        }

        [Fact]
        public void ValidAddTourCallTrueTest()
        {
            Assert.True(handler.ValidAddTourCall(testTour.Name, testTour.Start, testTour.Destination, testTour.Description));
        }

        [Fact]
        public void ValidAddTourCallFalseTest()
        {
            Assert.False(handler.ValidAddTourCall("#/CREATE", "@/SELECT", "%/INSERT", "§/DELETE"));
        }

        [Fact]
        public void ValidAddTourLogCallTrueTest()
        {
            Assert.True(handler.ValidLogCall(testTourLog));
        }

        [Fact]
        public void ValidAddTourLogCallFalseTest()
        {
            TourLog testTourLogFALSE = new TourLog(1, "§/SELECThalloINSTERT@//bDELETE@/destination/", "%/aUPDATE/vienna/#//bCREATE", 3, testTourLog.TotalTime, 3);
            Assert.False(handler.ValidLogCall(testTourLogFALSE));
        }

        [Fact]
        public void ContainsAnyTrueTest()
        {
            string[] invalidCharas = { "a", "b", "c" };
            string toBeTested = "Hallo :)";
            Assert.True(handler.ContainsAny(toBeTested, invalidCharas));
        }

        [Fact]
        public void ContainsAnyFalseTest()
        {
            string[] invalidCharas = { "!", "%", "?" };
            string toBeTested = "Hallo :)";
            Assert.False(handler.ContainsAny(toBeTested, invalidCharas));
        }


        // ------------- Still figuring out how mock works
        //[Fact]
        //public void AddTourTest()
        //{
        //    handler.AddTour(testTour.Name, testTour.Start, testTour.Destination, testTour.TransportType, testTour.Description);

        //    Assert.True(false);
        //}

        //[Fact]
        //public void AddTourLogTest()
        //{
        //    Assert.True(false);
        //}

        //[Fact]
        //public void GetTourTest()
        //{
        //    Assert.True(false);
        //}

        //[Fact]
        //public void ModifyTourTest()
        //{
        //    Assert.True(false);
        //}

        //[Fact]
        //public void ModifyTourLogEntryTest()
        //{
        //    Assert.True(false);
        //}

        //[Fact]
        //public void DeleteTourTest()
        //{
        //    Assert.True(false);
        //}

        //[Fact]
        //public void DeleteTourLogTest()
        //{
        //    Assert.True(false);
        //}

        [Fact]
        public void GetTourChildFriendlynessTest()
        {
            testTour.Id = 25;
            var result = handler.GetTourChildFriendlyness(testTour);
            Assert.Equal("Child friendly Route!", result);
        }

        //Always Failed --> have to figure out mocking for this
        [Fact]
        public void GetTourNotChildFriendlynessTest()
        {

            testTour.Id = 1;
            var result = handler.GetTourChildFriendlyness(testTour);
            Assert.Equal("Not a Child friendly Route!", result);
        }

        [Fact]
        public void GetPDFFilePathTest()
        {
            PDFGenerator generator = new PDFGenerator();

            var result = generator.GetPDFFilePath(testTour.Name);

            Assert.IsType<String>( result);
        }


        // ------------- problem mit ObservableCollection<TourLog>
        //[Fact]
        //public void GenerateSummarizeReportTest()
        //{
        //    testTour.Id = 1;

        //    TimeSpan testTimeSpan = new TimeSpan();
        //    TourLog testTourLog = new TourLog(1, "123:123:123", "abc123", 2, testTimeSpan, 2);

        //    var result = handler.GenerateSummarizeReport(testTour, testTourLog );


        //    Assert.True(result);
        //}
    }
}