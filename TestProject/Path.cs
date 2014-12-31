﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Path
    {

        private List<Node> path;
        private double score;

        public Path()
        {
            path = new List<Node>();
            score = 0;
        }

        public void addStep(Node step)
        {
            path.Add(step);
        }

        public void addStepWithScore(Node step, Vector rank)
        {
            path.Add(step);
            this.score = calcPathScore(this.path, rank);
        }

        public double getScore()
        {
            return this.score;
        }

        public List<Node> getPath()
        {
            return this.path;
        }

        public void setScore(double score)
        {
            this.score = score;
        }

        public double calcPathScore(List<Node> path, Vector rank)
        {
            if (path == null || path.Count == 1)
            {
                return 0;
            }
            double score = 0;

            for (int i = 1; i < path.Count; i++)
            {
                score += Math.Abs(rank.vector[path[i].name] - rank.vector[path[i - 1].name]);
            }
            return score;
        }


        public void pathToFile(Vector rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";


            result += "SCORE: " + calcPathScore(path, rank) + Environment.NewLine;

            foreach (Node step in path)
            {
                result += step.name + " | " + step.depth + " | " + rank.vector[step.name].ToString() + Environment.NewLine;
            }

            result += Environment.NewLine;
            result += "---------------------------------------------------------------------------";
            result += Environment.NewLine;

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public void pathsListToFile(List<Path> list, Vector rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";
            foreach (Path one in list)
            {
                result += "SCORE: " + calcPathScore(one.getPath(), rank) + Environment.NewLine;

                foreach (Node step in one.getPath())
                {
                    result +=  " | " + step.depth + " | " + step.name + "\t\t\t | " + rank.vector[step.name].ToString() + Environment.NewLine;
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

        public void reversePath()
        {
            path.Reverse();
        }
    }
}
