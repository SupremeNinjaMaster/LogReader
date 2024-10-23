using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogReader.Source.Windows
{
    public partial class ThemeDialog : Form
    {
        /// <summary>
        /// The color set to play around with
        /// </summary>
        ColorSet _colorSet;

        /// <summary>
        /// The path to the options file so we can save it when we're done
        /// </summary>
        string _path;

        /// <summary>
        /// Reference to the options
        /// </summary>
        LogOptions _cachedOptions;

        public ThemeDialog(string path, LogOptions logOptions)
        {
            _path = path;
            _cachedOptions = logOptions;            

            InitializeComponent();

    
            if ( _cachedOptions.ChosenColorThemeName == LogOptions.THEME_NAME_DARK)
            {
                _darkDefaultRadioButton.Checked = true;
            }
            else if( _cachedOptions.ChosenColorThemeName == LogOptions.THEME_NAME_LIGHT)
            {
                _lightDefaultRadioButton.Checked = true;
            }
            else
            {
                _customRadioButton.Checked = true;
            }
            
            _lightDefaultRadioButton.CheckedChanged += new System.EventHandler(this.LightDefaultRadioButton_CheckedChanged);
            _darkDefaultRadioButton.CheckedChanged += new System.EventHandler(this.DarkDefaultRadioButton_CheckedChanged);
            _customRadioButton.CheckedChanged += new System.EventHandler(this.CustomRadioButton_CheckedChanged);

            // Try to get the custom set from the options
            if (!_cachedOptions.TryGetColorThemeByName(LogOptions.THEME_NAME_CUSTOM, out _colorSet))
            {
                // if there is no custom set in the options, we need to use the one we just created
                _colorSet = new ColorSet();
                _cachedOptions.AddThemeColorSet(LogOptions.THEME_NAME_CUSTOM, _colorSet);
            }

            ApplyTestColorSet(_cachedOptions.CurrentColorTheme);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Save the custom theme we created
            _cachedOptions.AddThemeColorSet(LogOptions.THEME_NAME_CUSTOM, _colorSet);
            _cachedOptions.Save(_path);
            base.OnFormClosed(e);
        }          

        private void ApplyTestColorSet( ColorSet set)
        {
            NativeFunctions.ChangeWindowColor(this.Handle);

            //_mainForegroundButton.Enabled = _customRadioButton.Checked;
            _mainForegroundButton.ForeColor = set.OnBackground;            
            _mainForegroundButton.BackColor = set.Background;
            //_mainBackgroundButton.Enabled = _customRadioButton.Checked;
            _mainSampleTextbox.BackColor = set.Background;
            _mainSampleTextbox.ForeColor = set.OnBackground;
            _mainBackgroundButton.BackColor = set.Background;
            _mainBackgroundButton.ForeColor = set.OnBackground;            
            
            //_surfaceBackgroundButton.Enabled = _customRadioButton.Checked;
            //_surfaceForegroundButton.Enabled = _customRadioButton.Checked;
            _surfaceSampleTextbox.BackColor = set.Surface;
            _surfaceSampleTextbox.ForeColor = set.OnSurface;
            _surfaceBackgroundButton.BackColor = set.Surface;
            _surfaceBackgroundButton.ForeColor = set.OnSurface;            
            _surfaceForegroundButton.ForeColor = set.OnSurface;
            _surfaceForegroundButton.BackColor = set.Surface;

            //_primaryBackgroundButton.Enabled = _customRadioButton.Checked;
            //_primaryForegroundButton.Enabled = _customRadioButton.Checked;
            _primarySampleTextbox.BackColor = set.Primary;
            _primarySampleTextbox.ForeColor = set.OnPrimary;
            _primaryBackgroundButton.BackColor = set.Primary;      
            _primaryForegroundButton.ForeColor = set.OnPrimary;
            _primaryForegroundButton.BackColor = set.Primary;
            _primaryBackgroundButton.ForeColor = set.OnPrimary;

            //_secondaryBackgroundButton.Enabled = _customRadioButton.Checked;
            //_secondaryForegroundButton.Enabled = _customRadioButton.Checked;
            _secondarySampleTextbox.BackColor = set.Secondary;
            _secondarySampleTextbox.ForeColor = set.OnSecondary;
            _secondaryBackgroundButton.BackColor = set.Secondary;
            _secondaryForegroundButton.ForeColor = set.OnSecondary;            
            _secondaryBackgroundButton.ForeColor = set.OnSecondary;
            _secondaryForegroundButton.BackColor = set.Secondary;

            this.BackColor = set.Background;
            this.ForeColor = set.OnBackground;
        }

        private void UpdateCustomColorSet()
        {   
            _cachedOptions.ChosenColorThemeName = LogOptions.THEME_NAME_CUSTOM;
            ApplyTestColorSet(_colorSet);            
        }

        private void LightDefaultRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_lightDefaultRadioButton.Checked)
            {
                _darkDefaultRadioButton.Checked = false;
                _customRadioButton.Checked = false;
                _cachedOptions.ChosenColorThemeName = LogOptions.THEME_NAME_LIGHT;
                ApplyTestColorSet(_cachedOptions.CurrentColorTheme);
            }
        }

        private void DarkDefaultRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(_darkDefaultRadioButton.Checked)
            {
                _lightDefaultRadioButton.Checked = false;
                _customRadioButton.Checked = false;
                _cachedOptions.ChosenColorThemeName = LogOptions.THEME_NAME_DARK;
                ApplyTestColorSet(_cachedOptions.CurrentColorTheme);
            }
        }

        private void CustomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked)
            {
                _lightDefaultRadioButton.Checked = false;
                _darkDefaultRadioButton.Checked = false;
                UpdateCustomColorSet();
            }
        }

        private void MainBackgroundButton_Click(object sender, EventArgs e)
        {            
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.Background = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void MainForegroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.OnBackground = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void SurfaceBackgroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.Surface = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void SurfaceForegroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.OnSurface = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void PrimaryBackgroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.Primary = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void PrimaryForegroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&                
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.OnPrimary = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void SecondaryBackgroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.Secondary = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }

        private void SecondaryForegroundButton_Click(object sender, EventArgs e)
        {
            if (_customRadioButton.Checked &&
                DialogResult.OK == _colorDialog.ShowDialog())
            {
                _colorSet.OnSecondary = _colorDialog.Color;
                UpdateCustomColorSet();
            }
        }
    }
}
