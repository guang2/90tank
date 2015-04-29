using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace My90Tank
{
    /// <summary>
    /// 用于描述具有运动物体的基础类型，其子类包括：坦克，子弹等；
    /// 这类对象都具有生命值、运动方向、速度；
    /// 具有运动、显示等功能；
    /// </summary>
    public abstract class Member : Element
    {
        public DIRECTION direct;
        public int life;
        public int speed;
        public int power;

        public int bornTime;
        private bool isBlocked = false;

        public bool IsBlocked
        {
            set {               
                isBlocked = value; }
            get { return isBlocked; }
        }

        

        public Member(int x, int y, int life, int speed,int power,DIRECTION direction)
            : base(x, y)
        {
            this.X = x;
            this.Y = y;
            this.direct = direction;
            this.speed = speed;
            this.power = power;
            this.life = life;
        }

        /// <summary>
        /// 子类需重载；ChangePosition对对象进行移动，
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// 根据当前对象的速度和方向，确定下一个位置；
        /// </summary>
        public virtual void ChangePosition()
        {
            if (!isBlocked)
            {
                switch (direct)
                {
                    case DIRECTION.UP:
                        this.Y -= speed; break;
                    case DIRECTION.DOWN:
                        this.Y += speed; break;
                    case DIRECTION.LEFT:
                        this.X -= speed; break;
                    case DIRECTION.RIGHT:
                        this.X += speed; break;
                    default:
                        break;
                }
            }
        }

        public override System.Drawing.Rectangle GetRectangle()
        {
            Rectangle rect;
            switch (direct)
            {
                case DIRECTION.UP:
                    rect = new Rectangle(this.X, this.Y - speed,Width, Height); 
                    break;
                case DIRECTION.DOWN:
                    rect = new Rectangle(this.X, this.Y + speed, Width, Height); 
                    break;
                case DIRECTION.LEFT:
                    rect = new Rectangle(this.X-speed, this.Y , Width, Height);
                    break;
                case DIRECTION.RIGHT:
                    rect = new Rectangle(this.X+speed, this.Y , Width, Height); 
                    break;
                default:
                    rect = new Rectangle(this.X , this.Y, Width, Height);
                    break;
            }
            return rect;
        }


    }
}
