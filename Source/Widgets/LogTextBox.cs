using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static NativeFunctions;
using System.Runtime.InteropServices;


public class LogTextBox : RichTextBox, IColorable
{
    ColorSet _currentColorSet;
    LogScrollbar _customScrollbar = null;

    public LogTextBox() : base()
    {
        // This is a WIP   
        // _customScrollbar = new LogScrollbar();

        if (_customScrollbar != null)
        {
            Controls.Add(_customScrollbar);
            this.ScrollBars = RichTextBoxScrollBars.None;
        }
        else
        {
            this.ScrollBars = RichTextBoxScrollBars.Vertical;
        }
    }

    public void Search(SearchRequest search)
    {
        if (string.IsNullOrEmpty(search.searchText))
        {
            return;
        }

        RichTextBoxFinds searchFlags = RichTextBoxFinds.None;

        if (search.matchCase)
        {
            searchFlags |= RichTextBoxFinds.MatchCase;
        }

        if (search.matchWholeWord)
        {
            searchFlags |= RichTextBoxFinds.WholeWord;
        }

        if (search.searchBackwards)
        {
            searchFlags |= RichTextBoxFinds.Reverse;
        }

        if (search.useRegex)
        {
            // @todo: later one day?
        }

        int idx = Find(search.searchText, SelectionStart + SelectionLength, searchFlags);

        // if we fail to find something, go to the end and search again
        if (idx == -1)
        {
            if (search.searchBackwards)
            {
                idx = Find(search.searchText, TextLength - 1, searchFlags);
            }
            else
            {
                idx = Find(search.searchText, 0, searchFlags);
            }
        }

        if (idx != -1)
        {
            SelectionStart = idx;
            ScrollToCaret();
        }
    }

    public void SetColors(ColorSet colorSet)
    {
        _currentColorSet = colorSet;
        BackColor = colorSet.Surface;
        ForeColor = colorSet.OnSurface;

        if (_customScrollbar != null)
        {
            _customScrollbar.SetColors(colorSet);
        }

        this.BorderStyle = BorderStyle.None;
    }


    public void SetText(string text, bool useRtf)
    {
        if (useRtf)
        {
            // If we want to view the rtf we save it as normal text
            Rtf = "";
            Text = text;
            SelectionStart = Text.Length;
        }
        else
        {
            Text = "";
            Rtf = text;
            SelectionStart = Rtf.Length;
        }

        if (_customScrollbar != null)
        {
            _customScrollbar.Maximum = CalculateScrollMaximum();
            //_customScrollbar.ScrollValue = _customScrollbar.Maximum;
        }

        ScrollToCaret();        
    }

    /// <summary>
    /// This works only if it is scrolled all the way to the top
    /// </summary>
    /// <returns></returns>
    private int CalculateScrollMaximum()
    {
        int lineHeight;
        using (Graphics g = CreateGraphics())
        {
            lineHeight = TextRenderer.MeasureText(g, Lines[Lines.Length - 1], Font).Height;
        }

        // This gives us the position of the last character
        Point p = GetPositionFromCharIndex(TextLength - 1);
        return p.Y + lineHeight;
    }

    private int FindScrollbarMaximum()
    {

        SCROLLINFO info = new SCROLLINFO();
        info.fMask = (int)SCROLLINFO.ScrollInfoMask.SIF_ALL;
        info.cbSize = Marshal.SizeOf(info);
        if (NativeFunctions.GetScrollInfo(Handle, (int)SCROLLINFO.ScrollBarDirection.SB_VERT, ref info))
        {
            // maximum is equal to the location of the last line + small increment (15) + position (0)
            // Console.WriteLine("min: {0}, max: {1}, page: {2}, pos: {3}, trackPos: {4}", info.nMin, info.nMax, info.nPage, info.nPos, info.nTrackPos);
            return info.nMax;
        }

        return 0;
    }




}

