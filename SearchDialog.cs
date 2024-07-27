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

public delegate void OnSearchRequestedDelegate(object sender, SearchRequest in_req);

public partial class SearchDialog : Form
{
    public SearchDialog()
    {
        InitializeComponent();
    }

    private void TriggerSearch()
    {
        SearchRequest req;
        req.searchText = this.m_searchTextBox.Text;
        req.useRegex = this.checkBoxRegex.Checked;
        req.matchCase = this.checkBoxMatchCase.Checked;
        req.matchWholeWord = this.checkBoxMatchCase.Checked;
        req.searchBackwards = this.checkBoxBackwards.Checked;

        if( m_searchDelegate != null )
        {
            m_searchDelegate.Invoke(this, req);
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

    OnSearchRequestedDelegate m_searchDelegate;

    public OnSearchRequestedDelegate SearchRequest
    {
        get
        {
            return m_searchDelegate;
        }
        set
        {
            m_searchDelegate = value;
        }
    }
}

