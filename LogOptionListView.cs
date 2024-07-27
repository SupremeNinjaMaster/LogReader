using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

public delegate void LogOptionVerbositySelected(string in_logName, Rectangle in_rect );
public delegate void LogOptionColorSelected();

public partial class LogOptionListView : ListView
{
    private LogOptionVerbositySelected m_verbositySelectedDelegate;
    private LogOptionColorSelected m_colorSelectedDelegate;

    public LogOptionListView()
    {
        InitializeComponent();

        FullRowSelect = true;

        foreach (ColumnHeader ch in Columns)
        {
            ch.Width = -2;
        }
    }

    public void AddLogOption(string in_logName, LogOpt in_opt)
    {
        int currIndex = Items.IndexOfKey(in_logName);

        // Make sure we replace a value that is already there
        if (currIndex >= 0)
        {
            Items.RemoveAt(currIndex);
        }

        ListViewItem item = new ListViewItem();        
        item.Text = in_logName;
        item.Name = in_logName;
        item.UseItemStyleForSubItems = false;
        
        ListViewSubItem verNameSubItem = new ListViewSubItem();
        verNameSubItem.Text = in_opt.verbosity.ToString();
        verNameSubItem.Name = "Verbosity";
        item.SubItems.Add(verNameSubItem);

        ListViewSubItem colNameSubItem = new ListViewSubItem();
        // @todo: show color text and invert it
        // colNameSubItem.Text = in_opt.color.ToString();
        colNameSubItem.BackColor = in_opt.color;
        colNameSubItem.Name = "Color";
        item.SubItems.Add(colNameSubItem);
        
        Items.Add(item);
    }

    public void RemoveLogOption(string in_logName)
    {
        Items.RemoveByKey(in_logName);
    }

    protected override void OnResize(EventArgs e)
    {
        // this is expensive!!!
        //foreach (ColumnHeader ch in Columns)
        //{
        //    ch.Width = -2;
        //}

        base.OnResize(e);        
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        ListViewItem item = this.GetItemAt(e.X, e.Y);
        if (item == null)
        {
            return;
        }

        ListViewSubItem subItem = item.GetSubItemAt(e.X, e.Y);
        if (subItem == null)
        {
            return;
        }

        Rectangle targetItemRect = subItem.Bounds;

        // Verify that the column is completely scrolled off to the left.
        if ((targetItemRect.Left + this.Columns[1].Width) < 0)
        {
            // If the cell is out of view to the left, do nothing.
            return;
        }

        // Verify that the column is partially scrolled off to the left.
        else if (targetItemRect.Left < 0)
        {
            // Determine if column extends beyond right side of ListView.
            if ((targetItemRect.Left + this.Columns[1].Width) > this.Width)
            {
                // Set width of column to match width of ListView.
                targetItemRect.Width = this.Width;
                targetItemRect.X = 0;
            }
            else
            {
                // Right side of cell is in view.
                targetItemRect.Width = this.Columns[1].Width + targetItemRect.Left;
                targetItemRect.X = 2;
            }
        }
        else if (this.Columns[1].Width > this.Width)
        {
            targetItemRect.Width = this.Width;
        }
        else
        {
            //   targetItemRect.Width = this.m_logOptionListView.Columns[1].Width;
            //   targetItemRect.X = 2;
        }

        // Adjust the top to account for the location of the ListView.
        targetItemRect.Y += this.Top;
        targetItemRect.X += this.Left;

        if (subItem.Name == "Verbosity")
        {
            m_verbositySelectedDelegate.Invoke(item.Text, targetItemRect);
        }
        else if( subItem.Name == "Color")
        {
            m_colorSelectedDelegate.Invoke();
        }
    }

    public void SetVerbosity( string in_logName, EVerbosity in_newVerbosity)
    {
        foreach( ListViewItem item in Items)
        {
            if( item.Text == in_logName)
            {
                item.SubItems[1].Text = in_newVerbosity.ToString();
            }
        }
    }

    public void SetColor( string in_logName, Color in_color)
    {
        foreach (ListViewItem item in Items)
        {
            if (item.Text == in_logName)
            {
                item.SubItems[2].BackColor = in_color;
            }
        }
    }

    public string[] GetAllLogNames()
    {
        string[] names = new string[Items.Count];
        for (int i = 0; i < Items.Count; ++i)
        {
            names[i] = Items[i].Text;
        }

        return names;
    }

    public string[] GetSelectedLogNames()
    {
        string[] names = new string[SelectedItems.Count];
        for( int i = 0; i < SelectedItems.Count; ++i)
        {
            names[i] = SelectedItems[i].Text;
        }

        return names;
    }

    public LogOptionVerbositySelected VerbositySelected
    {
        get
        {
            return m_verbositySelectedDelegate;
        }
        set
        {
            m_verbositySelectedDelegate = value;
        }
    }
        
    public LogOptionColorSelected ColorSelected
    {
        get
        {
            return m_colorSelectedDelegate;
        }
        set
        {
            m_colorSelectedDelegate = value;
        }
    }
}

