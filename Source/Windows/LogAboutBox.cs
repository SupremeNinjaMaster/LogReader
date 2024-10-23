using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogReader.Source.Windows
{
    partial class LogAboutBox : Form, IColorable, ILanguageable
    {
        public LogAboutBox()
        {
            InitializeComponent();
                        
            _linkLabel.LinkClicked += OnLinkClicked;

            RefreshLanguageText();
        }

        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _linkLabel.LinkVisited = true;
            // @todo: move the url to the assembly
            string url = "https://github.com/SupremeNinjaMaster/LogReader";
            System.Diagnostics.Process.Start(url);
        }

        public void SetColors(ColorSet colorSet)
        {
            ForeColor = colorSet.OnBackground;
            BackColor = colorSet.Background;

            labelProductName.ForeColor = colorSet.OnBackground;
            labelVersion.ForeColor = colorSet.OnBackground;
            labelCopyright.ForeColor = colorSet.OnBackground;
            
            okButton.ForeColor = colorSet.OnBackground;
            labelWhereToFind.ForeColor = colorSet.OnBackground;

            textBoxDescription.ForeColor = colorSet.OnSurface;
            textBoxDescription.BackColor = colorSet.Surface;

            _linkLabel.ActiveLinkColor = colorSet.Secondary;
            _linkLabel.LinkColor = colorSet.Primary;
            _linkLabel.VisitedLinkColor = colorSet.OnSurface;

            NativeFunctions.ChangeWindowColor(Handle);
        }

        public void RefreshLanguageText()
        {
            this.Text = Lang.Text("TXT_ABOUT_LOG_READER");
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("{0} {1}", Lang.Text("TXT_VERSION"), AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelWhereToFind.Text = Lang.Text("TXT_NEW_VERSION");
            this.textBoxDescription.Text = Lang.Text("TXT_DESCRIPTION");
            this._linkLabel.Text = Lang.Text("TXT_HERE");
            this._linkLabel.LinkArea = new LinkArea(0, Text.Length);
        }
        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
