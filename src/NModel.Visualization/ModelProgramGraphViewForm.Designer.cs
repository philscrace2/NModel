//-------------------------------------
// Copyright (c) Microsoft Corporation
//-------------------------------------
namespace NModel.Visualization
{
    partial class ModelProgramGraphViewForm
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
            this.panelForTestMode = new System.Windows.Forms.Panel();
            this.labelIsCorrectView = new System.Windows.Forms.Label();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonYes = new System.Windows.Forms.Button();
            this.menuStripMPV = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemViewModelProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.modelProgramGraphView1 = new NModel.Visualization.ModelProgramGraphView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboPublicMethods = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBuild = new System.Windows.Forms.ToolStripButton();
            this.panelForTestMode.SuspendLayout();
            this.menuStripMPV.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForTestMode
            // 
            this.panelForTestMode.Controls.Add(this.labelIsCorrectView);
            this.panelForTestMode.Controls.Add(this.buttonNo);
            this.panelForTestMode.Controls.Add(this.buttonYes);
            this.panelForTestMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelForTestMode.Location = new System.Drawing.Point(0, 464);
            this.panelForTestMode.Margin = new System.Windows.Forms.Padding(4);
            this.panelForTestMode.Name = "panelForTestMode";
            this.panelForTestMode.Size = new System.Drawing.Size(1168, 38);
            this.panelForTestMode.TabIndex = 1;
            this.panelForTestMode.Visible = false;
            // 
            // labelIsCorrectView
            // 
            this.labelIsCorrectView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIsCorrectView.AutoSize = true;
            this.labelIsCorrectView.Location = new System.Drawing.Point(769, 12);
            this.labelIsCorrectView.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIsCorrectView.Name = "labelIsCorrectView";
            this.labelIsCorrectView.Size = new System.Drawing.Size(161, 17);
            this.labelIsCorrectView.TabIndex = 2;
            this.labelIsCorrectView.Text = "Is the view as expected?";
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNo.Location = new System.Drawing.Point(1052, 6);
            this.buttonNo.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(100, 28);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYes.Location = new System.Drawing.Point(944, 6);
            this.buttonYes.Margin = new System.Windows.Forms.Padding(4);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(100, 28);
            this.buttonYes.TabIndex = 0;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            // 
            // menuStripMPV
            // 
            this.menuStripMPV.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMPV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.buildToolStripMenuItem});
            this.menuStripMPV.Location = new System.Drawing.Point(0, 0);
            this.menuStripMPV.Name = "menuStripMPV";
            this.menuStripMPV.Size = new System.Drawing.Size(1168, 28);
            this.menuStripMPV.TabIndex = 4;
            this.menuStripMPV.Text = "File";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings ...";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemViewModelProgram});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // mnuItemViewModelProgram
            // 
            this.mnuItemViewModelProgram.Image = global::NModel.Visualization.Resource1.startwithoutdebugging_6556;
            this.mnuItemViewModelProgram.Name = "mnuItemViewModelProgram";
            this.mnuItemViewModelProgram.Size = new System.Drawing.Size(241, 26);
            this.mnuItemViewModelProgram.Text = "View Model Program...";
            this.mnuItemViewModelProgram.Click += new System.EventHandler(this.mnuItemViewModelProgram_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.modelProgramGraphView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.88889F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1168, 432);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // modelProgramGraphView1
            // 
            this.modelProgramGraphView1.CustomStateLabelProvider = null;
            this.modelProgramGraphView1.CustomStateTooltipProvider = null;
            this.modelProgramGraphView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelProgramGraphView1.InitialTransitions = -1;
            this.modelProgramGraphView1.Location = new System.Drawing.Point(4, 51);
            this.modelProgramGraphView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.modelProgramGraphView1.Name = "modelProgramGraphView1";
            this.modelProgramGraphView1.Size = new System.Drawing.Size(1160, 377);
            this.modelProgramGraphView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1162, 41);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboPublicMethods,
            this.toolStripSeparator1,
            this.btnBuild});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1162, 28);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(115, 25);
            this.toolStripLabel1.Text = "Public Members";
            // 
            // cboPublicMethods
            // 
            this.cboPublicMethods.DropDownWidth = 350;
            this.cboPublicMethods.Name = "cboPublicMethods";
            this.cboPublicMethods.Size = new System.Drawing.Size(350, 28);
            this.cboPublicMethods.SelectedIndexChanged += new System.EventHandler(this.CboPublicMethods_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // btnBuild
            // 
            this.btnBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBuild.Image = global::NModel.Visualization.Resource1.startwithoutdebugging_6556;
            this.btnBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(29, 25);
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // ModelProgramGraphViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1168, 502);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelForTestMode);
            this.Controls.Add(this.menuStripMPV);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ModelProgramGraphViewForm";
            this.Text = "ModelProgramGraphViewForm";
            this.panelForTestMode.ResumeLayout(false);
            this.panelForTestMode.PerformLayout();
            this.menuStripMPV.ResumeLayout(false);
            this.menuStripMPV.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelForTestMode;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Label labelIsCorrectView;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.MenuStrip menuStripMPV;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ModelProgramGraphView modelProgramGraphView1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboPublicMethods;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnBuild;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemViewModelProgram;
    }
}