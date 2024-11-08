﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

public class LogMenuStrip : MenuStrip, IColorable
{
    private CustomMenuColorTable _customMenuColorTable;

    public LogMenuStrip() : base()
    {
        _customMenuColorTable = new CustomMenuColorTable();
        Renderer = new CustomToolStripProfessionalRenderer(_customMenuColorTable);
    }

    public void SetColors(ColorSet colorSet)
    {       
        _customMenuColorTable.CurrentColorSet = colorSet;

        Invalidate();
    }
}
