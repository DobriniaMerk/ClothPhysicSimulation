using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace SFMLTueFri
{
    class PointRope
    {
        public Vector2f coord;
        public bool blocked = false;
        public Vector2f prevCoord;

        public PointRope(Vector2f coord)
        {
            this.coord = coord;
            this.prevCoord = coord;
        }
    }
}
