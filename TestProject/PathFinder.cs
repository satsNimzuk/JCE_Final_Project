using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class PathFinder
    {

        private TextManager textManager = new TextManager();
        private Dictionary<String, bool> articlesFilter = new Dictionary<String, bool>();

        public List<Path> findNPaths(Graph graph, Vector pageRank, int N)
        {
            List<Path> result = new List<Path>();

            List<Path> allPaths = findAllPathsRec(graph.getGraphHead(), pageRank, graph.getArticles());
            allPaths = sortAllPathsByScore(allPaths, pageRank);

            // Best results
            int startIndex = 0;
            List<Path> tmp = findNPathsFromIndex(allPaths, graph, startIndex, N);
            result.AddRange(tmp);

            // Middle
            int midIndex = allPaths.Count / 2;
            tmp = findNPathsFromIndex(allPaths, graph, midIndex, N);
            result.AddRange(tmp);

            //Worst
            allPaths.Reverse();
            tmp = findNPathsFromIndex(allPaths, graph, startIndex, N);
            result.AddRange(tmp);

            return result;
        }

        private List<Path> findNPathsFromIndex(List<Path> allPaths, Graph graph, int index, int N)
        {
            List<Path> result = new List<Path>();
            bool skip;
            while (true)
            {
                skip = false;
                if (index >= allPaths.Count || result.Count == N)
                {
                    break;
                }
                foreach (Path p in result)
                {
                    if (p.Equals(allPaths[index]))
                    {
                        skip = true;
                    }
                }
                if (allPaths[index].getPath().Count < 3)
                {
                    skip = true;
                }
                if (true && !skip)
                {
                    Node last = allPaths[index].getPath().Last();
                    List<String> links = textManager.getLinksFromArticle(last.name);
                    skip = true;
                    foreach (String link in links)
                    {
                        if (graph.getGraphHead().name.Equals(link) && links.Count > 1)
                        {
                            skip = false;
                        }
                    }
                }
                if (!skip)
                {
                    result.Add(allPaths[index]);
                }
                index++;
            }

            return result;
        }

        private List<Path> findAllPathsRec(Node root, Vector rank)
        {
            if (root.depth == 0 || root.links == null || root.links.Count == 0)
            {
                Path path = new Path();
                List<Path> result = new List<Path>();
                path.addStep(root);
                result.Add(path);
                return result;
            }
            else
            {
                List<Path> result = new List<Path>();
                foreach (Node link in root.links)
                {
                    List<Path> paths = findAllPathsRec(link, rank);
                    foreach (Path path in paths)
                    {
                        if (!path.stepInPath(root))
                        {
                            path.addStep(root);
                            result.Add(path);
                        }
                    }
                }

                return result;
            }
        }

        private List<Path> findAllPathsRec(Node root, Vector rank, Dictionary<String,Node> articles)
        {
            if (root.depth == 0 || root.links == null || root.links.Count == 0)
             {
                Path path = new Path();
                List<Path> result = new List<Path>();

                if (articles.ContainsKey(root.name) && !this.articlesFilter.ContainsKey(root.name))
                {
                    path.addStep(root);
                    result.Add(path);
                }
                return result;
            }
            else
            {
                List<Path> result = new List<Path>();
                foreach (Node link in root.links)
                {
                    List<Path> paths = findAllPathsRec(link, rank, articles);
                    foreach (Path path in paths)
                    {
                        if (!path.stepInPath(root) && articles.ContainsKey(root.name) && !this.articlesFilter.ContainsKey(root.name))
                        {
                            path.addStep(root);
                            result.Add(path);
                        }
                    }
                }

                return result;
            }
        }

        private List<Path> sortAllPathsByScore(List<Path> allPaths, Vector pageRank)
        {
            List<Path> allSortedByMiddleScore = new List<Path>();
            List<Path> allSortedByEndScore = new List<Path>();

            foreach (Path path in allPaths)
            {
                path.reversePath();
                path.calcAndSetRankScore(pageRank);
                allSortedByMiddleScore.Add(path);
                allSortedByEndScore.Add(path);
            }

            allSortedByMiddleScore = allSortedByMiddleScore.OrderBy(score => score.getScore().getMidRankScore()).ToList();
            allSortedByEndScore = allSortedByEndScore.OrderByDescending(score => score.getScore().getEndRankScore()).ToList();

            for (int i = 0; i < allSortedByMiddleScore.Count; i++)
            {
                allSortedByMiddleScore[i].saveMidIndex(i);
            }
            for (int i = 0; i < allSortedByEndScore.Count; i++)
            {
                allSortedByEndScore[i].saveEndIndex(i);
            }
            foreach (Path path in allPaths)
            {
                path.calcAndSetFinalParScore();
            }

            allPaths = allPaths.OrderBy(score => score.getScore().getFinalScore()).ToList();
            return allPaths;
        }


        public void setArticlesFilter(Dictionary<String, bool> articlesFilter)
        {
            if (articlesFilter != null)
            {
                this.articlesFilter = articlesFilter;
            }
        }

        public void clearArticlesFilter()
        {
            this.articlesFilter.Clear();
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // PATH FILE IO
        //---------------------------------------------------------------------------------------------------------------------------

        public void pathsListToFile(List<Path> list, Vector rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";
            foreach (Path one in list)
            {
                result += "SCORE: " + (one.getScore().getFinalScore()) + Environment.NewLine;

                foreach (Node step in one.getPath())
                {
                    int dec_rank = rank.calcDecRank(step.name);

                    result += " | " + step.depth + " | " + step.name + "\t\t\t | " + dec_rank.ToString() + Environment.NewLine;
                }

                result += Environment.NewLine;
                result += "---------------------------------------------------------------------------";
                result += Environment.NewLine;

            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

    }
}
