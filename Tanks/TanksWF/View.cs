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
        directions playerDirectionNumber = directions.up;
        Model model = new Model();
        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            Render();
            model.player.direction = ClassLibrary.Movable.directions.none;
            timer.Start();
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

            if (!model.isGameOver)
            {
                RenderPlayer(screen);
                RenderPrize(screen);
                RenderEnemy(screen);
                RenderBullet(screen);
            }


            using (Graphics g = Graphics.FromImage(background))
            {
                g.FillRectangle(Brushes.Black, 0, 0, 600, 600);
                g.DrawImage(screen, 0, 0, 600, 600);
            }
            pictureBox1.Image = background;
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
                case Keys.Down:
                    model.player.direction = ClassLibrary.Movable.directions.down;
                    break;
                case Keys.Up:
                    model.player.direction = ClassLibrary.Movable.directions.up;
                    break;
                case Keys.Left:
                    model.player.direction = ClassLibrary.Movable.directions.left;
                    break;
                case Keys.Right:
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
                case Keys.Down:
                    if (model.player.direction == ClassLibrary.Movable.directions.down)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.Up:
                    if (model.player.direction == ClassLibrary.Movable.directions.up)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.Left:
                    if (model.player.direction == ClassLibrary.Movable.directions.left)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;
                case Keys.Right:
                    if (model.player.direction == ClassLibrary.Movable.directions.right)
                    {
                        model.player.direction = ClassLibrary.Movable.directions.none;
                    }
                    break;

            }
        }
    }
}
