using System;
using System.Collections.Generic;
using System.Text;

namespace SFMLTueFri
{
    class StickRope
    {
        public PointRope A, B;
        public float length;

        public StickRope(PointRope _A, PointRope _B)
        {
            A = _A;
            B = _B;
            length = A.coord.Distanse(B.coord);
        }
    }
}
