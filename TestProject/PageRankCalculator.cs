using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class PageRankCalculator
    {

        private Dictionary<String, Vector> matrix;
        private Vector danglingVector;
        private Vector rankVector;
        private Dictionary<String, byte> recursionStarted;

        public PageRankCalculator(Node treeRoot)
        {
            matrix = new Dictionary<String, Vector>();
            rankVector = new Vector();
            danglingVector = new Vector();
            recursionStarted = new Dictionary<String, byte>();

            buildLinkMatrix(treeRoot);
            buildRankVector();

            recursionStarted.Clear();
            buildDanglingVector(treeRoot);

            
        }

        public void calculateRank(int numOfIterations)
        {
            for (int i = 0; i < numOfIterations; i++)
            {
                singleRankIteration();
            }
        }

        public Vector getPageRank()
        {
            return rankVector;
        }


        private void buildDanglingVector(Node node)
        {
            if (node == null)
            {
                return;
            }
            if (!recursionStarted.ContainsKey(node.name) && node.depth > 0)
            {
                recursionStarted.Add(node.name, node.depth);
            }

            if (node.links.Count == 0)
            {
                double weight = (double)1 / rankVector.vector.Count;


                if (!danglingVector.vector.ContainsKey(node.name))
                {
                    danglingVector.vector.Add(node.name, 0);
                }
                danglingVector.vector[node.name] = weight;

            }
            else
            {
                foreach (Node link in node.links)
                {
                    if (!recursionStarted.ContainsKey(link.name))
                    {
                        buildDanglingVector(link);
                    }
                    else if (node.depth - 1 > recursionStarted[link.name])
                    {
                        buildDanglingVector(link);
                    }
                }
            }
        }

        private void buildLinkMatrix(Node node)
        {
            if (node == null)
            {
                return;
            }

            if (!matrix.ContainsKey(node.name))
            {
                matrix.Add(node.name, new Vector(node.name));
            }
            if (!recursionStarted.ContainsKey(node.name) && node.depth > 0)
            {
                recursionStarted.Add(node.name, node.depth);
            }

            if (node.links.Count > 0)
            {
                double weight = (double)1 / node.links.Count;

                foreach (Node link in node.links)
                {
                    if (!matrix.ContainsKey(link.name))
                    {
                        matrix.Add(link.name, new Vector(link.name));
                    }

                    Vector vector = matrix[link.name];
                    vector.setValue(node.name, weight);

                    if (!recursionStarted.ContainsKey(link.name))
                    {
                        buildLinkMatrix(link);
                    }
                    else if (node.depth - 1 > recursionStarted[link.name])
                    {
                        buildLinkMatrix(link);
                    }
                }
            }

        }

        //private void buildRankVector() // 1 to first entry
        //{
        //    int count = 0;
        //    foreach (KeyValuePair<String, Vector> entry in matrix)
        //    {
        //        if (count == 0)
        //        {
        //            rankVector.setValue(entry.Key, 1);
        //        }
        //        else
        //        {
        //            rankVector.setValue(entry.Key, 0);
        //        }
        //        count++;
        //    }

        //}

        private void buildRankVector()
        {
            double initValue = (double)1 / (double)matrix.Count;

            foreach (KeyValuePair<String, Vector> entry in matrix)
            {
                rankVector.setValue(entry.Key, initValue);
            }
        }

        private void singleRankIteration()
        {
            double rankValue = 0;
            double danglingRankValue = 0;
            double oneRankValue = 0;
            Vector tempVector = new Vector();
            double alpha = 0.85;

            foreach (KeyValuePair<String, double> danglingEntry in danglingVector.vector)
            {
                danglingRankValue += danglingEntry.Value * rankVector.vector[danglingEntry.Key];
            }

            foreach(KeyValuePair<String, double> oneEntry in this.rankVector.vector)
            {
                oneRankValue += (double)oneEntry.Value * ((double)1 / rankVector.vector.Count);
            }

            
            foreach (KeyValuePair<String, Vector> matrixEntry in matrix)
            {
                rankValue = 0;
                Vector curMatrixRow = matrixEntry.Value;

                foreach (KeyValuePair<String, double> rowEntry in curMatrixRow.vector)
                {
                    rankValue += rowEntry.Value * rankVector.vector[rowEntry.Key];
                }

                tempVector.vector[matrixEntry.Key] = (alpha * rankValue) + (alpha * danglingRankValue) + ((1 - alpha) * oneRankValue);
            }

            rankVector = tempVector;
        }
    }
}
