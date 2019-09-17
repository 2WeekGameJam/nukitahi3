using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Device;

namespace Nukitashi2.Actor.Blocks
{
    class NextSpace : GameObject
    {
        public NextSpace(Vector2 position, GameDevice gameDevice)
             : base("", position, 32, 32, gameDevice)
        {
            
        }
        public NextSpace(NextSpace other)
            : this(other.position, other.gameDevice)
        {  }
        public override object Clone()
        {
            return new NextSpace(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Updata(GameTime gameTime)
        {
            
        }
    }
}
