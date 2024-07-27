using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class LinesReadResult
{
    public string newText;
    public string[] newLogTypesFound;
};

public delegate void OnLinesReadDelegate(LinesReadResult result );

public class FileController
{
    public readonly string path;
    BackgroundWorker m_worker;
    DataBuffer m_buffer;
    LogOptions m_options;
    bool m_shouldReload = false;
    OnLinesReadDelegate m_onLinesReadDelegate;

    public FileController(string in_path)
    {
        path = in_path;
    }

    public void OpenFile(LogOptions in_options)
    {
        if (m_buffer == null)
        {            
            m_buffer = new DataBuffer(path, in_options);
            m_worker = new BackgroundWorker();
            m_worker.WorkerReportsProgress = true;
            m_worker.DoWork += new DoWorkEventHandler(DoWork);
            m_worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            m_worker.RunWorkerAsync(m_buffer);
        }
        else
        {
            m_options = in_options;
            m_shouldReload = true;
            m_buffer.Stop();
        }
    }

    public void Stop()
    {
        m_onLinesReadDelegate = null;

        if (m_worker != null && m_worker.IsBusy && m_buffer != null)
        {
            m_buffer.Stop();
        }        
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
                buffer.CreateText();

                // Send the results to the main thread
                LinesReadResult res = new LinesReadResult();
                res.newText = buffer.text;
                res.newLogTypesFound = buffer.logTypes;
                worker.ReportProgress(100, res);
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    private void ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (m_onLinesReadDelegate != null)
        {
            m_onLinesReadDelegate.Invoke(e.UserState as LinesReadResult);
        }
    }

    private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Add new log types we haven't seen before        
        m_buffer = null;

        if( m_shouldReload)
        {
            OpenFile(m_options);
        }
    }

    public OnLinesReadDelegate onLinesReadDelegate
    {
        get
        {
            return m_onLinesReadDelegate;
        }
        set
        {
            m_onLinesReadDelegate = value;
        }
    }
}

