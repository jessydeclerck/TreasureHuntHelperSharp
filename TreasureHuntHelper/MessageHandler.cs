using Cookie.API.Protocol;
using Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntHelper;
using Cookie.API.Gamedata;
using Cookie.API.Gamedata.D2o.other;
using Cookie.API.Gamedata.D2o;
using TreasureHuntHelper.Web;
using Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay.TreasureHunt;
using Cookie.API.Protocol.Network.Types.Game.Context.Roleplay.TreasureHunt;
using Cookie.API.Gamedata.D2i;
using Cookie.API.Protocol.Network.Types.Game.Context.Roleplay;

namespace treasureHuntHelper
{
    public static class MessageHandler
    {

        public static void handleMessage(NetworkMessage message)
        {
            //Console.WriteLine("handling message");
            switch (message.MessageID)
            {
                case 226:
                    handleMapInfo(message);
                    break;
                case 6491:
                    handleTreasureHuntAvailableRetryCountUpdateMessage(message);
                    break;
                case 6509:
                    hanleTreasureHuntDigRequestAnswerFailedMessage(message);
                    break;
                case 6484:
                    handleTreasureHuntDigRequestAnswerMessage(message);
                    break;
                case 6485:
                    handleTreasureHuntDigRequestMessage(message);
                    break;
                case 6483:
                    handleTreasureHuntFinishedMessage(message);
                    break;
                case 6510:
                    handleTreasureHuntFlagRemoveRequestMessage(message);
                    break;
                case 6507:
                    handleTreasureHuntFlagRequestAnswerMessage(message);
                    break;
                case 6508:
                    handleTreasureHuntFlagRequestMessage(message);
                    break;
                case 6487:
                    handleTreasureHuntGiveUpRequestMessage(message);
                    break;
                case 6499:
                    handleTreasureHuntLegendaryRequestMessage(message);
                    break;
                case 6486:
                    handleTreasureHuntMessage(message);
                    break;
                case 6489:
                    handleTreasureHuntRequestAnswerMessage(message);
                    break;
                case 6488:
                    handleTreasureHuntRequestMessage(message);
                    break;
                case 6498:
                    handleTreasureHuntShowLegendaryUIMessage(message);
                    break;
                default:
                    break;
            }
        }

        private static void handleMapInfo(NetworkMessage message)
        {
            showMessageInfos(message);
            MapComplementaryInformationsDataMessage mapMessage = (MapComplementaryInformationsDataMessage)message;
            //Position position = jsonManager.getPosition(mapMessage.MapId);
            currentMap = D2OParsing.GetMapCoordinates(mapMessage.MapId);
            Console.Write("Carte actuelle : " + currentMap.x + "," + currentMap.y+"\n");

            foreach (GameRolePlayActorInformations actor in mapMessage.Actors)
            {
                if (actor.TypeID == 471)
                {
                    GameRolePlayTreasureHintInformations npc = (GameRolePlayTreasureHintInformations)actor;
                    Console.WriteLine(npc.NpcId + " " + npcIdToFind);
                    if (npc.NpcId == npcIdToFind)
                        Console.WriteLine("Phorreur trouvé !");
                }
                
            }

            

        }
        private static void showMessageInfos(NetworkMessage message)
        {
            //Console.WriteLine(message.MessageID + " " + message.GetType().Name);
        }

        private static void handleTreasureHuntShowLegendaryUIMessage(NetworkMessage message)
        {
            showMessageInfos(message);
        }

        private static void handleTreasureHuntRequestMessage(NetworkMessage message)
        {
            showMessageInfos(message);
        }

        private static void handleTreasureHuntRequestAnswerMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static Point currentMap;

        private static int npcIdToFind;

        private static int sizeStepList = 1;
        private static void handleTreasureHuntMessage(NetworkMessage message)
        {
            Point startMap;
            showMessageInfos(message);

            TreasureHuntMessage treasureHuntMessage = (TreasureHuntMessage)message;

            TreasureHuntStep lastStep = treasureHuntMessage.KnownStepsList.Last();

            if (treasureHuntMessage.KnownStepsList.Count == 1)
                startMap = D2OParsing.GetMapCoordinates(treasureHuntMessage.StartMapId);
            else
                startMap = currentMap;

            Console.Write(" " + startMap.x + "," + startMap.y + "\n");

            try
            {
                TreasureHuntStepFollowDirectionToHint stepToFollow = (TreasureHuntStepFollowDirectionToHint)lastStep;
                npcIdToFind = stepToFollow.NpcId;
                Console.WriteLine("On cherche : " + D2OParsing.GetNpcName(stepToFollow.NpcId)); 
            }
            catch (Exception e)
            {
                TreasureHuntStepFollowDirectionToPOI stepToFollow = (TreasureHuntStepFollowDirectionToPOI)lastStep;
                WebService.getData(startMap, stepToFollow.Direction, stepToFollow.PoiLabelId);
                //Console.WriteLine("On cherche : " + D2OParsing.GetPoiName(stepToFollow.PoiLabelId));
            }

        }

        private static void handleTreasureHuntLegendaryRequestMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntGiveUpRequestMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntFlagRequestMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntFlagRequestAnswerMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntFlagRemoveRequestMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntFinishedMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntDigRequestMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntDigRequestAnswerMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void hanleTreasureHuntDigRequestAnswerFailedMessage(NetworkMessage message)
        {

            showMessageInfos(message);
        }

        private static void handleTreasureHuntAvailableRetryCountUpdateMessage(NetworkMessage message)
        {
            showMessageInfos(message);
        }
    }
}
