using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace My90Tank
{
    public class Scene
    {
        public static int MAX_ENEMY_NUMBER = 20;
        private P1Player play1 = new P1Player(240, 540, 3, 8, 1, DIRECTION.UP);
        private P2Player play2 = new P2Player(480, 540, 3, 6, 1, DIRECTION.UP);
        private Symbol symbol = new Symbol(360, 560);

        private List<Water> waterList = new List<Water>();
        private List<Wall> wallList = new List<Wall>();
        private List<Grass> grassList = new List<Grass>();
        private List<Steel> steelList = new List<Steel>();
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Missile> missileList = new List<Missile>();



        public P1Player P1Play
        {
            get
            {
                return play1;
            }
            set
            {
                play1 = value;
            }
        }

       

        private static Scene instance = null;

        public static Scene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Scene();                    
                }
                return instance;
            }
        }

        private Scene()
        {
            this.CreateAnEnemyAndAdd();
        }

        public void CreateAnEnemyAndAdd()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int t = random.Next(3);
            EnemyType type = (EnemyType)t;
            int p = random.Next(3);
            int birthX = 0;
            int birthY = 0;
            if (p == 0)
            {
                birthX = 0;
            }
            else if (p == 1)
            {
                birthX = 60 * 6;
            }
            else
                birthX = 12 * 60;

            Enemy enemy = new Enemy(type, birthX, birthY, DIRECTION.DOWN);
            this.enemyList.Add(enemy);
        }

        public void AddElement(Element ele)
        {
            if (ele is P1Player)
            {
                this.play1 = ele as P1Player;
            }
            else if( ele is Wall)
            {
                this.wallList.Add(ele as Wall);
            }
            else if (ele is Grass)
            {
                this.grassList.Add(ele as Grass);
            }
            else if( ele is Steel)
            {
                this.steelList.Add(ele as Steel);
            }
            else if (ele is Water)
            {
                this.waterList.Add(ele as Water);
            }
            else if ( ele is Missile)
            {
                this.missileList.Add(ele as Missile);
            }
            

        }

        public void RemoveElement(Element e)
        {
            if (e is Wall)
            {
                this.wallList.Remove( e as Wall);
            }
            else if (e is Enemy)
            {
                //this..Remove(e as Enemy);
            }
            else if (e is Steel)
            {
                this.steelList.Remove(e as Steel);
            }          

        }

        public void Draw(Graphics g)
        {
            this.P1Play.Draw(g);
            
            foreach (Grass grass in grassList)
                grass.Draw(g);
            foreach (Wall wall in wallList)
                wall.Draw(g);
            foreach (Water water in waterList)
                water.Draw(g);
            foreach (Steel steel in steelList)
                steel.Draw(g);
            foreach (Missile missile in missileList)
                missile.Draw(g);
            foreach (Enemy enemy in enemyList)
                enemy.Draw(g);
            if (this.symbol != null)
                symbol.Draw(g);



        }

        public void CheckState()
        {
            List<Missile> deadMissile = new List<Missile>();
            CheckBlock(P1Play);
            
            foreach (Enemy enemy in enemyList)            
                CheckBlock(enemy);
            foreach (Missile missile in missileList)
                deadMissile = CheckDeadAndRemove(missile);

            for (int i = 0; i < deadMissile.Count; i++)
            {
                missileList.Remove(deadMissile[i]);
            }


        }

        //确定某个坦克是否处于堵塞状态（可以是敌人或者自己的坦克）
        public void CheckBlock(Member m)
        {

            int i = 0;
            Rectangle rect = m.GetRectangle();
            for (i = 0; i < wallList.Count; i++)
            {
                if (rect.IntersectsWith(wallList[i].GetRectangle()))
                {
                    m.IsBlocked = true;
                    return;
                }
            }            
            
            for (i = 0; i < waterList.Count; i++)
            {
                if (rect.IntersectsWith(waterList[i].GetRectangle()))
                {
                    m.IsBlocked = true;
                    return;
                }
            }
            for (i = 0; i < steelList.Count; i++)
            {
                if (rect.IntersectsWith(steelList[i].GetRectangle()))
                {
                    m.IsBlocked = true;
                    return;
                }
            }
            if (m is P1Player)
            {
                for (i = 0; i < enemyList.Count; i++)
                {
                    if (rect.IntersectsWith(enemyList[i].GetRectangle()))
                    {
                        m.IsBlocked = true;
                        return;
                    }
                }
            }
            
            if (m is Enemy)
            {
                if (rect.IntersectsWith(P1Play.GetRectangle()))
                {
                    m.IsBlocked = true;
                    return;
                }
            }
            
            if (rect.Right >= ParamSetting.Map_Width || rect.Left < 0 || rect.Bottom > ParamSetting.Map_Height || rect.Top < 0)
            {
                m.IsBlocked = true;
                return;
            }

            m.IsBlocked = false;
        }

        //用missile对象确定场景中目标的生命值小于等于0，并删除掉该对象
        public List<Missile> CheckDeadAndRemove(Missile m)
        {
            List<Missile> deadMissiles = new List<Missile>();
            int i = 0;
            for (i = 0; i < wallList.Count; i++)
            {
                if (m.GetRectangle().IntersectsWith(wallList[i].GetRectangle()))
                {
                    deadMissiles.Add(m);
                    wallList.RemoveAt(i);
                    return deadMissiles;
                }
            }
            
            for (i = 0; i < steelList.Count; i++)
            {
                if (m.GetRectangle().IntersectsWith(steelList[i].GetRectangle()))
                {
                    deadMissiles.Add(m);
                    if (m.power > 2)
                    {
                        steelList.RemoveAt(i);
                        i--;
                    }
                    return deadMissiles;
                }
            }
            
            
            if (m.GetRectangle().IntersectsWith(P1Play.GetRectangle()))
            {
                deadMissiles.Add(m);
                P1Play.life = 0;
                //MessageBox.Show("Game Over!");
                return deadMissiles;
            }


            for (i = 0; i < enemyList.Count; i++)
            {
                if (m.GetRectangle().IntersectsWith(enemyList[i].GetRectangle()))
                {
                    enemyList.RemoveAt(i);
                    i--;
                    deadMissiles.Add(m);
                    return deadMissiles;
                }
            }

            if (m.X == 0 || m.Y == 0 || m.X >= ParamSetting.Map_Width || m.Y >= ParamSetting.Map_Height)
                deadMissiles.Add(m);

            if (enemyList.Count == 0)
                MessageBox.Show("Yow Win!");
            
            //m.IsBlocked = false;
            return deadMissiles;
        }
    }
}
