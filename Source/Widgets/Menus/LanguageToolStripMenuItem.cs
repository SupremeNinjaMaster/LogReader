using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class LanguageToolStripMenuItem : ToolStripMenuItem, ILanguageable
{
    public readonly string Language;
    private string _textKey;

    public LanguageToolStripMenuItem(string textKey, EventHandler onClick, string language)
    : base(Lang.Text(textKey), null, onClick)
    {
        Language = language;
        _textKey = textKey;
    }

    public void RefreshLanguageText()
    {
        Text = Lang.Text(_textKey);
    }
}

