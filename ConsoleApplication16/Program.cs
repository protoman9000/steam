using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Globalization;

/* The purpose of this program is to automate through a json file downloading only
 the video files and images that we can file. The way it works is first the window 
 pop up stating which json file you want to use. It will then create a new file and 
 starts downloading the video files and images asynchronous. Once it is finished it will
 then pop up a window stating that everythin is finished.*/



namespace ConsoleApplication16
{
    class Parser
    {
        //loading the json file
        public void webVideo()
        {
            Console.WriteLine("Starting video download");
            Console.WriteLine("Enter the steampull number");
            string input = Console.ReadLine();
            
            //searching for the selected json
            using (StreamReader r = File.OpenText(@"C:\Users\Aziz\Downloads\Steampull" + input + ".json"))
            {
                JObject sources = (JObject)JToken.ReadFrom(new JsonTextReader(r));
                //the range number for the data.
                for (int i = 293940; i < 300000; i++)
                {
                    string newNumber = i.ToString();

                    //goes through the json based on the address of data/movies, if that is not found then skip. 
                    var a = sources[newNumber];
                    if (a == null)
                        continue;
                    var b = a["data"];
                    if (b == null)
                        continue;
                    var movies = a["data"]["movies"];

                    //creating the folder to save all the movies and images.
                    string fileName = (string)a["data"]["name"];
                    string secondNamePlus = fileName.Replace(":", "");
                    string thirdNamePlus = secondNamePlus.Replace("/", "");
                    string fourthNamePlus = thirdNamePlus.Replace("\"", "");
                    string fifthNamePlus = fourthNamePlus.Replace("*", "");
                    
                    //in the movies tab
                    if (movies != null)
                    {
                        var c = b["movies"];

                        foreach (var item in c)
                        {
                            //since the website uses webm for trailers that is what we are looking for.
                            foreach (var item2 in item["webm"])
                            {

                                string url = (string)item2;

                                //converting the string url into an actual url.
                                Uri uriResult;
                                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                if (result == false)
                                    continue;
                                
                                //checks if the link is broken or not.
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                                request.Method = "HEAD";
                                try
                                {
                                    request.GetResponse();
                                }
                                catch
                                {
                                    continue;
                                }

                                //Removing any uncleared characters and saving the video file.
                                if (url.Contains(".webm"))
                                {
                                    if (url.Contains("480"))
                                    {
                                        string webName = (string)item["name"] + " 480";
                                        string secondName = webName.Replace(":", "");
                                        string thirdName = secondName.Replace("/", "");
                                        string fourthName = thirdName.Replace(".", "");
                                        string fifthName = fourthName.Replace("\"", "");
                                        string sixthName = fifthName.Replace("*", "");
                                        
                                        string Ato = @"C:\Users\Aziz\Desktop";
                                        string pathString1 = System.IO.Path.Combine(Ato, fifthNamePlus);
                                        string pathStringN2 = @"C:\Users\Aziz\Desktop\Ato\" + fifthNamePlus + "\\Trailers ";

                                        //Converts to mpeg
                                        string extension2 = ".mpeg";
                                        string newName = Path.ChangeExtension(sixthName, extension2);

                                        string pathString4 = System.IO.Path.Combine(pathStringN2, newName);
                                        System.IO.Directory.CreateDirectory(pathStringN2);

                                        saveVideo(pathString4, url);
                                    }
                                    else
                                    {
                                        //same as above but for high quality videos.
                                        string webName = (string)item["name"] + " max";
                                        string secondName = webName.Replace(":", "");
                                        string thirdName = secondName.Replace("/", "");
                                        string fourthName = thirdName.Replace(".", "");
                                        string fifthName = fourthName.Replace("\"", "");
                                        string sixthName = fifthName.Replace("*", "");

                                        string Ato = @"C:\Users\Aziz\Desktop";
                                        string pathString1 = System.IO.Path.Combine(Ato, fifthNamePlus);
                                        string pathStringN2 = @"C:\Users\Aziz\Desktop\Ato\" + fifthNamePlus + "\\Trailers ";

                                        string extension2 = ".mpeg";
                                        string newName = Path.ChangeExtension(sixthName, extension2);

                                        string pathString4 = System.IO.Path.Combine(pathStringN2, newName);
                                        System.IO.Directory.CreateDirectory(pathStringN2);

                                        saveVideo(pathString4, url);
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Finished downloading videos.");
        }
        //Getting the header image of the game. 
        public void webHeader()
        {
            Console.WriteLine("Starting header image download");
            Console.WriteLine("Enter the steampull number");
            string input = Console.ReadLine();
            //searching for the selected json file.
            using (StreamReader r = File.OpenText(@"C:\Users\Aziz\Downloads\Steampull" + input + ".json"))
            {
                JObject sources = (JObject)JToken.ReadFrom(new JsonTextReader(r));
                for (int i = 293940; i < 300000; i++)
                {
                    //looking for data/header image if it exist. 
                    string newNumber = i.ToString();
                    var a = sources[newNumber];
                    if (a == null)
                        continue;
                    var b = a["data"];
                    if (b == null)
                        continue;
                    
                    //link to the image
                    string url = (string)a["data"]["header_image"];

                    //check if it is not broken.
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "HEAD";
                    try
                    {
                        request.GetResponse();
                    }
                    catch
                    {
                        continue;
                    }

                    //Removing any uncleared characters and saving the image.
                    string webName = (string)a["data"]["name"];
                    string secondName = webName.Replace(":", "");
                    string thirdName = secondName.Replace("/", "");
                    string fourthName = thirdName.Replace(".", "");
                    string fifthName = fourthName.Replace("\"", "");
                    string sixthName = fifthName.Replace("*", "");

                    string Ato = @"C:\Users\Aziz\Desktop";
                    string pathString = System.IO.Path.Combine(Ato, sixthName);
                    string pathString2 = @"C:\Users\Aziz\Desktop\Ato\" + sixthName + "\\Image ";

                    string extension = ".jpeg";
                    string randomName = Path.ChangeExtension(sixthName, extension);

                    string pathString3 = System.IO.Path.Combine(pathString2, randomName);
                    System.IO.Directory.CreateDirectory(pathString2);

                    saveImage(pathString3, url);
                }
            }
            Console.WriteLine("Finished downloading header images");
        }

        public void webImage()
        {
            //Downloading the images.
            Console.WriteLine("Starting screenshots download");
            Console.WriteLine("Enter the steampull number");
            string input = Console.ReadLine();
            //searching for the selected json file.
            using (StreamReader r = File.OpenText(@"C:\Users\Aziz\Downloads\Steampull" + input + ".json"))
            {
                JObject sources = (JObject)JToken.ReadFrom(new JsonTextReader(r));
                for (int i = 200000; i < 300000; i++)
                {
                    //Going to the location of the image files which is data/screenshots
                    string newNumber = i.ToString();
                    var a = sources[newNumber];
                    if (a == null)
                        continue;
                    var b = a["data"];
                    if (b == null)
                        continue;
                    var c = b["screenshots"];
                    if (c == null)
                        continue;

                    //removes all the punctautions before saving the file. 
                    string webName2 = (string)a["data"]["name"];
                    string secondName = webName2.Replace(":", "");
                    string thirdName = secondName.Replace("/", "");
                    string fourthName = thirdName.Replace(".", "");
                    string fifthName = fourthName.Replace("\"", "");
                    string sixthName = fifthName.Replace("*", "");

                    foreach (var item in c)
                    {
                        foreach (var item2 in item)
                        {

                            string url = (string)item2;

                            //Checking to see if the string contains a URL
                            Uri uriResult;
                            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                            if (result == false)
                                continue;

                            //If the the link is broken, then this section of the json file is skip completely
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.Method = "HEAD";
                            try
                            {
                                request.GetResponse();
                            }
                            catch
                            {
                                continue;
                            }

                            //setting the location of saving jpegs
                            if (url.Contains("jpg"))
                            {
                                string webName = "Image " + (string)item["id"];
                                string Ato = @"C:\Users\Aziz\Desktop";
                                string pathString = System.IO.Path.Combine(Ato, sixthName);
                                string pathString2 = @"C:\Users\Aziz\Desktop\Ato\" + sixthName + "\\Image ";

                                string extension = ".jpeg";
                                string randomName = Path.ChangeExtension(webName, extension);

                                string pathString3 = System.IO.Path.Combine(pathString2, randomName);
                                System.IO.Directory.CreateDirectory(pathString2);

                                saveImage(pathString3, url);
                            }
                            else
                                break;
                        }

                    }
                }
            }
            Console.WriteLine("Finished downloading screenshots.");
        }
        public void saveImage(string file_name, string url)
        {
            int count = 1;
            string fileNameOnly = Path.GetFileNameWithoutExtension(file_name);
            string extension = Path.GetExtension(file_name);
            string path = Path.GetDirectoryName(file_name);
            string newFileName = file_name;

            if (File.Exists(file_name))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFileName = Path.Combine(path, tempFileName + extension);
            }

            byte[] imageBytes;
           
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(url);
           


            WebResponse imageResponse = imageRequest.GetResponse();

            Stream responseStream = imageResponse.GetResponseStream();

            using (BinaryReader br = new BinaryReader(responseStream))
            {
                imageBytes = br.ReadBytes(500000000);
                br.Close();
            }
            responseStream.Close();
            imageResponse.Close();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(imageBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }
        }
        public void saveVideo(string file_name, string url)
        {
            WebProxy proxy = null;
            Uri address = new Uri(url);
            Uri proxyEndpoint = WebRequest.GetSystemWebProxy().GetProxy(address);
            if (address.Equals(proxyEndpoint))
                proxy = new WebProxy(proxyEndpoint.ToString());

            var client = new WebClient();

            client.DownloadFile(address, file_name);
        }

        class runParser
        {
            static void Main(string[] args)
            {
                Parser x = new Parser();
                x.webVideo();
                x.webHeader();
                x.webImage();
                DateTime now = DateTime.Now;
                Console.WriteLine("All Done");
                Console.WriteLine(now);
                Console.ReadLine();
            }
        }
    }
}

