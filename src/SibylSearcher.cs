using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibylSystem
{
    class SibylSearcher
    {
        public static void Main(String[] args)
        {
            SibylCore Core = new SibylCore();
            String dicari="";
            if(args.Length > 0)
                dicari += args[0];
            for(int i=1;i<args.Length;i++)
                dicari+=" "+args[i];
            String ans="";
            if (dicari != "")
            {
                ans = Core.Retrieve(dicari);
                Console.WriteLine(ans);
            }
        }
    }
}