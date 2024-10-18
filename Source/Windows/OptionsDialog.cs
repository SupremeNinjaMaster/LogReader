using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

public partial class OptionsDialog : Form
{
    string _path;     
    LogOptions _options;
        
    public OptionsDialog(string path, LogOptions logOptions)
    {
        _path = path;
        _options = logOptions;
        

        InitializeComponent();

        FormClosed += new FormClosedEventHandler( OptionsDialog_FormClosed);

        m_searchBox.TextChanged += SearchBox_TextChanged;

        // Fill up the 'applyAllVerbosityComboBox' selection
        string[] verbosities = LogOpt.GetVerbosityStringArray();
        if(verbosities.Length > 0)
        {
            _applyAllVerbosityComboBox.Items.AddRange(verbosities);
            _applyAllVerbosityComboBox.SelectedText = verbosities[0].ToString();
            _applyAllVerbosityComboBox.Text = verbosities[0].ToString();

            _logOptionListViewComboBox.Items.AddRange(verbosities);
            _logOptionListViewComboBox.SelectedText = verbosities[0].ToString();
            _logOptionListViewComboBox.Text = verbosities[0].ToString();
        }

        _logOptionListViewComboBox.Visible = false;
        _logOptionListViewComboBox.Leave += LogOptionComboBox_Leave;
        _logOptionListViewComboBox.LostFocus += LogOptionListViewComboBox_LostFocus;
        _logOptionListViewComboBox.MouseLeave += LogOptionListViewComboBox_MouseLeave;
        _logOptionListViewComboBox.SelectedValueChanged += LogOptionComboBox_SelectedValueChanged;

        m_logOptionListView.VerbositySelected += LogOptionListView_VerbositySelected;
        m_logOptionListView.ColorSelected += LogOptionsListView_ColorSelected;

        ShowItems();
    }



    private void ShowItems()
    {
        string searchString = m_searchBox.Text;
        List<string> logKeysToAdd = new List<string>(_options.optionsMap.Keys);        

        logKeysToAdd.Sort();

        for (int i = 0; i < logKeysToAdd.Count; ++i)
        {
            if (string.IsNullOrEmpty(searchString) || logKeysToAdd[i].IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                m_logOptionListView.AddLogOption(logKeysToAdd[i], _options.optionsMap[logKeysToAdd[i]]);
            }
            else
            {
                m_logOptionListView.RemoveLogOption(logKeysToAdd[i]);
            }
        }
    }

    private void SearchBox_TextChanged(object sender, EventArgs e)
    {
        ShowItems();
    }

    private void LogOptionListView_VerbositySelected(string logName, Rectangle rect)
    {
        LogOpt opt;
        if (_options.optionsMap.TryGetValue(logName, out opt))
        {
            _logOptionListViewComboBox.SelectedItem = opt.Verbosity.ToString();
            _logOptionListViewComboBox.Bounds = rect;
            _logOptionListViewComboBox.Visible = true;
            _logOptionListViewComboBox.BringToFront();
            _logOptionListViewComboBox.Focus();
        }
    }

    private void LogOptionsListView_ColorSelected()
    {
        // @todo: show current color

        if (DialogResult.OK == m_colorDialog.ShowDialog())
        {            
            foreach (string logName in m_logOptionListView.GetSelectedLogNames())
            {
                LogOpt opt;
                if (_options.optionsMap.TryGetValue(logName, out opt))
                {
                    opt.Color = m_colorDialog.Color;
                    _options.optionsMap[logName] = opt;
                    m_logOptionListView.SetColor(logName, opt.Color);
                }
            }
        }
    }

    private void LogOptionComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
        // change the value of the log option verbosity
        EVerbosity newVerbosity;
        if (Enum.TryParse((string) _logOptionListViewComboBox.SelectedItem, out newVerbosity))
        {
            ApplyVerbosity(newVerbosity, m_logOptionListView.GetSelectedLogNames());
        }
        
        // change the value of the item in the list view
        _logOptionListViewComboBox.Visible = false;        
    }

    private void LogOptionListViewComboBox_LostFocus(object sender, EventArgs e)
    {
        _logOptionListViewComboBox.Visible = false;
    }

    private void LogOptionListViewComboBox_MouseLeave(object sender, EventArgs e)
    {
        _logOptionListViewComboBox.Visible = false;
    }

    private void LogOptionComboBox_Leave(object sender, EventArgs e)
    {
        // change the value of the log option verbosity
        // change the value of the item in the list view
        _logOptionListViewComboBox.Visible = false;
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        // @todo: dispose of all things here

        _logOptionListViewComboBox.Dispose();

        _options.Save(_path);       
    }
    
    private void ApplyAllVerbosityButton_Click(object sender, EventArgs e)
    {
        EVerbosity newVerbosity;
        if (Enum.TryParse((string)_applyAllVerbosityComboBox.SelectedItem, out newVerbosity))
        {
            ApplyVerbosity(newVerbosity, m_logOptionListView.GetAllLogNames());
        }
    }

    private void ApplyVerbosity(EVerbosity verbosity, string[] logNames)
    {
        foreach (string logName in logNames)
        {
            LogOpt Opt;
            if (_options.optionsMap.TryGetValue(logName, out Opt))
            {
                Opt.Verbosity = verbosity;
                _options.optionsMap[logName] = Opt;
                m_logOptionListView.SetVerbosity(logName, verbosity);
            }
        }
    }
    

}

