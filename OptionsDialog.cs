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
    string m_path;     
    LogOptions m_options;
        
    public OptionsDialog(string in_path, LogOptions in_logOptions)
    {
        m_path = in_path;
        m_options = in_logOptions;
        

        InitializeComponent();

        FormClosed += new FormClosedEventHandler( OptionsDialog_FormClosed);

        m_searchBox.TextChanged += SearchBox_TextChanged;

        // Fill up the 'applyAllVerbosityComboBox' selection
        string[] verbosities = LogOpt.GetVerbosityStringArray();
        if(verbosities.Length > 0)
        {
            m_applyAllVerbosityComboBox.Items.AddRange(verbosities);
            m_applyAllVerbosityComboBox.SelectedText = verbosities[0].ToString();
            m_applyAllVerbosityComboBox.Text = verbosities[0].ToString();

            m_logOptionListViewComboBox.Items.AddRange(verbosities);
            m_logOptionListViewComboBox.SelectedText = verbosities[0].ToString();
            m_logOptionListViewComboBox.Text = verbosities[0].ToString();
        }

        m_logOptionListViewComboBox.Visible = false;
        m_logOptionListViewComboBox.Leave += LogOptionComboBox_Leave;
        m_logOptionListViewComboBox.LostFocus += LogOptionListViewComboBox_LostFocus;
        m_logOptionListViewComboBox.MouseLeave += LogOptionListViewComboBox_MouseLeave;
        m_logOptionListViewComboBox.SelectedValueChanged += LogOptionComboBox_SelectedValueChanged;

        m_logOptionListView.VerbositySelected += LogOptionListView_VerbositySelected;
        m_logOptionListView.ColorSelected += LogOptionsListView_ColorSelected;

        ShowItems();
    }



    private void ShowItems()
    {
        string searchString = m_searchBox.Text;
        List<string> logKeysToAdd = new List<string>(m_options.optionsMap.Keys);        

        logKeysToAdd.Sort();

        for (int i = 0; i < logKeysToAdd.Count; ++i)
        {
            if (string.IsNullOrEmpty(searchString) || logKeysToAdd[i].IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                m_logOptionListView.AddLogOption(logKeysToAdd[i], m_options.optionsMap[logKeysToAdd[i]]);
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

    private void LogOptionListView_VerbositySelected(string in_logName, Rectangle in_rect)
    {
        LogOpt opt;
        if (m_options.optionsMap.TryGetValue(in_logName, out opt))
        {
            m_logOptionListViewComboBox.SelectedItem = opt.verbosity.ToString();
            m_logOptionListViewComboBox.Bounds = in_rect;
            m_logOptionListViewComboBox.Visible = true;
            m_logOptionListViewComboBox.BringToFront();
            m_logOptionListViewComboBox.Focus();
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
                if (m_options.optionsMap.TryGetValue(logName, out opt))
                {
                    opt.color = m_colorDialog.Color;
                    m_options.optionsMap[logName] = opt;
                    m_logOptionListView.SetColor(logName, opt.color);
                }
            }
        }
    }

    private void LogOptionComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
        // change the value of the log option verbosity
        EVerbosity newVerbosity;
        if (Enum.TryParse((string) m_logOptionListViewComboBox.SelectedItem, out newVerbosity))
        {
            ApplyVerbosity(newVerbosity, m_logOptionListView.GetSelectedLogNames());
        }
        
        // change the value of the item in the list view
        m_logOptionListViewComboBox.Visible = false;        
    }

    private void LogOptionListViewComboBox_LostFocus(object sender, EventArgs e)
    {
        m_logOptionListViewComboBox.Visible = false;
    }

    private void LogOptionListViewComboBox_MouseLeave(object sender, EventArgs e)
    {
        m_logOptionListViewComboBox.Visible = false;
    }

    private void LogOptionComboBox_Leave(object sender, EventArgs e)
    {
        // change the value of the log option verbosity
        // change the value of the item in the list view
        m_logOptionListViewComboBox.Visible = false;
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        // @todo: dispose of all things here

        m_logOptionListViewComboBox.Dispose();

        m_options.Save(m_path);       
    }
    
    private void ApplyAllVerbosityButton_Click(object sender, EventArgs e)
    {
        EVerbosity newVerbosity;
        if (Enum.TryParse((string)m_applyAllVerbosityComboBox.SelectedItem, out newVerbosity))
        {
            ApplyVerbosity(newVerbosity, m_logOptionListView.GetAllLogNames());
        }
    }

    private void ApplyVerbosity(EVerbosity in_verbosity, string[] in_logNames)
    {
        foreach (string logName in in_logNames)
        {
            LogOpt Opt;
            if (m_options.optionsMap.TryGetValue(logName, out Opt))
            {
                Opt.verbosity = in_verbosity;
                m_options.optionsMap[logName] = Opt;
                m_logOptionListView.SetVerbosity(logName, in_verbosity);
            }
        }
    }
    

}

