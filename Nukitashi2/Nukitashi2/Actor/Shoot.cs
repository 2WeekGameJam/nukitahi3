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
        bool frontR;
        bool reflect;

        public Shoot(Vector2 pos, GameDevice gameDevice,bool frontR)
            : base("weapon", pos, 8, 16, gameDevice)
        {
            isDeadFlag = false;
            drop = false;
            this.frontR = frontR;
            reflect = false;

            velocity = new Vector2(-10.0f, 0.0f);
            if (frontR)
                velocity = -velocity;
        }

        public Shoot(Shoot other)
            : this(other.position, other.gameDevice,true)
        { }

        public override object Clone()
        {
            return new Shoot(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is B || gameObject is B2)
            {
                hitBlock(gameObject);
            }
            if(gameObject is Player)
            {
                isDeadFlag = true;
            }
            if(gameObject is Enemy)
            {
                velocity.Y = -6.0f;
                reflect = true;
            }
        }

        public override void Updata(GameTime gameTime)
        {
            if (!drop)
            {
                if (reflect)
                {
                    if(frontR)
                    {
                        velocity.X = -1.0f;
                    }
                    else
                    {
                        velocity.X = 1.0f;
                    }

                    velocity.Y += 0.2f;
                }
                else
                {
                    velocity.Y += 0.10f;

                    if (position.X <= 0 && velocity.X <= 0.1 || position.X >= Screen.Width - width && velocity.X >= -0.1)
                    {
                        velocity.Y += 5.0f;
                        velocity.X = -velocity.X;
                    }
                }

                position += velocity;
            }
        }

        private void hitBlock(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (dir == Direction.Top)
            {
                drop = true;
            }
            else if (dir == Direction.Bottom)
            {

            }
            else
            {
                velocity.Y += 5.0f;
                velocity.X = -velocity.X;
            }
        }
    }
}