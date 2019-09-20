using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Device;

namespace Nukitashi2.Actor.Blocks
{
    class Weapon : GameObject
    {
        public Weapon(Vector2 pos, GameDevice gameDevice)
           : base("Weapons_store", pos, 128, 128, gameDevice)
        { }
        public Weapon(Weapon other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new Weapon(this);
        }

        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Updata(GameTime gameTime)
        {
            
        }
    }
}
