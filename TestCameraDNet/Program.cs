using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.XPath;
using HtmlAgilityPack;
using System.Threading;
using System.Net;
using System.Text;
//using System.Drawing;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace TestCameraDNet
{
    class Program
    {
        static void Main(string[] args)
        {

            string html = @"http://10.202.90.148/axis-cgi/com/ptz.cgi?query=presetposcamdata";
            string URL = @"http://10.202.90.148/axis-cgi/com/ptz.cgi?query=presetposcamdata";            

            var client = new WebClient { Credentials = new NetworkCredential("root", "admin") };

            //CredentialCache myCache = new CredentialCache();

            //URL for Jpg
            URL = @"http://10.202.90.148/axis-cgi/jpg/image.cgi";

            byte[] data = client.DownloadData(URL);

            string dirnow = Directory.GetCurrentDirectory();

            using (Image img = Image.FromStream(new MemoryStream(data)))
            {

                img.Save("foo.jpg", ImageFormat.Jpeg);

            }


            //URL for Presets
            URL = @"http://10.202.90.148/axis-cgi/com/ptz.cgi?query=presetposcamdata";

            string presets = client.DownloadString(URL);

            using(var reader = new StringReader(presets))
            {
                //Read over Each Line
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    if (line.Contains('='))
                    {
                        Console.WriteLine(line + " : With = ");                        
                    }
                    else
                    {
                        Console.WriteLine(line + " : Without = ");
                    }
                    

                }

            }





            StringReader strReader = new StringReader(presets);

            //foreach (string line in new LineReader(() => new StringReader(presets)))
            //{
            //    Console.WriteLine(line);
            //}



            Uri uriz = new Uri("http://10.202.90.148/axis-cgi/com/ptz.cgi?query=presetposcamdata");

            var response = client.DownloadString(html);

            var r3 = client.DownloadString("http://10.202.90.148/axis-cgi/jpg/image.cgi");

            string r1 = @"http://10.202.90.148/axis-cgi/jpg/image.cgi";

            System.Drawing.Image zzzzz =  DownloadImageFromUrl(r1);

           //

            //WebRequest request = (WebRequest)base.GetWebRequest(address);



            //var resData =

            //var html = @"http://html-agility-pack.net/";

            //HtmlWeb web = new HtmlWeb();


            //NetworkCredential nc = new NetworkCredential("root", "admin");


            //var htmlDoc = web.Load(html, "POST", null, nc);

            //var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            //Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);

            Thread.Sleep(2000);

            Console.WriteLine("\npress any key to exit the process...");

            // basic use of "Console.ReadKey()" method
            Console.ReadKey();

            //HttpClient http = new HttpClient();
            //var response = await http.GetByteArrayAsync(url);
            //String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            //source = WebUtility.HtmlDecode(source);
            //HtmlDocument resultat = new HtmlDocument();
            //resultat.LoadHtml(source);


        }


        static public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);

                CredentialCache myCache = new CredentialCache();

                myCache.Add(new Uri(imageUrl), "Basic", new NetworkCredential("root", "admin"));

                webRequest.Credentials = myCache;

                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

    }

}
