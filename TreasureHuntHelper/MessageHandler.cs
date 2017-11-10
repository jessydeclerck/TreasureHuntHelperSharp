using Cookie.API.Protocol;
using Cookie.API.Protocol.Network.Messages.Game.Context.Roleplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHuntHelper;

namespace treasureHuntHelper
{
    public static class MessageHandler
    {
        private static JsonManager jsonManager = new JsonManager();

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
            Console.Write("MapId : " + mapMessage.MapId);
            Position position = jsonManager.getPosition(mapMessage.MapId);
            Console.Write(" " + position.X + "," + position.Y+"\n");

        }
        private static void showMessageInfos(NetworkMessage message)
        {
            Console.WriteLine(message.MessageID + " " + message.GetType().Name);
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

        private static void handleTreasureHuntMessage(NetworkMessage message)
        {

            showMessageInfos(message);
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
