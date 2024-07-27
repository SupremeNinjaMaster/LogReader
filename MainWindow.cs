using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

public partial class MainWindow : Form
{
    bool m_viewRTF = false;
    string m_filePath;
    string m_optionsPath = "LogTypes.xml";
    
    LogOptions m_logOptions;    
    SearchRequest m_lastSearchRequest;    
    Dictionary<string, FileController> m_fileControllers = new Dictionary<string, FileController>();

    public MainWindow()
    {
        InitializeComponent();

        openLogFileDialog.FileOk += OpenLogFileDialog_FileOk;
        saveOptionsFileDialog.FileOk += SaveOptionsFileDialog_FileOk;
        openOptionsFileDialog.FileOk += OpenOptionsFileDialog_FileOk;

        m_logOptions = new LogOptions();
        m_logOptions.Load(m_optionsPath);
    }

    private void LoadFile( string in_path)
    {
        if( File.Exists(in_path))
        {
            m_logOptions.Load("LogTypes.xml");
            if ( !m_fileControllers.ContainsKey(in_path))
            {
                // If there is no file controller, it means this is a new file request, so close all others
                while( m_fileControllers.Count > 0 )
                {
                    m_fileControllers.First().Value.Stop();
                    m_fileControllers.Remove(m_fileControllers.First().Key);
                }

                m_fileControllers.Add(in_path, new FileController(in_path));
                m_fileControllers[in_path].onLinesReadDelegate += OnLinesRead;
            }

            m_fileControllers[in_path].OpenFile(m_logOptions.Clone() as LogOptions);
        }
    }       

    private void Reloadfile()
    {        
        if(!string.IsNullOrEmpty(m_filePath))
        {
            LoadFile(m_filePath);            
        }
    }

    private void Search( SearchRequest in_search)
    {
        if (in_search.searchText == null)
        {
            return;
        }

        RichTextBoxFinds searchFlags = RichTextBoxFinds.None;

        if( in_search.matchCase)
        {
            searchFlags |= RichTextBoxFinds.MatchCase;
        }

        if( in_search.matchWholeWord)
        {
            searchFlags |= RichTextBoxFinds.WholeWord;
        }

        if( in_search.searchBackwards)
        {
            searchFlags |= RichTextBoxFinds.Reverse;
        }

        if( in_search.useRegex)
        {
            // @todo: later one day?
        }

        int idx = richTextBox1.Find(in_search.searchText, richTextBox1.SelectionStart + richTextBox1.SelectionLength, searchFlags);

        if (idx != -1)
        {
            richTextBox1.SelectionStart = idx;
            richTextBox1.ScrollToCaret();
            m_lastSearchRequest = in_search;
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("The Matrix is much better than Mean Girls");
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openLogFileDialog.ShowDialog();
    }

    private void OpenLogFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        m_filePath = openLogFileDialog.FileName;
        LoadFile(m_filePath);
    }

    private void OpenOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        m_logOptions.Load(openOptionsFileDialog.FileName);
        Reloadfile();        
    }

    private void SaveOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        m_logOptions.Save(saveOptionsFileDialog.FileName);
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Later");
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("One day");
    }

    private void nextSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(m_lastSearchRequest.searchText))
        {
            m_lastSearchRequest.searchBackwards = false;
            Search(m_lastSearchRequest);
        }
    }

    private void prevSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(m_lastSearchRequest.searchText))
        {
            m_lastSearchRequest.searchBackwards = true;
            Search(m_lastSearchRequest);
        }
    }
    
    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Reloadfile();        
    }

    private void findToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        SearchDialog searchDialog = new SearchDialog();
        searchDialog.SearchRequest += OnSearchRequested;
        searchDialog.ShowDialog();
    }

    private void OnSearchRequested(object in_sender, SearchRequest in_req)
    {
        Search(in_req);

        SearchDialog dialog = (SearchDialog)in_sender;
        dialog.Close();        
    }

    private void toggleRTFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        m_viewRTF = !m_viewRTF;
        Reloadfile();
    }

    private void logOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OptionsDialog optionsDialog = new OptionsDialog(m_optionsPath, m_logOptions);
        optionsDialog.FormClosed += new FormClosedEventHandler(OptionsDialog_FormClosed);
        optionsDialog.ShowDialog();
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        Reloadfile();
    }

    private void OnLinesRead(LinesReadResult res)
    {
        if (m_viewRTF)
        {
            // If we want to view the rtf we save it as normal text
            richTextBox1.Rtf = "";
            richTextBox1.Text = res.newText;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }
        else
        {
            richTextBox1.Text = "";
            richTextBox1.Rtf = res.newText;
            richTextBox1.SelectionStart = richTextBox1.Rtf.Length;
        }
        
        richTextBox1.ScrollToCaret();

        for (int i = 0; i < res.newLogTypesFound.Length; ++i)
        {
            m_logOptions.AddLogType(res.newLogTypesFound[i], Color.Black, EVerbosity.Log);
        }

        m_logOptions.Save(m_optionsPath);
    }

    private void repeatLastSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(m_lastSearchRequest.searchText))
        {
            Search(m_lastSearchRequest);
        }
    }

    private void exportLogOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        saveOptionsFileDialog.ShowDialog();
    }

    private void importLogOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openOptionsFileDialog.ShowDialog();
    }
}

