using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Device;

namespace Nukitashi2.Actor
{
    class Player : GameObject
    {
        //リスポーン用Cloneセット
        private string nameC;
        private Vector2 positionC;
        private GameDevice gameDeviceC;

        private Vector2 velocity;
        private float speed;

        private bool flontR;//true→右向いとる

        private bool isJump;
        private float jumpVel;

        /// <summary>
        /// Playerの生成
        /// </summary>
        /// <param name="name">描画ファイル</param>
        /// <param name="position">初期位置</param>
        /// <param name="gameDevice">ゲームデバイス</param>
        public Player(string name, Vector2 position, GameDevice gameDevice) : base(name, position, 64, 128, gameDevice)
        {
            nameC = name;
            positionC = position;
            gameDeviceC = gameDevice;

            velocity = new Vector2(0.0f, 0.0f);
            speed = 3.0f;

            flontR = true;

            isJump = false;
            jumpVel = 3.0f;
        }

        /// <summary>
        /// Playerリスポーン用
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Player(nameC, positionC, gameDeviceC);
        }

        public override void Hit(GameObject gameObject)
        {
            #region ブロックとの判定(仮)
            if (gameObject is B)
            {
                if (CheckDirection(gameObject) is Direction.Bottom)
                {
                    jumpVel=0.0f;
                }
                else if(CheckDirection(gameObject) is Direction.Top)
                {
                    isJump=false;
                    jumpVel=3.0f;
                }
                else if(CheckDirection(gameObject) is Direction.Left)
                {
                    if (velocity.X > 0.0f)
                    {
                        velocity.X = 0.0f;
                    }
                }
                else
                {
                    if (velocity.X< 0.0f)
                    {
                        velocity.X = 0.0f;
                    }
                }
            }
            #endregion
        }

        public override void Updata(GameTime gameTime)
        {
            //Inputの仕様上アローキーのみ
            velocity.X = velocity.X + Input.Velocity().X * speed;

            if (Input.GetKeyState(Keys.Right))
            {
                flontR = true;
            }
            if (Input.GetKeyState(Keys.Left))
            {
                flontR = false;
            }

            if (Input.GetKeyTrigger(Keys.Z))
            {
                if (flontR)
                {

                }
                else
                {

                }
            }

            #region ジャンプ関連
            if (Input.GetKeyTrigger(Keys.Space) && isJump is false)
            {
                isJump = true;
            }

            if (isJump)
            {
                velocity.Y = velocity.Y / 1.5f;
                velocity.Y = velocity.Y - jumpVel;
                if (jumpVel <= 3.0f && jumpVel >= -3.0f)
                {
                    jumpVel = jumpVel - 0.6f;
                }
            }
            #endregion

            position = position + velocity;

            velocity.X = 0;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
    }
}
