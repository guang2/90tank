using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    /// <summary>
    /// 所有地图上出现对象的基类；其主要包括所在的位置、大小；以及一个需要重写的Draw方法；
    /// </summary>
    public abstract class Element
    {
        private int x;
        private int y;
        private int width;
        private int height;
        public int X { 
            get{ return x;}
            set{
                if (value >= 0)
                    x = value;
                else
                    x= 0;
            } 
        }

        public int Y
        {
            get { return y; }
            set {
                if (value >= 0)
                    y = value;
                else
                    y = 0;
            }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Element(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public abstract void Draw(Graphics g);

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }

    public enum DIRECTION
    {
        UP,DOWN,LEFT,RIGHT
    }
}
