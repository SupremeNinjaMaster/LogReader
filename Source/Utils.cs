using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
/// </summary>
enum DWMWINDOWATTRIBUTE
{
    DWMWA_NCRENDERING_ENABLED,
    DWMWA_NCRENDERING_POLICY,
    DWMWA_TRANSITIONS_FORCEDISABLED,
    DWMWA_ALLOW_NCPAINT,
    DWMWA_CAPTION_BUTTON_BOUNDS,
    DWMWA_NONCLIENT_RTL_LAYOUT,
    DWMWA_FORCE_ICONIC_REPRESENTATION,
    DWMWA_FLIP3D_POLICY,
    DWMWA_EXTENDED_FRAME_BOUNDS,
    DWMWA_HAS_ICONIC_BITMAP,
    DWMWA_DISALLOW_PEEK,
    DWMWA_EXCLUDED_FROM_PEEK,
    DWMWA_CLOAK,
    DWMWA_CLOAKED,
    DWMWA_FREEZE_REPRESENTATION,
    DWMWA_PASSIVE_UPDATE_MODE,
    DWMWA_USE_HOSTBACKDROPBRUSH,
    DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
    DWMWA_WINDOW_CORNER_PREFERENCE = 33,
    DWMWA_BORDER_COLOR,
    DWMWA_CAPTION_COLOR,
    DWMWA_TEXT_COLOR,
    DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
    DWMWA_SYSTEMBACKDROP_TYPE,
    DWMWA_LAST
};



[StructLayout(LayoutKind.Sequential)]
public struct SCROLLINFO
{
    public enum ScrollBarDirection
    {
        SB_HORZ = 0,
        SB_VERT = 1,
        SB_CTL = 2,
        SB_BOTH = 3,
    };

    public enum ScrollInfoMask
    {
        SIF_RANGE = 0x01,
        SIF_PAGE = 0x02,
        SIF_POS = 0x04,
        SIF_DISABLENOSCROLL = 0x08,
        SIF_TRACKPOS = 0x10,
        SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
    }

    public int cbSize;
    public int fMask;
    public int nMin;
    public int nMax;
    public int nPage;
    public int nPos;
    public int nTrackPos;
}


public static class Utils
{
    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    public static void ChangeColor(IntPtr handle)
    {
        int attributeValue = 1;
        DwmSetWindowAttribute(handle, (int)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref attributeValue, Marshal.SizeOf(attributeValue));
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);



    [DllImport("user32.dll")]
    public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, ref SCROLLINFO lpsi);

    



}

/// <summary>
/// https://doc.windev.com/en-US/?6510001&name=CallDLL32_Value_of_constants
/// </summary>
public static class Win32APIConstants
{
    // Retrieves the parameters for a scroll bar control (hwnd must be handle to scroll bar control)
    /*public const int SB_CTL = 2;

    // Retrieves the parameters for the windows standard vertical scrollbar
    public const int SB_VERT = 1;

    // Retrieves the parameters for the windows standard horizontal scrollbar
    public const int SB_HORZ = 0;

    public const int SB_BOTH = 3;*/

    // Scrolls to the lower right.
    public const int SB_BOTTOM = 7;

    // Ends scroll.
    public const int SB_ENDSCROLL = 8;

    // Scrolls one line down.
    public const int SB_LINEDOWN = 1;

    // Scrolls one line right
    public const int SB_LINERIGHT = 1;

    // Scrolls one line up.
    public const int SB_LINEUP = 0;

    // Scrolls one line left
    public const int SB_LINELEFT = 0;

    // Scrolls one page down.
    public const int SB_PAGEDOWN = 3;

    // Scrolls one page up.
    public const int SB_PAGEUP = 2;

    // The user has dragged the scroll box (thumb) and released the mouse button. The HIWORD indicates the position of the scroll box at the end of the drag operation.    
    public const int SB_THUMBPOSITION = 5;

    // The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button.The HIWORD indicates the position that the scroll box has been dragged to.
    public const int SB_THUMBTRACK = 5;

    //Scrolls to the upper left.
    public const int SB_TOP = 6;



    public const int WM_VSCROLL = 0x115;
    public const int WM_HSCROLL = 0x114;





    public const int WM_NCHITTEST = 0x84;
    public const int WM_SETCURSOR = 0x20;
    public const int Wut = 0xc1b1;
    public const int WM_MOUSEMOVE = 0x200;
    public const int WM_REFLECT_WM_NOTIFY = 0x204e;
    public const int WM_MOUSELEAVE = 0x2a3;
    public const int WM_IME_NOTIFY = 0x282;
    public const int WM_SETFOCUS = 0x7;
    public const int WM_IME_SETCONTEXT = 0x281;
    public const int WM_REFLECT_WM_COMMAND = 0x2111;
    public const int WM_KILLFOCUS = 0x8;
    public const int WM_NCCREATE = 0x81;
    public const int WM_NCCALCSIZE = 0x83;
    public const int WM_CREATE = 0x1;
    public const int WM_WINDOWPOSCHANGING = 0x46;
    public const int WM_NCMOUSEMOVE = 0xa0;
    public const int Wut2 = 0x2a2;
    public const int WM_MOUSEHOVER = 0x2a1;



}
