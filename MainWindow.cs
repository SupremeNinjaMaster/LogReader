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
    /// <summary>
    /// if true, we will se ethe rtf code included with the real text
    /// </summary>
    bool m_viewRTF = false;

    /// <summary>
    /// path to the file we want to read
    /// </summary>
    string m_filePath;

    /// <summary>
    /// Path where we save the options
    /// </summary>
    string m_optionsPath = "LogTypes.xml";

    /// <summary>
    /// Used to save teh 
    /// </summary>
    //int m_lastKnownCaretPosition = -1;

    /// <summary>
    /// The current options
    /// </summary>
    LogOptions m_logOptions;    

    /// <summary>
    /// The last thing we searched
    /// </summary>
    SearchRequest m_lastSearchRequest;    

    /// <summary>
    /// The different file controllers
    /// </summary>
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

    private void LoadFile( string in_path, int in_position)
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

            // @todo: the position thingie
            //m_richTextContentsBox.SelectionStart = in_position;
            //m_richTextContentsBox.ScrollToCaret();
        }
    }       

    private void ReloadFile()
    {        
        if(!string.IsNullOrEmpty(m_filePath))
        {
            //m_lastKnownCaretPosition = m_richTextContentsBox.SelectionStart;

            LoadFile(m_filePath, m_richTextContentsBox.SelectionStart);
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

        int idx = m_richTextContentsBox.Find(in_search.searchText, m_richTextContentsBox.SelectionStart + m_richTextContentsBox.SelectionLength, searchFlags);

        if (idx != -1)
        {
            m_richTextContentsBox.SelectionStart = idx;
            m_richTextContentsBox.ScrollToCaret();
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
        LoadFile(m_filePath, 0);
    }

    private void OpenOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        m_logOptions.Load(openOptionsFileDialog.FileName);
        ReloadFile();        
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
        ReloadFile();        
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
        ReloadFile();
    }

    private void logOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OptionsDialog optionsDialog = new OptionsDialog(m_optionsPath, m_logOptions);
        optionsDialog.FormClosed += new FormClosedEventHandler(OptionsDialog_FormClosed);
        optionsDialog.ShowDialog();
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        ReloadFile();
    }

    private void OnLinesRead(LinesReadResult res)
    {
        if (m_viewRTF)
        {
            // If we want to view the rtf we save it as normal text
            m_richTextContentsBox.Rtf = "";
            m_richTextContentsBox.Text = res.newText;
            m_richTextContentsBox.SelectionStart = m_richTextContentsBox.Text.Length;
        }
        else
        {
            m_richTextContentsBox.Text = "";
            m_richTextContentsBox.Rtf = res.newText;
            m_richTextContentsBox.SelectionStart = m_richTextContentsBox.Rtf.Length;
        }
        
        m_richTextContentsBox.ScrollToCaret();

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

