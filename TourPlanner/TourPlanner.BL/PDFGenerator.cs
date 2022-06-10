using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TourPlanner.Library;


namespace TourPlanner.BL
{
    public class PDFGenerator
    {
        private string folderPath;
        public PDFGenerator()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsetting.json", false, true).Build();
            folderPath = config["Folderpath:PDF"];
        }

        public bool SummarizeReport(string tourname, ObservableCollection<TourLog> loglist, string timeAVG)
        {
            
            string pdfFilePath = GetPDFFilePath(tourname);
            Console.WriteLine(pdfFilePath);

            PdfWriter writer = new PdfWriter(pdfFilePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //Header of the document
            Paragraph reportHeader = new Paragraph("Specialreport of route: " + tourname + " \ndate: " + DateTime.Now.ToString("g"))
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(reportHeader);

            if (loglist.Count != 0)
            {
                double ratingAVG = loglist.Average(x => x.Rating);
                double difficultyAVG = loglist.Average(x => x.Difficulty);


                //tour details Header
                Paragraph routeDetailsHeader = new Paragraph("Details:")
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(12)
                        .SetBold()
                        .SetFontColor(ColorConstants.BLACK);
                document.Add(routeDetailsHeader);

                Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth()
                 .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                 .SetFontSize(12);
                table.AddHeaderCell("Average Time");
                table.AddHeaderCell("Average Rating");
                table.AddHeaderCell("Average Difficulty");
                table.AddCell(timeAVG);
                table.AddCell(ratingAVG.ToString());
                table.AddCell(difficultyAVG.ToString());
                document.Add(table);

                Paragraph logHeader = new Paragraph("Current logs:")
                       .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                       .SetFontSize(12)
                       .SetBold()
                       .SetFontColor(ColorConstants.BLACK);
                document.Add(logHeader);

                Table logtable = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth()
                 .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                 .SetFontSize(12);
                logtable.AddCell("Date and Time of Log");
                logtable.AddCell("Total Time:");
                logtable.AddCell("Difficulty (1-easy, 5-very hard");
                logtable.AddCell("Rating (1-good, 5-very bad)");
                logtable.AddCell("Comment");
                foreach (TourLog log in loglist)
                {
                    logtable.AddCell(log.DateTime);
                    logtable.AddCell(log.TotalTime.ToString());
                    logtable.AddCell(log.Difficulty.ToString());
                    logtable.AddCell(log.Rating.ToString());
                    logtable.AddCell(log.Comment);
                }
                document.Add(logtable);
            }
            else
            {
                Paragraph noLogs = new Paragraph("no logs available")
                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                   .SetFontSize(12);
                document.Add(noLogs);
            }

            document.Close();

            return true;
        }


        public void TourReport(Tour tour, ObservableCollection<TourLog> loglist) 
        {
            //all information of single tour
            //all tour logs from that tour
            string pdfFilePath = GetPDFFilePath(tour.Name);
            Console.WriteLine(pdfFilePath);

            PdfWriter writer = new PdfWriter(pdfFilePath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            //Header of the document
            Paragraph reportHeader = new Paragraph("Report of route: " + tour.Name + " \ndate: " + DateTime.Now.ToString("g"))
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(reportHeader);

          
            //tour details Header
            Paragraph routeDetailsHeader = new Paragraph("Tour Details:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(routeDetailsHeader);

            Table table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth()
             .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
             .SetFontSize(12);
            table.AddHeaderCell("Name");
            table.AddHeaderCell("Start");
            table.AddHeaderCell("Destination");
            table.AddHeaderCell("Distance in km");
            table.AddHeaderCell("Transport Type");
            table.AddHeaderCell("Duration");
            table.AddHeaderCell("Description");
            table.AddCell(tour.Name);
            table.AddCell(tour.Start);
            table.AddCell(tour.Destination);
            table.AddCell(tour.Distance.ToString());
            table.AddCell(tour.TransportType);
            table.AddCell(tour.Duration);
            table.AddCell(tour.Description);
            document.Add(table);

            Paragraph logHeader = new Paragraph("Current logs:")
                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                   .SetFontSize(12)
                   .SetBold()
                   .SetFontColor(ColorConstants.BLACK);
            document.Add(logHeader);

            if (loglist.Count != 0)
            {
                Table logtable = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth()
                 .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                 .SetFontSize(12);
                logtable.AddCell("Date and Time of Log");
                logtable.AddCell("Total Time:");
                logtable.AddCell("Difficulty (1-easy, 5-very hard");
                logtable.AddCell("Rating (1-good, 5-very bad)");
                logtable.AddCell("Comment");
                foreach (TourLog log in loglist)
                {
                    logtable.AddCell(log.DateTime);
                    logtable.AddCell(log.TotalTime.ToString());
                    logtable.AddCell(log.Difficulty.ToString());
                    logtable.AddCell(log.Rating.ToString());
                    logtable.AddCell(log.Comment);
                }
                document.Add(logtable);
            }
            else
            {
                Paragraph noLogs = new Paragraph("no logs available")
                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                   .SetFontSize(12);
                document.Add(noLogs);
            }
            

            Paragraph imageHeader = new Paragraph("Route Image")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(imageHeader);
            ImageData imageData = ImageDataFactory.Create(tour.Image);
            document.Add(new Image(imageData));

            document.Close();
        }

       

        public string GetPDFFilePath(string tourname)
        {
            
            string filename = tourname + DateTime.Now.ToString("ffff") + ".pdf";

            try
            {
                if (Directory.Exists(folderPath))
                {
                    Console.WriteLine("That path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(folderPath);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(folderPath));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            string fullImagePath = System.IO.Path.Combine(folderPath, filename);
            return fullImagePath;
        }
    }
}
