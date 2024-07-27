using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;

public struct LogLine
{
    public int index;
    public string logType;
    public string timestamp;
    public string content;
    public int logHash;
    public EVerbosity verbosity;

    public LogLine(int in_index, string in_logType, string in_timestamp, string in_content, EVerbosity in_verbosity)
    {
        index = in_index;
        logType = in_logType;
        timestamp = in_timestamp;
        content = in_content;
        logHash = in_logType.GetHashCode();
        verbosity = in_verbosity;
    }

    public void Append( string in_extraContent)
    {
        content += Environment.NewLine + in_extraContent;
    }

    public override string ToString()
    {
        // ignore timestamp for now

        StringBuilder str = new StringBuilder();
        if( !string.IsNullOrEmpty(logType))
        {
            str.Append(logType);
            str.Append(": ");

            if (verbosity != EVerbosity.Log)
            {
                str.Append(verbosity);
                str.Append(": ");
            }
        }        

        str.Append(content);
        return str.ToString();
    }

    public static readonly LogLine INVALID = new LogLine(-1, "", "", "", EVerbosity.Log);
}


public class DataBuffer
{   
    string m_text;

    HashSet<string> m_logTypes;    
    List<LogLine> m_lines;
    LogOptions m_logOptions;
    public readonly string path;
    private long m_shouldStop = 0;
    private long m_lastByteRead = 0;
    private long m_shouldReload = 0;

    public DataBuffer(string in_path, LogOptions in_options)
    {
        path = in_path;        
        m_lines = new List<LogLine>();
        m_logTypes = new HashSet<string>();
        m_logOptions = in_options;
    }

    /// <summary>
    /// Reads a file
    /// </summary>
    public int Read()
    {
        int linesRead = 0;

        if(Interlocked.Read(ref m_shouldReload) > 0L)
        {
            m_lastByteRead = 0;
        }

        if (new FileInfo(path).Length > m_lastByteRead)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.Seek(m_lastByteRead, SeekOrigin.Begin);

                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {                    
                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        LogLine logLine = CreateLine(m_lines.Count +1, line);

                        // Add the log type we found to the list of log types
                        if (!string.IsNullOrEmpty(logLine.logType))
                        {
                            m_logTypes.Add(logLine.logType);
                        }

                        // if there is no log type, it is the continuation of the previous line
                        if (string.IsNullOrEmpty(logLine.logType) &&
                            string.IsNullOrEmpty(logLine.timestamp) &&
                            m_lines.Count > 0)
                        {
                            logLine.logType = m_lines[m_lines.Count - 1].logType;
                            logLine.verbosity = m_lines[m_lines.Count - 1].verbosity;
                        }

                        m_lines.Add(logLine);
                        linesRead++;
                    }

                    m_lastByteRead = fileStream.Length;
                }
                
            }
        }        

        return linesRead;
    }

    public void Stop()
    {
        Interlocked.Increment(ref m_shouldStop);
    }

    public bool ShouldStop()
    {
        return Interlocked.Read(ref m_shouldStop) > 0L;
    }

    /// <summary>
    /// Reads and parses a line of text from the log
    /// </summary>
    /// <param name="in_str"></param>
    /// <returns></returns>
    private LogLine CreateLine(int in_index, string in_str)
    {
        string pattern = @"^([\[\d\.\-\:\]\s]{30})?(([A-Za-z0-9]+): )?((Log|Fatal|Error|Warning|Display|Verbose|VeryVerbose): )?(.+)";

        Regex reg = new Regex(pattern);
        Match match = reg.Match(in_str);

        if (match != null && match.Success)
        {
            string timestamp = match.Groups[1].ToString();
            string logType = match.Groups[3].ToString();
            string verbosity = match.Groups[5].ToString();
            string content = match.Groups[6].ToString();
                        
            return new LogLine(in_index,
                logType,
                timestamp,
                content,
                LogOpt.GetVerbosityFromString(verbosity));
        }
        
        return new LogLine(in_index, "", "", in_str, EVerbosity.Log);
    }

    /// <summary>
    /// Creates RTF text from the entire file read
    /// </summary>
    public void CreateText()
    {        
        Dictionary<int, string> colorCodeHashMap = new Dictionary<int, string>();
        StringBuilder builder = new StringBuilder();
                
        // header and font table
        builder.AppendLine(@"{\rtf1\ansi\deff0 {\fonttbl {\f0 Courier New;}}");

        // color table
        builder.AppendLine(@"{\colortbl;");
        int colorCount = 0;
        foreach( string str in m_logOptions.optionsMap.Keys)
        {
            int hash = str.GetHashCode();

            Color col = m_logOptions.optionsMap[str].color;
            builder.AppendFormat("\\red{0}\\green{1}\\blue{2};", col.R, col.G, col.B);
            builder.AppendLine();
            colorCodeHashMap.Add(hash, string.Format("\\cf{0} ", ++colorCount));

        }        
        builder.AppendLine(@"}");

        // paragraph start, font type, font size
        builder.AppendLine(@"\pard\f0\fs18");

        string maxLineCount = m_lines.Count.ToString();
        int padding = maxLineCount.Length;

        for ( int i = 0, max = m_lines.Count; i< max; ++i)
        {
            if (m_logOptions.CanShow(m_lines[i].logType, m_lines[i].verbosity))
            {
                if (colorCodeHashMap.ContainsKey(m_lines[i].logHash))
                {
                    builder.Append(colorCodeHashMap[m_lines[i].logHash]);
                }
                else
                {
                    builder.Append(@"\cf0 ");
                }
                                
                // @todo: make line numbers optional
                string lineNumberString = m_lines[i].index.ToString().PadLeft(padding, ' ');
                builder.Append(lineNumberString);
                builder.Append("  ");
                builder.AppendLine(m_lines[i].ToString());
                builder.Append(@"\par");
            }
        }

        builder.Append("}");

        m_text = builder.ToString();
    }
    
    public string text
    {
        get
        {
            return m_text;
        }
    }

    public string[] logTypes
    {
        get
        {
            return m_logTypes.ToArray();
        }
    }
}

