using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.DAL.Mapquest
{
    public class Mapquest
    {
        private string mapquestKey;
        public DirectionsRouteData directionsData;
        private HttpClient client;
        private string location;
        private string destination;
        private string filePath;
        private string transportType;
        private string folderPath;

        private int ErrorInvalidLocation = 402;
        private int ErrorPedestrianRouteTooLong = 607;

        public Mapquest(string startAddress, string endAddress, string routeType)
        {
            //mapquestKey = ConfigurationManager.AppSettings["KeyMapQuest"];
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsetting.json", false, true).Build();
            mapquestKey = config["Mapquest:Key"];
            folderPath = config["Folderpath:Image"];

            client = new HttpClient();
            location = startAddress;
            destination = endAddress;
            transportType = routeType;
            directionsData = GetDirections();
        }
        
        public void SaveImage()
        {
            filePath = GetImagePath();
            if (directionsData.info.statuscode.Equals(ErrorInvalidLocation) || directionsData == null || directionsData.info.statuscode.Equals(ErrorPedestrianRouteTooLong))
            {
                Console.WriteLine("Error, invalid location or destination or pedestrian route too long");
            }
            else
            {
                directionsData.PrintRouteInfo();
                GetImageStaticMap();
            }
            client.Dispose();
        }




        private DirectionsRouteData GetDirections()
        {
            string fullURL = CreateDirectionsAPIstring(location, destination, transportType);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //get response and save into object 
            HttpResponseMessage response = client.GetAsync(fullURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DirectionsRouteData>(res);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

        }


        private void GetImageStaticMap()
        {
            ///https://developer.mapquest.com/documentation/static-map-api/v5/map/

            string mapURL = CreateStaticmapAPIstring(directionsData);

            //System.IO.File.WriteAllText(path, "Testing valid path & permissions.");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));

            HttpResponseMessage response = client.GetAsync(mapURL).Result;
            if (response.IsSuccessStatusCode)
            {
                //save file 
                using (WebClient client = new())
                {

                    var data = client.DownloadData(mapURL);
                    using (var ms = new MemoryStream(data))
                    {
                        using (var image = Image.FromStream(ms))
                        {
                            image.Save(filePath, ImageFormat.Jpeg);
                            Console.WriteLine("saved");
                        }
                    }
                }
            }
            else
            {
                filePath = null; //if image not existing 
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }


        private string CreateDirectionsAPIstring(string startAddress, string endAddress, string routeType)
        {
            string BaseURL = "https://www.mapquestapi.com/directions/v2/route?";

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("key", mapquestKey);
            queryString.Add("from", startAddress);
            queryString.Add("to", endAddress);
            queryString.Add("routeType", routeType);

            string APIString = BaseURL + queryString.ToString();
            Console.WriteLine(APIString);

            return APIString;
        }


        private string CreateStaticmapAPIstring(DirectionsRouteData directionsData)
        {
            string BaseURL = "https://www.mapquestapi.com/staticmap/v5/map?";

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("key", mapquestKey);
            queryString.Add("session", directionsData.route.sessionId);
            queryString.Add("boundingBox", directionsData.getValuesBoundingBox());
            queryString.Add("zoom", "11");

            string APIString = BaseURL + queryString.ToString();
            Console.WriteLine(APIString);

            return APIString;
        }

        private string GenerateImageFilename()
        {
            string name = Path.GetRandomFileName();
            string imageFilename = name + ".jpg";
            return imageFilename;
        }

        private string GetImagePath()
        {
            //string folderPath = ConfigurationManager.AppSettings["ImgFolderPath"];
            string filename = GenerateImageFilename();

            try
            {
                if (Directory.Exists(folderPath))
                {
                    Console.WriteLine("That path exists already.");

                }
                else
                {
                    // Try to create the directory.
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

        public double GetDistance()
        {
            if(directionsData == null)
            {
                return 0;
            }
            double km = directionsData.route.distance / 0.62137;
            double roundedValue = Math.Round(km, 2);
            return roundedValue;

        }

        public string GetTime()
        {
            if (directionsData == null)
            {
                return null;
            }
            return directionsData.route.formattedTime;

        }

        public string GetImage()
        {
            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                return null;
            }

        }


    }
}
