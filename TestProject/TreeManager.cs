using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class TreeManager
    {
        TextManager tm = new TextManager();
        Node treeRoot;
        byte depth = 0;
        Dictionary<String, byte> uniqArticleNames = new Dictionary<String, byte>();

        public Node buildTree(String articleName, byte depth)
        {
            this.depth = depth;
            treeRoot = new Node();
            treeRoot.name = articleName;
            uniqArticleNames.Add(articleName, depth);
            treeRoot.depth = depth;
            buildTreeRec(treeRoot, depth);
            return treeRoot;
        }

        private void buildTreeRec(Node treeRoot, byte depth)
        {
            if (depth == 0)
            {
                return;
            }

            String articleName = treeRoot.name;
            List<String> links = tm.getLinksFromArticle(articleName);

            foreach (String link in links)
            {
                Node leaf = new Node();
                leaf.name = link;
                leaf.depth = (byte)(depth - 1);
                treeRoot.links.Add(leaf);

                if (!uniqArticleNames.ContainsKey(link))
                {
                    uniqArticleNames.Add(link, (byte)(depth - 1));
                    buildTreeRec(leaf, (byte)(depth - 1));
                }
                else
                {
                    if ((byte)(depth - 1) > uniqArticleNames[link])  //need to fix it + uniqArticleNames should be part of the output
                    {
                        uniqArticleNames[link] = (byte)(depth - 1);
                        buildTreeRec(leaf, (byte)(depth - 1));
                    }
                }
            }
        }



    }
}
