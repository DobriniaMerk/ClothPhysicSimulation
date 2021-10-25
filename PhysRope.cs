using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace SFMLTueFri
{
    class PhysRope
    {
        public PhysPoint A, B;
        public float length;
        public float k = 0.01f;
        public bool delete = false;

        public PhysRope(PhysPoint _A, PhysPoint _B)
        {
            A = _A;
            B = _B;
            length = A.pos.Distanse(B.pos);
        }

        public void Update()
        {
            Vector2f dir = A.pos - B.pos;  
            dir.Normalize();  //unit vector from B to A
            float x = length - A.pos.Distanse(B.pos);
            dir *= k * x;

            A.ApplyForce(dir);
            B.ApplyForce(dir * -1);

            if (Math.Abs(x) > length * 2)
                delete = true;
        }

    }
}
