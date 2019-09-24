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

        bool isJump;
        bool jumpCharge;
        bool jumpAttack;
        float attackVel;
        float chargeTime;

        Player player;
        float distance;

        public Enemy(Vector2 position, GameDevice gameDevice,Player player)
               : base("EnemyWark", position, 32, 32, gameDevice)
        {
            isJump = true;
            jumpAttack = false;
            this.player = player;
            motion = new Motion();
            for (int i = 0; i < 2; i++)
            {
                motion.Add(i, new Rectangle(64, 64 * (i / 2), 64, 64));
            }
            motion.Initialize(new Range(0, 1), new CountDownTimer(1.0f));

            jumpCharge = true;
        }

        public Enemy(Enemy other)
           : this(other.position, other.gameDevice,other.player)
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
            #region ジャンプのクールタイム
            if (jumpCharge ==false)
            {
                chargeTime -= 0.1f;
            }

            if(chargeTime <=0)
            {
                jumpCharge = true;
            }
            #endregion

            #region プレイヤーを追ってください
            distance = position.X - player.GetPosition().X;
            if (distance > 0)
            {
                velocity.X = -1.0f;
            }
            if (distance < 0)
            {
                velocity.X = 1.0f;
            }
            #endregion

            #region ジャンプ攻撃
            if (jumpCharge == true && ((192 >= distance && distance >= 0) || (0 >= distance && distance >= -192)))
            {
                velocity.Y = -4.0f;
                isJump = true;
                jumpAttack = true;
                jumpCharge = false;
                chargeTime = 10.0f;
                attackVel = velocity.X;
            }

            if(jumpAttack)
            {
                velocity.X = attackVel * 4.0f;
            }
            #endregion

            #region 重力
            if (isJump)
            {
                velocity.Y += 0.2f;
            }
            #endregion

            position = position + velocity;

            UpdateMotion();
        }

        private void hitBlock(GameObject gameObject)
        {
            Direction dir = CheckDirection(gameObject);
            if (dir == Direction.Top)
            {
                position.Y = gameObject.getRectangle().Top - height;
                isJump = false;
                jumpAttack = false;
            }
            //else if (dir == Direction.Right)
            //{
            //    position.X = gameObject.getRectangle().Right;
            //}
            //else if (dir == Direction.Left)
            //{
            //    position.X = gameObject.getRectangle().Left - width;
            //}
            else if (dir == Direction.Bottom)
            {
                position.Y = gameObject.getRectangle().Bottom;
                velocity.Y = 0.0f;
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position,motion.DrawingRange());
        }
    }
}
