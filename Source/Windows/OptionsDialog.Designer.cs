
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._searchBox = new System.Windows.Forms.TextBox();
            this.applyAllVerbosityButton = new System.Windows.Forms.Button();
            this._applyAllVerbosityComboBox = new System.Windows.Forms.ComboBox();
            this._logOptionListViewComboBox = new System.Windows.Forms.ComboBox();
            this._logOptionListView = new LogOptionListView();
            this.logColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.visibilityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colorDialog = new System.Windows.Forms.ColorDialog();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this._searchBox);
            this.panel2.Controls.Add(this.applyAllVerbosityButton);
            this.panel2.Controls.Add(this._applyAllVerbosityComboBox);
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(437, 83);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            // 
            // _searchBox
            // 
            this._searchBox.Location = new System.Drawing.Point(175, 7);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(142, 20);
            this._searchBox.TabIndex = 5;
            // 
            // applyAllVerbosityButton
            // 
            this.applyAllVerbosityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyAllVerbosityButton.Location = new System.Drawing.Point(11, 33);
            this.applyAllVerbosityButton.Name = "applyAllVerbosityButton";
            this.applyAllVerbosityButton.Size = new System.Drawing.Size(148, 23);
            this.applyAllVerbosityButton.TabIndex = 1;
            this.applyAllVerbosityButton.UseVisualStyleBackColor = true;
            this.applyAllVerbosityButton.Click += new System.EventHandler(this.ApplyAllVerbosityButton_Click);
            // 
            // _applyAllVerbosityComboBox
            // 
            this._applyAllVerbosityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._applyAllVerbosityComboBox.FormattingEnabled = true;
            this._applyAllVerbosityComboBox.Location = new System.Drawing.Point(167, 35);
            this._applyAllVerbosityComboBox.Name = "_applyAllVerbosityComboBox";
            this._applyAllVerbosityComboBox.Size = new System.Drawing.Size(121, 21);
            this._applyAllVerbosityComboBox.TabIndex = 0;
            // 
            // _logOptionListViewComboBox
            // 
            this._logOptionListViewComboBox.FormattingEnabled = true;
            this._logOptionListViewComboBox.Location = new System.Drawing.Point(168, 122);
            this._logOptionListViewComboBox.Name = "_logOptionListViewComboBox";
            this._logOptionListViewComboBox.Size = new System.Drawing.Size(121, 21);
            this._logOptionListViewComboBox.TabIndex = 5;
            // 
            // _logOptionListView
            // 
            this._logOptionListView.ColorSelected = null;
            this._logOptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logColumnHeader,
            this.visibilityColumnHeader,
            this.colorColumnHeader});
            this._logOptionListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._logOptionListView.FullRowSelect = true;
            this._logOptionListView.HideSelection = false;
            this._logOptionListView.Location = new System.Drawing.Point(0, 78);
            this._logOptionListView.MultiSelect = false;
            this._logOptionListView.Name = "_logOptionListView";
            this._logOptionListView.Size = new System.Drawing.Size(437, 396);
            this._logOptionListView.TabIndex = 1;
            this._logOptionListView.UseCompatibleStateImageBehavior = false;
            this._logOptionListView.VerbositySelected = null;
            this._logOptionListView.View = System.Windows.Forms.View.Details;
            // 
            // logColumnHeader
            // 
            this.logColumnHeader.Width = 131;
            // 
            // visibilityColumnHeader
            // 
            this.visibilityColumnHeader.Width = 149;
            // 
            // colorColumnHeader
            // 
            this.colorColumnHeader.Width = 152;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 474);
            this.Controls.Add(this._logOptionListView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._logOptionListViewComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsDialog";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button applyAllVerbosityButton;
    private System.Windows.Forms.ComboBox _applyAllVerbosityComboBox;
    private LogOptionListView _logOptionListView;
    private System.Windows.Forms.ColumnHeader logColumnHeader;
    private System.Windows.Forms.ColumnHeader visibilityColumnHeader;
    private System.Windows.Forms.ColumnHeader colorColumnHeader;
    private System.Windows.Forms.ComboBox _logOptionListViewComboBox;
    private System.Windows.Forms.TextBox _searchBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ColorDialog m_colorDialog;
}
