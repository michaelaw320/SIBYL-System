using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibylSystem
{
    class SibylSpider
    {
        public static void Main(String[] args)
        {
            Dominator spider = new Dominator();
            if (args.Length == 3)
            {
                String depth_str=args[1];
                String link_str=args[2];
                String type_str=args[0];
                int depth_int = Convert.ToInt32(depth_str);
                if (type_str.Equals("dfs", StringComparison.Ordinal))
                {
                    Dominator.DFS(depth_int, link_str);
                }
                else if (type_str.Equals("bfs", StringComparison.Ordinal))
                {
                    Dominator.BFS(depth_int, link_str);
                }
            }
        }
    }
}
