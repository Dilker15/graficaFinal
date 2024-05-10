using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameWindow myWindow = new GameWindow(1200,700);
            Game myGame = new Game(myWindow);
            myWindow.Run();
        }
    }
}
