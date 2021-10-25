using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFMLTueFri
{
    class Game
    {

        List<PhysPoint> points;
        List<PhysRope> ropes;
        public bool pause = true;
        PhysPoint PreviousPoint = null;
        double maxAngleDiff = 10;
        Vector2f mouseCoords;
        public int windowWidth = 0;
        public int windowHeight = 0;
        Boolean displayPoints = true;

        public Game()
        {
            points = new List<PhysPoint>();
            ropes = new List<PhysRope>();
        }

        public void Draw(RenderWindow rw)
        {
            foreach (PhysRope sr in ropes)
            {
                Vertex[] va = new Vertex[2];
                va[0] = new Vertex(sr.A.pos);
                va[1] = new Vertex(sr.B.pos);

                rw.Draw(va, PrimitiveType.Lines);
            }

            if(displayPoints)
                foreach(PhysPoint pr in points)
                {
                    CircleShape cr = new CircleShape(10);
                    cr.Position = pr.pos - new Vector2f(10, 10);

                    if (pr.blocked)
                        cr.FillColor = Color.Cyan;

                    rw.Draw(cr);
                }

            
        }

        public void Next()
        {
            if (!pause)
            {
                for (int i = ropes.Count - 1; i >= 0; i--)
                {
                    ropes[i].Update();

                    if (ropes[i].delete)
                        ropes.RemoveAt(i);
                }

                for (int i = points.Count - 1; i >= 0; i--)
                {
                    if (!points[i].blocked)
                        points[i].ApplyForce(new Vector2f(0.0f, 0.03f));

                    points[i].Update();
                    if (points[i].isOutOfBounds(windowWidth, windowHeight))
                        points.RemoveAt(i);

                }
            }
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        { 
            switch (e.Code)
            {
                case Keyboard.Key.Space:
                    pause = !pause;
                    break;
                case Keyboard.Key.G:
                    CreateGrid(50, 50);
                    break;
                case Keyboard.Key.H:
                    displayPoints = !displayPoints;
                    break;
                case Keyboard.Key.R:
                    points = new List<PhysPoint>();
                    ropes = new List<PhysRope>();
                    break;
            }
        }

        public void MousePressed(object sender, MouseButtonEventArgs e)
        {
            mouseCoords = new Vector2f(e.X, e.Y);

            if (pause)
            {
                PhysPoint lastClickedPoint = ChooseNearest(mouseCoords);
                switch (e.Button)
                {
                    case Mouse.Button.Left:
                        if (lastClickedPoint == null)
                        {
                            points.Add(new PhysPoint(mouseCoords));
                        }
                        else
                        {
                            lastClickedPoint.blocked = !lastClickedPoint.blocked;
                        }
                        break;
                    case Mouse.Button.Right:
                        if (lastClickedPoint == null)
                            PreviousPoint = null;
                        else
                        {
                            if (PreviousPoint == null)
                                PreviousPoint = lastClickedPoint;
                            else
                                if(PreviousPoint != lastClickedPoint)
                                {
                                    ropes.Add(new PhysRope(PreviousPoint, lastClickedPoint));
                                    PreviousPoint = null;
                                }
                        }
                        break;
                }
            }
        }

        public void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            mouseCoords = new Vector2f(e.X, e.Y);

            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                foreach (PhysRope rp in ropes)
                {
                    Vector2f bToMouse = rp.B.pos - mouseCoords;

                    double aAngle = Functions.AngleBetween(rp.A.pos - mouseCoords, rp.A.pos - rp.B.pos);
                    double bAngle = Functions.AngleBetween(rp.B.pos - mouseCoords, rp.B.pos - rp.A.pos);

                    if(aAngle < 90 & aAngle > -90 && bAngle < 90 & bAngle > -90)
                        if (aAngle < maxAngleDiff && bAngle < maxAngleDiff)
                            rp.delete = true;
                }
            }
        }

        PhysPoint ChooseNearest(Vector2f mCoords)
        {
            PhysPoint result = null;
            foreach (PhysPoint pr in points)
                if (pr.pos.Distanse(mCoords) < 10)
                    result = pr;

            return result;
        }

        void CreateGrid(int width, int height)
        {
            for(int j = 1; j < height + 1; j++)
            {
                for(int i = 1; i < width + 1; i++)
                {
                    points.Add(new PhysPoint(new Vector2f((windowWidth / width) * ((float)i - 0.5f), (windowHeight / height) * ((float)j - 0.8f))));
                }
            }

            int h = 1;
            int w = 1;

            for(int i = 0; i < points.Count - 1; i++)
            {
                if (h == 1)
                    if ((w % 2) == 1)
                        points[i].blocked = true;

                if (w < width)
                    ropes.Add(new PhysRope(points[i], points[i + 1]));

                if (h < height)
                    ropes.Add(new PhysRope(points[i], points[i + width]));

                w++;
                if (w > width)
                {
                    h++;
                    w = 1;
                }
            }
        }
    }
}
