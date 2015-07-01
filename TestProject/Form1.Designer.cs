namespace FinalProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.TextBox();
            this.articleNameLabel = new System.Windows.Forms.Label();
            this.articleNameTextBox = new System.Windows.Forms.TextBox();
            this.graphDepthLabel = new System.Windows.Forms.Label();
            this.graphDepthTextBox = new System.Windows.Forms.TextBox();
            this.buildGraphButton = new System.Windows.Forms.Button();
            this.saveGraphButton = new System.Windows.Forms.Button();
            this.loadGraphButton = new System.Windows.Forms.Button();
            this.resultLengthLabel = new System.Windows.Forms.Label();
            this.resultLengthTextBox = new System.Windows.Forms.TextBox();
            this.loadGraphDialog = new System.Windows.Forms.OpenFileDialog();
            this.enforceBacklinkCheckBox = new System.Windows.Forms.CheckBox();
            this.findPathButton = new System.Windows.Forms.Button();
            this.loadRankButton = new System.Windows.Forms.Button();
            this.calcPageRankButton = new System.Windows.Forms.Button();
            this.saveRankButton = new System.Windows.Forms.Button();
            this.graphInfoLabel = new System.Windows.Forms.Label();
            this.rankInfoLabel = new System.Windows.Forms.Label();
            this.reduceArticlesSetButton = new System.Windows.Forms.Button();
            this.reduceArticlesSetComboBox = new System.Windows.Forms.ComboBox();
            this.reduceArticlesSetLabel = new System.Windows.Forms.Label();
            this.articlesSetInfoLabel = new System.Windows.Forms.Label();
            this.savePathButton = new System.Windows.Forms.Button();
            this.pathInfoLabel = new System.Windows.Forms.Label();
            this.calcTfIdfButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Enabled = false;
            this.label.Location = new System.Drawing.Point(13, 13);
            this.label.Multiline = true;
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(453, 794);
            this.label.TabIndex = 0;
            this.label.WordWrap = false;
            // 
            // articleNameLabel
            // 
            this.articleNameLabel.AutoSize = true;
            this.articleNameLabel.Location = new System.Drawing.Point(44, 42);
            this.articleNameLabel.Name = "articleNameLabel";
            this.articleNameLabel.Size = new System.Drawing.Size(68, 13);
            this.articleNameLabel.TabIndex = 1;
            this.articleNameLabel.Text = "Article name:";
            // 
            // articleNameTextBox
            // 
            this.articleNameTextBox.Location = new System.Drawing.Point(138, 42);
            this.articleNameTextBox.Name = "articleNameTextBox";
            this.articleNameTextBox.Size = new System.Drawing.Size(271, 20);
            this.articleNameTextBox.TabIndex = 2;
            // 
            // graphDepthLabel
            // 
            this.graphDepthLabel.AutoSize = true;
            this.graphDepthLabel.Location = new System.Drawing.Point(44, 89);
            this.graphDepthLabel.Name = "graphDepthLabel";
            this.graphDepthLabel.Size = new System.Drawing.Size(69, 13);
            this.graphDepthLabel.TabIndex = 3;
            this.graphDepthLabel.Text = "Graph depth:";
            // 
            // graphDepthTextBox
            // 
            this.graphDepthTextBox.Location = new System.Drawing.Point(138, 86);
            this.graphDepthTextBox.Name = "graphDepthTextBox";
            this.graphDepthTextBox.Size = new System.Drawing.Size(271, 20);
            this.graphDepthTextBox.TabIndex = 4;
            // 
            // buildGraphButton
            // 
            this.buildGraphButton.Location = new System.Drawing.Point(47, 143);
            this.buildGraphButton.Name = "buildGraphButton";
            this.buildGraphButton.Size = new System.Drawing.Size(75, 23);
            this.buildGraphButton.TabIndex = 5;
            this.buildGraphButton.Text = "Build graph";
            this.buildGraphButton.UseVisualStyleBackColor = true;
            this.buildGraphButton.Click += new System.EventHandler(this.onBuildGraphButton);
            // 
            // saveGraphButton
            // 
            this.saveGraphButton.Location = new System.Drawing.Point(253, 143);
            this.saveGraphButton.Name = "saveGraphButton";
            this.saveGraphButton.Size = new System.Drawing.Size(75, 23);
            this.saveGraphButton.TabIndex = 6;
            this.saveGraphButton.Text = "Save graph";
            this.saveGraphButton.UseVisualStyleBackColor = true;
            this.saveGraphButton.Click += new System.EventHandler(this.onSaveGraphButton);
            // 
            // loadGraphButton
            // 
            this.loadGraphButton.Location = new System.Drawing.Point(334, 143);
            this.loadGraphButton.Name = "loadGraphButton";
            this.loadGraphButton.Size = new System.Drawing.Size(75, 23);
            this.loadGraphButton.TabIndex = 7;
            this.loadGraphButton.Text = "Load graph";
            this.loadGraphButton.UseVisualStyleBackColor = true;
            this.loadGraphButton.Click += new System.EventHandler(this.onLoadGraphButton);
            // 
            // resultLengthLabel
            // 
            this.resultLengthLabel.AutoSize = true;
            this.resultLengthLabel.Location = new System.Drawing.Point(44, 503);
            this.resultLengthLabel.Name = "resultLengthLabel";
            this.resultLengthLabel.Size = new System.Drawing.Size(72, 13);
            this.resultLengthLabel.TabIndex = 8;
            this.resultLengthLabel.Text = "Result length:";
            // 
            // resultLengthTextBox
            // 
            this.resultLengthTextBox.Location = new System.Drawing.Point(138, 503);
            this.resultLengthTextBox.Name = "resultLengthTextBox";
            this.resultLengthTextBox.Size = new System.Drawing.Size(271, 20);
            this.resultLengthTextBox.TabIndex = 9;
            // 
            // loadGraphDialog
            // 
            this.loadGraphDialog.FileName = "openFileDialog1";
            // 
            // enforceBacklinkCheckBox
            // 
            this.enforceBacklinkCheckBox.AutoSize = true;
            this.enforceBacklinkCheckBox.Location = new System.Drawing.Point(150, 565);
            this.enforceBacklinkCheckBox.Name = "enforceBacklinkCheckBox";
            this.enforceBacklinkCheckBox.Size = new System.Drawing.Size(106, 17);
            this.enforceBacklinkCheckBox.TabIndex = 11;
            this.enforceBacklinkCheckBox.Text = "Enforce backlink";
            this.enforceBacklinkCheckBox.UseVisualStyleBackColor = true;
            // 
            // findPathButton
            // 
            this.findPathButton.Location = new System.Drawing.Point(47, 559);
            this.findPathButton.Name = "findPathButton";
            this.findPathButton.Size = new System.Drawing.Size(97, 23);
            this.findPathButton.TabIndex = 14;
            this.findPathButton.Text = "Calculate path";
            this.findPathButton.UseVisualStyleBackColor = true;
            this.findPathButton.Click += new System.EventHandler(this.onFindPathButtonClick);
            // 
            // loadRankButton
            // 
            this.loadRankButton.Location = new System.Drawing.Point(334, 231);
            this.loadRankButton.Name = "loadRankButton";
            this.loadRankButton.Size = new System.Drawing.Size(75, 23);
            this.loadRankButton.TabIndex = 20;
            this.loadRankButton.Text = "Load rank";
            this.loadRankButton.UseVisualStyleBackColor = true;
            this.loadRankButton.Click += new System.EventHandler(this.onLoadRankButtonClick);
            // 
            // calcPageRankButton
            // 
            this.calcPageRankButton.Location = new System.Drawing.Point(47, 231);
            this.calcPageRankButton.Name = "calcPageRankButton";
            this.calcPageRankButton.Size = new System.Drawing.Size(96, 23);
            this.calcPageRankButton.TabIndex = 21;
            this.calcPageRankButton.Text = "Calculate rank";
            this.calcPageRankButton.UseVisualStyleBackColor = true;
            this.calcPageRankButton.Click += new System.EventHandler(this.onCalcPageRankButtonClick);
            // 
            // saveRankButton
            // 
            this.saveRankButton.Location = new System.Drawing.Point(253, 231);
            this.saveRankButton.Name = "saveRankButton";
            this.saveRankButton.Size = new System.Drawing.Size(75, 23);
            this.saveRankButton.TabIndex = 22;
            this.saveRankButton.Text = "Save rank";
            this.saveRankButton.UseVisualStyleBackColor = true;
            this.saveRankButton.Click += new System.EventHandler(this.onSaveRankButtonClick);
            // 
            // graphInfoLabel
            // 
            this.graphInfoLabel.AutoSize = true;
            this.graphInfoLabel.Location = new System.Drawing.Point(44, 184);
            this.graphInfoLabel.Name = "graphInfoLabel";
            this.graphInfoLabel.Size = new System.Drawing.Size(59, 13);
            this.graphInfoLabel.TabIndex = 23;
            this.graphInfoLabel.Text = "Graph info:";
            // 
            // rankInfoLabel
            // 
            this.rankInfoLabel.AutoSize = true;
            this.rankInfoLabel.Location = new System.Drawing.Point(44, 273);
            this.rankInfoLabel.Name = "rankInfoLabel";
            this.rankInfoLabel.Size = new System.Drawing.Size(56, 13);
            this.rankInfoLabel.TabIndex = 24;
            this.rankInfoLabel.Text = "Rank info:";
            // 
            // reduceArticlesSetButton
            // 
            this.reduceArticlesSetButton.Location = new System.Drawing.Point(47, 360);
            this.reduceArticlesSetButton.Name = "reduceArticlesSetButton";
            this.reduceArticlesSetButton.Size = new System.Drawing.Size(124, 23);
            this.reduceArticlesSetButton.TabIndex = 25;
            this.reduceArticlesSetButton.Text = "Rebuild articles set";
            this.reduceArticlesSetButton.UseVisualStyleBackColor = true;
            this.reduceArticlesSetButton.Click += new System.EventHandler(this.onReduceArticlesSetButtonClick);
            // 
            // reduceArticlesSetComboBox
            // 
            this.reduceArticlesSetComboBox.FormattingEnabled = true;
            this.reduceArticlesSetComboBox.Location = new System.Drawing.Point(219, 327);
            this.reduceArticlesSetComboBox.Name = "reduceArticlesSetComboBox";
            this.reduceArticlesSetComboBox.Size = new System.Drawing.Size(190, 21);
            this.reduceArticlesSetComboBox.TabIndex = 26;
            // 
            // reduceArticlesSetLabel
            // 
            this.reduceArticlesSetLabel.AutoSize = true;
            this.reduceArticlesSetLabel.Location = new System.Drawing.Point(44, 327);
            this.reduceArticlesSetLabel.Name = "reduceArticlesSetLabel";
            this.reduceArticlesSetLabel.Size = new System.Drawing.Size(139, 13);
            this.reduceArticlesSetLabel.TabIndex = 27;
            this.reduceArticlesSetLabel.Text = "Rebuild articles set strategy:";
            // 
            // articlesSetInfoLabel
            // 
            this.articlesSetInfoLabel.AutoSize = true;
            this.articlesSetInfoLabel.Location = new System.Drawing.Point(44, 404);
            this.articlesSetInfoLabel.Name = "articlesSetInfoLabel";
            this.articlesSetInfoLabel.Size = new System.Drawing.Size(81, 13);
            this.articlesSetInfoLabel.TabIndex = 28;
            this.articlesSetInfoLabel.Text = "Articles set info:";
            // 
            // savePathButton
            // 
            this.savePathButton.Location = new System.Drawing.Point(334, 559);
            this.savePathButton.Name = "savePathButton";
            this.savePathButton.Size = new System.Drawing.Size(75, 23);
            this.savePathButton.TabIndex = 29;
            this.savePathButton.Text = "Save path";
            this.savePathButton.UseVisualStyleBackColor = true;
            this.savePathButton.Click += new System.EventHandler(this.onSavePathButtonClick);
            // 
            // pathInfoLabel
            // 
            this.pathInfoLabel.AutoSize = true;
            this.pathInfoLabel.Location = new System.Drawing.Point(44, 604);
            this.pathInfoLabel.Name = "pathInfoLabel";
            this.pathInfoLabel.Size = new System.Drawing.Size(52, 13);
            this.pathInfoLabel.TabIndex = 30;
            this.pathInfoLabel.Text = "Path info:";
            // 
            // calcTfIdfButton
            // 
            this.calcTfIdfButton.Location = new System.Drawing.Point(47, 648);
            this.calcTfIdfButton.Name = "calcTfIdfButton";
            this.calcTfIdfButton.Size = new System.Drawing.Size(106, 23);
            this.calcTfIdfButton.TabIndex = 31;
            this.calcTfIdfButton.Text = "Calculate tf-idf";
            this.calcTfIdfButton.UseVisualStyleBackColor = true;
            this.calcTfIdfButton.Click += new System.EventHandler(this.onCalcTfIdfButton);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 819);
            this.Controls.Add(this.calcTfIdfButton);
            this.Controls.Add(this.pathInfoLabel);
            this.Controls.Add(this.savePathButton);
            this.Controls.Add(this.articlesSetInfoLabel);
            this.Controls.Add(this.reduceArticlesSetLabel);
            this.Controls.Add(this.reduceArticlesSetComboBox);
            this.Controls.Add(this.reduceArticlesSetButton);
            this.Controls.Add(this.rankInfoLabel);
            this.Controls.Add(this.graphInfoLabel);
            this.Controls.Add(this.saveRankButton);
            this.Controls.Add(this.calcPageRankButton);
            this.Controls.Add(this.loadRankButton);
            this.Controls.Add(this.findPathButton);
            this.Controls.Add(this.enforceBacklinkCheckBox);
            this.Controls.Add(this.resultLengthTextBox);
            this.Controls.Add(this.resultLengthLabel);
            this.Controls.Add(this.loadGraphButton);
            this.Controls.Add(this.saveGraphButton);
            this.Controls.Add(this.buildGraphButton);
            this.Controls.Add(this.graphDepthTextBox);
            this.Controls.Add(this.graphDepthLabel);
            this.Controls.Add(this.articleNameTextBox);
            this.Controls.Add(this.articleNameLabel);
            this.Controls.Add(this.label);
            this.Name = "Form1";
            this.Text = "Prototype Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox label;
        private System.Windows.Forms.Label articleNameLabel;
        private System.Windows.Forms.TextBox articleNameTextBox;
        private System.Windows.Forms.Label graphDepthLabel;
        private System.Windows.Forms.TextBox graphDepthTextBox;
        private System.Windows.Forms.Button buildGraphButton;
        private System.Windows.Forms.Button saveGraphButton;
        private System.Windows.Forms.Button loadGraphButton;
        private System.Windows.Forms.Label resultLengthLabel;
        private System.Windows.Forms.TextBox resultLengthTextBox;
        private System.Windows.Forms.OpenFileDialog loadGraphDialog;
        private System.Windows.Forms.CheckBox enforceBacklinkCheckBox;
        private System.Windows.Forms.Button findPathButton;
        private System.Windows.Forms.Button loadRankButton;
        private System.Windows.Forms.Button calcPageRankButton;
        private System.Windows.Forms.Button saveRankButton;
        private System.Windows.Forms.Label graphInfoLabel;
        private System.Windows.Forms.Label rankInfoLabel;
        private System.Windows.Forms.Button reduceArticlesSetButton;
        private System.Windows.Forms.ComboBox reduceArticlesSetComboBox;
        private System.Windows.Forms.Label reduceArticlesSetLabel;
        private System.Windows.Forms.Label articlesSetInfoLabel;
        private System.Windows.Forms.Button savePathButton;
        private System.Windows.Forms.Label pathInfoLabel;
        private System.Windows.Forms.Button calcTfIdfButton;

    }
}

