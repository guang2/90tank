using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My90Tank
{
    public partial class Form1 : Form
    {
        private Bitmap canvas = new Bitmap(ParamSetting.Map_Width, ParamSetting.Map_Height);
        private Graphics gra ;
        private int num = 0;
        public static int tank_type=0;
        public Form1(int type)
        {            
            InitializeComponent();
            tank_type = type;
            this.Width = ParamSetting.Map_Width+6;//两边边框的宽度
            this.Height = ParamSetting.Map_Height+24;//24是窗体中标题头的高度加上边框的高度
            InitGame();
            this.timer1.Start();
            gra = Graphics.FromImage(canvas);
            this.pictureBox1.Image = canvas;
     
        }
        public static int Tank_Type {
            set {
                tank_type = value;
            }
            get {
                return tank_type;
            }
        }
        
        private void InitGame()   //初始化游戏事件
        {
            //Scene.Instance.AddElement(new P1Player(240, 540, 5, 10,1, DIRECTION.UP));
            InitMap();           //调用初始化地图事件

        }

        private void InitMap() //初始化地图
        {
            //  Start();  //
            #region 在panel中初始化地图

            Scene.Instance.AddElement(new Wall(60, 60));
            Scene.Instance.AddElement(new Wall(420, 60));
            Scene.Instance.AddElement(new Wall(540, 60));
            Scene.Instance.AddElement(new Wall(660, 60));
            Scene.Instance.AddElement(new Wall(360, 120));
            Scene.Instance.AddElement(new Wall(420, 120));
            Scene.Instance.AddElement(new Wall(660, 120));
            Scene.Instance.AddElement(new Wall(180, 180));
            Scene.Instance.AddElement(new Wall(180, 240));
            Scene.Instance.AddElement(new Wall(300, 300));
            Scene.Instance.AddElement(new Wall(600, 300));
            Scene.Instance.AddElement(new Wall(720, 300));
            Scene.Instance.AddElement(new Wall(60, 360));
            Scene.Instance.AddElement(new Wall(120, 360));
            Scene.Instance.AddElement(new Wall(180, 360));
            Scene.Instance.AddElement(new Wall(600, 360));
            Scene.Instance.AddElement(new Wall(720, 360));
            Scene.Instance.AddElement(new Wall(300, 420));
            Scene.Instance.AddElement(new Wall(420, 420));
            Scene.Instance.AddElement(new Wall(540, 420));
            Scene.Instance.AddElement(new Wall(600, 420));
            Scene.Instance.AddElement(new Wall(660, 420));
            Scene.Instance.AddElement(new Wall(60, 480));
            Scene.Instance.AddElement(new Wall(300, 480));
            Scene.Instance.AddElement(new Wall(420, 480));
            Scene.Instance.AddElement(new Wall(660, 480));
            Scene.Instance.AddElement(new Wall(60, 540));
            Scene.Instance.AddElement(new Wall(180, 540));
            Scene.Instance.AddElement(new Wall(300, 540));
            Scene.Instance.AddElement(new Wall(420, 540));
            Scene.Instance.AddElement(new Wall(540, 540));
            Scene.Instance.AddElement(new Wall(660, 540));
            Scene.Instance.AddElement(new Wall(360, 500));
            //Scene.Instance.AddElement(new Wall(60, 600));
            //Scene.Instance.AddElement(new Wall(180, 600));
            //Scene.Instance.AddElement(new Wall(660, 600));
            //Scene.Instance.AddElement(new Wall(60, 660));
            //Scene.Instance.AddElement(new Wall(300, 660));
            //Scene.Instance.AddElement(new Wall(360, 660));
            //Scene.Instance.AddElement(new Wall(420, 660));
            //Scene.Instance.AddElement(new Wall(540, 660));
            //Scene.Instance.AddElement(new Wall(660, 660));
            //Scene.Instance.AddElement(new Wall(60, 720));
            //Scene.Instance.AddElement(new Wall(180, 720));
            //Scene.Instance.AddElement(new Wall(300, 720));
            //Scene.Instance.AddElement(new Wall(420, 720));
            //Scene.Instance.AddElement(new Wall(540, 720));
            //Scene.Instance.AddElement(new Wall(600, 720));
            //Scene.Instance.AddElement(new Wall(660, 720));


            // 实例化水
            Scene.Instance.AddElement(new Water(600, 300));
            Scene.Instance.AddElement(new Water(660, 300));
            Scene.Instance.AddElement(new Water(720, 300));



            // 实例化草地
            Scene.Instance.AddElement(new Grass(0, 240));
            Scene.Instance.AddElement(new Grass(0, 300));
            Scene.Instance.AddElement(new Grass(60, 300));
            Scene.Instance.AddElement(new Grass(240, 360));
            Scene.Instance.AddElement(new Grass(300, 360));
            Scene.Instance.AddElement(new Grass(360, 360));



            //实例化钢块
            Scene.Instance.AddElement(new Steel(180, 0));
            Scene.Instance.AddElement(new Steel(420, 0));
            Scene.Instance.AddElement(new Steel(180, 60));
            Scene.Instance.AddElement(new Steel(600, 120));
            Scene.Instance.AddElement(new Steel(540, 180));
            Scene.Instance.AddElement(new Steel(360, 240));
            Scene.Instance.AddElement(new Steel(480, 300));
            Scene.Instance.AddElement(new Steel(420, 360));
            Scene.Instance.AddElement(new Steel(660, 360));
            Scene.Instance.AddElement(new Steel(180, 420));
            //Scene.Instance.AddElement(new Steel(480, 480));
            //Scene.Instance.AddElement(new Steel(0, 480));
            //Scene.Instance.AddElement(new Steel(180, 480));
            //Scene.Instance.AddElement(new Steel(360, 540));
            //Scene.Instance.AddElement(new Steel(420, 360));

            ////实例化大本营
            //Scene.Instance.AddElement(new Symbol(360, 540));
            #endregion

        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            Scene.Instance.CheckState();
            if (num % 50 == 0)
                Scene.Instance.CreateAnEnemyAndAdd();
            num++;
            this.pictureBox1.Invalidate();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Scene.Instance.P1Play.KeyDown(e);
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Scene.Instance.P1Play.KeyUp(e);
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.gra.Clear(Color.LightGray);
            Scene.Instance.Draw(gra);
            //e.Graphics.DrawImage(canvas,0,0);
        }
    }
}
