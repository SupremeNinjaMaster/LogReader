﻿partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this._richTextContentsBox = new System.Windows.Forms.RichTextBox();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatLastSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleRTFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importLogOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openLogFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._openOptionsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveOptionsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _richTextContentsBox
            // 
            this._richTextContentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._richTextContentsBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._richTextContentsBox.Location = new System.Drawing.Point(0, 24);
            this._richTextContentsBox.Name = "_richTextContentsBox";
            this._richTextContentsBox.ReadOnly = true;
            this._richTextContentsBox.Size = new System.Drawing.Size(821, 649);
            this._richTextContentsBox.TabIndex = 0;
            this._richTextContentsBox.Text = "";
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.findToolStripMenuItem,
            this.optionsToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(821, 24);
            this._menuStrip.TabIndex = 1;
            this._menuStrip.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fIleToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.refreshToolStripMenuItem.Text = "Reload";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextSelectionToolStripMenuItem,
            this.prevSelectionToolStripMenuItem,
            this.findToolStripMenuItem1,
            this.repeatLastSearchToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.findToolStripMenuItem.Text = "Search";
            // 
            // nextSelectionToolStripMenuItem
            // 
            this.nextSelectionToolStripMenuItem.Name = "nextSelectionToolStripMenuItem";
            this.nextSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.nextSelectionToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.nextSelectionToolStripMenuItem.Text = "Next Selection";
            this.nextSelectionToolStripMenuItem.Click += new System.EventHandler(this.nextSelectionToolStripMenuItem_Click);
            // 
            // prevSelectionToolStripMenuItem
            // 
            this.prevSelectionToolStripMenuItem.Name = "prevSelectionToolStripMenuItem";
            this.prevSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F3)));
            this.prevSelectionToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.prevSelectionToolStripMenuItem.Text = "Prev Selection";
            this.prevSelectionToolStripMenuItem.Click += new System.EventHandler(this.prevSelectionToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem1
            // 
            this.findToolStripMenuItem1.Name = "findToolStripMenuItem1";
            this.findToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.findToolStripMenuItem1.Text = "Find";
            this.findToolStripMenuItem1.Click += new System.EventHandler(this.findToolStripMenuItem1_Click);
            // 
            // repeatLastSearchToolStripMenuItem
            // 
            this.repeatLastSearchToolStripMenuItem.Name = "repeatLastSearchToolStripMenuItem";
            this.repeatLastSearchToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.repeatLastSearchToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.repeatLastSearchToolStripMenuItem.Text = "Repeat Last Search";
            this.repeatLastSearchToolStripMenuItem.Click += new System.EventHandler(this.repeatLastSearchToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOptionsToolStripMenuItem,
            this.toggleRTFToolStripMenuItem,
            this.exportLogOptionsToolStripMenuItem,
            this.importLogOptionsToolStripMenuItem});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem1.Text = "Options";
            // 
            // logOptionsToolStripMenuItem
            // 
            this.logOptionsToolStripMenuItem.Name = "logOptionsToolStripMenuItem";
            this.logOptionsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.logOptionsToolStripMenuItem.Text = "Log Options";
            this.logOptionsToolStripMenuItem.Click += new System.EventHandler(this.logOptionsToolStripMenuItem_Click);
            // 
            // toggleRTFToolStripMenuItem
            // 
            this.toggleRTFToolStripMenuItem.Name = "toggleRTFToolStripMenuItem";
            this.toggleRTFToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.toggleRTFToolStripMenuItem.Text = "Toggle RTF";
            this.toggleRTFToolStripMenuItem.Click += new System.EventHandler(this.toggleRTFToolStripMenuItem_Click);
            // 
            // exportLogOptionsToolStripMenuItem
            // 
            this.exportLogOptionsToolStripMenuItem.Name = "exportLogOptionsToolStripMenuItem";
            this.exportLogOptionsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exportLogOptionsToolStripMenuItem.Text = "Export Log Options";
            this.exportLogOptionsToolStripMenuItem.Click += new System.EventHandler(this.exportLogOptionsToolStripMenuItem_Click);
            // 
            // importLogOptionsToolStripMenuItem
            // 
            this.importLogOptionsToolStripMenuItem.Name = "importLogOptionsToolStripMenuItem";
            this.importLogOptionsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.importLogOptionsToolStripMenuItem.Text = "Import Log Options";
            this.importLogOptionsToolStripMenuItem.Click += new System.EventHandler(this.importLogOptionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // _openLogFileDialog
            // 
            this._openLogFileDialog.DefaultExt = "log";
            this._openLogFileDialog.FileName = "*.log";
            this._openLogFileDialog.Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt";
            this._openLogFileDialog.RestoreDirectory = true;
            this._openLogFileDialog.ShowReadOnly = true;
            // 
            // _openOptionsFileDialog
            // 
            this._openOptionsFileDialog.FileName = "*.xml";
            this._openOptionsFileDialog.Filter = "Options files|*.xml";
            // 
            // _saveOptionsFileDialog
            // 
            this._saveOptionsFileDialog.DefaultExt = "xml";
            this._saveOptionsFileDialog.FileName = "LogTypes.xml";
            this._saveOptionsFileDialog.Filter = "Options files|*.xml";
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogsToolStripMenuItem});
            this._contextMenuStrip.Name = "contextMenuStrip1";
            this._contextMenuStrip.Size = new System.Drawing.Size(181, 48);
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearLogsToolStripMenuItem.Text = "Clear Logs";
            this.clearLogsToolStripMenuItem.Click += new System.EventHandler(this.clearLogsToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 673);
            this.Controls.Add(this._richTextContentsBox);
            this.Controls.Add(this._menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Name = "MainWindow";
            this.Text = "Log Reader";
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
       

    #endregion

    private System.Windows.Forms.RichTextBox _richTextContentsBox;
    private System.Windows.Forms.MenuStrip _menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog _openLogFileDialog;
    private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nextSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem prevSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem logOptionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toggleRTFToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem repeatLastSearchToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportLogOptionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importLogOptionsToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog _openOptionsFileDialog;
    private System.Windows.Forms.SaveFileDialog _saveOptionsFileDialog;
    private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
}


