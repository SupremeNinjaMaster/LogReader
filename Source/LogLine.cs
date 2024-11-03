using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public struct LogLine
{
    public int LineIndex;
    public string LogType;
    public string Timestamp;
    public string Content;
    public int LogHash;
    public EVerbosity Verbosity;

    public LogLine(int index, string logType, string timestamp, string content, EVerbosity verbosity)
    {
        LineIndex = index;
        LogType = logType;
        Timestamp = timestamp;
        Content = content;
        LogHash = logType.GetHashCode();
        Verbosity = verbosity;
    }

    public void Append(string extraContent)
    {
        Content += Environment.NewLine + extraContent;
    }

    public override string ToString()
    {
        // ignore timestamp for now
        StringBuilder str = new StringBuilder();

        if (!string.IsNullOrEmpty(LogType))
        {
            str.Append(LogType);
            str.Append(": ");

            if (Verbosity != EVerbosity.Log)
            {
                str.Append(Verbosity);
                str.Append(": ");
            }
        }

        str.Append(Content).Replace("\\", "\\\\");
        return str.ToString();
    }

    public bool IsSimilar( LogLine other)
    {
        return LogType == other.LogType &&
            Content == other.Content &&
            Verbosity == other.Verbosity;
    }

    public static readonly LogLine INVALID = new LogLine(-1, "", "", "", EVerbosity.Log);
}