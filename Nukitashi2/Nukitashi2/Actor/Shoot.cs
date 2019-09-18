using Microsoft.Xna.Framework;
using Nukitashi2.Actor.Blocks;
using Nukitashi2.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nukitashi2.Def;

namespace Nukitashi2.Actor
{
    class Shoot : GameObject
    {
        Vector2 velocity;
        bool drop;

        public Shoot(Vector2 pos, GameDevice gameDevice)
            : base("blockkusa", pos, 8, 16, gameDevice)
        {
            isDeadFlag = false;
            drop = false;
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
                hitBlock(gameObject);
            }
        }

        public override void Updata(GameTime gameTime)
        {
            velocity.Y += 0.10f;
            velocity.Y = (velocity.Y > 16.0f) ? (16.0f) : (velocity.Y);
            velocity.X = 10.0f;

            if (position.X <= 0 && velocity.X <= 0.1 || position.X >= Screen.Width - width && velocity.X >= -0.1)
            {
                velocity.X = -velocity.X;
            }

            position += velocity;
        }

        private void hitBlock(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (dir == Direction.Top)
            {

            }
            else if (dir == Direction.Bottom)
            {

            }
            else
            {
                velocity.X = -velocity.X;
            }
        }
    }
}