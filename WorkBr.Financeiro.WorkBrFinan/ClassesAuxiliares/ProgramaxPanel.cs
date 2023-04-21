using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Programax.Easy.View.ClassesAuxiliares
{
    public class ProgramaxPanel : Panel
    {
        public Color BorderColor { get; set; }

        public int Radius { get; set; }

        public ProgramaxPanel()
        {
            BorderColor = Color.FromArgb(200, 201, 203);

            Radius = 15;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush s = new SolidBrush(Color.FromArgb(243, 243, 244));

            //SolidBrush s1 = new SolidBrush(Color.FromArgb(200, 201, 203));

            var path1 = RetanguloArredondado.Create(0, 0, this.Width, this.Height, Radius, RetanguloArredondado.RectangleCorners.All);
            e.Graphics.FillPath(s, path1);

            var path = RetanguloArredondado.Create(0, 0, this.Width - 1, this.Height - 1, Radius, RetanguloArredondado.RectangleCorners.All);
            e.Graphics.DrawPath(new Pen(BorderColor, 1), path);
        }
    }
}
