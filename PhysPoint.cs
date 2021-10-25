using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace SFMLTueFri
{
    class PhysPoint
    {
        public Vector2f pos, acc, vel;
        public bool blocked = false;
        protected float maxSpeed = 5f;
        protected float airDamping = 0.999f;

        public PhysPoint(Vector2f pos)
        {
            this.pos = pos;
            acc = new Vector2f();
            vel = new Vector2f();
        }

        public void ApplyForce(Vector2f force)
        {
            acc += force;
        }

        public void Update()
        {
            vel += acc;
            vel.X = Math.Clamp(vel.X * airDamping, -maxSpeed, maxSpeed);
            vel.Y = Math.Clamp(vel.Y * airDamping, -maxSpeed, maxSpeed);

            if (!blocked)
                pos += vel;

            acc *= 0;
        }

        public bool isOutOfBounds(int width, int height)
        {
            if (pos.X < -50 || pos.X > width + 50 || pos.Y > width + 50 || pos.Y < -50)
                return true;
            return false;
        }
    }
}
