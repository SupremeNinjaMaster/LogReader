using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;


public class LogScrollbarRenderer : IColorable
{
    private Color _backColor = Color.FromArgb(242, 242, 242);
    private Color _borderColor = Color.FromArgb(242, 242, 242);
    private Color _borderColorDisabled = Color.FromArgb(242, 242, 242);

    private Color _thumbColorNormal = Color.FromArgb(194, 195, 201);
    private Color _thumbColorHover = Color.FromArgb(104, 104, 104);
    private Color _thumbColorPressed = Color.FromArgb(91, 91, 91);

    private Color _arrowColorNormal = Color.FromArgb(134, 137, 153);
    private Color _arrowColorHover = Color.FromArgb(70, 181, 255);
    private Color _arrowColorPressed = Color.FromArgb(0, 122, 204);

    private Rectangle _clickBarRectangle;
    private Rectangle _arrowRectangleTop;
    private Rectangle _arrowRectangleBottom;
    private Rectangle _rectThumb;
    private Rectangle _rectTopArrow;
    private Rectangle _rectChannel;

    private int _thumbWidth = 10;
    private int _thumbHeight;

    private int _arrowWidth = 18;
    private int _arrowHeight = 18;

    private ScrollOrientation _orientation;

    public LogScrollbarRenderer(ScrollOrientation orientation, Rectangle clientRectangle, int thumbSize)
    {
        _orientation = orientation;
        _clickBarRectangle = clientRectangle;
        _clickBarRectangle.Inflate(-1, -1);
        if (_orientation == ScrollOrientation.VerticalScroll)
        {
            _thumbWidth = 9;
            _thumbHeight = thumbSize;
            _clickBarRectangle.Y += _arrowHeight;
            _clickBarRectangle.Height -= _arrowHeight * 2;
            _rectChannel = _clickBarRectangle;
            _rectThumb = new Rectangle((clientRectangle.Right / 2) - (_thumbWidth / 2), clientRectangle.Y + _arrowHeight, _thumbWidth, _thumbHeight);
            _rectTopArrow = new Rectangle((clientRectangle.Right / 2) - (_arrowWidth / 2) + 1, clientRectangle.Y + 1, _arrowWidth, _arrowHeight);
            _arrowRectangleBottom = new Rectangle((clientRectangle.Right / 2) - (_arrowWidth / 2), clientRectangle.Bottom - _arrowHeight - 1, _arrowWidth, _arrowHeight);
        }
        else
        {
            _thumbHeight = 9;
            _thumbWidth = thumbSize;
            _clickBarRectangle.X += _arrowWidth;
            _clickBarRectangle.Width -= _arrowWidth * 2;
            _rectChannel = _clickBarRectangle;
            _rectThumb = new Rectangle(clientRectangle.X + _arrowWidth, (clientRectangle.Bottom / 2) - (_thumbHeight / 2), _thumbWidth, _thumbHeight);
            _rectTopArrow = new Rectangle(clientRectangle.X + 2, (clientRectangle.Bottom / 2) - (_arrowHeight / 2), _arrowWidth, _arrowHeight);
            _arrowRectangleBottom = new Rectangle(clientRectangle.Right - _arrowWidth - 2, (clientRectangle.Bottom / 2) - (_arrowHeight / 2) + 1, _arrowWidth, _arrowHeight);
        }
    }

    public void SetColors(ColorSet colorSet)
    {
        _backColor = colorSet.OnPrimary;
        _borderColor = colorSet.OnPrimary;
        _borderColorDisabled = colorSet.OnPrimary;

        //_thumbColorNormal = Color.FromArgb(194, 195, 201);
        _thumbColorHover = colorSet.Primary;
        _thumbColorPressed = colorSet.Secondary;

        //_arrowColorNormal = Color.FromArgb(134, 137, 153);
        _arrowColorHover = colorSet.Primary;
        _arrowColorPressed = colorSet.Secondary;
    }

    public void DrawBorder(Graphics g, bool isEnabled, int width, int height)
    {
        using (Pen p = new Pen(isEnabled ? _borderColor : _borderColorDisabled))
        {
            g.DrawRectangle(p, 0, 0, width - 1, height - 1);
        }
    }

    public void DrawClickedBottomBar(int thumbBottomLimitBototm)
    {
        if (_orientation == ScrollOrientation.VerticalScroll)
        {
            _clickBarRectangle.Y = _rectThumb.Bottom + 1;
            _clickBarRectangle.Height = thumbBottomLimitBototm - _clickBarRectangle.Y + 1;
        }
        else
        {
            _clickBarRectangle.X = _rectThumb.Right + 1;
            _clickBarRectangle.Width = thumbBottomLimitBototm - _clickBarRectangle.X + 1;
        }
    }

    public void DrawClickedTopBar(int thumbTopLimit)
    {
        if (_orientation == ScrollOrientation.VerticalScroll)
        {
            _clickBarRectangle.Y = thumbTopLimit;
            _clickBarRectangle.Height = _rectThumb.Y - thumbTopLimit;
        }
        else
        {
            _clickBarRectangle.X = thumbTopLimit;
            _clickBarRectangle.Width = _rectThumb.X - thumbTopLimit;
        }
    }

    public void DrawBackground(Graphics g, Rectangle rect)
    {
        if (g == null || rect.IsEmpty || g.IsVisibleClipEmpty || !g.VisibleClipBounds.IntersectsWith(rect))
        {
            return;
        }

        using (SolidBrush sb = new SolidBrush(_backColor))
        {
            g.FillRectangle(sb, rect);
        }
    }

    public void DrawThumb(Graphics g, Rectangle rect, ScrollBarState state)
    {
        if (g == null || rect.IsEmpty || g.IsVisibleClipEmpty || !g.VisibleClipBounds.IntersectsWith(rect) || state == ScrollBarState.Disabled)
        {
            return;
        }

        Color thumbColor = _thumbColorNormal;

        switch (state)
        {
            case ScrollBarState.Hot:
                thumbColor = _thumbColorHover;
                break;

            case ScrollBarState.Pressed:
                thumbColor = _thumbColorPressed;
                break;

            default:
                thumbColor = _thumbColorNormal;
                break;
        }

        using (SolidBrush sb = new SolidBrush(thumbColor))
        {
            g.FillRectangle(sb, rect);
        }
    }

    public void DrawArrowButton(Graphics g, Rectangle rect, ScrollBarArrowButtonState state, bool isUpArrow, ScrollOrientation orient)
    {
        if (g == null || rect.IsEmpty || g.IsVisibleClipEmpty || !g.VisibleClipBounds.IntersectsWith(rect))
        {
            return;
        }

        if (orient == ScrollOrientation.VerticalScroll)
        {
            DrawVerticalArrowButton(g, rect, state, isUpArrow);
        }
        else
        {
            DrawHorizontalArrowButton(g, rect, state, isUpArrow);
        }

    }

    public void DrawVerticalArrowButton(Graphics g, Rectangle rect, ScrollBarArrowButtonState state, bool arrowUp)
    {
        using (Image img = GetDownArrowButtonImage(state))
        {
            if (arrowUp)
            {
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            g.DrawImage(img, rect);
        }

    }

    private void DrawHorizontalArrowButton(Graphics g, Rectangle rect, ScrollBarArrowButtonState state, bool arrowUp)
    {
        using (Image img = GetDownArrowButtonImage(state))
        {

            if (arrowUp)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
            {
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            g.DrawImage(img, rect);
        }
    }

    public void SetThumbPosition( int position)
    {
        if (_orientation == ScrollOrientation.VerticalScroll)
        {
            _rectThumb.Y = position;
        }
        else
        {
            _rectThumb.X = position;
        }
    }

    private Image GetDownArrowButtonImage(ScrollBarArrowButtonState state)
    {
        Rectangle rect = new Rectangle(0, 0, _arrowWidth, _arrowHeight);
        Bitmap bitmap = new Bitmap(_arrowWidth, _arrowHeight, PixelFormat.Format32bppArgb);

        Graphics g = Graphics.FromImage(bitmap);

        g.SmoothingMode = SmoothingMode.None;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        Color arrowColor = _arrowColorNormal;

        switch (state)
        {
            case ScrollBarArrowButtonState.UpHot:
            case ScrollBarArrowButtonState.DownHot:
            case ScrollBarArrowButtonState.UpActive:
            case ScrollBarArrowButtonState.DownActive:
                arrowColor = _arrowColorHover;
                break;

            case ScrollBarArrowButtonState.UpPressed:
            case ScrollBarArrowButtonState.DownPressed:
                arrowColor = _arrowColorPressed;
                break;

            default:
                arrowColor = _arrowColorNormal;
                break;

        }

        using (SolidBrush sb = new SolidBrush(arrowColor))
        {
            g.FillPolygon(sb, GetDownArrowPos(rect));
        }

        g.Dispose();

        return bitmap;

    }

    private Point[] GetDownArrowPos(Rectangle r)
    {
        Point middle = new Point(r.Left + (r.Width / 2), r.Top + (r.Height / 2));
        return new Point[]
        {
            new Point(middle.X - 4, middle.Y - 3),
            new Point(middle.X + 4, middle.Y - 2),
            new Point(middle.X, middle.Y + 2)
        };
    }

    public Rectangle ThumbRectangle
    {
        get
        {
            return _rectThumb;
        }
    }

    public Rectangle TopArrowRectangle
    {
        get
        {
            return _rectTopArrow;
        }
    }

    public Rectangle BottomArrowRectangle
    {
        get
        {
            return _arrowRectangleBottom;
        }
    }

    public int ArrowWidth
    {
        get
        {
            return _arrowWidth;
        }
    }

    public int ArrowHeight
    {
        get
        {
            return _arrowHeight;
        }
    }
    
    public Rectangle ChannelRectangle
    {
        get
        {
            return _rectChannel;
        }
    }

    
}