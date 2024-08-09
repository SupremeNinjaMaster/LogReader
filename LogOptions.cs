using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.IO;

public enum EVerbosity
{
    NoLogging = 0,

    /** Always prints a fatal error to console (and log file) and crashes (even if logging is disabled) */
    Fatal,

    /** 
     * Prints an error to console (and log file). 
     * Commandlets and the editor collect and report errors. Error messages result in commandlet failure.
     */
    Error,

    /** 
     * Prints a warning to console (and log file).
     * Commandlets and the editor collect and report warnings. Warnings can be treated as an error.
     */
    Warning,

    /** Prints a message to console (and log file) */
    Display,

    /** Prints a message to a log file (does not print to console) */
    Log,

    /** 
     * Prints a verbose message to a log file (if Verbose logging is enabled for the given category, 
     * usually used for detailed logging) 
     */
    Verbose,

    /** 
     * Prints a verbose message to a log file (if VeryVerbose logging is enabled, 
     * usually used for detailed logging that would otherwise spam output) 
     */
    VeryVerbose,

    // Log masks and special Enum values

    //All = VeryVerbose,
    //NumVerbosity,
    //VerbosityMask = 0xf,
    //SetColor = 0x40, // not actually a verbosity, used to set the color of an output device 
    //BreakOnLog = 0x80

   
}

public struct LogOpt
{
    public LogOpt( Color col, string verbosityString)
    {        
        Color = col;
        Verbosity = GetVerbosityFromString(verbosityString);
    }

    public LogOpt( Color col, EVerbosity verbosity)
    {    
        Color = col;
        Verbosity = verbosity;
    }

    public static EVerbosity[] GetVerbosityArray()
    {
        return Enum.GetValues(typeof(EVerbosity)).Cast<EVerbosity>().ToArray();
    }

    public static string[] GetVerbosityStringArray()
    {
        List<string> vals = new List<string>();
        foreach (EVerbosity verbVal in GetVerbosityArray())
        {
            vals.Add(verbVal.ToString());
        }
        return vals.ToArray();
    }

    public static EVerbosity GetVerbosityFromString(string str)
    {
        EVerbosity result;
        if( Enum.TryParse(str, out result) )
        {
            return result;
        }

        // Log is the default used by unreal
        return EVerbosity.Log;
    }
        
    public Color Color;
    public EVerbosity Verbosity;    
}

public class LogOptions : ICloneable
{
    Dictionary<string, LogOpt> _logTypes = new Dictionary<string, LogOpt>();
    
    public LogOptions()
    {

    }

    public void AddLogType(string logType, Color color, EVerbosity verbosity)
    {
        if (!string.IsNullOrEmpty(logType) && !_logTypes.ContainsKey(logType))
        {
            _logTypes.Add(logType, new LogOpt(color, verbosity));
        }
    }

    public void ChangeLogType(string logType, Color color, EVerbosity verbosity)
    {
        if (!string.IsNullOrEmpty(logType) && _logTypes.ContainsKey(logType))
        {            
            _logTypes[logType] = new LogOpt(color, verbosity);
        }
    }

    public Dictionary<string, LogOpt> optionsMap
    {
        get
        {
            return _logTypes;
        }
    }

    public void Load(string path)
    {
        if( File.Exists(path))
        {
            _logTypes = new Dictionary<string, LogOpt>();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach( XmlNode node in doc.SelectNodes("log_options/log_type"))
            {
                string logTypeName = node.Attributes["name"].Value;
                string logColor = node.Attributes["color"].Value;
                EVerbosity verbosity = LogOpt.GetVerbosityFromString(node.Attributes["verbosity"].Value);
                
                Color col = ColorTranslator.FromHtml(logColor);
                AddLogType(logTypeName, col, verbosity);
            }
        }
    }

    public void Save(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
        XmlNode root = doc.AppendChild(doc.CreateElement("log_options"));

        string[] logTypes = _logTypes.Keys.ToArray();
        Array.Sort(logTypes);

        foreach (string logType in logTypes)
        {
            XmlNode node = root.AppendChild(doc.CreateElement("log_type"));
            node.Attributes.Append(doc.CreateAttribute("name")).Value = logType;
            node.Attributes.Append(doc.CreateAttribute("color")).Value = ColorTranslator.ToHtml(_logTypes[logType].Color);
            node.Attributes.Append(doc.CreateAttribute("verbosity")).Value = _logTypes[logType].Verbosity.ToString();
        }
        
        //XmlNode displayMessagesNode = doc.DocumentElement.AppendChild(doc.CreateElement("always_Show_Display_Messages"));
        //displayMessagesNode.Attributes.Append(doc.CreateAttribute("flag")).Value = m_alwaysShowDisplay.ToString();
        
        doc.Save(path);
    }

    public bool CanShow(string logType, EVerbosity verbosity)
    {
        // @todo use hashes 

        if(_logTypes.ContainsKey(logType))
        {
            return verbosity <= _logTypes[logType].Verbosity;
        }

        // no verbosity 
        return true;
    }

    public object Clone()
    {
        LogOptions options = new LogOptions();    
        foreach( var pair in this._logTypes)
        {
            options._logTypes.Add(pair.Key, pair.Value);

        }
        return options;
    }

    
}