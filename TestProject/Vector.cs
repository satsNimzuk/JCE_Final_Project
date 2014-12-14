using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestProject
{
    class Vector
    {
        public Dictionary<String, double> vector;
        private String name;

        public Vector(String name)
        {
            vector = new Dictionary<String, double>();
            this.name = name;
        }

        public Vector()
        {
            vector = new Dictionary<String, double>();
        }

        public Vector(Vector other)
        {
            this.vector = new Dictionary<String, double>();
            this.name = other.name;
            foreach (KeyValuePair<String, double> entry in other.vector)
            {
                this.vector.Add(entry.Key, 0);
            }
        }

        public void setValue(string id, double value)
        {
            vector[id] = value;
        }

        public double getValue(String id)
        {
            return vector[id];
        }

        //public void sortedToFile(String fileName)
        //{
        //    FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            
        //    StreamWriter fileWriter = new StreamWriter(outFileStream);
        //    fileWriter.Write(this.sortedToString());
        //    fileWriter.Flush();
        //    fileWriter.Close();
        //    outFileStream.Close();
        //}

        public void sortedToFile(String fileName)
        {

            FileStream outFileStream = File.Open(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";

            List<KeyValuePair<String, double>> myList = vector.ToList();

            myList.Sort(
                delegate(KeyValuePair<String, double> firstPair,
                KeyValuePair<String, double> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );

            foreach (KeyValuePair<String, double> entry in myList)
            {
                result = entry.Value + "\t" + entry.Key + Environment.NewLine;
                fileWriter.WriteLine(result);
            }

            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public String sortedToString()
        {
            String result = "";

            List<KeyValuePair<String, double>> myList = vector.ToList();

            myList.Sort(
                delegate(KeyValuePair<String, double> firstPair,
                KeyValuePair<String, double> nextPair)
                {
                    return nextPair.Value.CompareTo(firstPair.Value);
                }
            );
            
            foreach (KeyValuePair<String, double> entry in myList)
            {
                result += entry.Value + "\t" + entry.Key + Environment.NewLine;
            }
            return result;
        }      

        public String toString()
        {
            String result = "";
            foreach (KeyValuePair<String, double> entry in vector)
            {
                result += entry.Value + "\t" + entry.Key + Environment.NewLine;
            }
            return result;
        }
    }
}
