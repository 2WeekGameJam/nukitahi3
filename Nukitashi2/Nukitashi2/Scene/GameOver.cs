using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Def;
using Nukitashi2.Device;

namespace Nukitashi2.Scene
{
    class GameOver : IScene
    {
        bool isEndFlag;

        public GameOver()
        {
            isEndFlag = false;
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("en", Vector2.Zero);
            renderer.DrawTexture("pushspace", new Vector2(Screen.Width / 2, Screen.Height / 1.5f));
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }
        }
    }
}
