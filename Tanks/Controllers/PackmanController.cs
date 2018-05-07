using Logic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static ClassLibrary.Movable;

namespace Controllers
{
    public class PackmanController: Control
    {
        static Random positionRand = new Random();

        public IView view;
        public Model model;

        public PackmanController(IView givenView, Model givenModel)
        {
            view = givenView;
            model = givenModel;
            givenView.SetController(this);
        }

        public void Update(IView view, directions playerDirection, directions shootDirection, bool shoot)
        {
            model.Player.direction = playerDirection;

            if (shoot)
            {
                model.Shoot(model.Player, shootDirection);
                shoot = false;
            }
            model.CheckCollisions();

            model.MoveAfterCheck();

            model.MakeExplosions();

            model.MakePrizes(model.prizeCount);

            UpdateView();

            view.Render();
        }

        public void UpdateView()
        {
            view.player = model.Player;
            view.enemies = model.Enemies;
            view.lets = model.Lets;
            view.bullets = model.Bullets;
            view.prizes = model.Prizes;
            view.explosions = model.Explosions;
            view.score = model.Score;
            view.isGameOver = model.IsGameOver;
        }


        public void Restart()
        {
            model = new Model();
        }


    }
}
