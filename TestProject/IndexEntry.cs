using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class IndexEntry
    {
        public String articleName;
        public long offset;
        public long length;

        public IndexEntry()
        {

        }


        public IndexEntry(String str)
        {
            IndexEntry entry = new IndexEntry();
            String[] arr = str.Split(',');
            String articleName = "";

            for (int i = 0; i < arr.Length - 2; i++)
            {
                articleName += arr[i];
                if (i < arr.Length - 3)
                {
                    articleName += @",";
                }
            }
            this.articleName = articleName.Trim();
            this.offset = Convert.ToInt64(arr[arr.Length - 2].Trim());
            this.length = Convert.ToInt64(arr[arr.Length - 1].Trim());
        }

        
        public override string ToString()
        {
            string result = this.articleName + " , " + this.offset + " , " + this.length + Environment.NewLine;
            return result;
        }

        public IndexEntry parseStringToIndexEntry(String str)
        {
            IndexEntry entry = new IndexEntry();
            String[] arr = str.Split(',');
            String articleName = "";

            for (int i = 0; i < arr.Length - 2; i++)
            {
                articleName += arr[i];
                if (i < arr.Length - 3)
                {
                    articleName += @",";
                }
            }
            entry.articleName = articleName;
            entry.offset = Convert.ToInt64(arr[arr.Length - 2].Trim());
            entry.length = Convert.ToInt64(arr[arr.Length - 1].Trim());

            return entry;
        }
    }
}
