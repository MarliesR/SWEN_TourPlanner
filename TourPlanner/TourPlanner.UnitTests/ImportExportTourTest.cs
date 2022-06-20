using System;
using System.Collections.Generic;
using System.IO;
using TourPlanner.BL;
using TourPlanner.Library;
using Xunit;

namespace TourPlanner.UnitTests
{
    public class ImportExportTourTest
    {
        Tour testTour = new Tour
        {
            Id = 5,
            Name = "TestTour1",
            Start = "Vienna",
            Destination = "Dortmund",
            TransportType = "fastest",
            Distance = 123,
            Description = "This is my first tour",
            Duration = "10:10:10",
            Image = "image.png"
        };
        ExportTourLogic exportObjectTest = new ExportTourLogic();
        ImportTourLogic importObjectTest = new ImportTourLogic();


        // ====================================== Export Tests ======================================
        [Fact]
        public void GetDirectoryTest()
        {
            string testPath = exportObjectTest.GetDirectory(@"C:\TestDir");

            Assert.Equal(@"C:\TestDir", testPath);
            Directory.Exists(testPath);
        }

        [Fact]
        public void GetFullPathTest()
        {
            string testPath = exportObjectTest.GetFullFilePath(@"C:\TestDir", "TestFile");

            Assert.Equal(@"C:\TestDir\TestFile.txt", testPath);
        }

        [Fact]
        public void WriteTourIntoFileTest()
        {
            exportObjectTest.WriteTourIntoFile(testTour);
            bool result = File.Exists(@"C:\TestDir\TestTour1.txt");

            Assert.True(result);
        }


        // ====================================== Import Tests ======================================
        [Fact]
        public void DoesFileExistTest()
        {
            var result = importObjectTest.DoesFileExist(@"C:\TestDir\TestTour1.txt");
            Assert.True(result);
        }

        [Fact]
        public void DoesFileExistFalseTest()
        {
            var result = importObjectTest.DoesFileExist(@"C:\TestDir\Test.txt");
            Assert.False(result);
        }

        [Fact]
        public void SplitStringTest()
        {
            var result = importObjectTest.SplitString("TourName=Ha");
            Assert.Equal("Ha", result);
        }

        [Fact]
        public void SplitStringFailTest()
        {
            var result = importObjectTest.SplitString("TourName=Ha");
            Assert.NotEqual("TourName", result);
        }

        [Fact]
        public void ReadFileTest()
        {
            Tour readTourTest = importObjectTest.ReadFile(@"C:\TestDir\TestTour1.txt");

            Assert.Equal(testTour.Id, readTourTest.Id);
            Assert.Equal(testTour.Name, readTourTest.Name);
            Assert.Equal(testTour.Start, readTourTest.Start);
            Assert.Equal(testTour.Destination, readTourTest.Destination);
            Assert.Equal(testTour.TransportType, readTourTest.TransportType);
            Assert.Equal(testTour.Distance, readTourTest.Distance);
            Assert.Equal(testTour.Description, readTourTest.Description);
            Assert.Equal(testTour.Duration, readTourTest.Duration);
            Assert.Equal(testTour.Image, readTourTest.Image);
        }
    }
}
