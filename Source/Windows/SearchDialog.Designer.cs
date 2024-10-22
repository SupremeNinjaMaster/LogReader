
    partial class SearchDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDialog));
            this.m_searchTextBox = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRegex = new System.Windows.Forms.CheckBox();
            this.checkBoxBackwards = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_searchTextBox
            // 
            this.m_searchTextBox.Location = new System.Drawing.Point(42, 27);
            this.m_searchTextBox.Name = "m_searchTextBox";
            this.m_searchTextBox.Size = new System.Drawing.Size(188, 20);
            this.m_searchTextBox.TabIndex = 0;
            this.m_searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_searchTextBox_KeyDown);
            // 
            // buttonSearch
            // 
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Location = new System.Drawing.Point(90, 226);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxRegex);
            this.groupBox1.Controls.Add(this.checkBoxBackwards);
            this.groupBox1.Controls.Add(this.checkBoxMatchWholeWord);
            this.groupBox1.Controls.Add(this.checkBoxMatchCase);
            this.groupBox1.Location = new System.Drawing.Point(30, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 123);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // checkBoxRegex
            // 
            this.checkBoxRegex.AutoSize = true;
            this.checkBoxRegex.Location = new System.Drawing.Point(7, 92);
            this.checkBoxRegex.Name = "checkBoxRegex";
            this.checkBoxRegex.Size = new System.Drawing.Size(15, 14);
            this.checkBoxRegex.TabIndex = 3;
            this.checkBoxRegex.UseVisualStyleBackColor = true;
            // 
            // checkBoxBackwards
            // 
            this.checkBoxBackwards.AutoSize = true;
            this.checkBoxBackwards.Location = new System.Drawing.Point(7, 68);
            this.checkBoxBackwards.Name = "checkBoxBackwards";
            this.checkBoxBackwards.Size = new System.Drawing.Size(15, 14);
            this.checkBoxBackwards.TabIndex = 2;
            this.checkBoxBackwards.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(7, 44);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMatchWholeWord.TabIndex = 1;
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(7, 20);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMatchCase.TabIndex = 0;
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // SearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.m_searchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_searchTextBox;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxRegex;
        private System.Windows.Forms.CheckBox checkBoxBackwards;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
