using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Device;
using Nukitashi2.Def;
using Nukitashi2.Utility;

namespace Nukitashi2.Scene
{
    class Title : IScene
    {
        private bool isEndFlag;
        private Vector2 position;
        private bool downFlag;
        private Motion motion;
        private int scWidth;
        private int scHeight;
        private int count = 10;
        private int speed = 3;

        public Title()
        {
            isEndFlag = false;
            
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("haikei", Vector2.Zero);
            renderer.DrawTexture("pushspace", new Vector2(Screen.Width / 2, Screen.Height / 1.5f));
            renderer.DrawTexture("Title_Boss", position, motion.DrawingRange());
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            scWidth = Def.Screen.Width;
            scHeight = Def.Screen.Height;
            position = new Vector2(scWidth / 2 + scWidth / 4, scHeight / 2 + scHeight / 4);
            downFlag = true;
            motion = new Motion();
            for (int i = 0; i < 2; i++) 
            {
                motion.Add(i, new Rectangle(134 * i, 0, 128, 128));
            }
            motion.Initialize(new Range(0, 1), new CountDownTimer(0.2f));
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            Do();
            if(Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
        }

        public void Do()
        {
            Calculation();
            if (!(count <= 0)) return;
            switch(downFlag)
            {
                case true:downFlag = false; count = 10; break;
                case false:downFlag = true; count = 10; break;
            }
        }
        public void Calculation()
        {
            count--;
            if (downFlag)
                position -= new Vector2(0, 1) * speed;
            else
                position += new Vector2(0, 1) * speed;
        }
    }
}
