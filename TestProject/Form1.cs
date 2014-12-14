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

            PageRankTester tester = new PageRankTester();
            tester.test2();
            tester.test1();

            stopwatch.Start();
            Node tree = treeMan.buildTree("Motorcycle", 3);
            stopwatch.Stop();
            var t = stopwatch.Elapsed;

            tree.toFile(Const.WORK_DIR_PATH + @"result.txt");

            LinkMatrix lm = new LinkMatrix(tree);

            lm.calculateRank(60);

            Vector rank = lm.getPageRank();

            MaxVariancePathFinder pf = new MaxVariancePathFinder();
            List<Node> path = pf.findPath(tree, rank);

            rank.sortedToFile(Const.WORK_DIR_PATH + @"page_rank_result.txt");

            
            util.treeWithPageRankToFile(tree, rank, Const.WORK_DIR_PATH + @"tree_with_rank.txt");
           
        }
    }
}
