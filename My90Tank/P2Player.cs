using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using My90Tank.Properties;
using System.Windows.Forms;

namespace My90Tank
{
    public class P2Player:Member
    {
        public static Image[] images = new Image[] { Resources.p2tankU, Resources.p2tankD, Resources.p2tankL, Resources.p2tankR };
        public static Image[] bornImages = new Image[] {Resources.born1,Resources.born2,Resources.born3,Resources.born4};
        private bool isMove = false;
        public const int myTankBornTime = 16;
        private int lifeNum = 3;

        public P2Player(int x,int y, int life, int speed, int power,DIRECTION direct)
            :base(x,y,life,speed,power,direct)
        {
            this.Width = 60;
            this.Height = 60;
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.J:
                    if (this.direct != DIRECTION.LEFT)
                        IsBlocked = false;
                    this.direct = DIRECTION.LEFT;
                    isMove = true;
                    break;
                case Keys.I:
                    if (this.direct != DIRECTION.UP)
                        IsBlocked = false;
                    this.direct = DIRECTION.UP;
                    isMove = true;
                    break;
                case Keys.K:
                    if (this.direct != DIRECTION.DOWN)
                        IsBlocked = false;
                    this.direct = DIRECTION.DOWN;
                    isMove = true;
                    break;
                case Keys.L:
                    if (this.direct != DIRECTION.RIGHT)
                        IsBlocked = false;
                    this.direct = DIRECTION.RIGHT;
                    isMove = true;
                    break;
                default:
                    break;
                
            }
            //按下就开始根据按下的键移动
            Move();
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.J:
                    if(this.direct == DIRECTION.LEFT)
                        isMove = false; 
                    break;
                case Keys.I:
                    if(this.direct == DIRECTION.UP)
                        isMove = false; 
                    break;
                case Keys.K:
                    if(this.direct == DIRECTION.DOWN)
                        isMove = false; 
                        break;
                case Keys.L:
                    if(this.direct == DIRECTION.RIGHT)
                        isMove = false; 
                        break;
                case Keys.Enter:
                        Fire();break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 通过增加一个missile对象到singleton中
        /// </summary>
        public void Fire()
        {
            int xx = this.X, yy = this.Y;
            switch (this.direct)
            {
                case DIRECTION.UP:
                    yy -= this.speed;
                    xx += this.Width / 2;
                    break;
                case DIRECTION.DOWN:
                    yy += this.speed+this.Height;
                    xx += this.Width / 2;
                    break;
                case DIRECTION.LEFT:
                    xx -= this.speed;
                    yy += this.Width / 2;
                    break;
                case DIRECTION.RIGHT:
                    xx += this.speed+this.Width;
                    yy += this.Width/2;
                    break;
                default:
                    break;
            }
            Scene.Instance.AddElement(new Missile(xx,yy,1,25,1,this.direct));
        }

        public override void Move()
        {
            if (!isMove || IsBlocked)
            {
                return;
            }
            else
            {
                base.ChangePosition();
            }

        }

        public bool IsBorn()
        {
            if (bornTime < P1Player.myTankBornTime)
                return true;
            else
                return false;
        }

        public override void Draw(Graphics g)
        {
            //如果是出生状态，则需要出生时的闪烁图像序列；
            if (IsBorn())
            {
                switch (bornTime % 8)
                {
                    case 0:
                    case 1:
                        g.DrawImage(bornImages[0], this.X, this.Y);
                        break;
                    case 2:
                    case 3:
                        g.DrawImage(bornImages[1], this.X, this.Y);
                        break;
                    case 4:
                    case 5:
                        g.DrawImage(bornImages[2], this.X, this.Y);
                        break;
                    case 6:
                    case 7:
                        g.DrawImage(bornImages[3], this.X, this.Y);
                        break;
                }
                bornTime++;              
            }
            else //已经出生，就直接根据方向和位置话；
            {
                switch (direct)
                {
                    case DIRECTION.UP:
                        g.DrawImage(images[0], this.X, this.Y);
                        break;
                    case DIRECTION.DOWN:
                        g.DrawImage(images[1], this.X, this.Y);
                        break;
                    case DIRECTION.LEFT:
                        g.DrawImage(images[2], this.X, this.Y);
                        break;
                    case DIRECTION.RIGHT:
                        g.DrawImage(images[3], this.X, this.Y);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
