using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class CustomToolStripProfessionalRenderer : ToolStripProfessionalRenderer
{
    public CustomToolStripProfessionalRenderer()
    {
    }

    public CustomToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
        : base(professionalColorTable)
    {
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        CustomMenuColorTable customColorTable = (CustomMenuColorTable)ColorTable;
        e.Item.ForeColor = customColorTable.CurrentColorSet.OnBackground;

        base.OnRenderItemText(e);
    }
}

