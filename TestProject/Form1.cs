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

            //dlm.runBuildIndex();
            //dlm.testIndex();
            //dlm.getRawArticleText(1124300703);
            //indexer.sortFileLines();
            //dlm.getArticleByName("Euchrysops horus");

            //tm.getLinksFromArticle("Admiralty Shipyard");

            //PageRankTester tester = new PageRankTester();
            //tester.test2();
            //tester.test1();

            stopwatch.Start();
            Node tree = treeMan.buildTree("Tel Aviv", 2);
            stopwatch.Stop();
            var t = stopwatch.Elapsed;

            //tree.toFile(Const.RESULTS_DIR_PATH + @"tree.txt");

            LinkMatrix lm = new LinkMatrix(tree);

            lm.calculateRank(600);

            Vector rank = lm.getPageRank();

            MaxVariancePathFinder pf = new MaxVariancePathFinder();
            //Path gridyPath = pf.findGridyPath(tree, rank);
            Path path = pf.findPath(tree, rank);
            List<Path> paths = pf.findNPaths(tree,rank,5);


            //gridyPath.pathToFile(rank, Const.RESULTS_DIR_PATH + @"gridy_path_with_rank.txt");
            //path.pathToFile(rank, Const.RESULTS_DIR_PATH + @"path_with_rank.txt");
            path.pathsListToFile(paths, rank, Const.RESULTS_DIR_PATH + @"paths_with_rank.txt");
            rank.sortedToFile(Const.RESULTS_DIR_PATH + @"page_rank_result.txt");
            //util.treeWithPageRankToFile(tree, rank, Const.RESULTS_DIR_PATH + @"tree_with_rank.txt");
           
        }
    }
}
