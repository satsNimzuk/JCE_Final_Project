namespace TestProject
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
            this.findPathButton = new System.Windows.Forms.Button();
            this.loadGraphDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.label.Size = new System.Drawing.Size(470, 332);
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
            this.graphDepthLabel.Location = new System.Drawing.Point(47, 86);
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
            this.saveGraphButton.Location = new System.Drawing.Point(138, 143);
            this.saveGraphButton.Name = "saveGraphButton";
            this.saveGraphButton.Size = new System.Drawing.Size(75, 23);
            this.saveGraphButton.TabIndex = 6;
            this.saveGraphButton.Text = "Save graph";
            this.saveGraphButton.UseVisualStyleBackColor = true;
            this.saveGraphButton.Click += new System.EventHandler(this.onSaveGraphButton);
            // 
            // loadGraphButton
            // 
            this.loadGraphButton.Location = new System.Drawing.Point(231, 143);
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
            this.resultLengthLabel.Location = new System.Drawing.Point(47, 209);
            this.resultLengthLabel.Name = "resultLengthLabel";
            this.resultLengthLabel.Size = new System.Drawing.Size(72, 13);
            this.resultLengthLabel.TabIndex = 8;
            this.resultLengthLabel.Text = "Result length:";
            // 
            // resultLengthTextBox
            // 
            this.resultLengthTextBox.Location = new System.Drawing.Point(138, 209);
            this.resultLengthTextBox.Name = "resultLengthTextBox";
            this.resultLengthTextBox.Size = new System.Drawing.Size(271, 20);
            this.resultLengthTextBox.TabIndex = 9;
            // 
            // findPathButton
            // 
            this.findPathButton.Location = new System.Drawing.Point(50, 266);
            this.findPathButton.Name = "findPathButton";
            this.findPathButton.Size = new System.Drawing.Size(75, 23);
            this.findPathButton.TabIndex = 10;
            this.findPathButton.Text = "Find path";
            this.findPathButton.UseVisualStyleBackColor = true;
            this.findPathButton.Click += new System.EventHandler(this.onFindPathButton);
            // 
            // loadGraphDialog
            // 
            this.loadGraphDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 357);
            this.Controls.Add(this.findPathButton);
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
        private System.Windows.Forms.Button findPathButton;
        private System.Windows.Forms.OpenFileDialog loadGraphDialog;

    }
}

