
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
            this.m_searchBox = new System.Windows.Forms.TextBox();
            this.applyAllVerbosityButton = new System.Windows.Forms.Button();
            this.m_applyAllVerbosityComboBox = new System.Windows.Forms.ComboBox();
            this.m_logOptionListViewComboBox = new System.Windows.Forms.ComboBox();
            this.m_logOptionListView = new LogOptionListView();
            this.logColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.visibilityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colorDialog = new System.Windows.Forms.ColorDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_searchBox);
            this.panel2.Controls.Add(this.applyAllVerbosityButton);
            this.panel2.Controls.Add(this.m_applyAllVerbosityComboBox);
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
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log Type Search";
            // 
            // m_searchBox
            // 
            this.m_searchBox.Location = new System.Drawing.Point(115, 7);
            this.m_searchBox.Name = "m_searchBox";
            this.m_searchBox.Size = new System.Drawing.Size(142, 20);
            this.m_searchBox.TabIndex = 5;
            // 
            // applyAllVerbosityButton
            // 
            this.applyAllVerbosityButton.Location = new System.Drawing.Point(11, 33);
            this.applyAllVerbosityButton.Name = "applyAllVerbosityButton";
            this.applyAllVerbosityButton.Size = new System.Drawing.Size(148, 23);
            this.applyAllVerbosityButton.TabIndex = 1;
            this.applyAllVerbosityButton.Text = "Apply Verbosity to All";
            this.applyAllVerbosityButton.UseVisualStyleBackColor = true;
            this.applyAllVerbosityButton.Click += new System.EventHandler(this.ApplyAllVerbosityButton_Click);
            // 
            // applyAllVerbosityComboBox
            // 
            this.m_applyAllVerbosityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_applyAllVerbosityComboBox.FormattingEnabled = true;
            this.m_applyAllVerbosityComboBox.Location = new System.Drawing.Point(167, 35);
            this.m_applyAllVerbosityComboBox.Name = "applyAllVerbosityComboBox";
            this.m_applyAllVerbosityComboBox.Size = new System.Drawing.Size(121, 21);
            this.m_applyAllVerbosityComboBox.TabIndex = 0;
            // 
            // m_logOptionListViewComboBox
            // 
            this.m_logOptionListViewComboBox.FormattingEnabled = true;
            this.m_logOptionListViewComboBox.Location = new System.Drawing.Point(168, 122);
            this.m_logOptionListViewComboBox.Name = "m_logOptionListViewComboBox";
            this.m_logOptionListViewComboBox.Size = new System.Drawing.Size(121, 21);
            this.m_logOptionListViewComboBox.TabIndex = 5;
            // 
            // m_logOptionListView
            // 
            this.m_logOptionListView.ColorSelected = null;
            this.m_logOptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logColumnHeader,
            this.visibilityColumnHeader,
            this.colorColumnHeader});
            this.m_logOptionListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_logOptionListView.FullRowSelect = true;
            this.m_logOptionListView.HideSelection = false;
            this.m_logOptionListView.Location = new System.Drawing.Point(0, 78);
            this.m_logOptionListView.MultiSelect = false;
            this.m_logOptionListView.Name = "m_logOptionListView";
            this.m_logOptionListView.Size = new System.Drawing.Size(437, 396);
            this.m_logOptionListView.TabIndex = 1;
            this.m_logOptionListView.UseCompatibleStateImageBehavior = false;
            this.m_logOptionListView.VerbositySelected = null;
            this.m_logOptionListView.View = System.Windows.Forms.View.Details;
            // 
            // logColumnHeader
            // 
            this.logColumnHeader.Text = "Log Name";
            this.logColumnHeader.Width = 131;
            // 
            // visibilityColumnHeader
            // 
            this.visibilityColumnHeader.Text = "Visibility";
            this.visibilityColumnHeader.Width = 149;
            // 
            // colorColumnHeader
            // 
            this.colorColumnHeader.Text = "Color";
            this.colorColumnHeader.Width = 152;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 474);
            this.Controls.Add(this.m_logOptionListView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_logOptionListViewComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsDialog";
            this.Text = "OptionsDialog";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button applyAllVerbosityButton;
    private System.Windows.Forms.ComboBox m_applyAllVerbosityComboBox;
    private LogOptionListView m_logOptionListView;
    private System.Windows.Forms.ColumnHeader logColumnHeader;
    private System.Windows.Forms.ColumnHeader visibilityColumnHeader;
    private System.Windows.Forms.ColumnHeader colorColumnHeader;
    private System.Windows.Forms.ComboBox m_logOptionListViewComboBox;
    private System.Windows.Forms.TextBox m_searchBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ColorDialog m_colorDialog;
    private System.Windows.Forms.ColorDialog colorDialog1;
}
