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

        //public Path findGridyPath(Node treeRoot, Vector pageRank)
        //{
        //    double maxStepVar = 0;
        //    Node currentRoot = treeRoot;
        //    Node nextRoot = new Node();
        //    byte depth = treeRoot.depth;
        //    Path result = new Path();
        //    result.addStepWithScore(treeRoot, pageRank);

        //    while (currentRoot.links != null && currentRoot.links.Count > 0)
        //    {
        //        maxStepVar = 0;
        //        foreach (Node link in currentRoot.links)
        //        {
        //            if (pageRank.vector.ContainsKey(currentRoot.name) && pageRank.vector.ContainsKey(link.name))
        //            {
        //                double currentRootPR = pageRank.vector[currentRoot.name];
        //                double linkPR = pageRank.vector[link.name];
        //                if (Math.Abs(currentRootPR - linkPR) > maxStepVar && !result.stepInPath(link))
        //                {
        //                    maxStepVar = Math.Abs(currentRootPR - linkPR);
        //                    nextRoot = link;
        //                }
        //            }
        //        }

        //        result.addStepWithScore(nextRoot, pageRank);
        //        currentRoot = nextRoot;
        //    }

        //    return result;
        //}

        //public Path findPath(Node root, Vector rank)
        //{
        //    List<Path> all = findAllPathsRec(root, rank);
        //    double bestScore = 0;
        //    Path bestPath = null;
        //    foreach (Path path in all)
        //    {
        //        if (path.getScore() > bestScore)
        //        {
        //            bestScore = path.getScore();
        //            bestPath = path;
        //        }
        //    }
        //    bestPath.reversePath();
        //    return bestPath;
        //}

        public List<MaxVariancePath> findNPaths(Node root, Vector rank, int N)
        {
            List<MaxVariancePath> result = new List<MaxVariancePath>();
            List<MaxVariancePath> all = findAllPathsRec(root, rank);
            foreach (MaxVariancePath path in all)
            {
                path.setMaxVarianceScore(rank);
            }

            all = all.OrderByDescending(score => score.getScore()).ToList();
            int i = 0;
            bool skip;
            while (true)
            {
                skip = false;
                if (i >= all.Count || result.Count == N)
                {
                    break;
                }
                foreach (MaxVariancePath p in result)
                {
                    if (p.Equals(all[i]))
                    {
                        skip = true;
                    }
                }
                if (!skip)
                {
                    result.Add(all[i]);
                }
                i++;
            }
            //if (result.Count > N)
            //{
            //    result = result.GetRange(0, N);
            //}
            foreach (MaxVariancePath path in result)
            {
                path.reversePath();
            }
            return result;
        }

        

        private List<MaxVariancePath> findAllPathsRec(Node root, Vector rank)
        {
            if (root.depth == 0 || root.links == null || root.links.Count == 0)
            {
                MaxVariancePath path = new MaxVariancePath();
                List<MaxVariancePath> result = new List<MaxVariancePath>();
                path.addStep(root);
                result.Add(path);
                return result;
            }
            else
            {
                List<MaxVariancePath> result = new List<MaxVariancePath>();
                foreach (Node link in root.links)
                {
                    List<MaxVariancePath> paths = findAllPathsRec(link, rank);
                    foreach (MaxVariancePath path in paths)
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

    }
}
