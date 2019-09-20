using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nukitashi2.Actor.Blocks;
using Nukitashi2.Device;
using Nukitashi2.Utility;

namespace Nukitashi2.Actor
{
    class Enemy : GameObject
    {
        Vector2 velocity;
        Motion motion;
        public Enemy(Vector2 position, GameDevice gameDevice)
               : base("EnemyWark", position, 64, 64, gameDevice)
        {
            motion = new Motion();
            for (int i = 0; i < 2; i++)
            {
                motion.Add(i, new Rectangle(32, 32 * (i / 2), 32, 32));
            }
            motion.Initialize(new Range(0, 1), new CountDownTimer(1.0f));
        }
        public Enemy(Enemy other)
           : this(other.position, other.gameDevice)
        { }
        public override object Clone()
        {
            return new Enemy(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is B || gameObject is B2)
            {
                hitBlock(gameObject);
            }
        }
        private void UpdateMotion()
        {
            if (velocity.Length() <= 0.0f)
            { return; }
            else if (velocity.X > 0.0f)
            {
                motion.Initialize(new Range(0, 0), new CountDownTimer());
            }
            else if (velocity.X < 0.0f)
            {
                motion.Initialize(new Range(1, 1), new CountDownTimer());
            }
        }
        public override void Updata(GameTime gameTime)
        {
            velocity.Y += 0.2f;
            velocity.Y = (velocity.Y > 16.0f) ? (16.0f) : (velocity.Y);
            position = position + velocity;
        }
        private void hitBlock(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (dir == Direction.Top)
            {
                if (position.Y > 0.0f)
                {
                    position.Y = gameObject.getRectangle().Top - height;
                    velocity.Y = 0.0f;
                }
            }
            else if (dir == Direction.Right)
            {
                position.X = gameObject.getRectangle().Right;
            }
            else if (dir == Direction.Left)
            {
                position.X = gameObject.getRectangle().Left - width;
            }
            else if (dir == Direction.Bottom)
            {
                position.Y = gameObject.getRectangle().Bottom;
                velocity.Y = 0.0f;
            }
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position/*, motion.DrawingRange()*/);
        }
    }
}
