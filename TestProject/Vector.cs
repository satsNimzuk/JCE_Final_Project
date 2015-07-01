using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
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

        public void sortedToFile(String fileName)
        {
            System.IO.File.WriteAllText(fileName, String.Empty); //clearing file content

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
                result = entry.Value + "\t|\t" + entry.Key + Environment.NewLine;
                fileWriter.WriteLine(result);
            }

            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public Vector loadFromFile(String fileName)
        {
            Dictionary<String, double> result = new Dictionary<string, double>();
            FileStream inFileStream = File.OpenRead(fileName);
            StreamReader fileReader = new StreamReader(inFileStream);
            String line;
            while ((line = fileReader.ReadLine()) != null)
            {
                String[] splits = line.Split('|');
                if (splits != null && splits.Length == 2)
                {
                    String key = splits[1].Trim();
                    double value = Double.Parse(splits[0].Trim());

                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, value);
                    }
                }
            }
            Vector vector = new Vector();
            vector.vector = result;
            return vector;
        }

        public int calcDecRank(String name)
        {
            if (!vector.ContainsKey(name))
            {
                return -1;
            }
            double min = 0;
            double max = this.vector.Values.Count;

            List<double> list = this.vector.Values.ToList();
            list.Sort();
            double value = list.IndexOf(this.vector[name]);

            int result = (int)(((value - min) / (max - min)) * 10);
            return result;
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
