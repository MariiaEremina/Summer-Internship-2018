using Controllers;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TanksWF;

namespace TanksWF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameView gameView = new GameView();
            Model model = new Model();
            PackmanController controller = new PackmanController(gameView, model);
            gameView.ShowDialog();
        }
    }
}