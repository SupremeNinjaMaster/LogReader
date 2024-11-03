
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
            this._searchTextBox = new System.Windows.Forms.TextBox();
            this._buttonSearch = new System.Windows.Forms.Button();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._checkBoxRegex = new System.Windows.Forms.CheckBox();
            this._checkBoxBackwards = new System.Windows.Forms.CheckBox();
            this._checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this._checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this._groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_searchTextBox
            // 
            this._searchTextBox.Location = new System.Drawing.Point(42, 27);
            this._searchTextBox.Name = "m_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(188, 20);
            this._searchTextBox.TabIndex = 0;
            this._searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_searchTextBox_KeyDown);
            // 
            // buttonSearch
            // 
            this._buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._buttonSearch.Location = new System.Drawing.Point(90, 226);
            this._buttonSearch.Name = "buttonSearch";
            this._buttonSearch.Size = new System.Drawing.Size(75, 23);
            this._buttonSearch.TabIndex = 1;
            this._buttonSearch.UseVisualStyleBackColor = true;
            this._buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // groupBox1
            // 
            this._groupBox1.Controls.Add(this._checkBoxRegex);
            this._groupBox1.Controls.Add(this._checkBoxBackwards);
            this._groupBox1.Controls.Add(this._checkBoxMatchWholeWord);
            this._groupBox1.Controls.Add(this._checkBoxMatchCase);
            this._groupBox1.Location = new System.Drawing.Point(30, 66);
            this._groupBox1.Name = "groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(200, 123);
            this._groupBox1.TabIndex = 2;
            this._groupBox1.TabStop = false;
            // 
            // checkBoxRegex
            // 
            this._checkBoxRegex.AutoSize = true;
            this._checkBoxRegex.Location = new System.Drawing.Point(7, 92);
            this._checkBoxRegex.Name = "checkBoxRegex";
            this._checkBoxRegex.Size = new System.Drawing.Size(15, 14);
            this._checkBoxRegex.TabIndex = 3;
            this._checkBoxRegex.UseVisualStyleBackColor = true;
            // 
            // checkBoxBackwards
            // 
            this._checkBoxBackwards.AutoSize = true;
            this._checkBoxBackwards.Location = new System.Drawing.Point(7, 68);
            this._checkBoxBackwards.Name = "checkBoxBackwards";
            this._checkBoxBackwards.Size = new System.Drawing.Size(15, 14);
            this._checkBoxBackwards.TabIndex = 2;
            this._checkBoxBackwards.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this._checkBoxMatchWholeWord.AutoSize = true;
            this._checkBoxMatchWholeWord.Location = new System.Drawing.Point(7, 44);
            this._checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this._checkBoxMatchWholeWord.Size = new System.Drawing.Size(15, 14);
            this._checkBoxMatchWholeWord.TabIndex = 1;
            this._checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchCase
            // 
            this._checkBoxMatchCase.AutoSize = true;
            this._checkBoxMatchCase.Location = new System.Drawing.Point(7, 20);
            this._checkBoxMatchCase.Name = "checkBoxMatchCase";
            this._checkBoxMatchCase.Size = new System.Drawing.Size(15, 14);
            this._checkBoxMatchCase.TabIndex = 0;
            this._checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // SearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this._groupBox1);
            this.Controls.Add(this._buttonSearch);
            this.Controls.Add(this._searchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this._groupBox1.ResumeLayout(false);
            this._groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _searchTextBox;
        private System.Windows.Forms.Button _buttonSearch;
        private System.Windows.Forms.GroupBox _groupBox1;
        private System.Windows.Forms.CheckBox _checkBoxRegex;
        private System.Windows.Forms.CheckBox _checkBoxBackwards;
        private System.Windows.Forms.CheckBox _checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox _checkBoxMatchCase;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
