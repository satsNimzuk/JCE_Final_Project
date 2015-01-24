using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Stopwatch stopwatch = new Stopwatch();
            Utilities util = new Utilities();

            //Indexer indexer = new Indexer();
            //DataLayerManager dlm = new DataLayerManager();
            
            //TextManager tm = new TextManager();
            TreeManager treeMan = new TreeManager();

            //indexer.sortFileLines();


          

            //PageRankTester tester = new PageRankTester();
            //tester.test2();
            //tester.test1();

            
            
            Node tree = treeMan.buildTree("Tel Aviv", 1);


            

            //tree.toFile(Const.RESULTS_DIR_PATH + @"tree.txt");


            //Node otherTree = new Node(Const.RESULTS_DIR_PATH + @"tree_15_01_17_Berlin_depth_3.txt");

           

            //Node otherTree = new Node(Const.RESULTS_DIR_PATH + @"tree_21_01_17_Tel_Aviv_depth_2.txt");



            

            //LinkMatrix lm = new LinkMatrix(otherTree);
            //lm.calculateRank(60);
            //Vector rank = lm.getPageRank();
            //rank.sortedToFile(Const.RESULTS_DIR_PATH + @"page_rank_result.txt");



            //MaxVariancePathFinder mvpf = new MaxVariancePathFinder();
            //ParabolicPathFinder ppf = new ParabolicPathFinder();
            //Path gridyPath = pf.findGridyPath(tree, rank);
            //Path path = pf.findPath(tree, rank);
            //List<Path> paths_1 = pf.findNPaths(tree,rank,10);



            //List<ParabolicPath> par_paths = ppf.findNPathsParabolic(otherTree, rank, 10);


            //ppf.pathsListToFile(par_paths, rank, Const.RESULTS_DIR_PATH + @"parabolic_paths_with_rank.txt");

            //stopwatch.Start();

            DocumentCollection dc = new DocumentCollection(tree);

            //stopwatch.Stop();
            //var t = stopwatch.Elapsed;

            //List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Tel Aviv Keywords.txt");
            //dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            //List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Tel Aviv Keywords.txt");            
            //dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            //List<Document> idfRank = dc.calcIdfRank();
            //dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            //List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Tel Aviv Keywords.txt");
            //dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");

            //List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            //List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            //List<Document> idfRank = dc.calcIdfRank();
            //dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            //List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");


            List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            List<Document> idfRank = dc.calcIdfRank();
            dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");

           
            //gridyPath.pathToFile(rank, Const.RESULTS_DIR_PATH + @"gridy_path_with_rank.txt");
            //path.pathToFile(rank, Const.RESULTS_DIR_PATH + @"path_with_rank.txt");
            //(new Path()).pathsListToFile(paths, rank, Const.RESULTS_DIR_PATH + @"paths_with_rank.txt");
            
            //util.treeWithPageRankToFile(tree, rank, Const.RESULTS_DIR_PATH + @"tree_with_rank.txt");


           
        }
    }
}
