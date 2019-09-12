using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nukitashi2.Actor
{
    interface IGameObjectMediator
    {
        void AddGameObject(GameObject gameObject);
        GameObject GetPlayer();
        bool IsPlayerDead();
        GameObject GetGameObject(GameObjectID id);
        List<GameObject> GetGameObjectList(GameObjectID id);
        int MapX();
    }
}
