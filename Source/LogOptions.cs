using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Xml.Linq;


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
    public const int MAX_RECENT_FILES = 10;
    public const string THEME_NAME_DARK = "dark";
    public const string THEME_NAME_LIGHT = "light";
    public const string THEME_NAME_CUSTOM = "custom";

    /// <summary>
    /// All the log types with their colors and their visibilities
    /// </summary>
    Dictionary<string, LogOpt> _logTypes = new Dictionary<string, LogOpt>();

    /// <summary>
    /// A list of the last 10 files we opened
    /// </summary>
    List<string> _recentFiles = new List<string>();

    /// <summary>
    /// The default text color we use in the textbox
    /// </summary>
    Color _defaultColor;

    /// <summary>
    /// The chosen theme to use when loading the app
    /// </summary>
    string _chosenColorThemeName = THEME_NAME_LIGHT;

    /// <summary>
    /// The chosen language
    /// </summary>
    string _chosenLanguage = Lang.LANGUAGE_ENGLISH;

    /// <summary>
    /// All the custom themes there are
    /// </summary>
    Dictionary<string, ColorSet> _customColorThemes = new Dictionary<string, ColorSet>();

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

    public void AddThemeColorSet(string themeName, ColorSet colorSet)
    {
        if (themeName != THEME_NAME_DARK &&
            themeName != THEME_NAME_LIGHT )
        {
            _customColorThemes.Remove(themeName);
            _customColorThemes.Add(themeName, colorSet);            
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

            foreach( XmlNode node in doc.SelectNodes("log_options/log_types/log_type"))
            {
                string logTypeName = node.Attributes["name"].Value;
                string logColor = node.Attributes["color"].Value;
                EVerbosity verbosity = LogOpt.GetVerbosityFromString(node.Attributes["verbosity"].Value);

                Color col = ColorTranslator.FromHtml(logColor);
                AddLogType(logTypeName, col, verbosity);
            }

            foreach(XmlNode customThemeNode in doc.SelectNodes("log_options/custom_themes/theme"))
            {
                string themeName = customThemeNode.Attributes["name"].Value;
                _customColorThemes.Remove(themeName);

                ColorSet set = new ColorSet();

                foreach( XmlNode colorNode in customThemeNode.SelectNodes("color"))
                {
                    string colorName = colorNode.Attributes["name"].Value;
                    Color col = ColorTranslator.FromHtml(colorNode.Attributes["color"].Value);

                    switch(colorName)
                    {
                        case "background":      set.Background = col; break;
                        case "surface":         set.Surface = col; break;
                        case "primary":         set.Primary = col; break;
                        case "secondary":       set.Secondary = col; break;
                        case "on_background":   set.OnBackground = col; break;
                        case "on_surface":      set.OnSurface = col; break;
                        case "on_primary":      set.OnPrimary = col; break;
                        case "on_secondary":    set.OnSecondary = col; break;
                    }                   
                }

                _customColorThemes.Add(themeName, set);
            }

            XmlNode chosenThemeNode = doc.SelectSingleNode("log_options/chosen_theme");
            if (chosenThemeNode != null)
            {
                ChosenColorThemeName = chosenThemeNode.InnerText;
            }

            XmlNode chosenLanguageNode = doc.SelectSingleNode("log_options/chosen_language");
            if(chosenLanguageNode != null)
            {
                Language = chosenLanguageNode.InnerText;
            }

            foreach ( XmlNode node in doc.SelectNodes("log_options/recent_files/file"))
            {
                string recentFilePath = node.Attributes["path"].Value;
                if (!_recentFiles.Contains(recentFilePath))
                {
                    _recentFiles.Add(recentFilePath);
                }
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

        XmlNode lastOpenedFileNode = root.AppendChild(doc.CreateElement("recent_files"));
        foreach( string recentFilePath in _recentFiles)
        {
            XmlNode node = lastOpenedFileNode.AppendChild(doc.CreateElement("file"));
            node.Attributes.Append(doc.CreateAttribute("path")).Value = recentFilePath;
        }

        XmlNode chosenThemeNode = root.AppendChild(doc.CreateElement("chosen_theme"));
        chosenThemeNode.InnerText = _chosenColorThemeName;

        XmlNode chosenLanguageNode = root.AppendChild(doc.CreateElement("chosen_language"));
        chosenLanguageNode.InnerText = _chosenLanguage;

        XmlNode themesNode = root.AppendChild(doc.CreateElement("custom_themes"));
        foreach (string themeName in _customColorThemes.Keys)
        {
            XmlNode customThemeNode = themesNode.AppendChild(doc.CreateElement("theme"));
            customThemeNode.Attributes.Append(doc.CreateAttribute("name")).Value = themeName;
            
            Func<XmlDocument, string, Color, XmlNode> colorAdd = (d, name, col) =>
            {
                XmlNode color = d.CreateElement("color");
                color.Attributes.Append(d.CreateAttribute("name")).Value = name;
                color.Attributes.Append(d.CreateAttribute("color")).Value = ColorTranslator.ToHtml(col);
                return color;
            };

            ColorSet set = _customColorThemes[themeName];

            customThemeNode.AppendChild(colorAdd(doc, "background", set.Background));
            customThemeNode.AppendChild(colorAdd(doc, "surface", set.Surface));
            customThemeNode.AppendChild(colorAdd(doc, "primary", set.Primary));
            customThemeNode.AppendChild(colorAdd(doc, "secondary", set.Secondary));
            customThemeNode.AppendChild(colorAdd(doc, "on_background", set.OnBackground));
            customThemeNode.AppendChild(colorAdd(doc, "on_surface", set.OnSurface));
            customThemeNode.AppendChild(colorAdd(doc, "on_primary", set.OnPrimary));
            customThemeNode.AppendChild(colorAdd(doc, "on_secondary", set.OnSecondary));
        }

        XmlNode logTypesNode = root.AppendChild(doc.CreateElement("log_types"));
        foreach (string logType in logTypes)
        {
            XmlNode node = logTypesNode.AppendChild(doc.CreateElement("log_type"));
            node.Attributes.Append(doc.CreateAttribute("name")).Value = logType;
            node.Attributes.Append(doc.CreateAttribute("color")).Value = ColorTranslator.ToHtml(_logTypes[logType].Color);
            node.Attributes.Append(doc.CreateAttribute("verbosity")).Value = _logTypes[logType].Verbosity.ToString();
        }
        
        
        
        doc.Save(path);
    }

    public void AddRecentFilePath(string path)
    {
        _recentFiles.Remove(path);
        _recentFiles.Add(path);

        while (_recentFiles.Count > MAX_RECENT_FILES)
        {
            _recentFiles.RemoveAt(0);
        }
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


    public bool TryGetColorThemeByName(string name, out ColorSet set)
    {
        return _customColorThemes.TryGetValue(name, out set);
    }
        
    public object Clone()
    {
        LogOptions options = new LogOptions();    
        foreach( var pair in this._logTypes)
        {
            options._logTypes.Add(pair.Key, pair.Value);
        }

        options._recentFiles = new List<string>(_recentFiles);
        options._defaultColor = _defaultColor;
        options._chosenColorThemeName = _chosenColorThemeName;

        foreach( var pair in this._customColorThemes)
        {
            options._customColorThemes.Add(pair.Key, pair.Value);
        }
        
        return options;
    }

    public string[] RecentFiles
    {
        get
        {
            return _recentFiles.ToArray();
        }
    }

    public string MostRecentFile
    {
        get
        {
            if( _recentFiles.Count > 0 )
            {
                return _recentFiles.Last();
            }

            return "";
        }
    }

    public Color DefaultColor
    {
        get
        {
            return _defaultColor;
        }
        set
        {
            _defaultColor = value;
        }
    }

    public string ChosenColorThemeName
    {
        get
        {
            return _chosenColorThemeName;
        }
        set
        {
            if (_customColorThemes.ContainsKey(value) ||
                value == THEME_NAME_DARK ||
                value == THEME_NAME_LIGHT)
            {
                _chosenColorThemeName = value;
            }
        }
    }

    public ColorSet CurrentColorTheme
    {
        get
        {
            if(_customColorThemes.ContainsKey(ChosenColorThemeName))
            {
                return _customColorThemes[ChosenColorThemeName];
            }
            else if( ChosenColorThemeName == THEME_NAME_DARK)
            {
                return ColorSet.MainDarkMode;
            }
            else
            {
                return ColorSet.MainLightMode;
            }
        }
    }

    public string Language
    {
        get
        {
            return _chosenLanguage;
        }
        set
        {
            _chosenLanguage = value;
        }
    }
    
}