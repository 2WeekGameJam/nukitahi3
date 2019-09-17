using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nukitashi2.Actor;
using Nukitashi2.Device;
using Nukitashi2.Utility;

namespace Nukitashi2.Scene
{
    class GamePlay : IScene
    {
        bool isEndFlag;
        Map map;
        Player player;
        Enemy enemy;
        Shoot shoot;
        GameObjectManager gameObjectManager;

        public GamePlay()
        {
            isEndFlag = false;
            gameObjectManager = new GameObjectManager();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("luiman", Vector2.Zero);
            map.Draw(renderer);
            gameObjectManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            gameObjectManager.Initialize();
            map = new Map(GameDevice.Instance());
            map.Load("map.csv","./csv/");
            player = new Player(new Vector2(32 * 2, 32 * 10), GameDevice.Instance(), gameObjectManager);
            enemy = new Enemy(new Vector2(32 * 20, 32 * 10), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(map);
            gameObjectManager.Add(player);
            gameObjectManager.Add(enemy);
            //GameObjectCSVParser parser = new GameObjectCSVParser();
            //var dataList = parser.Parse("");
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.GameOver;
        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            if(Input.GetKeyTrigger(Keys.Z))
            {
                shoot = new Shoot(player.GetPosition(), GameDevice.Instance());
                gameObjectManager.Add(shoot);
            }
            if(player.GetNext())
            {
                isEndFlag = true;
            }

            gameObjectManager.Update(gameTime);
        }
    }
}
