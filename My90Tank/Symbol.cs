using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    public class Symbol:Module
    {
        private static Image img = My90Tank.Properties.Resources.symbol;
        public Symbol(int x, int y)
            : base(x, y,img)
        {
            this.Width = 40;
            this.Height = 40;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(img, X, Y);
        }

    }
}
