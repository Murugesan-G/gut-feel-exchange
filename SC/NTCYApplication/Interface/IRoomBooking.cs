using NTCYApplication.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface IRoomBooking
    {


        int Save();
        string AllocateRoom(Dictionary<string, object> RoomBookingIDictionary); 

        string UnallocateRoom(Dictionary<string, object> RoomBookingIDictionary); 
        Dictionary<string, object> ViewAvailableRooms();
        string DeleteRoomBooking(int BookingId,string RoomNo);  
        List<RoomBooking> ViewAllRoomBooking();
        string UpdateRoomBookingDetails(Dictionary<string, object> RoomBookingDict);
        Dictionary<string, object> ViewRoomBooking(int BookingId);
    }  
}
