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

        public Node(String fileName)
        {
            Node result = loadFromFile(fileName);
            this.name = result.name;
            this.depth = result.depth;
            this.links = result.links;
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

        public Node loadFromFile(String fileName)
        {
            Stack<Node> stack = new Stack<Node>();
            String[] lines = File.ReadAllLines(fileName);
            int i = 0;
            foreach (String line in lines)
            {
                if (line.Length < 3 || line.Split('|').Length < 2)
                {
                    continue;
                }
                byte depth = Convert.ToByte(line.Split('|')[0].Trim());
                String name = line.Split('|')[1];
                Node node = new Node();
                node.depth = depth;
                node.name = name;

                if (stack.Count == 0)
                {
                    stack.Push(node);
                }
                else if (stack.Peek().depth > node.depth)
                {
                    stack.Peek().links.Add(node);
                    stack.Push(node);
                }
                else if (stack.Peek().depth <= node.depth)
                {
                    while (!(stack.Peek().depth > node.depth))
                    {
                        stack.Pop();
                    }
                    stack.Peek().links.Add(node);
                    stack.Push(node);
                }
                i++;
            }
            Node result = new Node();
            while (stack.Count > 0)
            {
                result = stack.Pop();
            }
            return result;
        }

        private String recToString(Node node, int depth)
        {
            String result = "";
            String prefix = "";
            for (int i = 0; i < depth; i++)
            {
                prefix += '\t';
            }
            result = prefix + node.depth + "|" + node.name + Environment.NewLine;
            foreach (Node link in node.links)
            {
                result += recToString(link, depth + 1);
            }
            return result;
        }
    }
}
