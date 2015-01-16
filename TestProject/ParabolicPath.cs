using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class ParabolicPath
    {
        private List<Node> path;
        private ParabolicScore score;

        public ParabolicPath()
        {
            path = new List<Node>();
            score = new ParabolicScore();
        }

        public void addStep(Node step)
        {
            path.Add(step);
        }

        public bool stepInPath(Node step)
        {
            if (this.path == null)
            {
                return false;
            }
            foreach (Node node in path)
            {
                if (step.name == node.name)
                {
                    return true;
                }
            }

            return false;
        }

        public ParabolicScore getScore()
        {
            return this.score;
        }

        public List<Node> getPath()
        {
            return this.path;
        }

        public void setScore(ParabolicScore score)
        {
            this.score = score;
        }

        public void calcAndSetRankScore(Vector rank)
        {
            double midScore = 0;
            for (int i = 0; i < path.Count; i++)
            {
                if (i > 0 && i < path.Count - 1)
                {
                    midScore += rank.vector[path[i].name];
                    midScore = midScore / (path.Count - 2); //normalization
                }
            }
            double endScore = rank.vector[path.Last().name];

            this.score.setMidRankScore(midScore);
            this.score.setEndRankScore(endScore);

        }

        public void saveMidIndex(int i)
        {
            this.score.setMidIndex(i);
        }

        public void saveEndIndex(int i)
        {
            this.score.setEndIndex(i);
        }

        public void reversePath()
        {
            path.Reverse();
        }

        public bool Equals(ParabolicPath other)
        {
            if (other == null || other.getPath().Count != this.path.Count)
            {
                return false;
            }
            List<Node> otherPath = other.getPath();
            for (int i = 0; i < otherPath.Count; i++)
            {
                if (otherPath[i].name != this.path[i].name)
                {
                    return false;
                }
            }
            return true;
        }

        public void calcAndSetFinalParScore()
        {
            this.score.calcAndSetFinalParScore();
        }
    }
}
