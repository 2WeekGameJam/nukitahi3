using Microsoft.Xna.Framework;
using Nukitashi2.Actor.Blocks;
using Nukitashi2.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukitashi2.Actor
{
    class Shoot : GameObject
    {
        Vector2 velocity;
        public Shoot(Vector2 pos, GameDevice gameDevice)
            : base("blockkusa", pos, 8, 16, gameDevice)
        {
            isDeadFlag = false;
        }
        public Shoot(Shoot other)
            : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new Shoot(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is B || gameObject is B2 || gameObject is Enemy)
            {
                isDeadFlag = true;
            }
        }

        public override void Updata(GameTime gameTime)
        {
            velocity.Y += 0.10f;
            velocity.Y = (velocity.Y > 16.0f) ? (16.0f) : (velocity.Y);
            velocity.X = 10.0f;
            position += velocity;
        }
    }
}