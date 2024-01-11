using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recoil
{
    public partial class Form1 : Form
    {
        bool mouseIsClicked = false;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        SolidBrush beigeBrush = new SolidBrush(Color.Beige);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);
        SolidBrush hotpink = new SolidBrush(Color.HotPink);

        Pen stock = new Pen(Color.BurlyWood, 6);

        List<Rectangle> bullets = new List<Rectangle>();
        List<double> bulletSpeedsY = new List<double>();
        List<double> bulletSpeedsX = new List<double>();

        Rectangle testEnemy = new Rectangle(200, 400, 20, 20);
        Rectangle player = new Rectangle(500, 800, 20, 20);


        //this is set to running by default for testing. Later on when we add a menu and make the
        //player press a button to start we'll change this
        string gameState = "running";

        public Form1()
        {
            InitializeComponent();

            GameInitialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.X = this.Width / 2;
            player.Y = this.Height / 2;
        }

        public void GameInitialize()
        {

            gameTimer.Enabled = true;

            gameState = "running";
           
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            
            //any functions beginnign with base will be in the base for all of us, they're meant for
            //testing I advice against changing or removing them to begin with

            //also, change the last parameter of this function to change the player speed

            BaseMovePlayer(aDown, dDown, wDown, sDown, ref player, 8);
            BaseMoveBullets();
            //remember guys, just call your own functions here

            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            BaseShootPistol();
            mouseIsClicked = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {

            }
            else if (gameState == "gameover")
            {

            }

            else if (gameState == "running")
            {
                e.Graphics.FillEllipse(redBrush, testEnemy);
                e.Graphics.FillEllipse(beigeBrush, player);


                for (int i = 0; i < bullets.Count; i++)
                {
                    e.Graphics.FillEllipse(hotpink, bullets[i]);
                }

            }
        }
        
        //basefunctions here
        public void BaseMovePlayer(bool leftButton, bool rightButton, bool upButton, bool downButton, ref Rectangle player, int playerSpeed)
        {

            if (upButton == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }
            if (downButton == true )
            {
                player.Y += playerSpeed;
            }
            if (leftButton == true)
            {
                player.X -= playerSpeed;
            }
            if (rightButton == true)
            {
                player.X += playerSpeed;
            }
        }
        public void BaseMoveBullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                int x = (int)Math.Round(bullets[i].X + bulletSpeedsX[i], 0);
                int y = (int)Math.Round(bullets[i].Y + bulletSpeedsY[i], 0);
                bullets[i] = new Rectangle(x, y, 5, 5);

            }
        }
        public void BaseShootPistol()
        {
            Rectangle aim = new Rectangle(0, 0, 5, 5);
            int bulletSpeed = 25;


            double spread;

            double xStep;
            double yStep;
            double deltaX;
            double deltaY;
            double angle;

            aim.X = MousePosition.X;
            aim.Y = MousePosition.Y;
            deltaX = aim.X - player.X;
            deltaY = aim.Y - player.Y;
            angle = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;

            xStep = Math.Cos(angle * Math.PI / 180);
            yStep = Math.Sin(angle * Math.PI / 180);
            Rectangle newBullet = new Rectangle(player.X + 10, player.Y + 10, 5, 5);
            bullets.Add(newBullet);
            bulletSpeedsX.Add(xStep * bulletSpeed);
            bulletSpeedsY.Add(yStep * bulletSpeed);
        }

        //individual functions begin here
        public void noahFunction()
        {
        }

        public void dylanFunction()
        {

        }

        public void alistairFunction()
        {

        }





        //add completely new functions down here, please put them in your designated areas to avoid
        //confusion

        //Noah's area

        //calls this method if you want accelerated, physics based movement. Beware though,
        //there's a LOT of parameters
        public void advancedMovement(bool upButton, bool downButton, bool leftButton, bool rightButton, ref Rectangle player, ref float YplayerSpeed, ref float XplayerSpeed, float startingSpeed, float playerAcceleration, float playerDecceleration) 
        {
            if (upButton == true && player.Y > 0)
            {
                if (YplayerSpeed > -startingSpeed)
                {
                    YplayerSpeed = -startingSpeed;
                }
                YplayerSpeed = YplayerSpeed * playerAcceleration;
            }

            if (downButton == true && player.Y < this.Height - player.Height)
            {
                if (YplayerSpeed < startingSpeed)
                {
                    YplayerSpeed = startingSpeed;
                }
                YplayerSpeed = YplayerSpeed * playerAcceleration;
            }


            if (leftButton == true)
            {
                if (XplayerSpeed > -startingSpeed)
                {
                    XplayerSpeed = -startingSpeed;
                }
                XplayerSpeed = XplayerSpeed * playerAcceleration;


            }

            if (rightButton == true && player.X < this.Width - player.Width)
            {
                if (XplayerSpeed < startingSpeed)
                {
                    XplayerSpeed = startingSpeed;
                }
                XplayerSpeed = XplayerSpeed * playerAcceleration;

            }

            if (!leftButton && !rightButton)
            {
                if (XplayerSpeed > 0 || XplayerSpeed < 0)
                {
                    XplayerSpeed *= playerDecceleration;
                }
            }

            if (!upButton && !downButton)
            {
                if (YplayerSpeed > 0 || YplayerSpeed < 0)
                {
                    YplayerSpeed *= playerDecceleration;
                }
            }
        }



        //Dylan's area
        private void minigunTimer_Tick(object sender, EventArgs e)
        {

        }

        //Alistair's area




    }
}
