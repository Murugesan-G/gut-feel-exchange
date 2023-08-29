using NTCYApplication.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface IRoom
    {

        string CreateRoom(Dictionary<string, object> RoomsDictionary);
         
        string EditRoom(Dictionary<string, object> RoomsDictionary); 

        string DeleteRoom(int? RoomId);

        List<Room> ViewAllRoom();

        Dictionary<string, object> SelectRoom(int? RoomId);


    }
}
