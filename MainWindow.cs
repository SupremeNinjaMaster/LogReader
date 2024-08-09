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
    /// if true, we will see the rtf code included with the real text
    /// </summary>
    private bool _viewRTF = false;

    /// <summary>
    /// path to the file we want to read
    /// </summary>
    private string _filePath;

    /// <summary>
    /// Path where we save the options
    /// </summary>
    private string _optionsPath = "LogTypes.xml";

    /// <summary>
    /// Used to save teh 
    /// </summary>
    //int m_lastKnownCaretPosition = -1;

    /// <summary>
    /// The current options
    /// </summary>
    private LogOptions _logOptions;

    /// <summary>
    /// The last thing we searched
    /// </summary>
    private SearchRequest _lastSearchRequest;

    /// <summary>
    /// A file controller that runs a buffer on a separate thread
    /// </summary>
    private FileController _fileController;

    public MainWindow()
    {
        InitializeComponent();

        openLogFileDialog.FileOk += OpenLogFileDialog_FileOk;
        saveOptionsFileDialog.FileOk += SaveOptionsFileDialog_FileOk;
        openOptionsFileDialog.FileOk += OpenOptionsFileDialog_FileOk;

        _logOptions = new LogOptions();
        _logOptions.Load(_optionsPath);

        _richTextContentsBox.MouseUp += RichTextContentsBox_MouseUp;
        
    }

    private void LoadFile( string path, int position)
    {
        if( File.Exists(path))
        {
            _logOptions.Load("LogTypes.xml");

            if( _fileController != null)
            {
                // If we are loading a different file, we stop the old one
                if (_fileController.Path != path)
                {
                    _fileController.Stop();
                    _fileController = null;
                }
            }

            if (_fileController == null)
            {
                _fileController = new FileController(path);
                _fileController.onLinesReadDelegate += OnLinesRead;
            }

            _fileController.OpenFile(_logOptions.Clone() as LogOptions);

            // @todo: the position thingie
            //m_richTextContentsBox.SelectionStart = position;
            //m_richTextContentsBox.ScrollToCaret();
        }
    }       

    private void ReloadFile()
    {        
        if(!string.IsNullOrEmpty(_filePath))
        {
            //m_lastKnownCaretPosition = m_richTextContentsBox.SelectionStart;

            LoadFile(_filePath, _richTextContentsBox.SelectionStart);
        }
    }

    private void Search( SearchRequest search)
    {
        if (search.searchText == null)
        {
            return;
        }

        RichTextBoxFinds searchFlags = RichTextBoxFinds.None;

        if( search.matchCase)
        {
            searchFlags |= RichTextBoxFinds.MatchCase;
        }

        if( search.matchWholeWord)
        {
            searchFlags |= RichTextBoxFinds.WholeWord;
        }

        if( search.searchBackwards)
        {
            searchFlags |= RichTextBoxFinds.Reverse;
        }

        if( search.useRegex)
        {
            // @todo: later one day?
        }

        int idx = _richTextContentsBox.Find(search.searchText, _richTextContentsBox.SelectionStart + _richTextContentsBox.SelectionLength, searchFlags);

        if (idx != -1)
        {
            _richTextContentsBox.SelectionStart = idx;
            _richTextContentsBox.ScrollToCaret();
            _lastSearchRequest = search;
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
        _filePath = openLogFileDialog.FileName;
        LoadFile(_filePath, 0);
    }

    private void OpenOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        _logOptions.Load(openOptionsFileDialog.FileName);
        ReloadFile();        
    }

    private void SaveOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        _logOptions.Save(saveOptionsFileDialog.FileName);
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
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            _lastSearchRequest.searchBackwards = false;
            Search(_lastSearchRequest);
        }
    }

    private void prevSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            _lastSearchRequest.searchBackwards = true;
            Search(_lastSearchRequest);
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

    private void OnSearchRequested(object sender, SearchRequest req)
    {
        Search(req);

        SearchDialog dialog = (SearchDialog)sender;
        dialog.Close();        
    }

    private void toggleRTFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _viewRTF = !_viewRTF;
        ReloadFile();
    }

    private void logOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OptionsDialog optionsDialog = new OptionsDialog(_optionsPath, _logOptions);
        optionsDialog.FormClosed += new FormClosedEventHandler(OptionsDialog_FormClosed);
        optionsDialog.ShowDialog();
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        ReloadFile();
    }

    private void OnLinesRead(LinesReadResult res)
    {
        if (_viewRTF)
        {
            // If we want to view the rtf we save it as normal text
            _richTextContentsBox.Rtf = "";
            _richTextContentsBox.Text = res.NewText;
            _richTextContentsBox.SelectionStart = _richTextContentsBox.Text.Length;
        }
        else
        {
            _richTextContentsBox.Text = "";
            _richTextContentsBox.Rtf = res.NewText;
            _richTextContentsBox.SelectionStart = _richTextContentsBox.Rtf.Length;
        }
        
        _richTextContentsBox.ScrollToCaret();

        for (int i = 0; i < res.NewLogTypesFound.Length; ++i)
        {
            _logOptions.AddLogType(res.NewLogTypesFound[i], Color.Black, EVerbosity.Log);
        }

        _logOptions.Save(_optionsPath);
    }

    private void repeatLastSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            Search(_lastSearchRequest);
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

    private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _fileController.ClearLog();
    }

    private void RichTextContentsBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            contextMenuStrip1.Show(Cursor.Position);
        }
    }
}

