using System;
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

internal class CustomToolStripProfessionalRenderer : ToolStripProfessionalRenderer
{
    public CustomToolStripProfessionalRenderer()
    {
    }

    public CustomToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
        : base(professionalColorTable)
    {
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        CustomMenuColorTable customColorTable = (CustomMenuColorTable)ColorTable;
        e.Item.ForeColor = customColorTable.CurrentColorSet.OnSurface;

        base.OnRenderItemText(e);
    }
}

internal class CustomMenuColorTable : ProfessionalColorTable
{
    private ColorSet _colorSet;

    public CustomMenuColorTable()
    {
        // see notes
        base.UseSystemColors = false;
        
    }
    public override Color MenuBorder
    {
        get 
        {
            return _colorSet.Primary;
        }
    }
    public override Color MenuItemBorder
    {
        get
        {
            return _colorSet.Surface;
        }
    }
    public override Color MenuItemSelected
    {
        get 
        {
            return _colorSet.Primary;
        }
    }
    public override Color MenuItemSelectedGradientBegin
    {
        get 
        {
            return _colorSet.Primary;
        }
    }
    public override Color MenuItemSelectedGradientEnd
    {
        get 
        { 
            return _colorSet.Primary;
        }
    }
    public override Color MenuStripGradientBegin
    {
        get 
        {
            return _colorSet.Background;
        }
    }
    public override Color MenuStripGradientEnd
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public override Color ToolStripDropDownBackground
    {
        get
        {
            return _colorSet.Surface;
        }
    }

    public override Color MenuItemPressedGradientBegin
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public override Color MenuItemPressedGradientEnd
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public override Color ImageMarginGradientBegin
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public override Color ImageMarginGradientMiddle
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public override Color ImageMarginGradientEnd
    {
        get
        {
            return _colorSet.Background;
        }
    }

    public ColorSet CurrentColorSet
    {
        get 
        { 
            return _colorSet; 
        }
        set
        {
            _colorSet = value;
        }
    }
        

}