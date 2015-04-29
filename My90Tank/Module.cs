using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    public class Module:Element
    {
        public Module(int x, int y,Image img):base(x,y)
        {
            this.Width = img.Width;
            this.Height = img.Height;
        }

        public override void Draw(Graphics g)
        {
        }

        public Rectangle GetRect()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
    }
}
