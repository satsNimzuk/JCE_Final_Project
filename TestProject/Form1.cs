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

            
            //Node tree = treeMan.buildTree("Berlin", 3);
            

            //tree.toFile(Const.RESULTS_DIR_PATH + @"tree.txt");

            stopwatch.Start();
            Node otherTree = new Node(Const.RESULTS_DIR_PATH + @"tree_15_01_17_Berlin_depth_3.txt");


            LinkMatrix lm = new LinkMatrix(otherTree);

            lm.calculateRank(60);

            Vector rank = lm.getPageRank();

            MaxVariancePathFinder mvpf = new MaxVariancePathFinder();
            ParabolicPathFinder ppf = new ParabolicPathFinder();
            //Path gridyPath = pf.findGridyPath(tree, rank);
            //Path path = pf.findPath(tree, rank);
            //List<Path> paths_1 = pf.findNPaths(tree,rank,10);


            List<ParabolicPath> par_paths = ppf.findNPathsParabolic(otherTree, rank, 30);

            stopwatch.Stop();
            var t = stopwatch.Elapsed;

            ppf.pathsListToFile(par_paths, rank, Const.RESULTS_DIR_PATH + @"parabolic_paths_with_rank.txt");
            //gridyPath.pathToFile(rank, Const.RESULTS_DIR_PATH + @"gridy_path_with_rank.txt");
            //path.pathToFile(rank, Const.RESULTS_DIR_PATH + @"path_with_rank.txt");
            //(new Path()).pathsListToFile(paths, rank, Const.RESULTS_DIR_PATH + @"paths_with_rank.txt");
            //rank.sortedToFile(Const.RESULTS_DIR_PATH + @"page_rank_result.txt");
            //util.treeWithPageRankToFile(tree, rank, Const.RESULTS_DIR_PATH + @"tree_with_rank.txt");


           
        }
    }
}
