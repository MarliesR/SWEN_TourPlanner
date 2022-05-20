using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TourPlanner.DAL.Mapquest
{
    class Mapquest
    {
        private string mapquestKey;
        public DirectionsRouteData directionsData;
        private HttpClient client;
        private string location;
        private string destination;

        public Mapquest(string startAddress, string endAddress)
        {
            mapquestKey = ConfigurationManager.AppSettings["KeyMapQuest"];
            client = new HttpClient();
            this.location = startAddress;
            this.destination = endAddress;
            getDirectionsData();

            client.Dispose();



        }

        public void getDirectionsData()
        {

            DirectionsAPICall();
            if (directionsData.info.statuscode.Equals(402) || directionsData == null)
            {
                Console.WriteLine("Error, invalid location or destination");
            }
            else
            {

                directionsData.PrintRouteInfo();
                StaticMapAPICall();
            }

        }

        private void DirectionsAPICall()
        {
            string fullURL = CreateDirectionsAPIstring(location, destination);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //get response and save into object 
            HttpResponseMessage response = client.GetAsync(fullURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                directionsData = JsonConvert.DeserializeObject<DirectionsRouteData>(res);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }

        private void StaticMapAPICall()
        {
            ///https://developer.mapquest.com/documentation/static-map-api/v5/map/


            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;


            string path = ConfigurationManager.AppSettings["ImgFolderPath"];
            string filename = "files.jpeg"; //FILENAME GENERIEREN NOCH ERLEDIGEN
            string filePath = System.IO.Path.Combine(path, filename);
            Console.WriteLine(filePath);


            //System.IO.File.WriteAllText(path, "Testing valid path & permissions.");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
            string mapURL = CreateStaticmapAPIstring(directionsData);
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
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }


        private string CreateDirectionsAPIstring(string startAddress, string endAddress)
        {
            string BaseURL = "https://www.mapquestapi.com/directions/v2/route?";

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("key", mapquestKey);
            queryString.Add("from", startAddress);
            queryString.Add("to", endAddress);

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


    }
}
