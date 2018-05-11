using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Movable;

namespace Logic
{
    public class Model
    {

        static Random positionRand = new Random();
        private int width = 600;
        private int height = 600;
        public int prizeCount = 0;
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Explosion> explosions = new List<Explosion>();
        private List<Prize> prizes = new List<Prize>();
        private List<Let> lets = new List<Let>();
        private List<Bullet> bullets = new List<Bullet>();
        private int objSize = 40;
        private bool isGameOver = false;
        private bool youWin = false;
        private int score = 0;

        public Player Player
        {
            get
            {
                return player;
            }
        }
        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }
        }
        public List<Explosion> Explosions
        {
            get
            {
            return explosions;
            }
}
        public List<Prize> Prizes
        {
            get
            {
                return prizes;
            }
        }
        public List<Let> Lets
        {
            get
            {
                return lets;
            }
        }
        public List<Bullet> Bullets
        {
            get
            {
                return bullets;
            }
        }
        public int ObjSize
        {
            get
            {
                return objSize;
            }
        }
        public bool IsGameOver
        {
            get
            {
                return isGameOver;
            }
        }
        public bool YouWin
        {
            get
            {
                return youWin;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
        }

        public Model()
        {
            player = new Player((width - objSize) / 2, height - objSize, objSize);
            Enemy enemy1 = new Enemy(10, 10, objSize);
            enemies.Add(enemy1);
            Enemy enemy2 = new Enemy((width - objSize) / 2, 10, objSize);
            enemies.Add(enemy2);
            Enemy enemy3 = new Enemy(width - objSize - 10, 10, objSize);
            enemies.Add(enemy3);
            MakeLets();
            MakePrizes(prizeCount);
        }

        public void MakePrizes(int count)
        {
            for (int i = count; i < 10; i++)
            {
                int x = positionRand.Next(width - objSize);
                int y = positionRand.Next(height - objSize);
                if (CheckEmpty(x, y))
                {
                    Prize prize = new Prize(x, y);
                    prizes.Add(prize);
                    prizeCount++;
                }
                else
                {
                    i--;
                }
            }
        }

        void MakeLets()
        {
            string line;
            List<string> lines = new List<string>();
            int p_x = 0;
            int p_y = 0;

            System.IO.StreamReader file =
            new System.IO.StreamReader(@"../../Images/1.txt");
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            file.Close();

            for (int i = 0; i < 30; i++)
            {
                char[] tempArr = lines[i].ToCharArray();

                for (int j = 0; j < 30; j++)
                {
                    if (tempArr[j] != '0')
                    {
                        Let let = new Let(p_x, p_y, tempArr[j]);
                        lets.Add(let);
                    }
                    p_x += 20;
                }
                p_y += 20;
                p_x = 0;
            }
        }

        public void MakeExplosions()
        {
            for (int e = 0; e < explosions.Count; e++)
            {
                if (explosions[e].explocionCount < 60)
                {
                    explosions[e].explocionCount++;
                    if (explosions[e].explocionCount % 10 == 0)
                    {
                        if (explosions[e].explocionCount != 40)
                        {
                            explosions[e].position_x -= 3;
                            explosions[e].position_y -= 3;
                            explosions[e].size += 3;
                        }
                        else
                        {
                            explosions[e].position_x -= 10;
                            explosions[e].position_y -= 10;
                            explosions[e].size += 10;
                        }
                    }
                }

                else
                {
                    explosions.Remove(explosions[e]);
                    e--;
                }
            }
        }

        public void MoveAfterCheck()
        {
            if (player.direction != Movable.directions.none)
                player.Move();
            foreach (Enemy enemy in enemies)
            {

                if (enemy.time == 40)
                {
                    enemy.time = 0;
                    Shoot(enemy, enemy.direction);

                    int probability = positionRand.Next(10);
                    switch (probability)
                    {
                        case 1:
                            enemy.direction = directions.up;
                            break;
                        case 2:
                            enemy.direction = directions.down;
                            break;
                        case 3:
                            enemy.direction = directions.left;
                            break;
                        case 4:
                            enemy.direction = directions.right;
                            break;
                        default:
                            break;
                    }
                }
                enemy.Move();
                enemy.time++;
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();
                bullet.Move();
            }
        }

        bool CheckEmpty(int x, int y)
        {
            bool empty = true;

            if (BoxCollides(x, y, objSize, player.position_x, player.position_y, objSize))
            {
                empty = false;
            }

            foreach (Enemy enemy in enemies)
            {
                if (BoxCollides(x, y, objSize, enemy.position_x, enemy.position_y, objSize))
                {
                    empty = false;
                    break;
                }
            }

            foreach (Prize prize in prizes)
            {
                if (BoxCollides(x, y, objSize, prize.position_x, prize.position_y, objSize))
                {
                    empty = false;
                    break;
                }
            }

            foreach (Let let in lets)
            {
                if (BoxCollides(x, y, objSize, let.position_x, let.position_y, objSize / 2))
                {
                    empty = false;
                    break;
                }
            }

            return empty;
        }

        public void CheckCollisions()
        {
            CheckPlayerBounds();
            CheckPlayerCrash();
            CheckEnemiesCollisions();
            

            if (enemies.Count == 0)
            {
                GameOver();
            }

            CheckBulletCollisions();

            CheckPrizeCollisions();
        }

        bool BoxCollides(int x1, int y1, int size1, int x2, int y2, int size2)
        {
            bool collide = false;
            if (x1 <= x2 + size2 && x1 + size1 >= x2 &&
                y1 <= y2 + size2 && y1 + size1 >= y2)
            {
                collide = true;
            }
            return collide;
        }

        bool CheckBulletBounds(Bullet bullet)
        {
            bool remove = false;
            if (bullet.position_x < 0 || bullet.position_x > width - 5 ||
            bullet.position_y < 0 || bullet.position_y > height - 5)
            {
                bullets.Remove(bullet);
                remove = true;
            }
            return remove;
        }

        void CheckPlayerBounds()
        {
            if (player.position_x < 0)
            {
                player.position_x = 0;
            }
            else if (player.position_x > width - objSize)
            {
                player.position_x = width - objSize;
            }

            if (player.position_y < 0)
            {
                player.position_y = 0;
            }
            else if (player.position_y > height - objSize)
            {
                player.position_y = height - objSize;
            }
        }

        void CheckPlayerCrash()
        {
            for (int l = 0; l < lets.Count; l++)
            {
                if (BoxCollides(player.position_x + 2, player.position_y + 2, objSize - 3, lets[l].position_x, lets[l].position_y, objSize / 2))
                {
                    switch (player.direction)
                    {
                        case directions.right:
                            player.position_x -= 2;
                            break;
                        case directions.left:
                            player.position_x += 2;
                            break;
                        case directions.up:
                            player.position_y += 2;
                            break;
                        case directions.down:
                            player.position_y -= 2;
                            break;
                    }
                    player.direction = directions.none;
                    break;
                }
            }
        }

        void CheckEnemyBounds(Enemy enemy)
        {
            if (enemy.position_x < 0)
            {
                enemy.position_x = 0;

                int x = positionRand.Next(2);
                if (x == 0)
                {
                    enemy.direction = Movable.directions.up;
                }
                else
                {
                    enemy.direction = Movable.directions.down;
                }
            }
            else if (enemy.position_x > width - objSize)
            {
                enemy.position_x = width - objSize;

                int x = positionRand.Next(2);
                if (x == 0)
                {
                    enemy.direction = Movable.directions.up;
                }
                else
                {
                    enemy.direction = Movable.directions.down;
                }
            }

            if (enemy.position_y < 0)
            {
                enemy.position_y = 0;

                int x = positionRand.Next(2);
                if (x == 0)
                {
                    enemy.direction = Movable.directions.left;
                }
                else
                {
                    enemy.direction = Movable.directions.right;
                }
            }
            else if (enemy.position_y > height - objSize)
            {
                enemy.position_y = height - objSize;
                int x = positionRand.Next(2);
                if (x == 0)
                {
                    enemy.direction = Movable.directions.left;
                }
                else
                {
                    enemy.direction = Movable.directions.right;
                }
            }
        }

        void CheckEnemiesCollisions()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                CheckEnemyToEnemy(i);

                CheckEnemyBounds(enemies[i]);

                if (BoxCollides(player.position_x, player.position_y, objSize, enemies[i].position_x, enemies[i].position_y, objSize))
                {
                    GameOver();
                }

                for (int l = 0; l < lets.Count; l++)
                {
                    if (BoxCollides(enemies[i].position_x + 2, enemies[i].position_y + 2, objSize - 3, lets[l].position_x, lets[l].position_y, objSize / 2))
                    {
                        switch (enemies[i].direction)
                        {
                            case directions.right:
                                enemies[i].direction = directions.left;
                                break;
                            case directions.left:
                                enemies[i].direction = directions.right;
                                break;
                            case directions.up:
                                enemies[i].direction = directions.down;
                                break;
                            case directions.down:
                                enemies[i].direction = directions.up;
                                break;
                        }
                        break;
                    }
                }

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (BoxCollides(enemies[i].position_x, enemies[i].position_y, objSize, bullets[j].position_x, bullets[j].position_y, 5))
                    {
                        if (bullets[j].myOwner is Player)
                        {
                            Explosion explosion = new Explosion(enemies[i]);
                            explosions.Add(explosion);

                            enemies.Remove(enemies[i]);
                            i--;
                            score += 100;
                            bullets.Remove(bullets[j]);
                            j--;

                            break;
                        }
                    }
                }

            }
        }

        void CheckBulletCollisions()
        {
            for (int b = 0; b < bullets.Count; b++)
            {
                if (CheckBulletBounds(bullets[b]))
                {
                    b--;
                    break;
                }
                if (BoxCollides(player.position_x, player.position_y, objSize, bullets[b].position_x, bullets[b].position_y, 5))
                {
                    if (!(bullets[b].myOwner is Player))
                    {
                        GameOver();
                        break;
                    }
                }
                bool crash = false;
                for (int l = 0; l < lets.Count; l++)
                {
                    if (BoxCollides(bullets[b].position_x, bullets[b].position_y, 5, lets[l].position_x, lets[l].position_y, objSize / 2))
                    {
                        if (lets[l].type != 2)
                        {
                            if (lets[l].type == 0)
                            {
                                crash = true;
                                lets.Remove(lets[l]);
                                l--;
                                break;
                            }
                            crash = true;
                            break;
                        }
                    }
                }
                if (crash)
                {
                    bullets.Remove(bullets[b]);
                    b--;
                    break;
                }
            }
        }

        void CheckPrizeCollisions()
        {
            for (int k = 0; k < prizes.Count; k++)
            {
                if (BoxCollides(player.position_x, player.position_y, objSize, prizes[k].position_x, prizes[k].position_y, objSize))
                {
                    prizes.Remove(prizes[k]);
                    score += 100;
                    k--;
                    prizeCount--;
                }
            }
        }

        void CheckEnemyToEnemy(int i)
        {
            foreach (Enemy enemy in enemies)
            {
                if (!(enemies[i] == enemy))
                {
                    if (BoxCollides(enemies[i].position_x, enemies[i].position_y, objSize, enemy.position_x, enemy.position_y, objSize))
                    {
                        int r = positionRand.Next(2);
                        switch (enemies[i].direction)
                        {
                            case directions.right:

                                if (r == 0)
                                {
                                    enemy.direction = Movable.directions.up;
                                }
                                else
                                {
                                    enemy.direction = Movable.directions.down;
                                }
                                break;
                            case directions.left:

                                if (r == 0)
                                {
                                    enemy.direction = Movable.directions.up;
                                }
                                else
                                {
                                    enemy.direction = Movable.directions.down;
                                }
                                break;
                            case directions.up:

                                if (r == 0)
                                {
                                    enemy.direction = Movable.directions.left;
                                }
                                else
                                {
                                    enemy.direction = Movable.directions.right;
                                }
                                break;
                            case directions.down:

                                if (r == 0)
                                {
                                    enemy.direction = Movable.directions.left;
                                }
                                else
                                {
                                    enemy.direction = Movable.directions.right;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }


            }
        }

        

        public void Shoot(Tank tank, directions direction)
        {
            Bullet bullet = new Bullet(tank, direction);
            bullets.Add(bullet);
        }

        void GameOver()
        {
            isGameOver = true;
        }

        public void NoneDirection()
        {
            player.direction = directions.none;
        }

        public void ChangePlayerDirection(directions dir)
        {
            player.direction = dir;
        }
    }
}
