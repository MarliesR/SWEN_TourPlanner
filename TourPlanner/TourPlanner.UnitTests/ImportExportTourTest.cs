﻿using System;
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
        public void SplitStringTest()
        {
            var result = importObjectTest.SplitString("TourName:Ha");
            Assert.Equal("Ha", result);
        }

        // ---- Später anpassen
        //[Fact]
        //public void ReadFileTest()
        //{
        //    Tour testTour = importObjectTest.ReadFile("TourName:Ha");
        //    Assert.Equal("Ha", testTour.Name);
        //}
    }
}
