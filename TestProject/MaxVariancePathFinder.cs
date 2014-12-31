using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class MaxVariancePathFinder
    {

        public Path findGridyPath(Node treeRoot, Vector pageRank)
        {
            double maxStepVar = 0;
            Node currentRoot = treeRoot;
            Node nextRoot = new Node();
            byte depth = treeRoot.depth;
            Path result = new Path();
            result.addStepWithScore(treeRoot, pageRank);

            while (currentRoot.links != null && currentRoot.links.Count > 0)
            {
                maxStepVar = 0;
                foreach (Node link in currentRoot.links)
                {
                    if (pageRank.vector.ContainsKey(currentRoot.name) && pageRank.vector.ContainsKey(link.name))
                    {
                        double currentRootPR = pageRank.vector[currentRoot.name];
                        double linkPR = pageRank.vector[link.name];
                        if (Math.Abs(currentRootPR - linkPR) > maxStepVar && !result.stepInPath(link))
                        {
                            maxStepVar = Math.Abs(currentRootPR - linkPR);
                            nextRoot = link;
                        }
                    }
                }

                result.addStepWithScore(nextRoot, pageRank);
                currentRoot = nextRoot;
            }

            return result;
        }

        public Path findPath(Node root, Vector rank)
        {
            List<Path> all = findAllPathsRec(root, rank);
            double bestScore = 0;
            Path bestPath = null;
            foreach (Path path in all)
            {
                if (path.getScore() > bestScore)
                {
                    bestScore = path.getScore();
                    bestPath = path;
                }
            }
            bestPath.reversePath();
            return bestPath;
        }

        private List<Path> findAllPathsRec(Node root, Vector rank)
        {
            if (root.depth == 0 || root.links == null || root.links.Count == 0)
            {
                Path path = new Path();
                List<Path> result = new List<Path>();
                path.addStepWithScore(root, rank);
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
                            path.addStepWithScore(root, rank);
                            result.Add(path);
                        }
                    }
                }

                return result;
            }
        }


        public List<Path> findNPaths(Node root, Vector rank, int N)
        {
            List<Path> result = null;
            List<Path> all = findAllPathsRec(root, rank);
            result = all.OrderByDescending(score => score.getScore()).ToList();
            if (result.Count > N)
            {
                result = result.GetRange(0, N);
            }
            foreach (Path path in result)
            {
                path.reversePath();
            }
            return result;
        }



        //private List<Path> findNPathRec(Node root, Vector rank, int N)
        //{
        //    if (root.depth == 0 || root.links == null || root.links.Count == 0)
        //    {
        //        Path path = new Path();
        //        List<Path> result = new List<Path>();
        //        path.addStepWithScore(root, rank);
        //        result.Add(path);
        //        return result;
        //    }
        //    else
        //    {
        //        List<List<Path>> all = new List<List<Path>>();
        //        foreach (Node link in root.links)
        //        {
        //            List<Path> paths = findNPathRec(link, rank,N);
        //            all.Add(paths);
        //        }
        //        List<Path> result = merge(all, root, rank, N);
        //        return result;
        //    }
        //}

        //private List<Path> merge(List<List<Path>> all, Node root, Vector rank, int N)
        //{
        //    List<Path> result = new List<Path>();
        //    foreach (List<Path> paths in all)
        //    {
        //        //result.AddRange(paths);
        //        foreach (Path path in paths)
        //        {
        //            if (!path.stepInPath(root))
        //            {
        //                result.Add(path);
        //            }
        //        }
        //    }
        //    foreach (Path path in result)
        //    {
        //        path.addStepWithScore(root, rank);
        //    }
        //    result = result.OrderByDescending(score => score.getScore()).ToList();
        //    if (result.Count > N)
        //    {
        //        result = result.GetRange(0, N);
        //    }
        //    return result;
        //}

    }
}
