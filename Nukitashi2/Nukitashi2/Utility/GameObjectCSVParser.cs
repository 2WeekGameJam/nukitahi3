using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nukitashi2.Actor;
using Nukitashi2.Device;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Nukitashi2.Utility
{
    class GameObjectCSVParser
    {
        private CSVReader csvReader;
        private List<GameObject> gameObjects;
        private GameObjectManager mediator;
        private delegate GameObject iFunction
            (List<string> data);
        private Dictionary<string, iFunction> functionTable;
        public GameObjectCSVParser()
        {
            csvReader = new CSVReader();
            gameObjects = new List<GameObject>();
            functionTable = new Dictionary
                <string, iFunction>();
            //functionTable.Add
            //    ("Block", NewBlock);
        }
        public List<GameObject> Parse(string filename, string path = "./")
        {
            gameObjects.Clear();
            csvReader.Read(filename, path);
            var data = csvReader.GetData();
            foreach (var line in data)
            {
                if (line[0] == "#")
                {
                    continue;
                }
                if (line[0] == "")
                {
                    continue;
                }
                var temp = line.ToList();
                temp.RemoveAll(s => s == "");
                gameObjects.Add(functionTable[line[0]](temp));
            }
            return gameObjects;
        }
        //private Block NewBlock(List<string> data)
        //{
        //    Debug.Assert(
        //        (data.Count == 3),
        //        "CSVデータを確認してください");
        //    return new Block(
        //         new Vector2(float.Parse(data[1]), float.Parse(data[2])) * 32,
        //         GameDevice.Instance());
        //}
    }
}
