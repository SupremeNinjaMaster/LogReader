using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static Utils;
using System.Runtime.InteropServices;


public class LogTextBox : RichTextBox, IColorable
{
    ColorSet _currentColorSet;
    LogScrollbar _customScrollbar;

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
        if (Utils.GetScrollInfo(Handle, (int)SCROLLINFO.ScrollBarDirection.SB_VERT, ref info))
        {
            // maximum is equal to the location of the last line + small increment (15) + position (0)
            // Console.WriteLine("min: {0}, max: {1}, page: {2}, pos: {3}, trackPos: {4}", info.nMin, info.nMax, info.nPage, info.nPos, info.nTrackPos);
            return info.nMax;
        }

        return 0;
    }




}

