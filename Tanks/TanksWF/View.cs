using ClassLibrary;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClassLibrary.Movable;

namespace TanksWF
{
    public partial class View : Form
    {
        public Bitmap sprite = new Bitmap("Images/sprite.png");
        public Bitmap background = new Bitmap(600, 600);
        public List<Bitmap> playerImages = new List<Bitmap>();
        public List<Bitmap> enemyImages = new List<Bitmap>();
        public List<Bitmap> letImages = new List<Bitmap>();
        public List<Bitmap> explosionImages = new List<Bitmap>();
        directions playerDirectionNumber = directions.up;
        Interlayer interlayer = new Interlayer();
        bool pause = true;

        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            MakePlayerImages();
            MakeEnemyImages();
            MakeExplosionImages();
            MakeLetImages();
            interlayer.NoneDirection();
            Render();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            interlayer.Update();
            Render();
        }

        private void Render()
        {
            Bitmap screen = new Bitmap(600, 600);
            using (Graphics g = Graphics.FromImage(screen))
            {
                g.Clear(Color.Transparent);
            }

            

            if (!(interlayer.GameOver))
            {
                RenderPlayer(screen);
                RenderPrize(screen);
                RenderEnemy(screen);
                RenderBullet(screen);
                RenderExplosion(screen);
                RenderLets(screen);
            }
            else
            {
                if (interlayer.YouWin)
                { }
                else
                {
                    Bitmap end = new Bitmap(sprite.Clone(new Rectangle(288, 184, 32, 16), sprite.PixelFormat));
                    using (Graphics g = Graphics.FromImage(screen))
                    {
                        g.DrawImage(end, new Rectangle(100, 100, 200, 100));
                    }
                }
            }


            using (Graphics g = Graphics.FromImage(background))
            {
                g.FillRectangle(Brushes.Black, 0, 0, 600, 600);
                g.DrawImage(screen, 0, 0, 600, 600);
            }
            pictureBox1.Image = background;

            Score.Text = "Score:" + interlayer.Score.ToString();
        }

        public void RenderPlayer(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                switch (interlayer.GetPlayerDirection)
                {
                    case directions.up:
                        g.DrawImage(playerImages[0], new Rectangle(interlayer.GetPlayersX, interlayer.GetPlayersY, interlayer.ObjSize, interlayer.ObjSize));
                        playerDirectionNumber = directions.up;
                        break;
                    case directions.down:
                        g.DrawImage(playerImages[1], new Rectangle(interlayer.GetPlayersX, interlayer.GetPlayersY, interlayer.ObjSize, interlayer.ObjSize));
                        playerDirectionNumber = directions.down;
                        break;
                    case directions.left:
                        g.DrawImage(playerImages[2], new Rectangle(interlayer.GetPlayersX, interlayer.GetPlayersY, interlayer.ObjSize, interlayer.ObjSize));
                        playerDirectionNumber = directions.left;
                        break;
                    case directions.right:
                        g.DrawImage(playerImages[3], new Rectangle(interlayer.GetPlayersX, interlayer.GetPlayersY, interlayer.ObjSize, interlayer.ObjSize));
                        playerDirectionNumber = directions.right;
                        break;
                    case directions.none:
                        int i = (int)playerDirectionNumber;
                        g.DrawImage(playerImages[i], new Rectangle(interlayer.GetPlayersX, interlayer.GetPlayersY, interlayer.ObjSize, interlayer.ObjSize));
                        break;
                    default:
                        break;
                }


            }
        }

        public void RenderEnemy(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                for (int i = 0; i < interlayer.GetEnemiesCount; i++)
                {
                    switch (interlayer.GetEnemyDirection(i))
                    {
                        case directions.up:
                            g.DrawImage(enemyImages[0], new Rectangle(interlayer.GetEnemyX(i), interlayer.GetEnemyY(i), interlayer.ObjSize, interlayer.ObjSize));
                            break;
                        case directions.down:
                            g.DrawImage(enemyImages[2], new Rectangle(interlayer.GetEnemyX(i), interlayer.GetEnemyY(i), interlayer.ObjSize, interlayer.ObjSize));
                            break;
                        case directions.left:
                            g.DrawImage(enemyImages[1], new Rectangle(interlayer.GetEnemyX(i), interlayer.GetEnemyY(i), interlayer.ObjSize, interlayer.ObjSize));
                            break;
                        case directions.right:
                            g.DrawImage(enemyImages[3], new Rectangle(interlayer.GetEnemyX(i), interlayer.GetEnemyY(i), interlayer.ObjSize, interlayer.ObjSize));
                            break;
                        default:
                            break;
                    }
                }


            }
        }

        public void RenderPrize(Bitmap screen)
        {
            Bitmap prize = new Bitmap(sprite.Clone(new Rectangle(303, 95, 17, 17), sprite.PixelFormat));
            for (int i = 0; i < interlayer.GetPrizesCount; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(prize, new Rectangle(interlayer.GetPrizeX(i), interlayer.GetPrizeY(i), interlayer.ObjSize, interlayer.ObjSize));
                }
            }
        }

        public void RenderBullet(Bitmap screen)
        {
            Bitmap bullet = new Bitmap(sprite.Clone(new Rectangle(390, 0, 5, 5), sprite.PixelFormat));
            for (int i = 0; i < interlayer.GetBulletsCount; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(bullet, new Rectangle(interlayer.GetBulletX(i), interlayer.GetBulletY(i), 5, 5));
                }
            }
        }

        public void RenderExplosion(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                for (int i = 0; i < interlayer.GetExplosionsCount; i++)
                {
                    switch (Math.Round((double)(interlayer.GetExplosionCount(i)/10)))
                    {
                        case 1:
                            g.DrawImage(explosionImages[0], new Rectangle(interlayer.GetExplosionX(i), interlayer.GetExplosionY(i), interlayer.GetExplosionSize(i), interlayer.GetExplosionSize(i)));
                            break;
                        case 2:
                            g.DrawImage(explosionImages[1], new Rectangle(interlayer.GetExplosionX(i), interlayer.GetExplosionY(i), interlayer.GetExplosionSize(i), interlayer.GetExplosionSize(i)));
                            break;
                        case 3:
                            g.DrawImage(explosionImages[2], new Rectangle(interlayer.GetExplosionX(i), interlayer.GetExplosionY(i), interlayer.GetExplosionSize(i), interlayer.GetExplosionSize(i)));
                            break;
                        case 4:
                            g.DrawImage(explosionImages[3], new Rectangle(interlayer.GetExplosionX(i), interlayer.GetExplosionY(i), interlayer.GetExplosionSize(i), interlayer.GetExplosionSize(i)));
                            break;
                        case 5:
                            g.DrawImage(explosionImages[4], new Rectangle(interlayer.GetExplosionX(i), interlayer.GetExplosionY(i), interlayer.GetExplosionSize(i), interlayer.GetExplosionSize(i)));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void RenderLets(Bitmap screen)
        {
            for (int i = 0; i < interlayer.GetLetsCount; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(letImages[interlayer.GetLetType(i)], new Rectangle(interlayer.GetLetX(i), interlayer.GetLetY(i), interlayer.ObjSize / 2, interlayer.ObjSize / 2));

                }
            }
        }

        public void MakeExplosionImages()
        {
            Bitmap first = new Bitmap(sprite.Clone(new Rectangle(259, 130, 12, 11), sprite.PixelFormat));
            explosionImages.Add(first);
            Bitmap second = new Bitmap(sprite.Clone(new Rectangle(272, 129, 15, 14), sprite.PixelFormat));
            explosionImages.Add(second);
            Bitmap third = new Bitmap(sprite.Clone(new Rectangle(288, 128, 16, 17), sprite.PixelFormat));
            explosionImages.Add(third);
            Bitmap fourth = new Bitmap(sprite.Clone(new Rectangle(305, 129, 31, 30), sprite.PixelFormat));
            explosionImages.Add(fourth);
            Bitmap fifth = new Bitmap(sprite.Clone(new Rectangle(335, 129, 33, 33), sprite.PixelFormat));
            explosionImages.Add(fifth);
        }

        public void MakeLetImages()
        {
            Bitmap first = new Bitmap(sprite.Clone(new Rectangle(280, 0, 8, 8), sprite.PixelFormat));
            letImages.Add(first);
            Bitmap second = new Bitmap(sprite.Clone(new Rectangle(280, 16, 8, 8), sprite.PixelFormat));
            letImages.Add(second);
            Bitmap third = new Bitmap(sprite.Clone(new Rectangle(264, 32, 8, 8), sprite.PixelFormat));
            letImages.Add(third);
        }

        public void MakePlayerImages()
        {
            Bitmap up = new Bitmap(sprite.Clone(new Rectangle(0, 0, 14, 16), sprite.PixelFormat));
            playerImages.Add(up);
            Bitmap down = new Bitmap(sprite.Clone(new Rectangle(64, 0, 14, 16), sprite.PixelFormat));
            playerImages.Add(down);
            Bitmap left = new Bitmap(sprite.Clone(new Rectangle(33, 0, 15, 15), sprite.PixelFormat));
            playerImages.Add(left);
            Bitmap right = new Bitmap(sprite.Clone(new Rectangle(96, 0, 15, 15), sprite.PixelFormat));
            playerImages.Add(right);
        }

        public void MakeEnemyImages()
        {
            Bitmap up = new Bitmap(sprite.Clone(new Rectangle(128, 0, 15, 16), sprite.PixelFormat));
            enemyImages.Add(up);
            Bitmap left = new Bitmap(sprite.Clone(new Rectangle(161, 0, 15, 15), sprite.PixelFormat));
            enemyImages.Add(left);
            Bitmap down = new Bitmap(sprite.Clone(new Rectangle(192, 0, 15, 15), sprite.PixelFormat));
            enemyImages.Add(down);
            Bitmap right = new Bitmap(sprite.Clone(new Rectangle(224, 0, 15, 15), sprite.PixelFormat));
            enemyImages.Add(right);
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.S:
                    interlayer.ChangePlayerDirection(directions.down);
                    break;
                case Keys.W:
                    interlayer.ChangePlayerDirection(directions.up);
                    break;
                case Keys.A:
                    interlayer.ChangePlayerDirection(directions.left);
                    break;
                case Keys.D:
                    interlayer.ChangePlayerDirection(directions.right);
                    break;
                case Keys.Space:
                    interlayer.Shoot(playerDirectionNumber);
                    break;
            }
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.S:
                    if (interlayer.GetPlayerDirection == directions.down)
                    {
                        interlayer.NoneDirection();
                    }
                    break;
                case Keys.W:
                    if (interlayer.GetPlayerDirection == directions.up)
                    {
                        interlayer.NoneDirection();
                    }
                    break;
                case Keys.A:
                    if (interlayer.GetPlayerDirection == directions.left)
                    {
                        interlayer.NoneDirection();
                    }
                    break;
                case Keys.D:
                    if (interlayer.GetPlayerDirection == directions.right)
                    {
                        interlayer.NoneDirection();
                    }
                    break;

            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Focus();
            Start.Select();
            interlayer.New();
            interlayer.NoneDirection();
            timer.Start();
            pictureBox1.Focus();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Pause.Focus();
            Pause.Select();

            if (pause)
            {
                timer.Stop();
                pause = false;
            }
            else
            {
                timer.Start();
                pause = true;
            }
            pictureBox1.Focus();
        }
    }
}
