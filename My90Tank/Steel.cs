using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    public class Steel:Module
    {
        public static Image img = My90Tank.Properties.Resources.steels;
        public Steel(int x, int y)
            : base(x, y, img)
        {
            this.Width = 60;
            this.Height = 60;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(img, this.X, this.Y);
        }
    }
}
