using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Nukitashi2.Device;

namespace Nukitashi2.Actor
{
    class Player : GameObject
    {
        //リスポーン用Cloneセット
        private string nameC;
        private Vector2 positionC;
        private GameDevice gameDeviceC;

        private float speed;

        /// <summary>
        /// Playerの生成
        /// </summary>
        /// <param name="name">描画ファイル</param>
        /// <param name="position">初期位置</param>
        /// <param name="gameDevice">ゲームデバイス</param>
        public Player(string name,Vector2 position,GameDevice gameDevice):base(name,position,64,128,gameDevice)
        {
            nameC = name;
            positionC = position;
            gameDeviceC = gameDevice;

            speed = 3.0f;
        }

        /// <summary>
        /// Playerリスポーン用
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Player(nameC,positionC,gameDeviceC);
        }

        public override void Hit(GameObject gameObject)
        {

        }

        public override void Updata(GameTime gameTime)
        {
            //Inputの仕様上アローキーのみ
            position = position + Input.Velocity() * speed;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
    }
}
