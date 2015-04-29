using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using My90Tank.Properties;

namespace My90Tank
{
    class Grass : Module
    {
        public static Image img = Resources.grass;

        public Grass(int x, int y)
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
