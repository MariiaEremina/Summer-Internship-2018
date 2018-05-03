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
        public List<Bitmap> explosionImages = new List<Bitmap>();
        directions playerDirectionNumber = directions.up;
        Model model = new Model();
        bool pause = true;

        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            model.player.direction = ClassLibrary.Movable.directions.none;
            Render();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            model.Update();
            Render();
        }

        private void Render()
        {
            Bitmap screen = new Bitmap(600, 600);
            using (Graphics g = Graphics.FromImage(screen))
            {
                g.Clear(Color.Transparent);
            }

            MakePlayerImages();
            MakeEnemyImages();
            MakeExplosionImages();

            if (!model.isGameOver)
            {
                RenderPlayer(screen);
                RenderPrize(screen);
                RenderEnemy(screen);
                RenderBullet(screen);
                RenderExplosion(screen);
            }


            using (Graphics g = Graphics.FromImage(background))
            {
                g.FillRectangle(Brushes.Black, 0, 0, 600, 600);
                g.DrawImage(screen, 0, 0, 600, 600);
            }
            pictureBox1.Image = background;

            Score.Text = "Score:" + model.score.ToString();
        }

        public void RenderPlayer(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                switch (model.player.direction)
                {
                    case ClassLibrary.Movable.directions.up:
                        g.DrawImage(playerImages[0], new Rectangle(model.player.position_x, model.player.position_y, model.objSize, model.objSize));
                        playerDirectionNumber = directions.up;
                        break;
                    case ClassLibrary.Movable.directions.down:
                        g.DrawImage(playerImages[1], new Rectangle(model.player.position_x, model.player.position_y, model.objSize, model.objSize));
                        playerDirectionNumber = directions.down;
                        break;
                    case ClassLibrary.Movable.directions.left:
                        g.DrawImage(playerImages[2], new Rectangle(model.player.position_x, model.player.position_y, model.objSize, model.objSize));
                        playerDirectionNumber = directions.left;
                        break;
                    case ClassLibrary.Movable.directions.right:
                        g.DrawImage(playerImages[3], new Rectangle(model.player.position_x, model.player.position_y, model.objSize, model.objSize));
                        playerDirectionNumber = directions.right;
                        break;
                    case ClassLibrary.Movable.directions.none:
                        int i = (int)playerDirectionNumber;
                        g.DrawImage(playerImages[i], new Rectangle(model.player.position_x, model.player.position_y, model.objSize, model.objSize));
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
                for (int i = 0; i < model.enemies.Count; i++)
                {
                    switch (model.enemies[i].direction)
                    {
                        case ClassLibrary.Movable.directions.up:
                            g.DrawImage(enemyImages[0], new Rectangle(model.enemies[i].position_x, model.enemies[i].position_y, model.objSize, model.objSize));
                            break;
                        case ClassLibrary.Movable.directions.down:
                            g.DrawImage(enemyImages[2], new Rectangle(model.enemies[i].position_x, model.enemies[i].position_y, model.objSize, model.objSize));
                            break;
                        case ClassLibrary.Movable.directions.left:
                            g.DrawImage(enemyImages[1], new Rectangle(model.enemies[i].position_x, model.enemies[i].position_y, model.objSize, model.objSize));
                            break;
                        case ClassLibrary.Movable.directions.right:
                            g.DrawImage(enemyImages[3], new Rectangle(model.enemies[i].position_x, model.enemies[i].position_y, model.objSize, model.objSize));
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
            for (int i = 0; i < model.prizes.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(prize, new Rectangle(model.prizes[i].position_x, model.prizes[i].position_y, model.objSize, model.objSize));
                }
            }
        }

        public void RenderBullet(Bitmap screen)
        {
            Bitmap bullet = new Bitmap(sprite.Clone(new Rectangle(390, 0, 5, 5), sprite.PixelFormat));
            for (int i = 0; i < model.bullets.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(screen))
                {
                    g.DrawImage(bullet, new Rectangle(model.bullets[i].position_x, model.bullets[i].position_y, 5, 5));
                }
            }
        }

        public void RenderExplosion(Bitmap screen)
        {
            using (Graphics g = Graphics.FromImage(screen))
            {
                for (int i = 0; i < model.explosions.Count; i++)
                {
                    switch (Math.Round((double)(model.explosions[i].explocionCount/10)))
                    {
                        case 1:
                            g.DrawImage(explosionImages[0], new Rectangle(model.explosions[i].position_x, model.explosions[i].position_y, model.explosions[i].size, model.explosions[i].size));
                            break;
                        case 2:
                            g.DrawImage(explosionImages[1], new Rectangle(model.explosions[i].position_x, model.explosions[i].position_y, model.explosions[i].size, model.explosions[i].size));
                            break;
                        case 3:
                            g.DrawImage(explosionImages[2], new Rectangle(model.explosions[i].position_x, model.explosions[i].position_y, model.explosions[i].size, model.explosions[i].size));
                            break;
                        case 4:
                            g.DrawImage(explosionImages[3], new Rectangle(model.explosions[i].position_x, model.explosions[i].position_y, model.explosions[i].size, model.explosions[i].size));
                            break;
                        case 5:
                            g.DrawImage(explosionImages[4], new Rectangle(model.explosions[i].position_x, model.explosions[i].position_y, model.explosions[i].size, model.explosions[i].size));
                            break;
                        default:
                            break;
                    }
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
                    model.player.direction = ClassLibrary.Movable.directions.down;
                    break;
                case Keys.W:
                    model.player.direction = ClassLibrary.Movable.directions.up;
                    break;
                case Keys.A:
                    model.player.direction = ClassLibrary.Movable.directions.left;
                    break;
                case Keys.D:
                    model.player.direction = ClassLibrary.Movable.directions.right;
                    break;
                case Keys.Space:
                    model.Shoot(model.player, playerDirectionNumber);
                    break;
            }
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.S:
                    if (model.player.direction == ClassLibrary.Movable.directions.down)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.W:
                    if (model.player.direction == ClassLibrary.Movable.directions.up)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.A:
                    if (model.player.direction == ClassLibrary.Movable.directions.left)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.D:
                    if (model.player.direction == ClassLibrary.Movable.directions.right)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;

            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Focus();
            Start.Select();
            model = new Model();
            model.player.direction = ClassLibrary.Movable.directions.none;
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
