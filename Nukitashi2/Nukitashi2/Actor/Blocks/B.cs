using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Device;

namespace Nukitashi2.Actor
{
    class B : GameObject
    {
        public B(Vector2 pos,GameDevice gameDevice)
            :base("blockkusa", pos, 128, 128, gameDevice)
        { }
        public B(B other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new B(this);
        }

        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Updata(GameTime gameTime)
        {
            
        }
    }
}
