using ClassLibrary;
using Controllers;
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
    public partial class GameView : Form, IView
    {



        public Player player { get; set; }
        public List<Enemy> enemies { get; set; }
        public List<Bullet> bullets { get; set; }
        public List<Prize> prizes { get; set; }
        public List<Let> lets { get; set; }
        public List<Explosion> explosions { get; set; }
        public int score { get; set; }
        public bool isGameOver { get; set; }

        private int objSize = 40;
        //public bool isGameOver = false;
        private bool youWin = false;
        //private int score = 0;



        public Bitmap sprite = new Bitmap("../../Images/sprite2.png");
        public Bitmap background = new Bitmap(600, 600);
        public List<Bitmap> playerImages = new List<Bitmap>();
        public List<Bitmap> enemyImages = new List<Bitmap>();
        public List<Bitmap> letImages = new List<Bitmap>();
        public List<Bitmap> explosionImages = new List<Bitmap>();
        directions casePlayerDirection = directions.up;
        directions playerDirection = directions.up;

        bool shoot = false;
        bool pause = true;
        PackmanController controller;

        public GameView()
        {
            InitializeComponent();
        }

        public void SetController(PackmanController givenController)
        {
            controller = givenController;
        }

        private void View_Load(object sender, EventArgs e)
        {
            isGameOver = false;
            MakePlayerImages();
            MakeEnemyImages();
            MakeExplosionImages();
            MakeLetImages();
            playerDirection = directions.none;
            //Render();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            controller.Update(this, playerDirection, casePlayerDirection, shoot);
            Render();
            shoot = false;
        }

        public void Render()
        {
            Bitmap screen = new Bitmap(600, 600);
            using (Graphics g = Graphics.FromImage(screen))
            {
                g.Clear(Color.Transparent);
            }



            if (!(isGameOver))
            {
                RenderPlayer(screen);
                RenderPrize(screen);
                RenderEnemy(screen);
                RenderLets(screen);
                RenderBullet(screen);
                RenderExplosion(screen);

            }
            else
            {
                if (youWin)
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

            Score.Text = "Score:" + score.ToString();
        }

        public void RenderPlayer(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                switch (player.direction)
                {
                    case directions.up:
                        g.DrawImage(playerImages[0], new Rectangle(player.position_x, player.position_y, player.size_x, player.size_y));
                        casePlayerDirection = directions.up;
                        break;
                    case directions.down:
                        g.DrawImage(playerImages[1], new Rectangle(player.position_x, player.position_y, player.size_x, player.size_y));
                        casePlayerDirection = directions.down;
                        break;
                    case directions.left:
                        g.DrawImage(playerImages[2], new Rectangle(player.position_x, player.position_y, player.size_x, player.size_y));
                        casePlayerDirection = directions.left;
                        break;
                    case directions.right:
                        g.DrawImage(playerImages[3], new Rectangle(player.position_x, player.position_y, player.size_x, player.size_y));
                        casePlayerDirection = directions.right;
                        break;
                    case directions.none:
                        int i = (int)casePlayerDirection;
                        g.DrawImage(playerImages[i], new Rectangle(player.position_x, player.position_y, player.size_x, player.size_y));
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
                for (int i = 0; i < enemies.Count; i++)
                {
                    switch (enemies[i].direction)
                    {
                        case directions.up:
                            g.DrawImage(enemyImages[0], new Rectangle(enemies[i].position_x, enemies[i].position_y, enemies[i].size_x, enemies[i].size_y));
                            break;
                        case directions.down:
                            g.DrawImage(enemyImages[2], new Rectangle(enemies[i].position_x, enemies[i].position_y, enemies[i].size_x, enemies[i].size_y));
                            break;
                        case directions.left:
                            g.DrawImage(enemyImages[1], new Rectangle(enemies[i].position_x, enemies[i].position_y, enemies[i].size_x, enemies[i].size_y));
                            break;
                        case directions.right:
                            g.DrawImage(enemyImages[3], new Rectangle(enemies[i].position_x, enemies[i].position_y, enemies[i].size_x, enemies[i].size_y));
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
            for (int i = 0; i < prizes.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(prize, new Rectangle(prizes[i].position_x, prizes[i].position_y, objSize, objSize));
                }
            }
        }

        public void RenderBullet(Bitmap screen)
        {
            Bitmap bullet = new Bitmap(sprite.Clone(new Rectangle(390, 0, 5, 5), sprite.PixelFormat));
            for (int i = 0; i < bullets.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(bullet, new Rectangle(bullets[i].position_x, bullets[i].position_y, 5, 5));
                }
            }
        }

        public void RenderExplosion(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                for (int i = 0; i < explosions.Count; i++)
                {
                    switch (Math.Round((double)(explosions[i].explocionCount / 10)))
                    {
                        case 1:
                            g.DrawImage(explosionImages[0], new Rectangle(explosions[i].position_x, explosions[i].position_y, explosions[i].size, explosions[i].size));
                            break;
                        case 2:
                            g.DrawImage(explosionImages[1], new Rectangle(explosions[i].position_x, explosions[i].position_y, explosions[i].size, explosions[i].size));
                            break;
                        case 3:
                            g.DrawImage(explosionImages[2], new Rectangle(explosions[i].position_x, explosions[i].position_y, explosions[i].size, explosions[i].size));
                            break;
                        case 4:
                            g.DrawImage(explosionImages[3], new Rectangle(explosions[i].position_x, explosions[i].position_y, explosions[i].size, explosions[i].size));
                            break;
                        case 5:
                            g.DrawImage(explosionImages[4], new Rectangle(explosions[i].position_x, explosions[i].position_y, explosions[i].size, explosions[i].size));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void RenderLets(Bitmap screen)
        {
            for (int i = 0; i < lets.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(letImages[lets[i].type], new Rectangle(lets[i].position_x, lets[i].position_y, objSize / 2, objSize / 2));

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
                    playerDirection = directions.down;
                    break;
                case Keys.W:
                    playerDirection = directions.up;
                    break;
                case Keys.A:
                    playerDirection = directions.left;
                    break;
                case Keys.D:
                    playerDirection = directions.right;
                    break;
                case Keys.Space:
                    shoot = true;
                    break;
            }
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.S:
                    if (playerDirection == directions.down)
                    {
                        playerDirection = directions.none;
                    }
                    break;
                case Keys.W:
                    if (playerDirection == directions.up)
                    {
                        playerDirection = directions.none;
                    }
                    break;
                case Keys.A:
                    if (playerDirection == directions.left)
                    {
                        playerDirection = directions.none;
                    }
                    break;
                case Keys.D:
                    if (playerDirection == directions.right)
                    {
                        playerDirection = directions.none;
                    }
                    break;

            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Focus();
            Start.Select();
            playerDirection = directions.none;

            if (isGameOver)
            {
                casePlayerDirection = directions.up;
                controller.Restart();
                isGameOver = false;
            }
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
