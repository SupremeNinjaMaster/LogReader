using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public struct SearchRequest
{
    public string searchText;
    public bool useRegex;
    public bool searchBackwards;
    public bool matchWholeWord;
    public bool matchCase;
}

public delegate void OnSearchRequestedDelegate(object sender, SearchRequest req);

public partial class SearchDialog : Form, IColorable, ILanguageable
{
    private OnSearchRequestedDelegate _searchDelegate;
    private readonly LogOptions _options;

    public SearchDialog(LogOptions logOptions, SearchRequest lastSearchRequest)
    {
        _options = logOptions;

        InitializeComponent();

        if(!string.IsNullOrEmpty( lastSearchRequest.searchText))
        {
            _searchTextBox.Text = lastSearchRequest.searchText;
        }

        // @todo: do regex search
        _checkBoxRegex.Enabled = false;

        RefreshLanguageText();

        if (DesignMode)
        {
            SetColors(ColorSet.MainDarkMode);
        }
        else
        {
            SetColors(_options.CurrentColorTheme);
        }
    }

    private void TriggerSearch()
    {
        SearchRequest req;
        req.searchText = this._searchTextBox.Text;
        req.useRegex = this._checkBoxRegex.Checked;
        req.matchCase = this._checkBoxMatchCase.Checked;
        req.matchWholeWord = this._checkBoxMatchWholeWord.Checked;
        req.searchBackwards = this._checkBoxBackwards.Checked;

        if( _searchDelegate != null )
        {
            _searchDelegate.Invoke(this, req);
        }
             
    }

    private void m_searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            TriggerSearch();
        }
    }

    private void ButtonSearch_Click(object sender, EventArgs e)
    {
        TriggerSearch();
    }

    public void SetColors(ColorSet colorSet)
    {
        this.ForeColor = colorSet.OnSurface;
        this.BackColor = colorSet.Background;

        _searchTextBox.ForeColor = colorSet.OnSurface;
        _searchTextBox.BackColor = colorSet.Surface;

        _buttonSearch.ForeColor = colorSet.OnSurface;
        _buttonSearch.BackColor = colorSet.Surface;

        _groupBox1.ForeColor = colorSet.OnSurface;
        _groupBox1.BackColor = colorSet.Surface;

        _checkBoxRegex.ForeColor = colorSet.OnSurface;
        _checkBoxRegex.BackColor = colorSet.Surface;

        _checkBoxBackwards.ForeColor = colorSet.OnSurface;
        _checkBoxBackwards.BackColor = colorSet.Surface;

        _checkBoxMatchWholeWord.ForeColor = colorSet.OnSurface;
        _checkBoxMatchWholeWord.BackColor = colorSet.Surface;

        _checkBoxMatchCase.ForeColor = colorSet.OnSurface;
        _checkBoxMatchCase.BackColor = colorSet.Surface;

        NativeFunctions.ChangeWindowColor(this.Handle);
    }

    public void RefreshLanguageText()
    {
        this._buttonSearch.Text = Lang.Text("TXT_SEARCH");
        this._groupBox1.Text = Lang.Text("TXT_SEARCH_OPTIONS");
        this._checkBoxRegex.Text = Lang.Text("TXT_REGEX");
        this._checkBoxBackwards.Text = Lang.Text("TXT_SEARCH_BACK");
        this._checkBoxMatchWholeWord.Text = Lang.Text("TXT_MATCH_WORD");
        this._checkBoxMatchCase.Text = Lang.Text("TXT_MATCH_CASE");
        this.Text = Lang.Text("TXT_SEARCH");
    }

    public OnSearchRequestedDelegate SearchRequest
    {
        get
        {
            return _searchDelegate;
        }
        set
        {
            _searchDelegate = value;
        }
    }
}

