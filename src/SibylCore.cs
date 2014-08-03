using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SibylSystem
{
    class SibylCore
    {
        private string judul;
        private string link;
        private string fkeywords;
        private string dump;
        private string matched;
        private string ffkeywords;
        private string ffjudul;

        public void Dump(string title, string url, string keywords)
        {
            try
            {
                if (keywords != "" && title != "Error")
                {
                    /* HTML Filtering */

                    /* Eliminate tags */
                    Regex Tags = new Regex(@"<script(.|\n)*?</script>", RegexOptions.Multiline);
                    fkeywords = Tags.Replace(keywords, "");
                    Tags = new Regex(@"<(.|\n)*?>", RegexOptions.Multiline);
                    fkeywords = Tags.Replace(fkeywords, "");
                    Tags = new Regex(@"/(.|\n)*?/|(•?&#160;)|(&nbsp)|\.mw-fblink \{(.|\n)*?\}|js-content\{(.|\n)*?\}|requires-javascript \{(.|\n)*?\}|(a:lang.*?\})", RegexOptions.IgnoreCase);
                    fkeywords = Tags.Replace(fkeywords, "");

                    /* Eliminate newline, tabs, and punctuation */
                    Regex Newl = new Regex(@"([\n\r])|(\t)|(\.)|(,)|(\?)|(\u0022)|(\()|(\))", RegexOptions.IgnoreCase);
                    fkeywords = Newl.Replace(fkeywords, " ");
                    title = Newl.Replace(title, "");

                    /* Eliminate duplicate words */
                    fkeywords = fkeywords.ToLower();
                    fkeywords = string.Join(" ", fkeywords.Split(' ').Distinct().ToArray());

                    /* End HTML filtering */

                    if (isUrlExists(url))
                    {
                        /* URL Exists, lakukan update */
                        if (fkeywords != ffkeywords)
                        {
                            /* UPDATE KEYWORDS */
                            string forreplace = title + "[^SEPARATOR&]" + url + "[^SEPARATOR&]" + ffkeywords;
                            dump = title + "[^SEPARATOR&]" + url + "[^SEPARATOR&]" + fkeywords;
                            string strFile = File.ReadAllText("SibylDB.dat");
                            strFile = strFile.Replace(forreplace, dump);
                            File.WriteAllText("SibylDB.dat", strFile);
                        }
                        if (title != ffjudul) 
                        {
                            /* UPDATE JUDUL */
                            string forreplace = ffjudul + "[^SEPARATOR&]" + url + "[^SEPARATOR&]" + fkeywords;
                            dump = title + "[^SEPARATOR&]" + url + "[^SEPARATOR&]" + fkeywords;
                            string strFile = File.ReadAllText("SibylDB.dat");
                            strFile = strFile.Replace(forreplace, dump);
                            File.WriteAllText("SibylDB.dat", strFile);
                        }
                        
                    }
                    else
                    {
                        /* ADD NEW */
                        dump = title + "[^SEPARATOR&]" + url + "[^SEPARATOR&]" + fkeywords;
                        /* Dump to database */
                        FileStream FW = new FileStream("SibylDB.dat", FileMode.Append, FileAccess.Write);
                        using (StreamWriter sw = new StreamWriter(FW))
                        {
                            sw.WriteLine(dump);
                        }
                        FW.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool isUrlExists(string url)
        {
            FileStream FR = new FileStream("SibylDB.dat", FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader sr = new StreamReader(FR))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] splitted = Regex.Split(line, @"\[\^SEPARATOR&\]");
                        string tmpjudul = splitted[0];
                        string tmplink = splitted[1];
                        string tmpkey = splitted[2];
                        if (tmplink == url)
                        {
                            ffkeywords = tmpkey;
                            ffjudul = tmpjudul;
                            FR.Close();
                            return true;
                        }
                    }
                }
            }
            FR.Close();
            ffkeywords = "";
            return false;
        }

        public string Retrieve(string what)
        { 
            what.ToLower();
            what = string.Join("|", what.Split(' ').ToArray());
            StringBuilder SB = new StringBuilder();

            matched = string.Empty;
            FileStream FR = new FileStream("SibylDB.dat", FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader sr = new StreamReader(FR))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitted = Regex.Split(line, @"\[\^SEPARATOR&\]");
                    judul = splitted[0];
                    link = splitted[1];
                    fkeywords = splitted[2];
                    Match match = Regex.Match(fkeywords, what, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        SB = SB.Append(judul).Append("[^SEPARATOR&]").Append(link).Append("\n");
                    }
                }
            }
            FR.Close();
            matched = SB.ToString();
            return matched;
        }

    } 
} 
