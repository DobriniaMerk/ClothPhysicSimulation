using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace SFMLTueFri
{
    public static class Functions
    {
        public static float Distanse(this Vector2f a, Vector2f b)
        {
            Vector2f diff = a - b;
            return MathF.Sqrt(MathF.Pow(diff.X, 2) + MathF.Pow(diff.Y, 2));
        }

        public static Vector2f Normalize(this Vector2f a)
        {
            float l = MathF.Sqrt(MathF.Pow(a.X, 2) + (MathF.Pow(a.Y, 2)));
            return (a / l);
        }

        public static double AngleBetween(Vector2f vector1, Vector2f vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }
    }
}
