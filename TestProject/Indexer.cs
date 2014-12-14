using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestProject
{
    class Indexer
    {

        Utilities util;

        public Indexer()
        {
            util = new Utilities();
        }

        public void buildIndexLevel_0(String sourceFilePath)
        {
            String indexFilePath = Const.WORK_DIR_PATH + Const.INDEX_LEVEL_0_FILE_NAME;
            FileStream inFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
            FileStream outFileStream = File.Open(indexFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);
            long totalOffset = 0;
            String currentLine = "";
            String title = "";
            String indexEntry = "";
            int bytesRead = 0;
            byte[] lineBuffer = new byte[1000000];
            try
            {
                while ((bytesRead = util.getLine(inFileStream, ref lineBuffer)) > 0)
                {
                    totalOffset += bytesRead;
                    currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                    if (isTitle(ref currentLine, ref title))
                    {
                        indexEntry = title + " , " + (totalOffset - bytesRead).ToString() + Environment.NewLine;
                        fileWriter.Write(indexEntry);
                    }
                };
            }
            finally
            {
                fileWriter.Flush();
                fileWriter.Close();
                outFileStream.Close();
                inFileStream.Close();
            }
        }

        public void sortFileLines()
        {
            //buildIndexLevel_0(Const.WIKI_FILE_PATH);
            //int fileCount = buildIndexPartition(Const.WORK_DIR_PATH + @"index_level_0UNSORTED.txt");
            //mergeIndexPartition(fileCount);
            //mergeIndexPartition(144);
            //remove all temporary index partitions
            //buildIndexLevel_N(Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + 0 + @".txt", 10, 1);
            //buildIndexLevel_N(Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + 1 + @".txt", 10, 2);
            //buildIndexLevel_N(Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + 2 + @".txt", 10, 3);
            //buildIndexLevel_N(Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + 3 + @".txt", 10, 4);
        }

        public void buildIndexLevel_N(String sourceFilePath, int density, int level)
        {
            String indexFilePath = Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + level + @".txt";
            FileStream inFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
            FileStream outFileStream = File.Open(indexFilePath, FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);
            long totalOffset = 0;
            String currentLine = "";
            String[] strArray;
            String indexEntry = "";
            int bytesRead = 0;
            int counter = 0;
            byte[] lineBuffer = new byte[10000];
            try
            {
                while ((bytesRead = util.getLine(inFileStream, ref lineBuffer)) > 0)
                {
                    totalOffset += bytesRead;
                    if (counter % density == 0)
                    {
                        currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                        strArray = currentLine.Split(',');
                        strArray[strArray.Count() - 1] = "";
                        currentLine = "";
                        foreach (String str in strArray)
                        {
                            currentLine += str;
                        }
                        currentLine = currentLine.Trim();
                        indexEntry = currentLine + " , " + (totalOffset - bytesRead).ToString() + Environment.NewLine;
                        fileWriter.Write(indexEntry);
                    }
                    counter ++;
                };
            }
            finally
            {
                fileWriter.Flush();
                fileWriter.Close();
                outFileStream.Close();
                inFileStream.Close();
            }
        }

        private void mergeIndexPartition(int N)
        {
            byte[] lineBuffer = new byte[10000];
            string currentLine;
            int bytesRead = 0;
            int idx = 0;
            String outputFile = Const.WORK_DIR_PATH + @"index_level_0.txt";

            FileStream outFileStream = File.Open(outputFile, FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);
            String[] fullLineArray = new String[N+1];

            FileStream[] inFileStreamArray = new FileStream[N + 1];
            for (int i = 0; i <= N; i++)
            {
                String fileName = Const.WORK_DIR_PATH + Const.INDEX_PART_FILE_NAME + i + @".txt";
                inFileStreamArray[i] = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }

            SortedDictionary<string, int> sort = new SortedDictionary<string, int>();

            foreach (FileStream fstream in inFileStreamArray)
            {
                bytesRead = util.getLine(fstream, ref lineBuffer);
                currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                fullLineArray[idx] = currentLine;
                try
                {
                    sort.Add(extractArticleNameFromIndexEntry(currentLine), idx);
                }
                catch
                {
                }
                idx ++;
            }

            while (sort.Count > 0)
            {
                int fileNum = sort.ElementAt(0).Value;
                //String str = sort.ElementAt(0).Key;
                String str = fullLineArray[fileNum];
                //sort.Remove(str);
                sort.Remove(sort.ElementAt(0).Key);

                fileWriter.Write(str + Environment.NewLine);

                bytesRead = util.getLine(inFileStreamArray[fileNum], ref lineBuffer);
                currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                fullLineArray[fileNum] = currentLine;
                if (!currentLine.Equals(""))
                {
                    try
                    {
                        sort.Add(extractArticleNameFromIndexEntry(currentLine), fileNum);
                    }
                    catch
                    {
                    }
                }

            }

            fileWriter.Flush();
            fileWriter.Close();

        }

        private int buildIndexPartition(String indexFilePath)
        {
            FileStream inFileStream = new FileStream(indexFilePath, FileMode.Open, FileAccess.Read);
            List<String> array;
            int N = 100000;

            byte[] lineBuffer = new byte[1000];
            int fileCount = 0;
            String fileName = Const.INDEX_PART_FILE_NAME;


            while (true)
            {

                array = util.readNLinesToList(N, inFileStream);
                if (array.Count == 0)
                {
                    break;
                }
                array = quickSort(array);

                String tmpFileName = Const.WORK_DIR_PATH + fileName + fileCount.ToString() + @".txt";
                FileStream outFileStream = File.Open(tmpFileName, FileMode.Append, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(outFileStream);
                foreach (String str in array)
                {
                    fileWriter.Write(str + Environment.NewLine);
                }
                array.Clear();
                fileWriter.Flush();
                fileWriter.Close();
                fileCount++;

            }

            inFileStream.Close();
            return fileCount;
        }

        private String extractArticleNameFromIndexEntry(String entry)
        {
            String result = "";
            String[] arr = entry.Split(',');
            for (int i = 0; i < arr.Length - 1; i++)
            {
                result += arr[i];
                if (i < arr.Length - 2)
                {
                    result += @",";
                }
            }
            result = result.Trim();
            return result;
        }

        public List<String> quickSort(List<String> arr)
        {
            int numOfStr = arr.Count;
            if (numOfStr <= 1)
            {
                return arr;
            }
            List<String> lesser = new List<String>();
            List<String> greater = new List<String>();
            int sameAsPivot = 0;
            int pivot = numOfStr / 2;

            foreach (String str in arr)
            {
                if (extractArticleNameFromIndexEntry(str).CompareTo(extractArticleNameFromIndexEntry(arr[pivot])) > 0)
                {
                    greater.Add(str);
                }
                else if (extractArticleNameFromIndexEntry(str).CompareTo(extractArticleNameFromIndexEntry(arr[pivot])) < 0)
                {
                    lesser.Add(str);
                }
                else
                {
                    sameAsPivot++;
                }
            }

            lesser = quickSort(lesser);
            for (int i = 0; i < sameAsPivot; i++)
            {
                lesser.Add(arr[pivot]);
            }
            greater = quickSort(greater);

            List<String> sorted = new List<String>();
            foreach (String str in lesser)
            {
                sorted.Add(str);
            }
            foreach (String str in greater)
            {
                sorted.Add(str);
            }
            return sorted;
        }

        private bool isTitle(ref String currentLine, ref String title)
        {
            if (currentLine == null || title == null || currentLine.Length < 16)
            {
                return false;
            }
            title = "WARNING title wasn't parsed as expected";

            if (currentLine.Substring(0, 7).Equals(@"<title>") && currentLine.Substring(currentLine.Length - 8, 8).Equals(@"</title>"))
            {
                title = currentLine.Substring(7, currentLine.Length - 15).Trim();
                return true;
            }
            return false;
        }

    }
}
