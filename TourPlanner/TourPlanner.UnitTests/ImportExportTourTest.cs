using System;
using System.Collections.Generic;
using TourPlanner.BL;
using TourPlanner.Library;
using Xunit;

namespace TourPlanner.UnitTests
{
    public class ImportExportTourTest
    {
        ExportTour exportObjectTest = new ExportTour();
        ImportTour importObjectTest = new ImportTour();


        // ====================================== Export Tests ======================================
        [Fact]
        public void GetDirectoryTest()
        {
            string testPath = exportObjectTest.GetDirectory(@"C:\TestDirectory2");

            Assert.Equal(@"C:\TestDir", testPath);

            // Deleting Directory ??
            //DeleteDirectory();
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
            
        }

        // ====================================== Import Tests ======================================
        [Fact]
        public void CreateTourTest()
        {
            Tour testTour = new Tour();
            testTour = importObjectTest.CreateTour("TourName:Ha");
            Assert.Equal("Ha", testTour.Name);
        }

        [Fact]
        public void ReadFileTest()
        {
            
        }
    }
}
