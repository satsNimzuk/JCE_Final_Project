using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TestProject
{
    class DataLayerManager
    {

        Utilities util;
        List<String> index;

        FileStream[] indexFileStreamArr = new FileStream[Const.INDEX_DEPTH - 1];
        FileStream wikiFileStream = new FileStream(Const.WIKI_FILE_PATH, FileMode.Open, FileAccess.Read);


        public DataLayerManager()
        {
            util = new Utilities();
            
            for (int i = 0; i < Const.INDEX_DEPTH - 1; i++)
            {
                String fileName = Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + i + @".txt";
                this.indexFileStreamArr[i] = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }

            index = readIndex();
        }

        public String getArticleByName(String name)
        {
            String result = "";
            int line = 0;

            line = findIndexLineByName(name);

            long offset = Convert.ToInt64(index[line].Split(',').Last().Trim());

            for (int i = Const.INDEX_DEPTH - 2; i >= 0; i--)
            {
                offset = findOffsetByName(name, i, offset);
            }

            result = getArticleByOffset(offset);


            return result;
        }

        private int findIndexLineByName(String name)
        {
            int left = 0;
            int right = index.Count - 1;

            if (index[left].CompareTo(name) >= 0)
            {
                return left;
            }
            if (index[right].CompareTo(name) <= 0)
            {
                return right;
            }           

            while (right >= left)
            {
                int mid = (right + left + 1) / 2;
                if (index[mid].CompareTo(name) < 0)
                {
                    left = mid;
                }
                else if (index[mid].CompareTo(name) > 0)
                {
                    if (index[mid - 1].CompareTo(name) <= 0)
                    {
                        return mid - 1;
                    }
                    else
                    {
                        right = mid;
                    }
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }

        private long findOffsetByName(String name, int level, long startOffset)
        {
            long result = startOffset;
            byte[] lineBuffer = new byte[10000];
            int bytesRead = 0;
            String currentLine = "";
            String[] strArray;
            int sanity_counter = 0;

            //String fileName = Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + level + @".txt";
            //FileStream inFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //inFileStream.Position = startOffset;
            this.indexFileStreamArr[level].Position = startOffset;

            //while ((bytesRead = util.getLine(inFileStream, ref lineBuffer)) > 0)
            while ((bytesRead = util.getLine(this.indexFileStreamArr[level], ref lineBuffer)) > 0)
            {
                if (sanity_counter > 100)
                {
                    return -1;
                }

                currentLine = System.Text.Encoding.Default.GetString(lineBuffer, 0, bytesRead).Trim();
                strArray = currentLine.Split(',');

                currentLine = "";

                for (int i = 0; i < strArray.Length - 1; i++ )
                {
                    currentLine += strArray[i];
                }
                currentLine = currentLine.Trim();

                

                if (currentLine.CompareTo(name) > 0)
                {
                    return result;
                }

                result = Convert.ToInt64(strArray.Last().Trim());

                sanity_counter++;
            }

            return result;
        }

        public String getArticleByOffset(long offset)
        {
            //FileStream fileStream = new FileStream(Const.WIKI_FILE_PATH, FileMode.Open, FileAccess.Read);
            //fileStream.Position = offset;
            wikiFileStream.Position = offset;

            byte[] buffer = new byte[1000000];
            String article = "";

            int watchdog = 0;

            while (true)
            {
                int bytesRead = util.getLine(wikiFileStream, ref buffer);
                String line = System.Text.Encoding.Default.GetString(buffer, 0, bytesRead);
                article += line;
                if (isEndOfArticle(line))
                {
                    //fileStream.Close();
                    return article;
                }
                if (watchdog > 1000000)
                {
                    return "WARNING: END OF ARTICLE WAS NOT FOUND";
                }
                watchdog++;
            }
        }





        private int getText(FileStream fileStream, ref byte[] buffer, long startPosition, int bytesToRead)
        {
            fileStream.Position = startPosition;
            int offset = 0;
            int ch;

            while ((ch = fileStream.ReadByte()) >= 0 && bytesToRead > 0)
            {
                buffer[offset] = (byte)ch;
                offset++;
                bytesToRead--;
            }
            return offset;
        }


        private bool isEndOfArticle(String line)
        {
            line = line.Trim();
            return line.Equals(@"</page>");
        }

        private List<String> readIndex()
        {
            String indexFilePath = Const.WORK_DIR_PATH + Const.INDEX_LEVEL_N_FILE_NAME + @"4.txt";
            FileStream inFileStream = new FileStream(indexFilePath, FileMode.Open, FileAccess.Read);
            List<String> result = util.readNLinesToList(-1, inFileStream);
            return result;
        }


    }
}


