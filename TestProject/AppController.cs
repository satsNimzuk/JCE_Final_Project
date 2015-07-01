using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class AppController
    {

        private Graph graph;
        private Vector pageRank;
        private PathFinder pathFinder;
        private List<Path> paths;
        TfIdfCalculator tfIdfCalculator;

        public AppController()
        {
            graph = new Graph();
            pageRank = new Vector();
            pathFinder = new PathFinder();
            paths = new List<Path>();
            tfIdfCalculator = new TfIdfCalculator();
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // GRAPH
        //--------------------------------------------------------------------------------------------------------------------------

        public void buildGraph(String rootArticleName, int depth)
        {
            this.graph.buildGraph(rootArticleName, (byte)depth);
        }

        public void saveCurrentGraphToFile()
        {
            if (this.graph == null)
            {
                return;
            }
            this.graph.saveTreeToFile();
        }

        public void loadGraph(String fileName)
        {
            if (fileName == null || fileName.Length < 1)
            {
                return;
            }
            this.graph.loadTreeFromFile(fileName);
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // PAGE RANK
        //---------------------------------------------------------------------------------------------------------------------------

        public void calcPageRank()
        {
            if (this.graph == null)
            {
                return;
            }
            PageRankCalculator pageRankCalculator = new PageRankCalculator(this.graph.getGraphHead());
            pageRankCalculator.calculateRank(Const.PAGE_RANK_NUM_OF_ITERATIONS);
            this.pageRank = pageRankCalculator.getPageRank();
        }

        public void savePageRank()
        {
            if (this.pageRank == null)
            {
                return;
            }
            String fileName = Const.RESULTS_DIR_PATH + this.graph.getMaxDepth() + " " + this.graph.getGraphHead().name + @".rank";
            this.pageRank.sortedToFile(fileName);
        }

        public void loadPageRank(String fileName)
        {
            if (fileName == null || fileName.Length < 1)
            {
                return;
            }
            this.pageRank = pageRank.loadFromFile(fileName);
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // ARTICLES SET
        //---------------------------------------------------------------------------------------------------------------------------

        public void buildArticlesSubSet(String strategy)
        {
            if (strategy == null)
            {
                return;
            }
            switch (strategy)
            {
                case "NONE":
                    {
                        this.graph.setPrunningStrategy(Graph.PrunningStrategy.NONE);
                        break;
                    }
                case "NORMAL":
                    {
                        this.graph.setPrunningStrategy(Graph.PrunningStrategy.NORMAL);
                        break;
                    }
                case "STRICT":
                    {
                        this.graph.setPrunningStrategy(Graph.PrunningStrategy.STRICT);
                        break;
                    }
            }

            this.graph.buildArticlesSubSet();
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // FIND PATH
        //---------------------------------------------------------------------------------------------------------------------------

        public void findPath(int resultLength)
        {
            Dictionary<String, bool> articlesFilter = new Dictionary<string, bool>();
            //articlesFilter.Add("Israel", true);
            //articlesFilter.Add("Jerusalem", true);
            this.pathFinder.setArticlesFilter(articlesFilter);
            this.paths = this.pathFinder.findNPaths(this.graph, this.pageRank, resultLength);
        }

        public void saveCurrentPathToFile()
        {
            if (this.paths == null || this.paths.Count == 0)
            {
                return;
            }
            String fileName = Const.RESULTS_DIR_PATH + this.graph.getMaxDepth() + " " + this.graph.getGraphHead().name + @".path";
            this.pathFinder.pathsListToFile(this.paths, this.pageRank, fileName);
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // TF-IDF
        //---------------------------------------------------------------------------------------------------------------------------

        public void calcTfIdf()
        {
            if (this.paths == null || this.paths.Count == 0)
            {
                return;
            }
            this.tfIdfCalculator = new TfIdfCalculator(this.paths);

            String keywordsFileName = Const.RESULTS_DIR_PATH + this.graph.getGraphHead().name + @" keywords.txt";
            List<TfIdfDocument> tfIdfRank = tfIdfCalculator.calcTfIdfQuery(keywordsFileName);

            String resultFileName = Const.RESULTS_DIR_PATH + this.graph.getMaxDepth() + " " + this.graph.getGraphHead().name + @" paths.tfidf";
            this.tfIdfCalculator.rankToFile(tfIdfRank, tfIdfRank.Count / 3, resultFileName);

            // TFIDF calculation for articles in paths
            List<String> listOfArticleNames = new List<String>();
            foreach (Path path in this.paths)
            {
                foreach (Node node in path.getPath())
                {
                    if (!listOfArticleNames.Contains(node.name))
                    {
                        listOfArticleNames.Add(node.name);
                    }
                }
            }
            this.tfIdfCalculator = new TfIdfCalculator(listOfArticleNames);
            tfIdfRank = tfIdfCalculator.calcTfIdfQuery(keywordsFileName);
            resultFileName = Const.RESULTS_DIR_PATH + this.graph.getMaxDepth() + " " + this.graph.getGraphHead().name + @" articles.tfidf";
            tfIdfCalculator.rankToFile(tfIdfRank, this.pageRank, resultFileName);
        }

    }
}
