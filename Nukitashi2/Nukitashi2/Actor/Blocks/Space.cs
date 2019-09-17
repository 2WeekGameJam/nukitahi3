using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Device;

namespace Nukitashi2.Actor.Block
{
    class Space : GameObject
    {
        public Space(Vector2 position, GameDevice gameDevice)
            : base("", position, 32, 32, gameDevice)
        {
        }
        public Space(Space other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new Space(this);
        }

        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Updata(GameTime gameTime)
        {
            
        }
    }
}
