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

    public SearchDialog(LogOptions logOptions)
    {
        _options = logOptions;

        InitializeComponent();

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
        req.searchText = this.m_searchTextBox.Text;
        req.useRegex = this.checkBoxRegex.Checked;
        req.matchCase = this.checkBoxMatchCase.Checked;
        req.matchWholeWord = this.checkBoxMatchCase.Checked;
        req.searchBackwards = this.checkBoxBackwards.Checked;

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

        m_searchTextBox.ForeColor = colorSet.OnSurface;
        m_searchTextBox.BackColor = colorSet.Surface;

        buttonSearch.ForeColor = colorSet.OnSurface;
        buttonSearch.BackColor = colorSet.Surface;

        groupBox1.ForeColor = colorSet.OnSurface;
        groupBox1.BackColor = colorSet.Surface;

        checkBoxRegex.ForeColor = colorSet.OnSurface;
        checkBoxRegex.BackColor = colorSet.Surface;

        checkBoxBackwards.ForeColor = colorSet.OnSurface;
        checkBoxBackwards.BackColor = colorSet.Surface;

        checkBoxMatchWholeWord.ForeColor = colorSet.OnSurface;
        checkBoxMatchWholeWord.BackColor = colorSet.Surface;

        checkBoxMatchCase.ForeColor = colorSet.OnSurface;
        checkBoxMatchCase.BackColor = colorSet.Surface;

        NativeFunctions.ChangeWindowColor(this.Handle);
    }

    public void RefreshLanguageText()
    {

        this.buttonSearch.Text = Lang.Text("TXT_SEARCH");
        this.groupBox1.Text = Lang.Text("TXT_SEARCH_OPTIONS");
        this.checkBoxRegex.Text = Lang.Text("TXT_REGEX");
        this.checkBoxBackwards.Text = Lang.Text("TXT_SEARCH_BACK");
        this.checkBoxMatchWholeWord.Text = Lang.Text("TXT_MATCH_WORD");
        this.checkBoxMatchCase.Text = Lang.Text("TXT_MATCH_CASE");
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

