namespace LogReader.Source.Windows
{
    partial class ThemeDialog
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
            this._lightDefaultRadioButton = new System.Windows.Forms.RadioButton();
            this._darkDefaultRadioButton = new System.Windows.Forms.RadioButton();
            this._customRadioButton = new System.Windows.Forms.RadioButton();
            this._mainBackgroundButton = new System.Windows.Forms.Button();
            this._colorDialog = new System.Windows.Forms.ColorDialog();
            this._mainSampleTextbox = new System.Windows.Forms.TextBox();
            this._mainForegroundButton = new System.Windows.Forms.Button();
            this._mainLabel = new System.Windows.Forms.Label();
            this._surfaceLabel = new System.Windows.Forms.Label();
            this._surfaceForegroundButton = new System.Windows.Forms.Button();
            this._surfaceSampleTextbox = new System.Windows.Forms.TextBox();
            this._surfaceBackgroundButton = new System.Windows.Forms.Button();
            this._primaryLabel = new System.Windows.Forms.Label();
            this._primaryForegroundButton = new System.Windows.Forms.Button();
            this._primarySampleTextbox = new System.Windows.Forms.TextBox();
            this._primaryBackgroundButton = new System.Windows.Forms.Button();
            this._secondaryLabel = new System.Windows.Forms.Label();
            this._secondaryForegroundButton = new System.Windows.Forms.Button();
            this._secondarySampleTextbox = new System.Windows.Forms.TextBox();
            this._secondaryBackgroundButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lightDefaultRadioButton
            // 
            this._lightDefaultRadioButton.AutoSize = true;
            this._lightDefaultRadioButton.Location = new System.Drawing.Point(27, 24);
            this._lightDefaultRadioButton.Name = "_lightDefaultRadioButton";
            this._lightDefaultRadioButton.Size = new System.Drawing.Size(85, 17);
            this._lightDefaultRadioButton.TabIndex = 0;
            this._lightDefaultRadioButton.TabStop = true;
            this._lightDefaultRadioButton.Text = "Light Default";
            this._lightDefaultRadioButton.UseVisualStyleBackColor = true;
            // 
            // _darkDefaultRadioButton
            // 
            this._darkDefaultRadioButton.AutoSize = true;
            this._darkDefaultRadioButton.Location = new System.Drawing.Point(154, 24);
            this._darkDefaultRadioButton.Name = "_darkDefaultRadioButton";
            this._darkDefaultRadioButton.Size = new System.Drawing.Size(85, 17);
            this._darkDefaultRadioButton.TabIndex = 1;
            this._darkDefaultRadioButton.TabStop = true;
            this._darkDefaultRadioButton.Text = "Dark Default";
            this._darkDefaultRadioButton.UseVisualStyleBackColor = true;
            // 
            // _customRadioButton
            // 
            this._customRadioButton.AutoSize = true;
            this._customRadioButton.Location = new System.Drawing.Point(284, 24);
            this._customRadioButton.Name = "_customRadioButton";
            this._customRadioButton.Size = new System.Drawing.Size(60, 17);
            this._customRadioButton.TabIndex = 2;
            this._customRadioButton.TabStop = true;
            this._customRadioButton.Text = "Custom";
            this._customRadioButton.UseVisualStyleBackColor = true;
            // 
            // _mainBackgroundButton
            // 
            this._mainBackgroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mainBackgroundButton.Location = new System.Drawing.Point(200, 70);
            this._mainBackgroundButton.Name = "_mainBackgroundButton";
            this._mainBackgroundButton.Size = new System.Drawing.Size(82, 23);
            this._mainBackgroundButton.TabIndex = 3;
            this._mainBackgroundButton.Text = "Background";
            this._mainBackgroundButton.UseVisualStyleBackColor = true;
            this._mainBackgroundButton.Click += new System.EventHandler(this.MainBackgroundButton_Click);
            // 
            // _mainSampleTextbox
            // 
            this._mainSampleTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._mainSampleTextbox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._mainSampleTextbox.Location = new System.Drawing.Point(94, 72);
            this._mainSampleTextbox.Name = "_mainSampleTextbox";
            this._mainSampleTextbox.ReadOnly = true;
            this._mainSampleTextbox.Size = new System.Drawing.Size(84, 16);
            this._mainSampleTextbox.TabIndex = 4;
            this._mainSampleTextbox.Text = "Abc 123 @#$";
            // 
            // _mainForegroundButton
            // 
            this._mainForegroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mainForegroundButton.Location = new System.Drawing.Point(299, 69);
            this._mainForegroundButton.Name = "_mainForegroundButton";
            this._mainForegroundButton.Size = new System.Drawing.Size(82, 23);
            this._mainForegroundButton.TabIndex = 5;
            this._mainForegroundButton.Text = "Foreground";
            this._mainForegroundButton.UseVisualStyleBackColor = true;
            this._mainForegroundButton.Click += new System.EventHandler(this.MainForegroundButton_Click);
            // 
            // _mainLabel
            // 
            this._mainLabel.AutoSize = true;
            this._mainLabel.Location = new System.Drawing.Point(22, 73);
            this._mainLabel.Name = "_mainLabel";
            this._mainLabel.Size = new System.Drawing.Size(30, 13);
            this._mainLabel.TabIndex = 6;
            this._mainLabel.Text = "Main";
            // 
            // _surfaceLabel
            // 
            this._surfaceLabel.AutoSize = true;
            this._surfaceLabel.Location = new System.Drawing.Point(22, 102);
            this._surfaceLabel.Name = "_surfaceLabel";
            this._surfaceLabel.Size = new System.Drawing.Size(44, 13);
            this._surfaceLabel.TabIndex = 10;
            this._surfaceLabel.Text = "Surface";
            // 
            // _surfaceForegroundButton
            // 
            this._surfaceForegroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._surfaceForegroundButton.Location = new System.Drawing.Point(299, 98);
            this._surfaceForegroundButton.Name = "_surfaceForegroundButton";
            this._surfaceForegroundButton.Size = new System.Drawing.Size(82, 23);
            this._surfaceForegroundButton.TabIndex = 9;
            this._surfaceForegroundButton.Text = "Foreground";
            this._surfaceForegroundButton.UseVisualStyleBackColor = true;
            this._surfaceForegroundButton.Click += new System.EventHandler(this.SurfaceForegroundButton_Click);
            // 
            // _surfaceSampleTextbox
            // 
            this._surfaceSampleTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._surfaceSampleTextbox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._surfaceSampleTextbox.Location = new System.Drawing.Point(94, 101);
            this._surfaceSampleTextbox.Name = "_surfaceSampleTextbox";
            this._surfaceSampleTextbox.ReadOnly = true;
            this._surfaceSampleTextbox.Size = new System.Drawing.Size(84, 16);
            this._surfaceSampleTextbox.TabIndex = 8;
            this._surfaceSampleTextbox.Text = "Abc 123 @#$";
            // 
            // _surfaceBackgroundButton
            // 
            this._surfaceBackgroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._surfaceBackgroundButton.Location = new System.Drawing.Point(200, 99);
            this._surfaceBackgroundButton.Name = "_surfaceBackgroundButton";
            this._surfaceBackgroundButton.Size = new System.Drawing.Size(82, 23);
            this._surfaceBackgroundButton.TabIndex = 7;
            this._surfaceBackgroundButton.Text = "Background";
            this._surfaceBackgroundButton.UseVisualStyleBackColor = true;
            this._surfaceBackgroundButton.Click += new System.EventHandler(this.SurfaceBackgroundButton_Click);
            // 
            // _primaryLabel
            // 
            this._primaryLabel.AutoSize = true;
            this._primaryLabel.Location = new System.Drawing.Point(22, 131);
            this._primaryLabel.Name = "_primaryLabel";
            this._primaryLabel.Size = new System.Drawing.Size(41, 13);
            this._primaryLabel.TabIndex = 14;
            this._primaryLabel.Text = "Primary";
            // 
            // _primaryForegroundButton
            // 
            this._primaryForegroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._primaryForegroundButton.Location = new System.Drawing.Point(299, 127);
            this._primaryForegroundButton.Name = "_primaryForegroundButton";
            this._primaryForegroundButton.Size = new System.Drawing.Size(82, 23);
            this._primaryForegroundButton.TabIndex = 13;
            this._primaryForegroundButton.Text = "Foreground";
            this._primaryForegroundButton.UseVisualStyleBackColor = true;
            this._primaryForegroundButton.Click += new System.EventHandler(this.PrimaryForegroundButton_Click);
            // 
            // _primarySampleTextbox
            // 
            this._primarySampleTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._primarySampleTextbox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._primarySampleTextbox.Location = new System.Drawing.Point(94, 130);
            this._primarySampleTextbox.Name = "_primarySampleTextbox";
            this._primarySampleTextbox.ReadOnly = true;
            this._primarySampleTextbox.Size = new System.Drawing.Size(84, 16);
            this._primarySampleTextbox.TabIndex = 12;
            this._primarySampleTextbox.Text = "Abc 123 @#$";
            // 
            // _primaryBackgroundButton
            // 
            this._primaryBackgroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._primaryBackgroundButton.Location = new System.Drawing.Point(200, 128);
            this._primaryBackgroundButton.Name = "_primaryBackgroundButton";
            this._primaryBackgroundButton.Size = new System.Drawing.Size(82, 23);
            this._primaryBackgroundButton.TabIndex = 11;
            this._primaryBackgroundButton.Text = "Background";
            this._primaryBackgroundButton.UseVisualStyleBackColor = true;
            this._primaryBackgroundButton.Click += new System.EventHandler(this.PrimaryBackgroundButton_Click);
            // 
            // _secondaryLabel
            // 
            this._secondaryLabel.AutoSize = true;
            this._secondaryLabel.Location = new System.Drawing.Point(22, 160);
            this._secondaryLabel.Name = "_secondaryLabel";
            this._secondaryLabel.Size = new System.Drawing.Size(58, 13);
            this._secondaryLabel.TabIndex = 18;
            this._secondaryLabel.Text = "Secondary";
            // 
            // _secondaryForegroundButton
            // 
            this._secondaryForegroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._secondaryForegroundButton.Location = new System.Drawing.Point(299, 156);
            this._secondaryForegroundButton.Name = "_secondaryForegroundButton";
            this._secondaryForegroundButton.Size = new System.Drawing.Size(82, 23);
            this._secondaryForegroundButton.TabIndex = 17;
            this._secondaryForegroundButton.Text = "Foreground";
            this._secondaryForegroundButton.UseVisualStyleBackColor = true;
            this._secondaryForegroundButton.Click += new System.EventHandler(this.SecondaryForegroundButton_Click);
            // 
            // _secondarySampleTextbox
            // 
            this._secondarySampleTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._secondarySampleTextbox.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._secondarySampleTextbox.Location = new System.Drawing.Point(94, 159);
            this._secondarySampleTextbox.Name = "_secondarySampleTextbox";
            this._secondarySampleTextbox.ReadOnly = true;
            this._secondarySampleTextbox.Size = new System.Drawing.Size(84, 16);
            this._secondarySampleTextbox.TabIndex = 16;
            this._secondarySampleTextbox.Text = "Abc 123 @#$";
            // 
            // _secondaryBackgroundButton
            // 
            this._secondaryBackgroundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._secondaryBackgroundButton.Location = new System.Drawing.Point(200, 157);
            this._secondaryBackgroundButton.Name = "_secondaryBackgroundButton";
            this._secondaryBackgroundButton.Size = new System.Drawing.Size(82, 23);
            this._secondaryBackgroundButton.TabIndex = 15;
            this._secondaryBackgroundButton.Text = "Background";
            this._secondaryBackgroundButton.UseVisualStyleBackColor = true;
            this._secondaryBackgroundButton.Click += new System.EventHandler(this.SecondaryBackgroundButton_Click);            
            // 
            // ThemeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 205);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._secondaryLabel);
            this.Controls.Add(this._secondaryForegroundButton);
            this.Controls.Add(this._secondarySampleTextbox);
            this.Controls.Add(this._secondaryBackgroundButton);
            this.Controls.Add(this._primaryLabel);
            this.Controls.Add(this._primaryForegroundButton);
            this.Controls.Add(this._primarySampleTextbox);
            this.Controls.Add(this._primaryBackgroundButton);
            this.Controls.Add(this._surfaceLabel);
            this.Controls.Add(this._surfaceForegroundButton);
            this.Controls.Add(this._surfaceSampleTextbox);
            this.Controls.Add(this._surfaceBackgroundButton);
            this.Controls.Add(this._mainLabel);
            this.Controls.Add(this._mainForegroundButton);
            this.Controls.Add(this._mainSampleTextbox);
            this.Controls.Add(this._mainBackgroundButton);
            this.Controls.Add(this._customRadioButton);
            this.Controls.Add(this._darkDefaultRadioButton);
            this.Controls.Add(this._lightDefaultRadioButton);
            this.Name = "ThemeDialog";
            this.Text = "ThemeDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton _lightDefaultRadioButton;
        private System.Windows.Forms.RadioButton _darkDefaultRadioButton;
        private System.Windows.Forms.RadioButton _customRadioButton;
        private System.Windows.Forms.Button _mainBackgroundButton;
        private System.Windows.Forms.ColorDialog _colorDialog;
        private System.Windows.Forms.TextBox _mainSampleTextbox;
        private System.Windows.Forms.Button _mainForegroundButton;
        private System.Windows.Forms.Label _mainLabel;
        private System.Windows.Forms.Label _surfaceLabel;
        private System.Windows.Forms.Button _surfaceForegroundButton;
        private System.Windows.Forms.TextBox _surfaceSampleTextbox;
        private System.Windows.Forms.Button _surfaceBackgroundButton;
        private System.Windows.Forms.Label _primaryLabel;
        private System.Windows.Forms.Button _primaryForegroundButton;
        private System.Windows.Forms.TextBox _primarySampleTextbox;
        private System.Windows.Forms.Button _primaryBackgroundButton;
        private System.Windows.Forms.Label _secondaryLabel;
        private System.Windows.Forms.Button _secondaryForegroundButton;
        private System.Windows.Forms.TextBox _secondarySampleTextbox;
        private System.Windows.Forms.Button _secondaryBackgroundButton;
        private System.Windows.Forms.Label label1;
    }
}