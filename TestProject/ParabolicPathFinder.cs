using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class ParabolicPathFinder
    {

        public List<ParabolicPath> findNPathsParabolic(Node root, Vector rank, int N)
        {
            List<ParabolicPath> result = new List<ParabolicPath>();
            List<ParabolicPath> all = findAllPathsRec(root, rank);

            List<ParabolicPath> allSortedByMiddleScore = new List<ParabolicPath>();
            List<ParabolicPath> allSortedByEndScore = new List<ParabolicPath>();

            foreach (ParabolicPath path in all)
            {
                path.reversePath();
                path.calcAndSetRankScore(rank);
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
            foreach (ParabolicPath path in all)
            {
                path.calcAndSetFinalParScore();
            }

            all = all.OrderBy(score => score.getScore().getFinalScore()).ToList();

            int j = 0;
            bool skip;
            while (true)
            {
                skip = false;
                if (j >= all.Count || result.Count == N)
                {
                    break;
                }
                foreach (ParabolicPath p in result)
                {
                    if (p.Equals(all[j]))
                    {
                        skip = true;
                    }
                }
                if (all[j].getPath().Count < 3)
                {
                    skip = true;
                }
                if (!skip)
                {
                    result.Add(all[j]);
                }
                j++;
            }

            return result;
        }

        public void pathsListToFile(List<ParabolicPath> list, Vector rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";
            foreach (ParabolicPath one in list)
            {
                result += "SCORE: " + (one.getScore().getFinalScore()) + Environment.NewLine;

                foreach (Node step in one.getPath())
                {
                    result += " | " + step.depth + " | " + step.name + "\t\t\t | " + rank.vector[step.name].ToString() + Environment.NewLine;
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



        private List<ParabolicPath> findAllPathsRec(Node root, Vector rank)
        {
            if (root.depth == 0 || root.links == null || root.links.Count == 0)
            {
                ParabolicPath path = new ParabolicPath();
                List<ParabolicPath> result = new List<ParabolicPath>();
                path.addStep(root);
                result.Add(path);
                return result;
            }
            else
            {
                List<ParabolicPath> result = new List<ParabolicPath>();
                foreach (Node link in root.links)
                {
                    List<ParabolicPath> paths = findAllPathsRec(link, rank);
                    foreach (ParabolicPath path in paths)
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
