using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class PathScore
    {
        private double endRankScore;
        private double middleRankScore;
        private int midIndex;
        private int endIndex;
        private int finalScore;


        public void setMidRankScore(double score)
        {
            this.middleRankScore = score;
        }
        public void setEndRankScore(double score)
        {
            this.endRankScore = score;
        }

        public double getMidRankScore()
        {
            return middleRankScore;
        }

        public double getEndRankScore()
        {
            return endRankScore;
        }

        public void setMidIndex(int i)
        {
            midIndex = i;
        }

        public void setEndIndex(int i)
        {
            endIndex = i;
        }

        public void calcAndSetFinalParScore()
        {
            finalScore = midIndex + endIndex;
        }

        public int getFinalScore()
        {
            return finalScore;
        }
    }
}
