using static ClassLibrary.Movable;

namespace Logic
{
    public class Interlayer
    {
        Model model;

        public Interlayer()
        {
            model = new Model();
        }

        public void New()
        {
            model = new Model();
        }

        public void Update()
        {
            model.Update();
        }

        public void NoneDirection()
        {
            model.NoneDirection();
        }

        public bool GameOver
        {
            get
            {
                return model.IsGameOver;
            }
        }

        public bool YouWin
        {
            get
            {
                return model.YouWin;
            }
        }

        public int Score
        {
            get
            {
                return model.Score;
            }
        }

        public int GetPrizesCount
        {
            get
            {
                return model.Prizes.Count;
            }
        }

        public int GetLetsCount
        {
            get
            {
                return model.Lets.Count;
            }
        }

        public int GetEnemiesCount
        {
            get
            {
                return model.Enemies.Count;
            }
        }

        public int GetExplosionsCount
        {
            get
            {
                return model.Explosions.Count;
            }
        }

        public int GetBulletsCount
        {
            get
            {
                return model.Bullets.Count;
            }
        }

        public int ObjSize
        {
            get
            {
                return model.ObjSize;
            }
        }

        public directions GetPlayerDirection
        {
            get
            {
                return model.Player.direction;
            }
        }

        public int GetPlayersX 
        {
            get
            {
                return model.Player.position_x;
            }
        }

        public int GetPlayersY
        {
            get
            {
                return model.Player.position_y;
            }
        }

        public int GetEnemyX(int i)
        {
                return model.Enemies[i].position_x;
        }

        public int GetEnemyY(int i)
        {
            return model.Enemies[i].position_y;
        }

        public directions GetEnemyDirection(int i)
        {
            return model.Enemies[i].direction;
        }

        public int GetExplosionX(int i)
        {
            return model.Explosions[i].position_x;
        }

        public int GetExplosionY(int i)
        {
            return model.Explosions[i].position_y;
        }

        public int GetPrizeX(int i)
        {
            return model.Prizes[i].position_x;
        }

        public int GetPrizeY(int i)
        {
            return model.Prizes[i].position_y;
        }

        public int GetExplosionCount(int i)
        {
            return model.Explosions[i].explocionCount;
        }

        public int GetExplosionSize(int i)
        {
            return model.Explosions[i].size;
        }

        public int GetBulletX(int i)
        {
            return model.Bullets[i].position_x;
        }

        public int GetBulletY(int i)
        {
            return model.Bullets[i].position_y;
        }

        public directions GetBulletDirection(int i)
        {
            return model.Bullets[i].direction;
        }

        public int GetLetX(int i)
        {
            return model.Lets[i].position_x;
        }

        public int GetLetY(int i)
        {
            return model.Lets[i].position_y;
        }

        public int GetLetType(int i)
        {
            return model.Lets[i].type;
        }

        public void ChangePlayerDirection(directions dir)
        {
            model.ChangePlayerDirection(dir);
        }

        public void Shoot (directions dir)
        {
            model.Shoot(model.Player, dir);
        }


    }
}
