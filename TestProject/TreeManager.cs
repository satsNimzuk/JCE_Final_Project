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
        //int nodeCount = 0;
        Dictionary<String, byte> uniqArticleNames = new Dictionary<String, byte>();

        public Node buildTree(String articleName, byte depth)
        {
            this.depth = depth;
            treeRoot = new Node();
            treeRoot.name = articleName;
            uniqArticleNames.Add(articleName, depth);
            //treeRoot.index = nodeCount;
            treeRoot.depth = depth;
            //nodeCount++;
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
                //leaf.index = nodeCount;
                leaf.depth = (byte)(depth - 1);
                //nodeCount++;
                treeRoot.links.Add(leaf);

                if (!uniqArticleNames.ContainsKey(link))
                {
                    uniqArticleNames.Add(link, (byte)(depth - 1));
                    buildTreeRec(leaf, (byte)(depth - 1));
                }
                else
                {
                    if ((byte)(depth - 1) > 0 && uniqArticleNames[link] == 0)
                    {
                        uniqArticleNames[link] = (byte)(depth - 1);
                        buildTreeRec(leaf, (byte)(depth - 1));
                    }
                }
            }
        }

        private bool linkInTree(Node leaf)
        {
            return linkInTreeRec(leaf, treeRoot);
        }

        private bool linkInTreeRec(Node leaf, Node node)
        {
            bool result = leaf.name.Equals(node.name) && (leaf.index != node.index) && (node.depth > 0);
            if (result == true)
            {
                leaf.index = node.index;
                return result;
            }

            foreach (Node n in node.links)
            {
                result = result || linkInTreeRec(leaf, n);
                if (result == true)
                {
                    return true;
                }
            }

            return result;
        }

        private void fixPreviousIndexes(Node leaf)
        {
            fixPreviousIndexesRec(leaf, treeRoot);
        }

        private void fixPreviousIndexesRec(Node leaf, Node node)
        {
            bool needFixing = leaf.name.Equals(node.name) && (leaf.index != node.index) && (node.depth == 0);
            if (needFixing == true)
            {
                node.index = leaf.index;
            }

            foreach (Node n in node.links)
            {
                fixPreviousIndexesRec(leaf, n);
            }
        }

    }
}
