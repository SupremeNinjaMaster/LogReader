using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Based on Google's dark theme anatomy
/// https://m2.material.io/design/color/dark-theme.html#anatomy
/// </summary>
public struct ColorSet
{
    public Color Background;

    public Color Surface;

    public Color Primary;

    public Color Secondary;

    public Color OnBackground;

    public Color OnSurface;

    public Color OnPrimary;

    public Color OnSecondary;

    public readonly static ColorSet MainDarkMode = new ColorSet
    {
        Background = Color.FromArgb(255, 31, 31, 31),
        Surface = Color.FromArgb(255, 43, 43, 43),
        Primary = Color.FromArgb(255, 187, 134, 252),
        Secondary = Color.FromArgb(255, 3, 218, 198),
        OnBackground = Color.FromArgb(255, 221, 221, 221),
        OnSurface = Color.FromArgb(255, 150, 150, 150),
        OnPrimary = Color.FromArgb(255, 10, 10, 10),
        OnSecondary = Color.FromArgb(255, 0, 0, 0)
    };

    public readonly static ColorSet MainLightMode = new ColorSet
    {
        Background = SystemColors.MenuBar,
        Surface = SystemColors.Control,
        Primary = Color.FromArgb(255, 187, 134, 252),
        Secondary = Color.FromArgb(255, 3, 218, 198),
        OnBackground = SystemColors.WindowText,
        OnSurface = SystemColors.ControlText,
        OnPrimary = SystemColors.ControlDarkDark,
        OnSecondary = SystemColors.ControlText,
    };
};


public interface IColorable
{
    void SetColors(ColorSet colorSet);

};

