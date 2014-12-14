using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class MaxVariancePathFinder
    {

        public List<Node> findPath (Node treeRoot, Vector pageRank)
        {
            double maxStepVar = 0;
            Node currentRoot = treeRoot;
            Node nextRoot = new Node();
            byte depth = treeRoot.depth;
            List<Node> result = new List<Node>();
            result.Add(treeRoot);

            while (currentRoot.links != null && currentRoot.links.Count > 0)
            {
                maxStepVar = 0;
                foreach (Node link in currentRoot.links)
                {
                    if (pageRank.vector.ContainsKey(currentRoot.name) && pageRank.vector.ContainsKey(link.name))
                    {
                        double currentRootPR = pageRank.vector[currentRoot.name];
                        double linkPR = pageRank.vector[link.name];
                        if (Math.Abs(currentRootPR - linkPR) > maxStepVar)
                        {
                            maxStepVar = Math.Abs(currentRootPR - linkPR);
                            nextRoot = link;
                        }
                    }
                }

                result.Add(nextRoot);
                currentRoot = nextRoot;
            }

            return result;
        }
    }
}
