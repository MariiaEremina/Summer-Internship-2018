using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace Controllers
{
    public interface IView
    {
        void Render();
        void SetController(PackmanController givenController);


        Player player { get; set; }
        List<Enemy> enemies { get; set; }
        List<Bullet> bullets { get; set; }
        List<Prize> prizes { get; set; }
        List<Let> lets { get; set; }
        List<Explosion> explosions { get; set; }
        int score { get; set; }
        bool isGameOver { get; set; }

    }
}
