using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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