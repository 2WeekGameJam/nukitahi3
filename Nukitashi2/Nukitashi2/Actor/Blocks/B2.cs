using Microsoft.Xna.Framework;
using Nukitashi2.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukitashi2.Actor.Blocks
{
    class B2 : GameObject
    {
        public B2(Vector2 pos, GameDevice gameDevice)
            : base("blockkabe", pos, 64, 64, gameDevice)
        { }
        public B2(B2 other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new B2(this);
        }

        public override void Hit(GameObject gameObject)
        {

        }

        public override void Updata(GameTime gameTime)
        {

        }
    }
}