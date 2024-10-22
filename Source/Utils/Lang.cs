using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


public class Lang
{
    private static Lang _instance = null;
    
    public const string LANGUAGE_SPANISH = "es";
    public const string LANGUAGE_ENGLISH = "en";
    public const string LANGUAGE_ITAILAN = "it";
    public const string LANGUAGE_PORTUGUESE = "pt";
    public const string LANGUAGE_POLISH = "pl";
    public const string LANGUAGE_FRENCH = "fr";

    private const string WRONG_TEXT = "Wrong Text";

    private string _currentLanguage = "";

    private Dictionary<string, int> _indices = new Dictionary<string, int>();
    private Dictionary<string, List<string>> _values = new Dictionary<string, List<string>>();
    
    public static string Text(string key)
    {
        Lang lang = Instance;

        int i;
        List<string> column;
        if (lang._indices.TryGetValue(key, out i) &&
            lang._values.TryGetValue(  lang._currentLanguage, out column))
        {
            return column[i];
        }

        return "";
    }

    public void LoadLanguages()
    {
        string fileName = "LogReader.Languages.csv";
        
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string[] columnIndices = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split( new char[] { ',', '\"' }, StringSplitOptions.RemoveEmptyEntries);

                    if (columnIndices == null)
                    {
                        columnIndices = tokens;

                        for (int i = 0; i < tokens.Length; ++i)
                        {
                            _values.Add(tokens[i], new List<string>());
                        }
                    }
                    else
                    {                        
                        string key = tokens[0];
                        _indices.Add(key, _indices.Count);

                        for (int i = 1; i < tokens.Length; ++i)
                        {
                            string language = columnIndices[i];
                            List<string> column = _values[language];
                            column.Add(tokens[i]);
                        }
                    }                    
                }
            }
        }
    }

    public static Lang Instance
    {
        get
        {
            if(_instance == null )
            {
                _instance = new Lang();
            }
            return _instance;
        }
    }

    public string CurrentLanguage
    {
        get
        {
            return _currentLanguage;
        }
        set
        {
            _currentLanguage = value;
        }
    }
}

