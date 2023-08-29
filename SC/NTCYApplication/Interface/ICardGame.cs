using NTCYApplication.Models.CardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface ICardGame 
    {  
        string CreateKittyCollection(Dictionary<string, object> CardIGameDictionary);
        string EditKittyCollection(Dictionary<string, object> CardIGameDictionary);
        Dictionary<string, object> ViewKittYCollection(int GameId);  
        List<CardRoomGame> ViewAllKittyCollection(string Status); 
        string DeleteCardGame(int GameId);
        List<CardRoomGame> ViewPaidKittyCollection(string Status);
        List<CardRoomGame> SearchMemberById(string MemberShipNo,string Status);
    }  
}
 