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

namespace FinalProject
{
    public partial class Form1 : Form
    {

        private AppController appControl = new AppController();

        public Form1()
        {
            InitializeComponent();

            reduceArticlesSetComboBox.Items.Add("NONE");
            reduceArticlesSetComboBox.Items.Add("NORMAL");
            reduceArticlesSetComboBox.Items.Add("STRICT");
            reduceArticlesSetComboBox.SelectedItem = "NONE";

            
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // GRAPH
        //---------------------------------------------------------------------------------------------------------------------------

        private void onBuildGraphButton(object sender, EventArgs e)
        {
            if (articleNameTextBox.Text == "" )
            {
                return;
            }

            this.Enabled = false;

            int depth;
            if (!Int32.TryParse(graphDepthTextBox.Text, out depth))
            {
                depth = 2;
            }
            if (depth < 1){ depth = 1; }

            appControl.buildGraph(articleNameTextBox.Text, depth);

            this.Enabled = true;
        }

        private void onSaveGraphButton(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.saveCurrentGraphToFile();
            this.Enabled = true;
        }

        private void onLoadGraphButton(object sender, EventArgs e)
        {
            this.Enabled = false;
            loadGraphDialog.InitialDirectory = Const.RESULTS_DIR_PATH;
            loadGraphDialog.FileName = @"graph.txt";
            DialogResult result = loadGraphDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                String fileName = loadGraphDialog.FileName;
                this.appControl.loadGraph(fileName);
            }
            this.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // PAGE RANK
        //---------------------------------------------------------------------------------------------------------------------------

        private void onCalcPageRankButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.calcPageRank();
            this.Enabled = true;
        }

        private void onSaveRankButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.savePageRank();
            this.Enabled = true;
        }

        private void onLoadRankButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            loadGraphDialog.InitialDirectory = Const.RESULTS_DIR_PATH;
            loadGraphDialog.FileName = @".rnk";
            DialogResult result = loadGraphDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                String fileName = loadGraphDialog.FileName;
                this.appControl.loadPageRank(fileName);
            }
            this.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // ARTICLES SET
        //---------------------------------------------------------------------------------------------------------------------------

        private void onReduceArticlesSetButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.buildArticlesSubSet(this.reduceArticlesSetComboBox.SelectedItem.ToString());
            this.Enabled = true;
        }


        //---------------------------------------------------------------------------------------------------------------------------
        // FIND PATH
        //---------------------------------------------------------------------------------------------------------------------------

        private void onFindPathButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            int resultLength = 0;
            if (!Int32.TryParse(resultLengthTextBox.Text, out resultLength))
            {
                resultLength = 15;
            }
            if (resultLength < 1) { resultLength = 1; }

            this.appControl.findPath(resultLength);

            this.Enabled = true;
        }

        private void onSavePathButtonClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.saveCurrentPathToFile();
            this.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // TF-IDF
        //---------------------------------------------------------------------------------------------------------------------------

        private void onCalcTfIdfButton(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.appControl.calcTfIdf();
            this.Enabled = true;
        }

    }
}
