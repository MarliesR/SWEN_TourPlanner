using System;
using Xunit;
using TourPlanner.Library;
using TourPlanner.BL;

namespace TourPlanner.UnitTests
{
    public class TourFactoryImplTest
    {
        TourPlannerFactoryImpl handler = new TourPlannerFactoryImpl();

        Tour testTour = new Tour("abc", "hier", "dort", "fastest", 123, "abc123", "123:123:123", "abc.png");

        [Fact]
        public void ValidateNameTrueTest()
        {
            Assert.True(handler.ValidateStringInput("hallo"));
        }

        [Fact]
        public void ValidateNameFalseTest()
        {
            Assert.False(handler.ValidateStringInput("INSTERT@//b"));
        }

        [Fact]
        public void ValidateStartTrueTest()
        {
            Assert.True(handler.ValidateStringInput("Vienna"));
        }

        [Fact]
        public void ValidateStartFalseTest()
        {
            Assert.False(handler.ValidateStringInput("DELETE@//"));
        }

        [Fact]
        public void ValidateDestinationTrueTest()
        {
            Assert.True(handler.ValidateStringInput("Berlin"));
        }

        [Fact]
        public void ValidateDestinationFalseTest()
        {
            Assert.False(handler.ValidateStringInput("@//bSELECT"));
        }

        [Fact]
        public void ValidateDescriptionTrueTest()
        {
            Assert.True(handler.ValidateStringInput("This is my first tour"));
        }

        [Fact]
        public void ValidateDescriptionFalseTest()
        {
            Assert.False(handler.ValidateStringInput("@//bINSERT"));
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
            testTour.Id = 1;
            var result = handler.GetTourChildFriendlyness(testTour);
            Assert.Equal("Child friendly Route!", result);
        }

        //Always Failed --> have to figure out mocking for this
        [Fact]
        public void GetTourNotChildFriendlynessTest()
        {

            testTour.Id = 8;
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