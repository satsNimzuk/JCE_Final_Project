using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    class Utilities
    {
        public List<String> readNLinesToList(int N, FileStream inFileStream)
        {
            List<String> result = new List<String>();
            int bytesRead;
            byte[] lineBuffer = new byte[10000];
            int lineCount = 1;
            String currentLine = "";

            while ((bytesRead = getLine(inFileStream, ref lineBuffer)) > 0)
            {
                currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                result.Add(currentLine);

                if (lineCount == N)
                {
                    return result;
                }

                lineCount++;
            }
            return result;
        }

        public int getLine(FileStream fileStream, ref byte[] lineBuffer)
        {
            int lineOffset = 0;
            int ch;

            while ((ch = fileStream.ReadByte()) >= 0)
            {
                lineBuffer[lineOffset] = (byte)ch;
                lineOffset++;
                if (ch == 10)
                {
                    return lineOffset;
                }
            }
            return lineOffset;
        }

        public void treeWithPageRankToFile(Node treeRoot, Vector pageRank, String fileName)
        {

            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            fileWriter.Write(treeToString(treeRoot, pageRank));
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public void pathToFile(List<Node> path, Vector rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";

            foreach (Node step in path)
            {
                result += step.name + " | " + step.depth + " | " + rank.vector[step.name].ToString() + Environment.NewLine;
            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public void ListToFile(Node graph, Vector pageRank, List<String> backlinks)
        {
            String fileName = Const.RESULTS_DIR_PATH + graph.depth + " " + graph.name + " backlinks.txt";
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            foreach (String link in backlinks)
            {
                String line = link + "\t\t\t | \t\t\t" + pageRank.vector[link].ToString() + Environment.NewLine;
                fileWriter.Write(line);
            }

            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }


        private String treeToString(Node treeRoot, Vector pageRank)
        {
            String result = "";

            result = recTreeToString(treeRoot, pageRank, 0);

            return result;
        }

        private String recTreeToString(Node treeRoot, Vector pageRank, int depth)
        {
            String result = "";
            String prefix = "";
            for (int i = 0; i < depth; i++)
            {
                prefix += '\t';
            }
            String rank = "not found";
            if (pageRank.vector.ContainsKey(treeRoot.name))
            {
                rank = pageRank.vector[treeRoot.name].ToString();
            }
            result = prefix + treeRoot.depth + " | " + rank + ", " + treeRoot.name + Environment.NewLine;
            foreach (Node link in treeRoot.links)
            {
                result += recTreeToString(link, pageRank , depth + 1);
            }
            return result;
        }

    }
}
