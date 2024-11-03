using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class LogContextMenu : ContextMenuStrip, IColorable
{
    private CustomMenuColorTable _customMenuColorTable;

    public LogContextMenu() : base()
    {
        _customMenuColorTable = new CustomMenuColorTable();
        Renderer = new CustomToolStripProfessionalRenderer(_customMenuColorTable);
    }

    public LogContextMenu(System.ComponentModel.IContainer component) : base(component)
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

