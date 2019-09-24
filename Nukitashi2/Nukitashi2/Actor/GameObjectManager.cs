using Nukitashi2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nukitashi2.Def;

namespace Nukitashi2.Actor
{
    class GameObjectManager : IGameObjectMediator
    {
        private List<GameObject> gameObjectList;
        private List<GameObject> addgameObjects;

        private Map map;
        public GameObjectManager()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<GameObject>();
            }
            if (addgameObjects != null)
            {
                addgameObjects.Clear();
            }
            else
            {
                addgameObjects = new List<GameObject>();
            }
        }
        public void Add(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addgameObjects.Add(gameObject);
        }
        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }
        private void hitToMap()
        {
            if (map == null)return;
            foreach (var obj in gameObjectList)
            {
                map.Hit(obj);
            }
        }
        private void hitToGameObject()
        {
            foreach (var c1 in gameObjectList)
            {
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        continue;
                    }
                    if (c1.IsCollision(c2))
                    {
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }
        private void removeDeadCharacters()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }
        public void Update(GameTime gameTime)
        {
            foreach (var c in gameObjectList)
            {
                c.Updata(gameTime);
            }
            foreach (var c in addgameObjects)
            {
                gameObjectList.Add(c);
            }
            addgameObjects.Clear();
            hitToMap();
            hitToGameObject();
            removeDeadCharacters();
        }
        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addgameObjects.Add(gameObject);
        }
        public GameObject GetPlayer()
        {
            GameObject find = gameObjectList.Find(c => c is Player);
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }
        public bool IsPlayerDead()
        {
            return false;
        }
        public int MapX()
        {
            return map.GetMapX();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObjectID id)
        {
            GameObject find = gameObjectList.Find(c => c.GetID() == id);
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }

        public List<GameObject> GetGameObjectList(GameObjectID id)
        {
            List<GameObject> list = gameObjectList.FindAll(c => c.GetID() == id);
            List<GameObject> aliveList = new List<GameObject>();
            foreach (var c in list)
            {
                if (c != null && !c.IsDead())
                {
                    aliveList.Add(c);
                }
            }
            return aliveList;
        }
    }
}

