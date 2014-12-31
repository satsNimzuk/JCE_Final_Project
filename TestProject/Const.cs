using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    static class Const
    {
        public static String WORK_DIR_PATH = @"C:\Users\Stas\Downloads\WikiPedia\";
        public static String DB_DIR_PATH = WORK_DIR_PATH + @"DB\";
        public static String RESULTS_DIR_PATH = WORK_DIR_PATH + @"Results\";
        public static String WIKI_FILE_PATH =  DB_DIR_PATH + @"enwiki-20140502-pages-articles.xml";

        public static String INDEX_PART_FILE_NAME = @"indexPartition";
        public static String INDEX_LEVEL_0_FILE_NAME = @"index_level_0.txt";
        public static String INDEX_LEVEL_N_FILE_NAME = @"index_level_";

        public static int INDEX_DEPTH = 5;

    }
}
