using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Graph
    {
        public enum PrunningStrategy
        {
            NONE,       //all nodes are left alive, no prunning at all
            NORMAL,     //nodes with depth 1 + (inverse depth 1 + their relatives) are left alive
            NORMAL_POST,
            STRICT      //nodes with inverse depth 1 are left alive
        };


        private Node graphHead;
        private Dictionary<String, Node> articles = new Dictionary<String, Node>();

        private PrunningStrategy strategy;
        private TextManager tm = new TextManager();

        private byte max_depth = 0;

        private Dictionary<String, String> redirectArticles = new Dictionary<String, String>();

        public Graph()
        {
            this.strategy = PrunningStrategy.NONE;
            this.graphHead = new Node();
        }

        public void setPrunningStrategy(PrunningStrategy strategy)
        {
            this.strategy = strategy;
        }



        public void buildGraph(String articleName, byte depth)
        {
            this.graphHead = new Node();
            this.graphHead.name = articleName;
            this.graphHead.depth = depth;
            this.max_depth = depth;

            this.articles.Clear();

            this.articles.Add(articleName, this.graphHead);

            buildGraphRec(this.graphHead, depth);


            if (this.redirectArticles.Count > 0)
            {
                replaceRedirectArticles(this.graphHead);
                this.redirectArticles.Clear();  // added to free memory, functionaly not neccesary
            }
        }

        public void buildArticlesSubSet()
        {
            switch (this.strategy)
            {
                case PrunningStrategy.NONE:
                    {
                        this.articles = getAllArticlesSubSet(this.graphHead);
                        break;
                    }
                case PrunningStrategy.NORMAL:
                    {
                        this.articles = getNormalPrunnedArticlesSubSet(this.graphHead);
                        break;
                    }
                case PrunningStrategy.STRICT:
                    {
                        this.articles = getSubSetByInverseDepth(this.graphHead, 1);
                        this.articles.Add(this.graphHead.name, this.graphHead);
                        break;
                    }
                default:
                    {
                        break;
                    }
            } 
        }

        private void buildGraphRec(Node node, byte depth)
        {
            if (depth == 0)
            {
                return;
            }

            List<String> links = tm.getLinksFromArticle(node.name);

            if (links.Count == 1) //Dealing with redirect pages. Need to improve this code
            {
                if (!redirectArticles.ContainsKey(node.name))
                {
                    redirectArticles.Add(node.name, links[0]);
                }
                node.name = links[0];
                links = tm.getLinksFromArticle(links[0]);
            }

            foreach (String link in links)
            {
                if (link.Length > 0)
                {
                    Node leaf = new Node();
                    leaf.name = link;
                    leaf.depth = (byte)(depth - 1);
                    node.links.Add(leaf);

                    if (!articles.ContainsKey(link))
                    {
                        articles.Add(link, leaf);
                        buildGraphRec(leaf, (byte)(depth - 1));
                    }
                    else if ((byte)(depth - 1) > articles[link].depth)
                    {
                        articles[link] = leaf;
                        buildGraphRec(leaf, (byte)(depth - 1));
                    }
                }
            }
        }

        private Dictionary<String, Node> getNormalPrunnedArticlesSubSet(Node node)
        {
            Dictionary<String, Node> result = new Dictionary<string, Node>();
            getNormalPrunnedSubSetRec(node, result);
            return result;
        }

        private void getNormalPrunnedSubSetRec(Node node, Dictionary<String, Node> result)
        {
            if (node.depth == 0)
            {
                return;
            }
            foreach (Node link in node.links)
            {
                if (link.name == this.graphHead.name)
                {
                    if (!result.ContainsKey(node.name))
                    {
                        result.Add(node.name, node);
                    }
                    else if (result[node.name].depth < node.depth)
                    {
                        result[node.name] = node;
                    }
                    foreach (Node link1 in node.links)
                    {
                        if (!result.ContainsKey(link1.name))
                        {
                            result.Add(link1.name, link1);
                        }
                        else if (result[link1.name].depth < link1.depth)
                        {
                            result[link1.name] = link1;
                        }
                    }
                }
                getNormalPrunnedSubSetRec(link, result);
            }  
        }

        private Dictionary<String, Node> getAllArticlesSubSet(Node node)
        {
            Dictionary<String, Node> result = new Dictionary<string, Node>();
            getAllArticlesSubSetRec(node, result);
            return result;
        }

        private void getAllArticlesSubSetRec(Node node, Dictionary<String, Node> result)
        {
            if (node == null)
            {
                return;
            }
            if (!result.ContainsKey(node.name))
            {
                result.Add(node.name, node);
                foreach(Node link in node.links)
                {
                    getAllArticlesSubSetRec(link, result);
                }
            }
            else if (result[node.name].depth < node.depth)
            {
                result[node.name] = node;
                foreach (Node link in node.links)
                {
                    getAllArticlesSubSetRec(link, result);
                }
            }
        }

        private Dictionary<String, Node> getSubSetByInverseDepth(Node node, byte inverseDepth)
        {
            //only depth 1 for now
            Dictionary<String, Node> result = new Dictionary<string, Node>();
            Dictionary<String, Node> target = new Dictionary<string, Node>();
            target.Add(node.name, node);
            getSubSetByInverseDepthRec(node, target, result);
            return result;
        }

        private void getSubSetByInverseDepthRec(Node node, Dictionary<String, Node> target, Dictionary<String, Node> subSet)
        {
            if (node.depth == 0)
            {
                return;
            }
            foreach (Node link in node.links)
            {
                if (link.name == this.graphHead.name)
                {
                    if (!subSet.ContainsKey(node.name))
                    {
                        subSet.Add(node.name, node);
                    }
                    else if (subSet[node.name].depth < node.depth)
                    {
                        subSet[node.name] = node;
                    }
                }
                getSubSetByInverseDepthRec(link, target, subSet);
            }     
        }

        private void replaceRedirectArticles(Node node)
        {
            if (this.redirectArticles.ContainsKey(node.name))
            {
                node.name = redirectArticles[node.name];
            }
            foreach (Node link in node.links)
            {
                replaceRedirectArticles(link);
            }
        }

        public Node getGraphHead()
        {
            return this.graphHead;
        }

        public Dictionary<String, Node> getArticles()
        {
            return this.articles;
        }

        public byte getMaxDepth()
        {
            return this.max_depth;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // GRAPH FILE IO
        //---------------------------------------------------------------------------------------------------------------------------


        public void saveTreeToFile()
        {
            String fileName = Const.RESULTS_DIR_PATH + this.max_depth + " " + this.graphHead.name + @".tree";
            this.graphHead.toFile(fileName);
        }

        public void loadTreeFromFile(String fileName)
        {
            this.graphHead = this.graphHead.loadFromFile(fileName);
            this.articles = getAllArticlesSubSet(this.graphHead);
            this.max_depth = this.graphHead.depth;
        }

    }
}
