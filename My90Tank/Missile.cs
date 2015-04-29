using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    public class Missile:Member
    {
        public static Image img = My90Tank.Properties.Resources.enemymissile;
        public Missile(int x, int y, int life, int speed, int power, DIRECTION direct)
            : base(x, y, life, speed, power, direct)
        {
            this.Width = 15;
            this.Height = 15;
        }

        public override void Draw(Graphics g)
        {
            Move();
            g.DrawImage(img, this.X, this.Y);
        }

        public override  void Move()
        {
            base.ChangePosition();
        }

    }
}
