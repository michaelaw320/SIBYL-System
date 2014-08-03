using System;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using HtmlAgilityPack;

namespace SibylSystem
{
    public class Dominator
    {

        public static String ReadUrl(String url)
        {
            String htmlText = "";

            try
            {    
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = ".NET Web Crawler";
                WebResponse response = request.GetResponse();

                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);

                htmlText = reader.ReadToEnd();

                response.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString() + " at address: " + url);
            }
            return htmlText;
        }

        public static ArrayList LinkSearchAgility(String htmlText, String url)
        {
            HtmlDocument web = new HtmlDocument();
            web.LoadHtml(htmlText);
            
            ArrayList Links = new ArrayList();
            
            try
            {

                foreach (HtmlNode link in web.DocumentNode.SelectNodes("//a[@href]"))
                {
                    String temps;
                    temps = link.Attributes["href"].Value.ToString();
                    try
                    {
                        if (temps.Contains(".jpeg") || temps.Contains(".doc") || temps.Contains(".docx") || temps.Contains(".ppt") || temps.Contains(".pptx") || temps.Contains(".pdf") || temps.Contains(".mp3") || temps.Contains(".mp4") || temps.Contains(".flv") || temps.Contains(".3gp") || temps.Contains(".mpeg") || temps.Contains(".pps") || temps.Contains(".jpg") || temps.Contains(".png") || temps.Contains(".gif") || temps.Contains(".zip") || temps.Contains(".rar") || temps.Contains(".7z") || temps.Contains("#") || temps.Contains("@") || temps.Contains("mailto:") || temps.Contains(".wmv") || temps.Contains(".bmp") || temps.Contains("javascript:") || temps.Contains("#"))
                        {
                            /*Do Nothing! Tidak mengakses file-file atau melaksanakan javascript*/
                        }
                        else if (temps.Contains(".JPEG") || temps.Contains(".DOC") || temps.Contains(".DOCS") || temps.Contains(".PPT") || temps.Contains(".PPTX") || temps.Contains(".PDF") || temps.Contains(".MP3") || temps.Contains(".MP4") || temps.Contains(".FLV") || temps.Contains(".3GP") || temps.Contains(".MPEG") || temps.Contains(".PPS") || temps.Contains(".JPG") || temps.Contains(".PNG") || temps.Contains(".GIF") || temps.Contains(".ZIP") || temps.Contains(".RAR") || temps.Contains(".7Z") || temps.Contains("MAILTO:") || temps.Contains(".WMV") || temps.Contains(".BMP") || temps.Contains("JAVASCRIPT:"))
                        {
                            /*Do Nothing! Tidak mengakses file-file atau melaksanakan javascript*/
                        }
                        else if (temps.Contains("http://"))
                        {
                            var tempuri = new Uri(temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);
                        }
                        else if (temps.Contains("://"))
                        {
                            var tempuri = new Uri(temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);
                        }
                        else if (temps.Contains("//"))
                        {

                            var tempuri = new Uri("http:" + temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);

                        }
                        else if (temps[0] == '/')
                        {
                            var tempurl = new Uri(url);
                            var tempurl2 = tempurl.GetLeftPart(System.UriPartial.Authority);
                            temps = tempurl2 + temps;

                            var tempuri = new Uri(temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);
                        }
                        else if (temps.Contains("./")){
                            temps.Replace("./", (url.TrimEnd('/')).Substring(0, (url.TrimEnd('/')).LastIndexOf('/')));
                            var tempuri = new Uri(temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);
                        }
                        else
                        {
                            String tempurl = url;
                            if (tempurl.Contains(".htm/")){
                                tempurl = url.Substring(0, url.Length - 1);
                            }
                            if (tempurl.Contains(".htm")){
                                tempurl = url.Substring(0, url.LastIndexOf("/"));
                            }
                            url.Replace("\n\r", "");
                            if (temps[0] != '/' && tempurl[tempurl.Length - 1] != '/')
                            {
                                temps = tempurl + "/" + temps;
                            }
                            else
                            {
                                temps = tempurl + temps;
                            }

                            var tempuri = new Uri(temps);
                            temps = tempuri.AbsoluteUri;
                            Links.Add(temps);
                        }
                    }

                    catch (Exception Ex)
                    {
                    }
                }
            }
            catch (Exception Ex) { }
            return Links;
        }
        
        public static String GetTitle(String htmlText)
        {
            Regex title = new Regex(@"<title>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            String temp = "";
            if (htmlText.Contains("<title>"))
            {
                try
                {
                    Match match = title.Match(htmlText);
                    temp = htmlText.Substring(htmlText.IndexOf("<title>") + 7, htmlText.IndexOf("</title>") - htmlText.IndexOf("<title>") - 7);
                    return temp;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
                return temp;
            }
            else
                return ("Untitled Page");


        }

        public static void BFS(int depth, String url)
        {
            /*Baca alamat web dari url dan masukkan text keseluruhan website dalam htmlText dan masukkan semua link ke Links, kemudian pencarian dilakukan lagi dengan address-address pada dari array of string Links.*/
            SibylCore Core = new SibylCore();
            String htmlText = ReadUrl(url);
            Core.Dump(GetTitle(htmlText), url, htmlText);
            ArrayList Links = new ArrayList();
            Links.Add(url);
            ArrayList tempLinks = LinkSearchAgility(htmlText, url);

            foreach (Object X in tempLinks)
            {
                if (!(Links.Contains(X)))
                    Links.Add(X);
            }
            String temptext;
            int i = 1;
            int j = 1;

            while (i <= depth)
            {
                int last = Links.Count;
                for (; j < last; j++)
                {

                    System.Console.WriteLine(Links[j].ToString() + " - " + j);

                    temptext = ReadUrl(Links[j].ToString());
                    tempLinks = LinkSearchAgility(temptext, Links[j].ToString());
                    Core.Dump(GetTitle(temptext), Links[j].ToString(), temptext);

                    //System.Console.WriteLine(Links[j].ToString());
                    for (int k = 0; k <= tempLinks.Count - 1; k++)
                    {
                        if (!(Links.Contains(tempLinks[k])))
                        {
                            Links.Add(tempLinks[k]);
                        }

                    }
                }

                i++;
            }
        }



        private static ArrayList DFSLinks;

        private static void DFSrecursive(int depth_left, String url)
        {
            if (depth_left < 0) return;
            if (DFSLinks.Contains(url)) return;
            DFSLinks.Add(url);

            String htmlText = ReadUrl(url);
            ArrayList Links = LinkSearchAgility(htmlText, url);

            int last = Links.Count;
            for (int i = 0; i < last; i++)
                DFSrecursive(depth_left - 1, Links[i].ToString());
        }

        public static void DFS(int depth, String url)
        {
            SibylCore Core = new SibylCore();
            DFSLinks = new ArrayList();
            DFSrecursive(depth, url);
            foreach (Object X in DFSLinks)
            {
                System.Console.WriteLine(X.ToString());
                String htmlText = ReadUrl(X.ToString());
                Core.Dump(GetTitle(htmlText), X.ToString(), htmlText);
            }
        }
    }
}
