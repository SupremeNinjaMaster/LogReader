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
using LogReader.Source.Windows;
using System.Globalization;
using static System.Windows.Forms.LinkLabel;


public partial class MainWindow : Form, IColorable, ILanguageable
{
    const string APP_NAME = "Log Reader";

    /// <summary>
    /// if true, we will see the rtf code included with the real text
    /// </summary>
    private bool _useViewRTF = false;

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

    private ToolStripItem[] _languageMenuItems;
    
    public MainWindow()
    {
        Text = APP_NAME;

        // Load the options
        _logOptions = new LogOptions();
        _logOptions.Load(_optionsPath);

        // Load and set the language
        Lang.Instance.LoadLanguages();
        Lang.Instance.CurrentLanguage = _logOptions.Language;

        // Initialize the controls and components
        InitializeComponent();

        // Create a menu for languages
        _languageMenuItems = new ToolStripItem[]
        {
            new LanguageToolStripMenuItem("TXT_ENGLISH", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_ENGLISH),
            new LanguageToolStripMenuItem("TXT_SPANISH", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_SPANISH),
            new LanguageToolStripMenuItem("TXT_FRENCH", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_FRENCH),
            new LanguageToolStripMenuItem("TXT_ITALIAN", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_ITAILAN),
            new LanguageToolStripMenuItem("TXT_PORTUGUESE", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_PORTUGUESE),
            new LanguageToolStripMenuItem("TXT_POLISH", new EventHandler(LanguageToolStripMenuItem_Click), Lang.LANGUAGE_POLISH)
        };

        _languageToolStripMenuItem.DropDownItems.AddRange(_languageMenuItems);

        UpdateRecentFilesMenu();        

        _openLogFileDialog.FileOk += OpenLogFileDialog_FileOk;
        _saveOptionsFileDialog.FileOk += SaveOptionsFileDialog_FileOk;
        _openOptionsFileDialog.FileOk += OpenOptionsFileDialog_FileOk;


        _richTextContentsBox.MouseUp += RichTextContentsBox_MouseUp;


        // Set the right language
        RefreshLanguageText();
                
        if (DesignMode)
        {
            SetColors(ColorSet.MainDarkMode);
        }
        else
        {
            SetColors(_logOptions.CurrentColorTheme);
        }


    }

    private void LoadFile( string path, int position, bool resetStartLineIndex)
    {        
        if ( File.Exists(path))
        {
            _filePath = path;

            // @todo: why are we doing this???
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

            _logOptions.AddRecentFilePath(path);
            _fileController.OpenFile(_logOptions.Clone() as LogOptions, resetStartLineIndex);

            SetTitleBarText(path);

            // @todo: the position thingie
            //m_richTextContentsBox.SelectionStart = position;
            //m_richTextContentsBox.ScrollToCaret();
        }
    }       

    private void ReloadFile(bool resetStartLineIndex)
    {        
        if(!string.IsNullOrEmpty(_filePath))
        {
            //m_lastKnownCaretPosition = m_richTextContentsBox.SelectionStart;

            LoadFile(_filePath, _richTextContentsBox.SelectionStart, resetStartLineIndex);
        }
    }

    private void SetTitleBarText( string path)
    {
        //using (Graphics g = CreateGraphics())
        {
            // @todo: add some dots
            //Size textSize = TextRenderer.MeasureText(g, path, Font);

            Text = string.Format("{0} - {1}", path, APP_NAME);
        }
    }

    public void RefreshLanguageText()
    {
        fIleToolStripMenuItem.Text = Lang.Text("TXT_FILE");
        openToolStripMenuItem.Text = Lang.Text("TXT_OPEN");
        loadRecentToolStripMenuItem.Text = Lang.Text("TXT_OPEN_LAST");
        refreshToolStripMenuItem.Text = Lang.Text("TXT_RELOAD");
        recentFilesToolStripMenuItem.Text = Lang.Text("TXT_RECENT_FILES");
        findToolStripMenuItem.Text = Lang.Text("TXT_SEARCH");
        nextSelectionToolStripMenuItem.Text = Lang.Text("TXT_NEXT_SELECTION");
        prevSelectionToolStripMenuItem.Text = Lang.Text("TXT_PREV_SELECTION");
        findToolStripMenuItem1.Text = Lang.Text("TXT_FIND");
        repeatLastSearchToolStripMenuItem.Text = Lang.Text("TXT_REPEAT_LAST_SEARCH");
        optionsToolStripMenuItem1.Text = Lang.Text("TXT_LOG_OPTIONS");
        _languageToolStripMenuItem.Text = Lang.Text("TXT_LANGUAGE");
        themesToolStripMenuItem.Text = Lang.Text("TXT_THEMES");
        toggleRTFToolStripMenuItem.Text = Lang.Text("TXT_TOGGLE_RTF");
        logOptionsToolStripMenuItem.Text = Lang.Text("TXT_LOG_OPTIONS");
        exportLogOptionsToolStripMenuItem.Text = Lang.Text("TXT_EXPORT_LOG_OPTIONS");
        importLogOptionsToolStripMenuItem.Text = Lang.Text("TXT_IMPORT_LOG_OPTIONS");
        helpToolStripMenuItem.Text = Lang.Text("TXT_HELP");
        clearLogsToolStripMenuItem.Text = Lang.Text("TXT_CLEAR_LOGS");
        aboutToolStripMenuItem.Text = Lang.Text("TXT_ABOUT");

        foreach(LanguageToolStripMenuItem menuItem in _languageMenuItems)
        {
            menuItem.RefreshLanguageText();
        }
    }

    public void UpdateRecentFilesMenu()
    {   
        recentFilesToolStripMenuItem.DropDownItems.Clear();

        for ( int i = 0; i < _logOptions.RecentFiles.Length; ++i)
        {
            recentFilesToolStripMenuItem.DropDownItems.Add(new FilePathToolStripMenuItem(_logOptions.RecentFiles[i] , new EventHandler(RecentFilesToolStripMenuItem_Click)));
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

    public void SetColors(ColorSet colorSet)
    {
        _logOptions.DefaultColor = colorSet.OnSurface;
        
        _menuStrip.SetColors(colorSet);
        _richTextContentsBox.SetColors(colorSet);

        NativeFunctions.ChangeWindowColor(this.Handle);
    }

#region InputHandlers

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LogAboutBox aboutDialog = new LogAboutBox();
        aboutDialog.SetColors(_logOptions.CurrentColorTheme);
        aboutDialog.ShowDialog();        
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _openLogFileDialog.ShowDialog();
    }

    private void OpenLogFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        _filePath = _openLogFileDialog.FileName;
        ReloadFile(false);        
    }

    private void OpenOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        _logOptions.Load(_openOptionsFileDialog.FileName);
        //ReloadFile(true); @rodo: figure out if we need to reload the file when we open options
    }

    private void SaveOptionsFileDialog_FileOk(object sender, CancelEventArgs e)
    {
        _logOptions.Save(_saveOptionsFileDialog.FileName);
    }

    private void LanguageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LanguageToolStripMenuItem menuItem = sender as LanguageToolStripMenuItem;
        if( menuItem != null)
        {
            _logOptions.Language = menuItem.Language;
            Lang.Instance.CurrentLanguage = menuItem.Language;
            RefreshLanguageText();
        }               
    }

    private void RecentFilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FilePathToolStripMenuItem menuItem = sender as FilePathToolStripMenuItem;
        if (menuItem != null)
        {
            LoadFile(menuItem.FullPath, 0, true);
        }
    }

    private void NextSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            _lastSearchRequest.searchBackwards = false;
            Search(_lastSearchRequest);
        }
    }

    private void PrevSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            _lastSearchRequest.searchBackwards = true;
            Search(_lastSearchRequest);
        }
    }
    
    private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ReloadFile(true);
    }

    private void FindToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        SearchDialog searchDialog = new SearchDialog(_logOptions);
        searchDialog.SearchRequest += OnSearchRequested;
        searchDialog.ShowDialog();
    }

    private void ToggleRTFToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _useViewRTF = !_useViewRTF;
        ReloadFile(false);
    }

    private void LogOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OptionsDialog optionsDialog = new OptionsDialog(_optionsPath, _logOptions);
        optionsDialog.FormClosed += new FormClosedEventHandler(OptionsDialog_FormClosed);
        optionsDialog.ShowDialog();
    }

    private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        ReloadFile(false);
    }

    private void RepeatLastSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastSearchRequest.searchText))
        {
            Search(_lastSearchRequest);
        }
    }

    private void ExportLogOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _saveOptionsFileDialog.ShowDialog();
    }

    private void ImportLogOptionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _openOptionsFileDialog.ShowDialog();
    }

    private void ClearLogsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _fileController.ClearLog();
    }

    private void RichTextContentsBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            _contextMenuStrip.Show(Cursor.Position);
        }
    }

    private void LoadRecentToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_logOptions.MostRecentFile))
        {
            _filePath = _logOptions.MostRecentFile;
            ReloadFile(false); // @todo: this should be true not false?
        }
    }

    private void ThemeDialog_FormClosed(object sender, FormClosedEventArgs e)
    {
        SetColors(_logOptions.CurrentColorTheme);
        Invalidate();
        ReloadFile(false);
    }

    private void ThemesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ThemeDialog themeDialog = new ThemeDialog(_optionsPath, _logOptions);
        themeDialog.FormClosed += new FormClosedEventHandler(ThemeDialog_FormClosed);
        themeDialog.ShowDialog();
    }

    #endregion

    private void OnSearchRequested(object sender, SearchRequest req)
    {
        Search(req);

        SearchDialog dialog = (SearchDialog)sender;
        dialog.Close();        
    }

    private void OnLinesRead(LinesReadResult res)
    {
        _richTextContentsBox.SetText(res.NewText, _useViewRTF);

        for (int i = 0; i < res.NewLogTypesFound.Length; ++i)
        {
            _logOptions.AddLogType(res.NewLogTypesFound[i], Color.Empty, EVerbosity.Log);
        }

        _logOptions.Save(_optionsPath);
    }


}

