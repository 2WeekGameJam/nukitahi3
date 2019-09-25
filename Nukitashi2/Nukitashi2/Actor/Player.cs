using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Device;
using Nukitashi2.Def;
using Nukitashi2.Actor.Blocks;
using Nukitashi2.Utility;

namespace Nukitashi2.Actor
{
    class Player : GameObject
    {
        private Vector2 velocity;
        private bool isJump;//ジャンプしているかどうか
        private bool goal;
        private Motion motion;
        private Motion rMotion;
        private Motion aMotion;
        private bool iHave;//剣を持っているか
        private bool frontR;//右を向いているか

        private bool stndFlag;//動いているかどうか(動いていなければtrue)
        public bool attackFlag;//攻撃しているかどうか

        public Player(Vector2 position, GameDevice gameDevice)
               : base("PlayerStand", position, 32, 32, gameDevice)
        {
            stndFlag = false;
            attackFlag = false;
            velocity = Vector2.Zero;
            isJump = true;
            goal = false;
            iHave = true;
            frontR = true;
            motion = new Motion();
            motion.Add(0, new Rectangle(0, 0, 64, 128));
            motion.Add(1, new Rectangle(64, 0, 64, 128));
            motion.Initialize(new Range(0, 1), new CountDownTimer(0.2f));

            rMotion = new Motion();
            rMotion.Add(0, new Rectangle(0, 0, 64, 128));
            rMotion.Add(1, new Rectangle(64, 0, 64, 128));
            rMotion.Add(2, new Rectangle(128, 0, 64, 128));
            rMotion.Add(3, new Rectangle(192, 0, 64, 128));
            rMotion.Initialize(new Range(0, 3), new CountDownTimer(0.2f));

            aMotion = new Motion();
            aMotion.Add(0, new Rectangle(0, 0, 64, 128));
            aMotion.Add(1, new Rectangle(64, 0, 64, 128));
            aMotion.Add(2, new Rectangle(128, 0, 64, 128));
            aMotion.Initialize(new Range(0, 2), new CountDownTimer(0.4f));
        }

        public Player(Player other)
            : this(other.position, other.gameDevice)
        { }

        public override object Clone()
        {
            return new Player(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is B||gameObject is B2)
            {
                hitBlock(gameObject);
            }
            if(gameObject is Shoot)
            {
                iHave = true;
            }
            if (gameObject is NextSpace)
                goal = true;
        }

        public override void Updata(GameTime gameTime)
        {
            motion.Update(gameTime);
            rMotion.Update(gameTime);
            aMotion.Update(gameTime);
            if ((isJump == false) &&
                (Input.GetKeyTrigger(Keys.Space)))
            {
                //8.0
                velocity.Y = -6.0f;
                isJump = true;
            }
            else
            {
                //0.4
                velocity.Y += 0.2f;
                velocity.Y = (velocity.Y > 16.0f) ? (16.0f) : (velocity.Y);
            }

            float speed = 4.0f;
            //if(Input.GetKeyTrigger(Keys.X))
            //{
            //    isDeadFlag = true;
            //}
            velocity.X = Input.Velocity().X * speed;
            stndFlag = false;

            if (velocity.X >0.0f)
            {
                frontR = true;
            }
            if (velocity.X < 0.0f)
            {
                frontR = false;
            }

            if (position.X <= 0&&velocity.X<=0.1 || position.X >= Screen.Width - width&&velocity.X>=-0.1)
            {
                velocity.X = 0;
            }

            

            position = position + velocity;
            //UpdateMotion();
            
            if (velocity.X != 0) return;
            stndFlag = true;
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
                    isJump = false;
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
                if (isJump)
                {
                    velocity.Y = 0.0f;
                }
            }
        }

        public override void Draw(Renderer renderer)
        {
            if (isJump)
            {
                renderer.DrawTexture("PlayerJump", position);
            }
            else if(stndFlag)
            {
                renderer.DrawTexture(name, position, motion.DrawingRange());
            }
            else if(attackFlag)
            {
                renderer.DrawTexture("PlayerAttack", position, aMotion.DrawingRange());
                attackFlag = false;
            }
            else
            {
                renderer.DrawTexture("PlayerRun", position, rMotion.DrawingRange());
            }
            //renderer.DrawTexture(name, position, motion.DrawingRange());

            
        }

        //private void UpdateMotion()
        //{
        //    Vector2 velocity = Input.Velocity();
        //    if (velocity.Length() <= 0.0f)
        //    { return; }
        //    else if (velocity.X > 0.0f)
        //    {
        //        motion.Initialize(new Range(0, 0), new CountDownTimer());
        //    }
        //    else if (velocity.X < 0.0f)
        //    {
        //        motion.Initialize(new Range(1, 1), new CountDownTimer());
        //    }
        //}

        public bool GetNext()
        {
            return goal;
        }

        public void DontHave()
        {
            iHave = false;
        }

        public bool ReturnHave()
        {
            return iHave;
        }

        public bool CheckFront()
        {
            return frontR;
        }
    }
}
