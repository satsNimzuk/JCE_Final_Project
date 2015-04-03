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
        private Node graph;

        public Form1()
        {
            InitializeComponent();
            
            
            //Stopwatch stopwatch = new Stopwatch();
            //Utilities util = new Utilities();

            //Indexer indexer = new Indexer();
            //DataLayerManager dlm = new DataLayerManager();
            
            //TextManager tm = new TextManager();
            

            //indexer.sortFileLines();


          

            //PageRankTester tester = new PageRankTester();
            //tester.test2();
            //tester.test1();



            


            

            


            //Node otherTree = new Node(Const.RESULTS_DIR_PATH + @"tree_15_01_17_Berlin_depth_3.txt");

           

            //Node otherTree = new Node(Const.RESULTS_DIR_PATH + @"tree_21_01_17_Tel_Aviv_depth_2.txt");









            //MaxVariancePathFinder mvpf = new MaxVariancePathFinder();
            
            //Path gridyPath = pf.findGridyPath(tree, rank);
            //Path path = pf.findPath(tree, rank);
            //List<Path> paths_1 = pf.findNPaths(tree,rank,10);



            


            

            //stopwatch.Start();

            //DocumentCollection dc = new DocumentCollection(tree);

            //stopwatch.Stop();
            //var t = stopwatch.Elapsed;

            //List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            //List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            //List<Document> idfRank = dc.calcIdfRank();
            //dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            //List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");

            //List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            //List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            //List<Document> idfRank = dc.calcIdfRank();
            //dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            //List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Jaffa Clock Tower Keywords.txt");
            //dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");


            //List<Document> rawTfRank = dc.calcRawTfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            //dc.rankToFile(rawTfRank, Const.RESULTS_DIR_PATH + @"raw_tf_rank.txt");
            //List<Document> logTfRank = dc.calcLogTfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            //dc.rankToFile(logTfRank, Const.RESULTS_DIR_PATH + @"log_tf_rank.txt");
            //List<Document> idfRank = dc.calcIdfRank();
            //dc.rankToFile(idfRank, Const.RESULTS_DIR_PATH + @"idf_rank.txt");
            //List<Document> tfIdfRank = dc.calcTfIdfQuery(Const.RESULTS_DIR_PATH + @"Israel Keywords.txt");
            //dc.rankToFile(tfIdfRank, Const.RESULTS_DIR_PATH + @"tf_idf_rank.txt");

           
            //gridyPath.pathToFile(rank, Const.RESULTS_DIR_PATH + @"gridy_path_with_rank.txt");
            //path.pathToFile(rank, Const.RESULTS_DIR_PATH + @"path_with_rank.txt");
            //(new Path()).pathsListToFile(paths, rank, Const.RESULTS_DIR_PATH + @"paths_with_rank.txt");
            
            //util.treeWithPageRankToFile(tree, rank, Const.RESULTS_DIR_PATH + @"tree_with_rank.txt");


           
        }

        private void onBuildGraphButton(object sender, EventArgs e)
        {

            if (articleNameTextBox.Text == "" )
            {
                return;
            }

            this.Enabled = false;

            int depth = 0;
            if (!Int32.TryParse(graphDepthTextBox.Text, out depth))
            {
                depth = 2;
            }
            if (depth > 3){ depth = 3; }
            if (depth < 1){ depth = 1; }

            TreeManager treeManager = new TreeManager();
            this.graph = treeManager.buildTree(articleNameTextBox.Text, (byte)depth);

            this.Enabled = true;
        }

        private void onSaveGraphButton(object sender, EventArgs e)
        {
            if (this.graph == null)
            {
                return;
            }

            this.Enabled = false;

            this.graph.toFile(Const.RESULTS_DIR_PATH + @"graph.txt");

            this.Enabled = true;
        }

        private void onLoadGraphButton(object sender, EventArgs e)
        {
            this.Enabled = false;

            loadGraphDialog.InitialDirectory = Const.RESULTS_DIR_PATH;
            loadGraphDialog.FileName = @"graph.txt";
            DialogResult result = loadGraphDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                String fileName = loadGraphDialog.FileName;
                this.graph = new Node(fileName);
            }

            this.Enabled = true;
        }

        private void onFindPathButton(object sender, EventArgs e)
        {
            if (this.graph == null)
            {
                return;
            }
            this.Enabled = false;

            LinkMatrix lm = new LinkMatrix(this.graph);
            lm.calculateRank(60);
            Vector pageRank = lm.getPageRank();
            pageRank.sortedToFile(Const.RESULTS_DIR_PATH + @"page_rank.txt");

            int resultLength = 0;
            if (!Int32.TryParse(resultLengthTextBox.Text, out resultLength))
            {
                resultLength = 3;
            }
            if (resultLength < 1) { resultLength = 1; }

            ParabolicPathFinder ppf = new ParabolicPathFinder();
            //List<ParabolicPath> par_paths = ppf.findNPathsParabolic(this.graph, pageRank, resultLength);
            List<ParabolicPath> par_paths = ppf.findNPathsParabolicWithBackLink(this.graph, pageRank, resultLength,true);
            ppf.pathsListToFile(par_paths, pageRank, Const.RESULTS_DIR_PATH + @"paths.txt");

            this.Enabled = true;

        }
    }
}
