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

public delegate void LogOptionVerbositySelected(string logName, Rectangle rect );
public delegate void LogOptionColorSelected();

public partial class LogOptionListView : ListView, IColorable
{
    private ColorSet _colorSet;
    private LogOptionVerbositySelected _verbositySelectedDelegate;
    private LogOptionColorSelected _colorSelectedDelegate;
    private Brush _primaryBrush;    
    private Brush _backgroundBrush;
    private Pen _backgroundPen;
    private Pen _surfacePen;
    private Pen _primaryPen;
    private Brush _onPrimaryBrush;
    
    public LogOptionListView()
    {
        this.OwnerDraw = true;
        this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(OnColumnHeaderDraw);
        this.DrawItem += new DrawListViewItemEventHandler(OnItemDraw);
        this.DrawSubItem += new DrawListViewSubItemEventHandler(OnSubItemDraw);
        this.Disposed += OnDisposed;

        InitializeComponent();
          
        FullRowSelect = true;

        foreach (ColumnHeader ch in Columns)
        {
            ch.Width = -2;
        }
    }

    private void OnDisposed(object sender, EventArgs e)
    {        
        _primaryBrush?.Dispose();
        _backgroundBrush?.Dispose();
        _backgroundPen?.Dispose();
        _surfacePen?.Dispose();
        _primaryPen?.Dispose();
        _onPrimaryBrush?.Dispose();
    }

    public void AddLogOption(string logName, LogOpt opt)
    {
        int currIndex = Items.IndexOfKey(logName);

        // Make sure we replace a value that is already there
        if (currIndex >= 0)
        {
            Items.RemoveAt(currIndex);
        }

        ListViewItem item = new ListViewItem();        
        item.Text = logName;
        item.Name = logName;
        item.UseItemStyleForSubItems = false;
        
        ListViewSubItem verNameSubItem = new ListViewSubItem();
        verNameSubItem.Text = opt.Verbosity.ToString();
        verNameSubItem.Name = "Verbosity";
        verNameSubItem.ForeColor = _colorSet.OnSurface;
        verNameSubItem.BackColor = _colorSet.Surface;
        item.SubItems.Add(verNameSubItem);

        ListViewSubItem colNameSubItem = new ListViewSubItem();
        colNameSubItem.Text = Utils.ColorToHexString( opt.Color);
        colNameSubItem.ForeColor = Utils.InvertColor(opt.Color);
        colNameSubItem.BackColor = opt.Color;
        colNameSubItem.Name = "Color";
        item.SubItems.Add(colNameSubItem);
        
        Items.Add(item);
    }

    public void RemoveLogOption(string logName)
    {
        Items.RemoveByKey(logName);
    }

    private void OnColumnHeaderDraw(object sender, DrawListViewColumnHeaderEventArgs e)
    {          
        e.Graphics.FillRectangle(_primaryBrush, e.Bounds);
        Rectangle rect = e.Bounds;
        rect.Width--;
        rect.Height--;
        e.Graphics.DrawRectangle(_backgroundPen, rect);
        rect.Width--;
        rect.Height--;
        e.Graphics.DrawLine(_primaryPen, rect.X, rect.Y, rect.Right, rect.Y);
        e.Graphics.DrawLine(_primaryPen, rect.X, rect.Y, rect.X, rect.Bottom);
        e.Graphics.DrawLine(_surfacePen, rect.X + 1, rect.Bottom, rect.Right, rect.Bottom);
        e.Graphics.DrawLine(_surfacePen, rect.Right, rect.Y + 1, rect.Right, rect.Bottom);

        StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        e.Graphics.DrawString(e.Header.Text, e.Font, _onPrimaryBrush, e.Bounds, stringFormat);        
    }

    private void OnItemDraw(object sender, DrawListViewItemEventArgs e)
    {
        Color textColor = _colorSet.OnSurface;

        using (SolidBrush foreBrush = new SolidBrush(textColor))
        {
            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };

            e.Graphics.DrawString(e.Item.Text, e.Item.Font, foreBrush, e.Item.Bounds, stringFormat);
        }
    }

    private void OnSubItemDraw(object sender, DrawListViewSubItemEventArgs e)
    {        
        e.DrawBackground();

        if ((e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
        {
            ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(e.Bounds, -1, -1), e.Item.ForeColor, e.Item.BackColor);
        }

        using (SolidBrush foreBrush = new SolidBrush(e.SubItem.ForeColor))
        {
            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
                        
            e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, foreBrush, e.SubItem.Bounds, stringFormat);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        // this is very expensive!!!
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
            _verbositySelectedDelegate.Invoke(item.Text, targetItemRect);
        }
        else if( subItem.Name == "Color")
        {
            _colorSelectedDelegate.Invoke();
        }
    }

    public void SetVerbosity( string logName, EVerbosity newVerbosity)
    {
        foreach( ListViewItem item in Items)
        {
            if( item.Text == logName)
            {
                item.SubItems[1].Text = newVerbosity.ToString();
            }
        }
    }

    public void SetColor( string logName, Color color)
    {
        foreach (ListViewItem item in Items)
        {
            if (item.Text == logName)
            {
                item.SubItems[2].BackColor = color;
                item.SubItems[2].Text = Utils.ColorToHexString(color);
                item.SubItems[2].ForeColor = Utils.InvertColor(color);                
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

    public void SetColors(ColorSet colorSet)
    {
        BackColor = colorSet.Surface;
        ForeColor = colorSet.OnSurface;

        _colorSet = colorSet;

        _primaryBrush?.Dispose();
        _backgroundBrush?.Dispose();
        _backgroundPen?.Dispose();
        _surfacePen?.Dispose();
        _primaryPen?.Dispose();
        _onPrimaryBrush?.Dispose();

        _primaryBrush = new SolidBrush(_colorSet.Primary);
        _backgroundBrush = new SolidBrush(_colorSet.Background); ;
        _backgroundPen = new Pen(_colorSet.Background);
        _surfacePen = new Pen(_colorSet.Surface);
        _primaryPen = new Pen(_colorSet.Primary);
        _onPrimaryBrush = new SolidBrush(_colorSet.OnPrimary);
    }

    public LogOptionVerbositySelected VerbositySelected
    {
        get
        {
            return _verbositySelectedDelegate;
        }
        set
        {
            _verbositySelectedDelegate = value;
        }
    }
        
    public LogOptionColorSelected ColorSelected
    {
        get
        {
            return _colorSelectedDelegate;
        }
        set
        {
            _colorSelectedDelegate = value;
        }
    }
}

