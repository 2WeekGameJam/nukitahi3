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
        Shoot shoot;
        GameObjectManager gameObjectManager;
        List<Enemy> enemies;
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
            enemies = new List<Enemy>();
            player = new Player(new Vector2(32 * 2, 32 * 10), GameDevice.Instance());
            gameObjectManager.Add(map);
            gameObjectManager.Add(player);
            enemies = map.EnemyAdd();
            for(int i=0;i<enemies.Count;i++)
            {
                gameObjectManager.Add(enemies[i]);
            }
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
            if(Input.GetKeyTrigger(Keys.Z) && player.ReturnHave())
            {
                if (player.CheckFront())
                {
                    shoot = new Shoot(player.GetPosition() + new Vector2(69, 0), GameDevice.Instance(), player.CheckFront());
                }
                else
                {
                    shoot = new Shoot(player.GetPosition() + new Vector2(-13, 0), GameDevice.Instance(), player.CheckFront());
                }
                gameObjectManager.Add(shoot);
                player.DontHave();
            }
            if(player.GetNext())
            {
                isEndFlag = true;
            }

            if (enemies.Count <= 0)
                isEndFlag = true;

            if (Input.GetKeyTrigger(Keys.X))
            {
                enemies.RemoveAt(0);
            }


            gameObjectManager.Update(gameTime);
        }
    }
}
