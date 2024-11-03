using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.Security.Policy;



public class DataBuffer
{   
    string _text;

    HashSet<string> _logTypes;    
    List<LogLine> _lines;
    LogOptions _logOptions;
    public readonly string Path;
    private long _shouldStop = 0;
    private long _lastByteRead = 0;

    /// <summary>
    /// We only start creating text starting from this index
    /// </summary>
    private readonly int _lineStart = 0;

    public DataBuffer(string path, LogOptions options, int lineStart)
    {
        Path = path; 
        _lines = new List<LogLine>();
        _logTypes = new HashSet<string>();
        _logOptions = options;
        _lineStart = lineStart;
    }

    /// <summary>
    /// Reads a file
    /// </summary>
    public int Read()
    {
        int linesRead = 0;

        if (new FileInfo(Path).Length > _lastByteRead)
        {
            using (FileStream fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.Seek(_lastByteRead, SeekOrigin.Begin);

                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {                    
                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        LogLine logLine = CreateLine(_lines.Count +1, line);

                        // Add the log type we found to the list of log types
                        if (!string.IsNullOrEmpty(logLine.LogType))
                        {
                            _logTypes.Add(logLine.LogType);
                        }

                        // if there is no log type, it is the continuation of the previous line
                        if (string.IsNullOrEmpty(logLine.LogType) &&
                            string.IsNullOrEmpty(logLine.Timestamp) &&
                            _lines.Count > 0)
                        {
                            logLine.LogType = _lines[_lines.Count - 1].LogType;
                            logLine.Verbosity = _lines[_lines.Count - 1].Verbosity;
                        }

                        _lines.Add(logLine);
                        linesRead++;
                    }

                    _lastByteRead = fileStream.Length;
                }
                
            }
        }        

        return linesRead;
    }

    public void Stop()
    {
        Interlocked.Increment(ref _shouldStop);
    }

    public bool ShouldStop()
    {
        return Interlocked.Read(ref _shouldStop) > 0L;
    }

    /// <summary>
    /// Reads and parses a line of text from the log
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private LogLine CreateLine(int index, string str)
    {
        string pattern = @"^([\[\d\.\-\:\]\s]{30})?(([A-Za-z0-9]+): )?((Log|Fatal|Error|Warning|Display|Verbose|VeryVerbose): )?(.+)";

        Regex reg = new Regex(pattern);
        Match match = reg.Match(str);

        if (match != null && match.Success)
        {
            string timestamp = match.Groups[1].ToString();
            string logType = match.Groups[3].ToString();
            string verbosity = match.Groups[5].ToString();
            string content = match.Groups[6].ToString();
                        
            return new LogLine(index,
                logType,
                timestamp,
                content,
                LogOpt.GetVerbosityFromString(verbosity));
        }
        
        return new LogLine(index, "", "", str, EVerbosity.Log);
    }
        
    /// <summary>
    /// Creates RTF text from the entire file read
    /// @todo: the color table could be greatly optimized
    /// </summary>
    /// <param name="out_hasTextChanged"></param>
    public void CreateText(out bool out_hasTextChanged)
    {        
        Dictionary<int, string> colorCodeHashMap = new Dictionary<int, string>();
        StringBuilder builder = new StringBuilder();
                
        // header and font table
        builder.AppendLine(@"{\rtf1\ansi\deff0 {\fonttbl {\f0 Courier New;}}");

        // color table
        builder.AppendLine(@"{\colortbl;");
        int colorCount = 0;

        // Add the default color to the color table
        builder.AppendFormat("\\red{0}\\green{1}\\blue{2};", _logOptions.DefaultColor.R, _logOptions.DefaultColor.G, _logOptions.DefaultColor.B);
        builder.AppendLine();
        colorCodeHashMap.Add(0, string.Format("\\cf{0} ", ++colorCount));

        // Add the color table
        foreach ( string str in _logOptions.optionsMap.Keys)
        {
            int hash = str.GetHashCode();

            Color col = _logOptions.optionsMap[str].Color;
            if( col.IsEmpty)
            {
                col = _logOptions.DefaultColor;
            }
            builder.AppendFormat("\\red{0}\\green{1}\\blue{2};", col.R, col.G, col.B);
            builder.AppendLine();
            colorCodeHashMap.Add(hash, string.Format("\\cf{0} ", ++colorCount));

        }
        builder.AppendLine(@"}");

        // paragraph start, font type, font size
        builder.AppendLine(@"\pard\f0\fs18");
       
        string maxLineCount = _lines.Count.ToString();
        int padding = maxLineCount.Length;
        int similarLineCount = 0;

        for ( int i = _lineStart, max = _lines.Count; i< max; ++i)
        {
            if (_logOptions.CanShow(_lines[i].LogType, _lines[i].Verbosity))
            {
                if (colorCodeHashMap.ContainsKey(_lines[i].LogHash))
                {
                    builder.Append(colorCodeHashMap[_lines[i].LogHash]);
                }
                else
                {
                    // Use the default color from the theme, not the default from the control
                    builder.Append(colorCodeHashMap[0]);
                    //builder.Append(@"\cf0 ");
                }

                if(_logOptions.UseSimilarLineGrouping)
                {
                    if (i < _lines.Count - 1 && _lines[i].IsSimilar(_lines[i+1]))
                    {
                        similarLineCount++;
                        continue;
                    }
                }
                                
                // @todo: make line numbers optional
                string lineNumberString = _lines[i].LineIndex.ToString().PadLeft(padding, ' ');
                builder.Append(lineNumberString);
                builder.Append("  ");
                if(_logOptions.UseSimilarLineGrouping && similarLineCount > 0)
                {
                    builder.Append("(x" + ++similarLineCount + ") ");
                }
                builder.AppendLine(_lines[i].ToString());
                builder.Append(@"\par");

                similarLineCount = 0;
            }
        }

        builder.Append("}");
                
        string newText = builder.ToString();
        out_hasTextChanged = newText != _text;
        if (out_hasTextChanged)
        {
            _text = builder.ToString();
        }
    }
    
    public string text
    {
        get
        {
            return _text;
        }
    }

    public string[] logTypes
    {
        get
        {
            return _logTypes.ToArray();
        }
    }

    public int maxLinesRead
    {
        get
        {
            return _lines.Count;
        }
    }
}

