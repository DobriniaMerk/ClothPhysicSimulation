using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLTueFri
{
    class Program
    {
        static Game myGame;

        static void Main(string[] args)
        {
            VideoMode vdm = new VideoMode(800, 600);

            RenderWindow window = new RenderWindow(vdm, "SFMLtest");

            myGame = new Game();

            window.Closed += OnClose;
            window.KeyPressed += OnKey;
            window.MouseButtonPressed += OnMouseClick;
            window.MouseMoved += OnMouseMove;

            myGame.windowWidth = (int)window.Size.X;
            myGame.windowHeight = (int)window.Size.Y;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                myGame.Next();
                window.Clear(myGame.pause ? new Color(30, 30, 30) : new Color(0, 0, 0));
                myGame.Draw(window);
                window.Display();
            }
        }

        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow rw = sender as RenderWindow;
            rw.Close();
        }

        static void OnKey(object sender, KeyEventArgs e)
        {
            myGame.KeyPressed(sender, e);

            if (e.Code == Keyboard.Key.Escape)
            { 
                RenderWindow rw = sender as RenderWindow;
                rw.Close();
            }
        }

        static void OnMouseClick(object sender, MouseButtonEventArgs e)
        {
            myGame.MousePressed(sender, e);
        }

        static void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            myGame.MouseMoved(sender, e);
        }
    }
}
