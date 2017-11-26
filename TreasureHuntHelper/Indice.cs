using Cookie.API.Gamedata.D2o.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureHuntHelper
{
    class Indice
    {
        public int indiceId = 0;

        public Point position;

        public int direction = 0;

        public Indice(int indiceId, Point position, int direction)
        {
            this.indiceId = indiceId;
            this.position = position;
            this.direction = direction;
        }
    }
}
