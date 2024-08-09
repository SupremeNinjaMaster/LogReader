using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class LinesReadResult
{
    public string NewText;
    public string[] NewLogTypesFound;
};

public delegate void OnLinesReadDelegate(LinesReadResult result );

public class FileController
{
    public readonly string Path;
    private BackgroundWorker _worker;
    private DataBuffer _buffer;
    private LogOptions _options;
    private bool _shouldReload = false;

    /// <summary>
    /// The index from which we need to start creating text from the buffer
    /// </summary>
    private int _startLineIndex = 0;

    private int _maxLinesRead = 0;

    /// <summary>
    /// Delegate invoked in the main thread when we are done reading lines
    /// </summary>
    OnLinesReadDelegate _onLinesReadDelegate;

    public FileController(string path)
    {
        Path = path;
    }

    public void OpenFile(LogOptions options)
    {
        _options = options;

        if (_buffer == null)
        {            
            _buffer = new DataBuffer(Path, options, _startLineIndex);
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(DoWork);
            _worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _worker.RunWorkerAsync(_buffer);
        }
        else
        {            
            _shouldReload = true;
            _buffer.Stop();
        }
    }

    public void Stop()
    {
        //m_onLinesReadDelegate = null;

        if (_worker != null && _worker.IsBusy && _buffer != null)
        {
            _buffer.Stop();
        } 
    }

    public void ClearLog()
    {
        // Save atomically the start line index so we can reload from that start index
        Interlocked.Exchange(ref _startLineIndex, _maxLinesRead);
        _shouldReload = true;
        Stop();
    }

    /// <summary>
    /// The background thread doing work
    /// </summary>
    /// <param name="sender">the background thread</param>
    /// <param name="e">the params containing the buffer doing the work</param>
    private void DoWork(object sender, DoWorkEventArgs e)
    {
        // This is done in the background thread
        BackgroundWorker worker = (BackgroundWorker)(sender);
        DataBuffer buffer = (DataBuffer)(e.Argument);
         
        while (!buffer.ShouldStop())
        {
            int linesRead = buffer.Read();

            if (linesRead > 0)
            {
                bool hasTextChanged;
                buffer.CreateText(out hasTextChanged);

                if (hasTextChanged)
                {
                    // Send the results to the main thread but only if the resulting text has changed
                    LinesReadResult res = new LinesReadResult();
                    res.NewText = buffer.text;
                    res.NewLogTypesFound = buffer.logTypes;
                    worker.ReportProgress(100, res);
                }
            }
            else
            {
                Thread.Sleep(1000);
            }

            // Save the max lines read into the main thread counter
            Interlocked.Exchange(ref _maxLinesRead, buffer.maxLinesRead);
        }
    }

    private void ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (_onLinesReadDelegate != null)
        {
            _onLinesReadDelegate.Invoke(e.UserState as LinesReadResult);
        }
    }

    private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Add new log types we haven't seen before        
        _buffer = null;

        if( _shouldReload)
        {
            OpenFile(_options);
        }
    }

    public OnLinesReadDelegate onLinesReadDelegate
    {
        get
        {
            return _onLinesReadDelegate;
        }
        set
        {
            _onLinesReadDelegate = value;
        }
    }
}

