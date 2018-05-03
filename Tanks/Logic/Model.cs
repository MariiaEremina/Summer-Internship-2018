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
        int width = 600;
        int height = 600;
        public Player player;
        public List<Enemy> enemies = new List<Enemy>();
        public List<Prize> prizes = new List<Prize>();
        int prizeCount = 0;
        List<Let> lets = new List<Let>();
        List<Bullet> bullets = new List<Bullet>();
        public int objSize = 40;

        DateTime lastFire = DateTime.Now;
        public bool isGameOver = false;
        int score = 0;

        public Model()
        {
            player = new Player((width - objSize) / 2, height - objSize);
            Enemy enemy1 = new Enemy(10, 10);
            enemies.Add(enemy1);
            Enemy enemy2 = new Enemy((width - objSize) / 2, 10);
            enemies.Add(enemy2);
            Enemy enemy3 = new Enemy(width - objSize - 10, 10);
            enemies.Add(enemy3);

            for (int i = 0; i < 10; i++)
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

        public bool CheckEmpty(int x, int y)
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

            return empty;
        }

        public void checkCollisions()
        {
            CheckPlayerBounds();

            for (int i = 0; i < enemies.Count; i++)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (!(enemies[i] == enemy))
                    {
                        if (BoxCollides(enemies[i].position_x, enemies[i].position_y, objSize, enemy.position_x, enemy.position_y, objSize))
                        {
                            switch (enemies[i].direction)
                            {
                                case directions.right:
                                    enemies[i].direction = directions.up;
                                    break;
                                case directions.left:
                                    enemies[i].direction = directions.down;
                                    break;
                                case directions.up:
                                    enemies[i].direction = directions.left;
                                    break;
                                case directions.down:
                                    enemies[i].direction = directions.right;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }


                }


                for (int j = 0; j < bullets.Count; j++)
                {
                    if (CheckBulletBounds(bullets[j]))
                    {
                        j--;
                        break;
                    }

                    if (BoxCollides(enemies[i].position_x, enemies[i].position_y, objSize, bullets[j].position_x, bullets[j].position_y, 5))
                    {
                        if (bullets[j].myOwner is Player)
                        {
                            enemies.Remove(enemies[i]);

                            i--;
                            score += 100;
                            bullets.Remove(bullets[j]);
                            j--;
                            break;
                        }
                    }
                }

                CheckEnemyBounds(enemies[i]);

                // Walls

                //foreach (Let let in lets)
                //    {
                //    if(BoxCollides(pos, size, pos3, size3))
                //    {
                //        if (i%2) 
                //        {   
                //            pos[1] = pos[1]+40; 
                //        }
                //        else
                //        {
                //            pos[1] = pos[1]-40; 
                //        }
                //    }

                //}


                //    // Add an explosion
                //    explosions.push({
                //    pos: pos,
                //sprite: new Sprite('img/sprites.png',
                //                   [0, 117],
                //                   [39, 39],
                //                   16,
                //                   [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                //                   null,
                //                   true)
                //    });



                if (BoxCollides(player.position_x, player.position_y, objSize, enemies[i].position_x, enemies[i].position_y, objSize))
                {
                    GameOver();
                }
            }

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

        public bool BoxCollides(int x1, int y1, int size1, int x2, int y2, int size2)
        {
            bool collide = false;
            if (x1 <= x2 + size2 && x1 + size1 >= x2 &&
                y1 <= y2 + size2 && y1 + size1 >= y2)
            {
                collide = true;
            }
            return collide;
        }

        public bool CheckBulletBounds(Bullet bullet)
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

        public void CheckPlayerBounds()
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

        public void CheckEnemyBounds(Enemy enemy)
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

        public void GameOver()
        {
            isGameOver = true;
        }

        public void Update()
        {
            checkCollisions();

            if (player.direction != Movable.directions.none)
                player.Move();
            foreach (Enemy enemy in enemies)
            {
                if (enemy.time == 40)
                {
                    enemy.time = 0;
                    
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
    }
}
