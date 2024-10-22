using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class FilePathToolStripMenuItem : ToolStripMenuItem
{
    private readonly string _fullPath;

    public FilePathToolStripMenuItem(string fullPath, EventHandler onClick)
    : base(Path.GetFileName(fullPath), null, onClick)
    {
        ToolTipText = fullPath;
        _fullPath = fullPath;
    }

    public string FullPath
    {
        get
        {
            return _fullPath;
        }
    }
}

