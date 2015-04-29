using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My90Tank.Properties;
using System.Drawing;

namespace My90Tank
{
    public class Enemy:Member
    {
        private  const int BORNTIME = 24;
        private int bornTime = 0;
        public static Image[] bornImages = new Image[] { Resources.born1, Resources.born2, Resources.born3, Resources.born4 };

        private static TankType[] tankType = new TankType[] { new TankType(6, 2, 2), new TankType(12, 1, 2), new TankType(4,6, 2) };

        private static Image[] imgEnemy1 = new Image[]  //加载一号敌人图片
        {
          Resources.enemy1U,
          Resources.enemy1D,
          Resources.enemy1L,
          Resources.enemy1R
        };

        private static Image[] imgEnemy2 = new Image[]  //加载二号敌人图片
        {
          Resources.enemy2U,
          Resources.enemy2D,
          Resources.enemy2L,
          Resources.enemy2R
        };

        private static Image[] imgEnemy3 = new Image[]  //加载三号敌人图片
        {
          Resources.enemy3U,
          Resources.enemy3D,
          Resources.enemy3L,
          Resources.enemy3R
        };

        private EnemyType type;  //定义敌人的类型

        public EnemyType Type   //设置属性
        {
            get { return type; }
            set { type = value; }
        }


        public static int GetLife(EnemyType type)
        {
            return tankType[(int)type].life;
        }
        public static int GetSpeed(EnemyType type)
        {
            return tankType[(int)type].speed;
        }
        public static int GetPower(EnemyType type)
        {
            return tankType[(int)type].power;
        }

        public Enemy(EnemyType type, int x, int y, DIRECTION direct)
            : base(x, y, GetLife(type), GetSpeed(type), GetPower(type), direct)
        {
            this.type = type;
            this.Width = 60;
            this.Height = 60;
        }

        public override void Move()
        {
            if (IsBlocked)
            {
                ChangeDirection();
            }
            else
            {
                base.ChangePosition();
            }
        }
        public void ChangeDirection()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int t = rand.Next(4);
            this.direct = (DIRECTION)t;
        }

        public override void Draw(Graphics g)
        {
            //如果是出生状态，则需要出生时的闪烁图像序列；
            if (bornTime < BORNTIME)
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
                return;
            }

            Image[] images = imgEnemy2;
            //if (type == EnemyType.FAST)
            //{
            //    images = imgEnemy2;
            //}
            if (type == EnemyType.HEAVY)
            {
                images = imgEnemy3;
            }
            else if (type == EnemyType.NORMAL)
            {
                images = imgEnemy1;
            }
            Move();
            Fire();
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

        public void Fire()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            int a  = r.Next(50);
            if (a < 48)
                return;
            int xx = this.X, yy = this.Y;
            switch (this.direct)
            {
                case DIRECTION.UP:
                    yy -= this.speed;
                    xx += this.Width / 2;
                    break;
                case DIRECTION.DOWN:
                    yy += this.speed + this.Height;
                    xx += this.Width / 2;
                    break;
                case DIRECTION.LEFT:
                    xx -= this.speed;
                    yy += this.Width / 2;
                    break;
                case DIRECTION.RIGHT:
                    xx += this.speed + this.Width;
                    yy += this.Width / 2;
                    break;
                default:
                    break;
            }
            Scene.Instance.AddElement(new Missile(xx, yy, 1, 25, 1, this.direct));
        }


    }

    public struct TankType
    {
        public int speed;
        public int life;
        public int power;
        public TankType(int speed, int life, int power)
        {
            this.speed = speed;
            this.life = life;
            this.power = power;
        }
    }

    public enum EnemyType
    {
        NORMAL=1,FAST=2,HEAVY=3
    }
}
