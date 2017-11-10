using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using TreasureHuntHelper;

namespace treasureHuntHelper
{
    class JsonManager
    {

        Dictionary<double, Position> mapPos = null;
        public JsonManager()
        {
            mapPos = new Dictionary<double, Position>();
            initMapPos("mapPositions.json");
            //initMapMessage("paquetsBase.json");
            //initMapMessage("infos241.json");
        }

        private void initMapPos(String fileName)
        {
            JObject jsonObject = JObject.Parse(File.ReadAllText(fileName));
            JToken mapInfos = jsonObject.GetValue("MapInfos");
            //foreach (JProperty property in mapInfos.Properties())
            //Console.WriteLine(property.Name);

            foreach (JToken mapInfo in mapInfos.Children())
                mapPos[double.Parse(mapInfo.SelectToken("id").ToString())] = new Position(int.Parse(mapInfo.SelectToken("posX").ToString()), int.Parse(mapInfo.SelectToken("posY").ToString()));
            
        }

        public Position getPosition(double idMap)
        {
        
                return mapPos[idMap];

        }

    }
}
