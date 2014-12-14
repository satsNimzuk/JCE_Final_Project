using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestProject
{
    class Node
    {
        public String name;
        public int index;
        public byte depth;
        public List<Node> links;

        public Node()
        {
            this.name = "";
            this.links = new List<Node>();
            this.index = 0;
        }

        public String toString()
        {
            String result = "";

            result = recToString(this, 0);

            return result;
        }

        public void toFile(String fileName)
        {
            
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            
            StreamWriter fileWriter = new StreamWriter(outFileStream);
            fileWriter.Write(this.toString());
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        private String recToString(Node node, int depth)
        {
            String result = "";
            String prefix = "";
            for (int i = 0; i < depth; i++)
            {
                prefix += '\t';
            }
            result = prefix + node.depth + " | " + node.index + ", "  + node.name + Environment.NewLine;
            foreach (Node link in node.links)
            {
                result += recToString(link, depth + 1);
            }
            return result;
        }
    }
}
