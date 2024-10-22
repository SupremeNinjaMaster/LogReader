using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms.VisualStyles;


/*
 * @todo:
 * 
 * 
 * 4. Do scrolling
 * 5. set default color for text
 * *
 */


public enum ScrollBarArrowButtonState
{
    /// <summary>
    /// Indicates the up arrow is in normal state.
    /// </summary>
    UpNormal,

    /// <summary>
    /// Indicates the up arrow is in hot state.
    /// </summary>
    UpHot,

    /// <summary>
    /// Indicates the up arrow is in active state.
    /// </summary>
    UpActive,

    /// <summary>
    /// Indicates the up arrow is in pressed state.
    /// </summary>
    UpPressed,

    /// <summary>
    /// Indicates the up arrow is in disabled state.
    /// </summary>
    UpDisabled,

    /// <summary>
    /// Indicates the down arrow is in normal state.
    /// </summary>
    DownNormal,

    /// <summary>
    /// Indicates the down arrow is in hot state.
    /// </summary>
    DownHot,

    /// <summary>
    /// Indicates the down arrow is in active state.
    /// </summary>
    DownActive,

    /// <summary>
    /// Indicates the down arrow is in pressed state.
    /// </summary>
    DownPressed,

    /// <summary>
    /// Indicates the down arrow is in disabled state.
    /// </summary>
    DownDisabled,

};




/// <summary>
/// Custom scrollbar based on the FlatScrollBar from ahmedosama007
/// https://gist.github.com/ahmedosama007/c1b0cd327d395a5698c1e17e96d0f8f9
/// </summary>
public class LogScrollbar : Control, IColorable
{
    private const int MINIMUM_SIZE = 10;

    private static readonly object EVENT_SCROLL = new object();

    private static readonly object EVENT_VALUECHANGED = new object();

    private ScrollOrientation _barOrientation = ScrollOrientation.VerticalScroll;



    //Indicates if top arrow was clicked.
    private bool _isTopArrowClicked;

    ////Indicates if down arrow was clicked.
    private bool _isBottomArrowClicked;
    //Indicates if channel rectangle above the thumb was clicked.
    private bool _isTopBarClicked;

    //Indicates if channel rectangle under the thumb was clicked.
    private bool _isBottomBarClicked;

    //Indicates if the thumb was clicked.
    private bool _isThumbClicked;

    // //The state of the thumb.
    private ScrollBarState _thumbState = ScrollBarState.Normal;

    ////The state of the top arrow.
    private ScrollBarArrowButtonState _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
    ////The state of the bottom arrow.
    private ScrollBarArrowButtonState _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;

    private int _minimum = 0;
    private int _maximum = 100;
    private int _smallChange = 1;
    private int _largeChange = 10;
    private int _scrollValue = 0;
    private int _wheelDelta = 0;

    //The bottom limit for the thumb bottom.
    private int _thumbBottomLimitBottom;
    //The bottom limit for the thumb top.

    //The top limit for the thumb top.
    private int _thumbBottomLimitTop;
    private int _thumbTopLimit;

    //The current position of the thumb.
    private int _thumbPosition;

    //The track position.
    private int _trackPosition;

    //The progress timer for moving the thumb.
    private readonly Timer _scrollTimer = new Timer();

    private LogScrollbarRenderer _scrollbarRenderer;

    private ColorSet _colorSet;


    public LogScrollbar()
    {
        _scrollTimer.Tick += ScrollTimer_Tick;

        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        SetUpScrollBar();
    }

    #region Public Properties

    /// <summary>
    /// Gets or sets the ScrollBar orientation.
    /// </summary>
    public ScrollOrientation Orientation
    {
        get
        {
            return _barOrientation;
        }

        set
        {
            if (value != _barOrientation)
            {
                _barOrientation = value;

                if (DesignMode) //only in DesignMode switch width and height
                {
                    Size = new Size(Height, Width);
                }

                SetUpScrollBar();

            }
        }
    }


    /// <summary>
    /// Gets or sets the ScrollBar minimum value.
    /// </summary>
    public int Minimum
    {
        get
        {
            return _minimum;
        }
        set
        {
            if (_minimum == value || value < 0 || value >= _maximum)
            {
                return;
            }

            _minimum = value;

            //Current large change value invalid - adjust
            if (_largeChange > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }

            SetUpScrollBar();

            if (_scrollValue < ScrollValue) //Current value less than new minimum value - adjust
            {
                ScrollValue = value;
            }
            else
            {
                ChangeThumbPosition(GetThumbPosition()); //Current value is valid - adjust thumb position
                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets the ScrollBar minimum value.
    /// </summary>
    public int Maximum
    {
        get
        {
            return _maximum;
        }
        set
        {
            if (value == _maximum || value < 1 || value <= _minimum)
            {
                return;
            }

            _maximum = value;

            if (_largeChange > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }

            SetUpScrollBar();

            if (_scrollValue > _maximum)
            {
                ScrollValue = _maximum;
            }
            else
            {
                ChangeThumbPosition(GetThumbPosition());
                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets the ScrollBar small change value.
    /// </summary>
    public int SmallChange
    {
        get
        {
            return _smallChange;
        }
        set
        {
            if (value == _smallChange || value < 1 || value >= _largeChange)
            {
                return;
            }

            _smallChange = value;
            SetUpScrollBar();
        }
    }

    // Is this needed?????????

    /// <summary>
    /// Gets or sets the ScrollBar large change value.
    /// </summary>
    public int LargeChange
    {
        get
        {
            return _largeChange;
        }
        set
        {
            if (value == _largeChange || value < _smallChange || value < 2)
            {
                return;
            }

            if (value > _maximum - _minimum)
            {
                _largeChange = _maximum - _minimum;
            }
            else
            {
                _largeChange = value;
            }

            SetUpScrollBar();
        }
    }

    /// <summary>
    /// Gets or sets the actual value of the scrolling
    /// </summary>
    public int ScrollValue
    {
        get
        {
            return _scrollValue;
        }
        set
        {
            if (_scrollValue == value || value < _minimum || _scrollValue > _maximum)
            {
                return;
            }

            _scrollValue = value;
            ChangeThumbPosition(GetThumbPosition());


            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, _scrollValue, _barOrientation));
            Refresh();
        }
    }


    #endregion



    public void SetColors(ColorSet colorSet)
    {
        _colorSet = colorSet;

    }



    #region Overridden Methods


    /// <summary>
    /// When scrolling happens we send a message to the parent control to scroll
    /// </summary>
    /// <param name="e"></param>
    protected void OnScroll(ScrollEventArgs e)
    {
        int msg = e.ScrollOrientation == ScrollOrientation.VerticalScroll ? Win32APIConstants.WM_VSCROLL : Win32APIConstants.WM_HSCROLL;
        int wParam, lParam = 0;

        switch (e.Type)
        {
            case ScrollEventType.ThumbPosition:
                wParam = Win32APIConstants.SB_THUMBPOSITION;
                lParam = e.NewValue;
                break;
            case ScrollEventType.SmallDecrement:
                wParam = Win32APIConstants.SB_LINEUP;
                break;
            case ScrollEventType.SmallIncrement:
                wParam = Win32APIConstants.SB_LINEDOWN;
                break;
            case ScrollEventType.LargeDecrement:
                wParam = Win32APIConstants.SB_PAGEUP;
                break;
            case ScrollEventType.LargeIncrement:
                wParam = Win32APIConstants.SB_PAGEDOWN;
                break;
            case ScrollEventType.ThumbTrack:
                wParam = Win32APIConstants.SB_THUMBTRACK;
                lParam = e.NewValue;
                break;
            case ScrollEventType.First:
                wParam = Win32APIConstants.SB_TOP;
                break;
            case ScrollEventType.Last:
                wParam = Win32APIConstants.SB_BOTTOM;
                break;
            case ScrollEventType.EndScroll:
                wParam = Win32APIConstants.SB_ENDSCROLL;
                break;
            default:
                return;
        }

        Console.WriteLine("{0}, {1} -> {2} / {3}", e.Type.ToString(), e.OldValue, e.NewValue, Maximum);

        NativeFunctions.SendMessage(Parent.Handle, msg, (IntPtr)(wParam), (IntPtr)(lParam));

    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        _scrollbarRenderer.DrawBackground(e.Graphics, ClientRectangle);
        _scrollbarRenderer.DrawThumb(e.Graphics, _scrollbarRenderer.ThumbRectangle, _thumbState);

        _scrollbarRenderer.DrawArrowButton(e.Graphics, _scrollbarRenderer.TopArrowRectangle, _topArrowButtonState, true, _barOrientation);
        _scrollbarRenderer.DrawArrowButton(e.Graphics, _scrollbarRenderer.BottomArrowRectangle, _bottomArrowButtonState, false, _barOrientation);

        if (_isTopBarClicked)
        {
            _scrollbarRenderer.DrawClickedTopBar(_thumbTopLimit);
        }
        else if (_isBottomBarClicked)
        {
            _scrollbarRenderer.DrawClickedBottomBar(_thumbBottomLimitBottom);
        }

        _scrollbarRenderer.DrawBorder(e.Graphics, Enabled, Width, Height);
    }

    protected override void OnMouseHover(EventArgs e)
    {
        Cursor = Cursors.Default;
        base.OnMouseHover(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        Focus();

        if (e.Button == MouseButtons.Left)
        {
            Point mouseLocation = e.Location;

            if (_scrollbarRenderer.ThumbRectangle.Contains(mouseLocation))
            {
                _isThumbClicked = true;
                _thumbPosition = (_barOrientation == ScrollOrientation.VerticalScroll ? mouseLocation.Y - _scrollbarRenderer.ThumbRectangle.Y : mouseLocation.X - _scrollbarRenderer.ThumbRectangle.X);
                _thumbState = ScrollBarState.Pressed;
                Invalidate(_scrollbarRenderer.ThumbRectangle);
            }
            else if (_scrollbarRenderer.TopArrowRectangle.Contains(mouseLocation))
            {
                _isTopArrowClicked = true;
                _topArrowButtonState = ScrollBarArrowButtonState.UpPressed;
                Invalidate(_scrollbarRenderer.TopArrowRectangle);
                StartScrollTimer();
                _wheelDelta += 120;
            }
            else if (_scrollbarRenderer.BottomArrowRectangle.Contains(mouseLocation))
            {
                _isBottomArrowClicked = true;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownPressed;
                Invalidate(_scrollbarRenderer.BottomArrowRectangle);
                StartScrollTimer();
                _wheelDelta -= 120;
            }
            else
            {
                _trackPosition = (_barOrientation == ScrollOrientation.VerticalScroll ? mouseLocation.Y : mouseLocation.X);

                if (_trackPosition < (_barOrientation == ScrollOrientation.VerticalScroll ? _scrollbarRenderer.ThumbRectangle.Y : _scrollbarRenderer.ThumbRectangle.X))
                {
                    _isTopBarClicked = true;
                }
                else
                {
                    _isBottomBarClicked = true;
                }

                ProgressThumb();

            }
        }
        else if (e.Button == MouseButtons.Right)
        {
            _trackPosition = (_barOrientation == ScrollOrientation.VerticalScroll ? e.Y : e.X);
        }

    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (e.Button == MouseButtons.Left)
        {
            /*if (_isThumbClicked)
            {
                _isThumbClicked = false;
                _thumbState = ScrollBarState.Normal;
                OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, -1, _scrollValue, _barOrientation));
            }
            else*/

            if (_isTopArrowClicked)
            {
                _isTopArrowClicked = false;
                _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
            }
            else if (_isBottomArrowClicked)
            {
                _isBottomArrowClicked = false;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
            }




            //else if (_isTopBarClicked)
            //{
            //    _isTopBarClicked = false;
            //    _scrollTimer.Stop();
            //}
            //else if (_isBottomBarClicked)
            //{
            //    _isBottomBarClicked = false;
            //    _scrollTimer.Stop();

            //}

            Invalidate();
        }

    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        RefreshScrollBar();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        /*
        if (e.Button == MouseButtons.Left) //Moving and holding the left mouse button
        {
            if (_isThumbClicked)
            {
                int oldValue = _scrollValue;

                int pos = (_barOrientation == ScrollOrientation.Vertical ? e.Location.Y : e.Location.X);

                if (pos <= (_thumbTopLimit + _thumbPosition)) //The thumb is all the way to the top
                {
                    ChangeThumbPosition(_thumbTopLimit);
                    _scrollValue = _minimum;
                }
                else if (pos >= (_thumbBottomLimitTop + _thumbPosition)) //The thumb is all the way to the bottom
                {
                    ChangeThumbPosition(_thumbBottomLimitTop);
                    _scrollValue = _maximum;
                }
                else //The thumb is between the ends of the track.
                {
                    ChangeThumbPosition(pos - _thumbPosition);


                    int pixelRange, thumbPos, arrowSize;

                    //Calculate the value - first some helper variables dependent on the current orientation

                    if (_barOrientation == ScrollOrientation.Vertical)
                    {
                        pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
                        thumbPos = _scrollbarRenderer.ThumbRectangle.Y;
                        arrowSize = _arrowHeight;
                    }
                    else
                    {
                        pixelRange = Width - (2 * _arrowWidth) - _thumbWidth;
                        thumbPos = _scrollbarRenderer.ThumbRectangle.X;
                        arrowSize = _arrowWidth;
                    }


                    float perc = 0.0f;

                    if (pixelRange != 0)
                    {
                        perc = (float)(thumbPos - arrowSize) / (float)(pixelRange);
                    }


                    _scrollValue = Convert.ToInt32((perc * (_maximum - _minimum)) + _minimum);
                }



                if (oldValue != _scrollValue)
                {
                    OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldValue, _scrollValue, _barOrientation));
                    Refresh();
                }
            }

        }

        else if (!ClientRectangle.Contains(e.Location))
        {
            RefreshScrollBar();
        }
        else if (e.Button == MouseButtons.None) //Only moving the mouse
        { 
            if (_scrollbarRenderer.TopArrowRectangle.Contains(e.Location))
            {
                _topArrowButtonState = ScrollBarArrowButtonState.UpHot;
                Invalidate(_scrollbarRenderer.TopArrowRectangle);
            }
            else if (_scrollbarRenderer.BottomArrowRectangle.Contains(e.Location))
            {
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownHot;
                Invalidate(_scrollbarRenderer.BottomArrowRectangle);
            }
            else if (_scrollbarRenderer.ThumbRectangle.Contains(e.Location))
            {
                _thumbState = ScrollBarState.Hot;
                Invalidate(_scrollbarRenderer.ThumbRectangle);
            }
            else
            {
                _thumbState = ScrollBarState.Normal;
                _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;

                Refresh();
            }
        }*/
    }



    protected override void OnMouseWheel(MouseEventArgs e)
    {
        _wheelDelta += e.Delta;

        StartScrollTimer();

        if (e is HandledMouseEventArgs)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        base.OnMouseWheel(e);
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (DesignMode)
        {
            if (_barOrientation == ScrollOrientation.VerticalScroll)
            {
                var minHeight = (2 * _scrollbarRenderer.ArrowHeight) + MINIMUM_SIZE;

                if (height < minHeight)
                {
                    height = minHeight;
                }
                width = SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                var minWidth = (2 * _scrollbarRenderer.ArrowWidth) + MINIMUM_SIZE;

                if (width < minWidth)
                {
                    width = minWidth;
                }
                height = SystemInformation.VerticalScrollBarWidth;
            }
        }

        base.SetBoundsCore(x, y, width, height, specified);

        if (DesignMode)
        {
            SetUpScrollBar();
        }
    }

    protected override void OnParentChanged(EventArgs e)
    {
        // Make sure we know when the parent has been resized
        Parent.Resize += OnParentResize;
        Parent.MouseWheel += OnParentMouseWheel;
        base.OnParentChanged(e);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        SetUpScrollBar();
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
        bool isHandled = false;

        var oldValue = _scrollValue;
        ScrollEventType scrollType = ScrollEventType.First;


        switch (keyData)
        {
            case Keys.Up:

                _scrollValue = GetScrollValue(true, true);

                if (_scrollValue == _minimum)
                {
                    scrollType = ScrollEventType.First;
                    ChangeThumbPosition(_thumbTopLimit);
                }
                else
                {
                    scrollType = ScrollEventType.SmallDecrement;
                    ChangeThumbPosition(Math.Max(_thumbTopLimit, GetThumbPosition()));
                }

                isHandled = true;
                break;

            case Keys.Down:

                _scrollValue = GetScrollValue(true, false);

                if (_scrollValue == _maximum)
                {
                    scrollType = ScrollEventType.Last;
                    ChangeThumbPosition(_thumbBottomLimitTop);
                }
                else
                {
                    scrollType = ScrollEventType.SmallIncrement;
                    ChangeThumbPosition(Math.Min(_thumbBottomLimitTop, GetThumbPosition()));
                }

                isHandled = true;
                break;

            case Keys.PageUp:

                _scrollValue = GetScrollValue(false, true);

                if (_scrollValue == _minimum)
                {
                    scrollType = ScrollEventType.First;
                    ChangeThumbPosition(_thumbTopLimit);
                }
                else
                {
                    scrollType = ScrollEventType.LargeDecrement;
                    ChangeThumbPosition(Math.Max(_thumbTopLimit, GetThumbPosition()));
                }

                isHandled = true;
                break;

            case Keys.PageDown:

                _scrollValue = GetScrollValue(false, false);

                if (_scrollValue == _maximum)
                {
                    scrollType = ScrollEventType.Last;
                    ChangeThumbPosition(_thumbBottomLimitTop);
                }
                else
                {
                    scrollType = ScrollEventType.SmallIncrement;
                    ChangeThumbPosition(Math.Min(_thumbBottomLimitTop, GetThumbPosition()));
                }

                isHandled = true;
                break;

            case Keys.Home:

                _scrollValue = _minimum;

                scrollType = ScrollEventType.First;
                ChangeThumbPosition(_thumbTopLimit);

                isHandled = true;
                break;

            case Keys.End:

                _scrollValue = _maximum;

                scrollType = ScrollEventType.Last;
                ChangeThumbPosition(_thumbBottomLimitTop);

                isHandled = true;
                break;

        }

        if (isHandled && oldValue != _scrollValue)
        {
            OnScroll(new ScrollEventArgs(scrollType, oldValue, _scrollValue, _barOrientation));
            Invalidate(_scrollbarRenderer.ChannelRectangle);
        }

        return isHandled && base.ProcessDialogKey(keyData);

    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);

        if (Enabled)
        {
            _thumbState = ScrollBarState.Normal;
            _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
            _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
        }
        else
        {
            _thumbState = ScrollBarState.Disabled;
            _topArrowButtonState = ScrollBarArrowButtonState.UpDisabled;
            _bottomArrowButtonState = ScrollBarArrowButtonState.DownDisabled;
        }

        Refresh();

    }

    #endregion



    #region Private Methods

    private void SetUpScrollBar()
    {








        _scrollbarRenderer = new LogScrollbarRenderer(_barOrientation, ClientRectangle, GetThumbSize());
        _scrollbarRenderer.SetColors(_colorSet);

        if (_barOrientation == ScrollOrientation.VerticalScroll)
        {
            _thumbPosition = (_scrollbarRenderer.ThumbRectangle.Height / 2);
            _thumbBottomLimitBottom = ClientRectangle.Bottom - _scrollbarRenderer.ArrowHeight - 2;
            _thumbBottomLimitTop = _thumbBottomLimitBottom - _scrollbarRenderer.ThumbRectangle.Height;
            _thumbTopLimit = ClientRectangle.Y + _scrollbarRenderer.ArrowHeight + 2;
        }
        else
        {
            _thumbPosition = (_scrollbarRenderer.ThumbRectangle.Width / 2);
            _thumbBottomLimitBottom = ClientRectangle.Right - _scrollbarRenderer.ArrowWidth - 3;
            _thumbBottomLimitTop = _thumbBottomLimitBottom - _scrollbarRenderer.ThumbRectangle.Width;
            _thumbTopLimit = ClientRectangle.X + _scrollbarRenderer.ArrowWidth + 3;
        }

        ChangeThumbPosition(GetThumbPosition());
        Refresh();
    }

    /// <summary>
    /// When the parent is resized we need to adjust our size and our location
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnParentResize(object sender, EventArgs e)
    {
        if (_barOrientation == ScrollOrientation.VerticalScroll)
        {
            Size = new Size(SystemInformation.VerticalScrollBarWidth, Parent.ClientRectangle.Height);
            Location = new Point(Parent.ClientRectangle.Width - Width, 0);
        }
        else
        {
            Size = new Size(Parent.ClientRectangle.Width, SystemInformation.HorizontalScrollBarHeight);
            Location = new Point(0, Parent.ClientRectangle.Height - Height);
        }
    }

    private void OnParentMouseWheel(object sender, MouseEventArgs e)
    {
        this.OnMouseWheel(e);
    }

    private void RefreshScrollBar()
    {
        Point pt = PointToClient(Cursor.Position);

        if (ClientRectangle.Contains(pt))
        {
            if (_scrollbarRenderer.ThumbRectangle.Contains(pt))
            {
                _thumbState = ScrollBarState.Hot;
                _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
            }
            else if (_scrollbarRenderer.TopArrowRectangle.Contains(pt))
            {
                _thumbState = ScrollBarState.Normal;
                _topArrowButtonState = ScrollBarArrowButtonState.UpActive;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
            }
            else if (_scrollbarRenderer.BottomArrowRectangle.Contains(pt))
            {
                _thumbState = ScrollBarState.Normal;
                _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownActive;
            }
            else
            {
                _thumbState = ScrollBarState.Normal;
                _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
                _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
            }
        }
        else
        {
            _thumbState = ScrollBarState.Normal;
            _topArrowButtonState = ScrollBarArrowButtonState.UpNormal;
            _bottomArrowButtonState = ScrollBarArrowButtonState.DownNormal;
        }

        _isTopArrowClicked = false;
        _isBottomArrowClicked = false;
        _isTopBarClicked = false;
        _isBottomBarClicked = false;

        _scrollTimer.Stop();
        Refresh();

    }

    private int GetScrollValue(bool isSmallChange, bool isDecreaseValue)
    {
        int newValue;

        if (isDecreaseValue)
        {
            newValue = _scrollValue - (isSmallChange ? _smallChange : _largeChange);
            if (newValue < _minimum)
            {
                newValue = _minimum;
            }
        }
        else
        {
            newValue = _scrollValue + (isSmallChange ? _smallChange : _largeChange);
            if (newValue > _maximum)
            {
                newValue = _maximum;
            }

        }

        return newValue;
    }

    private int GetThumbPosition()
    {
        var pixelRange = (_barOrientation == ScrollOrientation.VerticalScroll ? _scrollbarRenderer.ChannelRectangle.Height : _scrollbarRenderer.ChannelRectangle.Width);

        int realRange = _maximum - _minimum;
        float perc = 0f;

        if (realRange != 0)
        {
            perc = (float)((_scrollValue - _minimum) / realRange);
        }

        return Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, Convert.ToInt32(perc * pixelRange)));

    }

    private int GetThumbSize()
    {
        int trackSize = (_barOrientation == ScrollOrientation.VerticalScroll ? Height : Width);

        if (_maximum == 0 || _largeChange == 0)
        {
            return trackSize;
        }

        float thumbSize = (float)(_largeChange * trackSize / _maximum);

        return Convert.ToInt32(Math.Min(trackSize, Math.Max(thumbSize, 10.0F)));

    }

    private void ChangeThumbPosition(int position)
    {
        _scrollbarRenderer.SetThumbPosition(position);

        Point pt = PointToClient(Cursor.Position);

        if (_scrollbarRenderer.ThumbRectangle.Contains(pt))
        {
            _thumbState = ScrollBarState.Hot;
            Invalidate(_scrollbarRenderer.ThumbRectangle);
        }
    }

    private void ProgressThumb()
    {
        var oldValue = _scrollValue;
        ScrollEventType type = ScrollEventType.First;
        int thumbSize, thumbPos;

        if (_barOrientation == ScrollOrientation.VerticalScroll)
        {
            thumbPos = _scrollbarRenderer.ThumbRectangle.Y;
            thumbSize = _scrollbarRenderer.ThumbRectangle.Height;
        }
        else
        {
            thumbPos = _scrollbarRenderer.ThumbRectangle.X;
            thumbSize = _scrollbarRenderer.ThumbRectangle.Width;
        }

        if (_isBottomArrowClicked || (_isBottomBarClicked && (thumbPos + thumbSize) < _trackPosition))
        {
            type = (_isBottomArrowClicked ? ScrollEventType.SmallIncrement : ScrollEventType.LargeIncrement);
            _scrollValue = GetScrollValue(_isBottomArrowClicked, false);

            if (_scrollValue == _maximum)
            {
                ChangeThumbPosition(_thumbBottomLimitTop);
                type = ScrollEventType.Last;
            }
            else
            {
                ChangeThumbPosition(Math.Min(_thumbBottomLimitTop, GetThumbPosition()));
            }
        }
        else if (_isTopArrowClicked || (_isTopBarClicked && thumbPos > _trackPosition))
        {
            type = (_isTopArrowClicked ? ScrollEventType.SmallDecrement : ScrollEventType.LargeDecrement);
            _scrollValue = GetScrollValue(_isTopArrowClicked, true);

            if (_scrollValue == _minimum)
            {
                ChangeThumbPosition(_thumbTopLimit);
                type = ScrollEventType.First;
            }
            else
            {
                ChangeThumbPosition(Math.Max(_thumbTopLimit, GetThumbPosition()));
            }
        }
        else if (!((_isTopArrowClicked && thumbPos == _thumbTopLimit) || (_isBottomArrowClicked && thumbPos == _thumbBottomLimitTop)))
        {
            RefreshScrollBar();
            return;
        }

        if (oldValue != _scrollValue)
        {
            OnScroll(new ScrollEventArgs(type, oldValue, _scrollValue, _barOrientation));
            Invalidate(_scrollbarRenderer.ChannelRectangle);
        }
    }

    /*public void DoScroll(ScrollEventType type)
    {
        int newValue = _scrollValue;
        int oldValue = _scrollValue;
        switch (type)
        {
            case ScrollEventType.First:
                newValue = _minimum;
                break;
            case ScrollEventType.Last:
                newValue = _maximum - LargeChange + 1;
                break;
            case ScrollEventType.SmallDecrement:
                newValue = Math.Max(_scrollValue - SmallChange, _minimum);
                break;
            case ScrollEventType.SmallIncrement:
                newValue = Math.Min(_scrollValue + SmallChange, _maximum - LargeChange + 1);
                break;
            case ScrollEventType.LargeDecrement:
                newValue = Math.Max(_scrollValue - LargeChange, _minimum);
                break;
            case ScrollEventType.LargeIncrement:
                newValue = Math.Min(_scrollValue + LargeChange, _maximum - LargeChange + 1);
                break;
            case ScrollEventType.ThumbPosition:
                {
                    SCROLLINFO info = new SCROLLINFO();
                    info.fMask = 16;
                    Utils.GetScrollInfo(Parent.Handle, Win32APIConstants.SB_CTL, ref info);
                    newValue = info.nTrackPos;

                }
                break;

            //case ScrollEventType.ThumbTrack:
            //    {
            //        NativeMethods.SCROLLINFO sCROLLINFO = new NativeMethods.SCROLLINFO();
            //        sCROLLINFO.fMask = 16;
            //        SafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 2, sCROLLINFO);
            //        newValue = ((RightToLeft != RightToLeft.Yes) ? sCROLLINFO.nTrackPos : ReflectPosition(sCROLLINFO.nTrackPos));
            //        break;
            //    }
        }

        if (newValue != oldValue)
        {
            ScrollEventArgs scrollEventArgs = new ScrollEventArgs(type, oldValue, newValue, _barOrientation);
            OnScroll(scrollEventArgs);
            ScrollValue = scrollEventArgs.NewValue;         
        }
    }*/

    #endregion

    private void ScrollTimer_Tick(object sender, EventArgs e)
    {
        //ProgressThumb();






        if (_isTopArrowClicked)
        {
            _wheelDelta += 50;
        }
        else if (_isBottomArrowClicked)
        {
            _wheelDelta -= 50;
        }









        ScrollEventType scrollType = _wheelDelta < 0 ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement;

        _wheelDelta = (int)((float)_wheelDelta * 0.8f);

        Console.WriteLine("wheel delta: {0}", _wheelDelta);

        if (_wheelDelta == 0)
        {
            scrollType = ScrollEventType.EndScroll;
            _scrollTimer.Stop();
        }

        OnScroll(new ScrollEventArgs(scrollType, 0, _scrollValue, _barOrientation));
        Invalidate(_scrollbarRenderer.ChannelRectangle);
    }

    private void StartScrollTimer()
    {
        if (!_scrollTimer.Enabled)
        {
            _scrollTimer.Interval = 10;
            _scrollTimer.Start();
        }
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case Win32APIConstants.WM_VSCROLL:
                Console.WriteLine("Scrolling msg,  wp: {0}, lp:{1} ", m.WParam, m.LParam);
                break;

        }

        base.WndProc(ref m);
    }
}
