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
        //noah's globals
        float playerYSpeed = 0f;
        float playerXSpeed = 0f;
        float maxPlayerSpeed = 10f;

        int shotgunAmmo = 2;
        int shotgunAmmoMax = 2;
        int bulletSpeed = 25;
        int enemyHealth = 5;
        int count = 0;

        double spread;
        double xStep;
        double yStep;
        double deltaX;
        double deltaY;
        double angle;
        //

        //dylan's globals
        Rectangle aim = new Rectangle(0, 0, 5, 5);
        bool shotgunfire = false;
        bool minigunfire = false;
        bool pistolfire = false;
        bool rocketfire = false;
        Rectangle explode = new Rectangle(0, 0, 10, 10);
        Random randomSpread = new Random();
        Point endpoint;
        //

        //alistair's gllobals
        //Map information
        int wallWidth = 25;

        int barrelDimentions = 25;
        int enemyDimentions = 20;
        int spikeWidth = 10;
        int spikeHeight = 20;

        List<Rectangle> walls = new List<Rectangle>();
        List<Rectangle> enemies = new List<Rectangle>();
        List<Rectangle> strongEnemies = new List<Rectangle>();
        List<Rectangle> spikes = new List<Rectangle>();
        List<Rectangle> explosiveBarrels = new List<Rectangle>();
        List<Rectangle> landMines = new List<Rectangle>();
        //



        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        SolidBrush wallBrush = new SolidBrush(Color.Beige);
        SolidBrush enemyBrush = new SolidBrush(Color.Red);
        SolidBrush playerBrush = new SolidBrush(Color.Gold);
        SolidBrush bulletBrush  = new SolidBrush(Color.HotPink);

        Pen stock = new Pen(Color.BurlyWood, 6);

        List<Rectangle> bullets = new List<Rectangle>();
        List<double> bulletSpeedsY = new List<double>();
        List<double> bulletSpeedsX = new List<double>();

        Rectangle testEnemy = new Rectangle(200, 400, 40, 40);
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
            moveBullets();
            //remember guys, just call your own functions here
            noahFunction();
            dylanFunction();

            player.X += (int)playerXSpeed;
            player.Y += (int)playerYSpeed;

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
                case Keys.Escape:
                    this.Close();
                    break;
                    //Dylan keys
                case Keys.D1:
                    pistolfire = true;
                    shotgunfire = false;
                    minigunfire = false;
                    rocketfire = false;
                    break;
                case Keys.D2:
                    pistolfire = false;
                    shotgunfire = true;
                    minigunfire = false;
                    rocketfire = false;
                    break;
                case Keys.D3:
                    pistolfire = false;
                    shotgunfire = false;
                    minigunfire = true;
                    rocketfire = false;
                    break;
                case Keys.D4:
                    pistolfire = false;
                    shotgunfire = false;
                    minigunfire = false;
                    rocketfire = true;
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
            shootPistol();
            ShootShotgun();
            RocketLauncher();
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
                if (enemyHealth > 0)
                {
                    e.Graphics.FillEllipse(enemyBrush, testEnemy);
                }
                e.Graphics.FillEllipse(wallBrush, player);
                

                for (int i = 0; i < bullets.Count; i++)
                {
                    e.Graphics.FillEllipse(bulletBrush , bullets[i]);
                }


                //dylan's stuff
                endpoint.X = player.X + 10 + (int)(20 * Math.Cos(angle * Math.PI / 180.0));
                endpoint.Y = player.Y + 10 + (int)(20 * Math.Sin(angle * Math.PI / 180.0));

                e.Graphics.DrawLine(stock, player.X + 10, player.Y + 10, endpoint.X, endpoint.Y);
                //

                //noah's stuff\

                //

                //alistair's stuff
                for (int i = 0; i < walls.Count; i++)
                {
                    e.Graphics.FillRectangle(wallBrush, walls[i]);
                }

                //


            }
        }

        //basefunctions here. Let's just not change these alright guys?
        public void BaseMovePlayer(bool leftButton, bool rightButton, bool upButton, bool downButton, ref Rectangle player, int playerSpeed)
        {

            if (upButton == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }
            if (downButton == true)
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
        public void moveBullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                int x = (int)Math.Round(bullets[i].X + bulletSpeedsX[i], 0);
                int y = (int)Math.Round(bullets[i].Y + bulletSpeedsY[i], 0);
                bullets[i] = new Rectangle(x, y, 5, 5);
                
                //Put this next bit on bullet move
                if (bullets[i].IntersectsWith(explode))
                {
                    bullets[i] = new Rectangle(explode.X, explode.Y, 30, 30);
                    bullets.RemoveAt(i);
                    bulletSpeedsX.RemoveAt(i);
                    bulletSpeedsY.RemoveAt(i);
                }
                if (bullets[i].IntersectsWith(testEnemy))
                {
                    bullets.RemoveAt(i);
                    bulletSpeedsX.RemoveAt(i);
                    bulletSpeedsY.RemoveAt(i);
                    enemyHealth--;
                    count = 0;
                }


            }

        }
        public void shootPistol()
        {
            if (pistolfire)
            {
                funMath();
                Rectangle newBullet = new Rectangle(player.X + 10, player.Y + 10, 5, 5);
                bullets.Add(newBullet);
                bulletSpeedsX.Add(xStep * bulletSpeed);
                bulletSpeedsY.Add(yStep * bulletSpeed);
                applyRecoil(22);
            }
        }
        //

        //individual functions begin here
        public void noahFunction()
        {
            advancedMovement(wDown, sDown, aDown, dDown, ref player, ref playerYSpeed, ref playerXSpeed, 1f, 1.20f, 0.90f);
            limitPlayArea();
            testLabel.Text = $"Shotgun Ammo: {shotgunAmmo}";
            testLabel2.Text = $"Enemy Health: {enemyHealth}";
            testLabel3.Text = $"Count = {count}";
            if(shotgunAmmo < shotgunAmmoMax)
            {
                shotgunTimer.Enabled = true;
            }
            else shotgunTimer.Enabled = false;  
            if (count < 10)
            {
                enemyBrush = new SolidBrush(Color.Pink);
            }
            else enemyBrush = new SolidBrush(Color.Red);
        }
        public void dylanFunction()
        {
            funMath();
        }
        public void alistairFunction()
        {
            CheckCollisions();  //gameTimer.Tick
            LoadMapSegment1();  //Game init
        }
        //and end here




        //add completely new functions down here, please put them in your designated areas to avoid
        //confusion

        //Noah's area
        public void advancedMovement(bool upButton, bool downButton, bool leftButton, bool rightButton, ref Rectangle player, ref float YplayerSpeed, ref float XplayerSpeed, float startingSpeed, float playerAcceleration, float playerDecceleration)
        {
            if (playerYSpeed < maxPlayerSpeed && playerYSpeed > -maxPlayerSpeed)
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
            }

            if (playerXSpeed < maxPlayerSpeed && playerXSpeed > -maxPlayerSpeed)
            {
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
            }
            if ((!leftButton && !rightButton) || (XplayerSpeed > maxPlayerSpeed || XplayerSpeed < -maxPlayerSpeed))
            {
                if (XplayerSpeed > 0 || XplayerSpeed < 0)
                {
                    XplayerSpeed *= playerDecceleration;
                }
            }

            if ((!upButton && !downButton) || (YplayerSpeed > maxPlayerSpeed || YplayerSpeed < -maxPlayerSpeed))
            {
                if (YplayerSpeed > 0 || YplayerSpeed < 0)
                {
                    YplayerSpeed *= playerDecceleration;
                }
            }
        }
        public void limitSpeed(ref float XplayerSpeed, ref float YplayerSpeed)
        {
            if (XplayerSpeed >= maxPlayerSpeed)
            {
                XplayerSpeed = maxPlayerSpeed;
            }
            if (XplayerSpeed <= -maxPlayerSpeed)
            {
                XplayerSpeed = -maxPlayerSpeed;
            }

            if (YplayerSpeed >= maxPlayerSpeed)
            {
                YplayerSpeed = maxPlayerSpeed;
            }
            if (YplayerSpeed <= -maxPlayerSpeed)
            {
                YplayerSpeed = -maxPlayerSpeed;
            }
        }
        public void applyRecoil(int recoilStrength)
        {
            funMath();
            //angle = angle * -1;
            playerXSpeed -= (float)xStep * recoilStrength;
            playerYSpeed -= (float)yStep * recoilStrength;
        }
        public void funMath()
        {



            aim.X = MousePosition.X;
            aim.Y = MousePosition.Y;
            deltaX = aim.X - player.X;
            deltaY = aim.Y - player.Y;
            angle = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;

            xStep = Math.Cos(angle * Math.PI / 180);
            yStep = Math.Sin(angle * Math.PI / 180);
        }
        public void limitPlayArea()
        {
            if(player.X > this.Width - player.Width)
            {
                player.X = this.Width - player.Width;
            }
            if(player.Y > this.Height - player.Height)
            {
                player.Y = this.Height - player.Height;
            }
            if(player.X < 0)
            {
                player.X = 0;
            }
            if(player.Y < 0)
            {
                player.Y = 0;
            }
        }
        private void shotgunTimer_Tick(object sender, EventArgs e)
        {
            shotgunAmmo++;
        }
        private void countingTimer_Tick(object sender, EventArgs e)
        {
            count++;
        }
        //

        //Dylan's area
        private void minigunTimer_Tick(object sender, EventArgs e)
        {
            if (minigunfire == true)
            {
                Rectangle newBullet = new Rectangle(player.X + 10, player.Y + 10, 5, 5);
                bullets.Add(newBullet);
                bulletSpeedsX.Add(xStep * bulletSpeed);
                bulletSpeedsY.Add(yStep * bulletSpeed);
            }
        }
        private void ShootShotgun()
        {
            if (shotgunfire == true && shotgunAmmo != 0)
            {
                funMath();
                for (int i = 0; i < 5; i++)
                {
                    spread = randomSpread.Next(-20, 21);
                    angle = Math.Atan2(deltaY, deltaX) * 180 / Math.PI + spread;

                    xStep = Math.Cos(angle * Math.PI / 180);
                    yStep = Math.Sin(angle * Math.PI / 180);
                    Rectangle newBullet = new Rectangle(player.X + 10, player.Y + 10, 5, 5);
                    bullets.Add(newBullet);
                    bulletSpeedsX.Add(xStep * bulletSpeed);
                    bulletSpeedsY.Add(yStep * bulletSpeed);
                }
                applyRecoil(32);
                shotgunAmmo--;
            }
        }
        public void RocketLauncher()
        {
            if (rocketfire == true)
            {
                Rectangle newBullet = new Rectangle(player.X + 10, player.Y + 10, 15, 15);
                bullets.Add(newBullet);
                bulletSpeedsX.Add(xStep * bulletSpeed);
                bulletSpeedsY.Add(yStep * bulletSpeed);
                explode.X = aim.X;
                explode.Y = aim.Y;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            minigunTimer.Enabled = true;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            minigunTimer.Enabled = false;
        }
        //

        //Alistair's area
        public void LoadMapSegment1()
        {
            Rectangle rightWall = new Rectangle(0, 0, wallWidth, this.Height);
            Rectangle leftWall = new Rectangle(this.Width - wallWidth, 0, wallWidth, this.Height);

            walls.Add(rightWall);
            walls.Add(leftWall);
        }
        public void CheckCollisions()
        {
            for (int i = 0; i < walls.Count; i++)
            {
                //if (walls[i].IntersectsWith(player) && playerSpeed < 0)
                //{

                //}
            }
        }
        //


    }
}


