using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Device;

namespace Nukitashi2.Actor.Blocks
{
    class SpeedUP : GameObject
    {
        public SpeedUP(Vector2 pos, GameDevice gameDevice)
           : base("Socks", pos, 128, 128, gameDevice)
        { }
        public SpeedUP(SpeedUP other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new SpeedUP(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Updata(GameTime gameTime)
        {
        }
    }
}
