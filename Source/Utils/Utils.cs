using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Utils
{
    public static Color InvertColor( Color color)
    {
        return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
    }

    public static string ColorToHexString( Color color)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
    }
}

