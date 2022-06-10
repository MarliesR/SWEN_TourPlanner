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
        public void AddTourTest()
        {
            handler.AddTour(testTour.Name, testTour.Start, testTour.Destination, testTour.TransportType, testTour.Description);

            Assert.True(false);
        }

        [Fact]
        public void EditTourTest()
        {
            Assert.True(false);
        }

        [Fact]
        public void GetTourChildFriendlynessTest()
        {
            testTour.Id = 1;
            var result = handler.GetTourChildFriendlyness(testTour);
            Assert.Equal("Child friendly Route!", result);
        }

        [Fact]
        public void GetTourNotChildFriendlynessTest()
        {
            testTour.Id = 1;
            var result = handler.GetTourChildFriendlyness(testTour);
            Assert.Equal("ot a Child friendly Route!", result);
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