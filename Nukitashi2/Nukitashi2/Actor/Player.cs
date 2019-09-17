﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukitashi2.Actor
{
    class Player:GameObject
    {
        private Vector2 velocity;
        private bool isJump;
        private IGameObjectMediator mediator;
        private Vector2 slideModify;
        private bool gool;
        //private Motion motion;
        public Player(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
               : base("player", position, 32, 32, gameDevice)
        {
            velocity = Vector2.Zero;
            isJump = true;
            this.mediator = mediator;
            slideModify = Vector2.Zero;
            gool = false;
            //motion = new Motion();
            //for (int i = 0; i < 2; i++)
            //{
            //    motion.Add(i, new Rectangle(32, 32 * (i / 2), 32, 32));
            //}
            //motion.Initialize(new Range(0, 1), new CountDownTimer(1.0f));
        }
        public Player(Player other)
            : this(other.position, other.gameDevice, other.mediator)
        { }
        public override object Clone()
        {
            return new Player(this);
        }
        public override void Hit(GameObject gameObject)
        {
            if (gameObject is B )
            {
                hitBlock(gameObject);
            }
        }
        public override void Updata(GameTime gameTime)
        {
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

            velocity.X = Input.Velocity().X * speed ;
            position = position + velocity + slideModify;
            //UpdateMotion();
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
            renderer.DrawTexture(name, position/*, motion.DrawingRange()*/);
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
    }
}