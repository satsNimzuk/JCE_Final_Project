using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    public class Node
    {
        public String name;
        public byte depth;
        public byte inverse_depth;
        public List<Node> links;

        public Node()
        {
            this.name = "";
            this.links = new List<Node>();
        }

        public Dictionary<String,Node> findByDepth(byte depth)
        {
            Dictionary<String, Node> result = new Dictionary<String, Node>();
            if (this.links == null || this.links.Count == 0)
            {
                return result;
            }

            byte head_depth = this.depth;
            findByDepthRec(this, depth, head_depth, result);

            return result;
        }

        private void findByDepthRec(Node node, byte depth, byte head_depth, Dictionary<String, Node> result)
        {
            if (node == null)
            {
                return;
            }
            if (node.depth < head_depth && node.depth >= head_depth - depth)
            {
                if (result.ContainsKey(node.name))
                {
                    if (result[node.name].depth < node.depth)
                    {
                        result[node.name] = node;
                    }
                }
                else
                {
                    result.Add(node.name, node);
                }
            }
            foreach (Node link in node.links)
            {
                findByDepthRec(link, depth, head_depth, result);
            }
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

            toFileRec(this, 0, fileWriter);

            fileWriter.Close();
            outFileStream.Close();
        }

        private void toFileRec(Node node, int depth, StreamWriter fileWriter)
        {
            String prefix = "";
            String result = "";

            for (int i = 0; i < depth; i++)
            {
                prefix += '\t';
            }

            result = prefix + node.depth + "|" + node.name + Environment.NewLine;
            fileWriter.Write(result);

            foreach (Node link in node.links)
            {
                toFileRec(link, depth + 1, fileWriter);
            }
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
