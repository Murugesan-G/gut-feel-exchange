using Microsoft.AspNetCore.Mvc;
using NTCY.Models.RoomDetails;
using Microsoft.Extensions.Options;
using AutoMapper;
using NTCY.Utils;
using NTCY.Services.Table;
using NTCY.Entities;
using NTCY.Services.RoomDetail;

namespace NTCY.Controllers.RoomDetails
{
    public class RoomController : Controller
    {
        private IRoomDetails _roomService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public RoomController(IRoomDetails roomService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _roomService = roomService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult CreateRoom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRoom(Room room)
        {
            _roomService.Add(room);
            TempData["msg"] = "<script>alert('Room Added Succesfully');</script>";
            return RedirectToAction("ViewRoom", "Room");
        }
        [HttpGet]
        public IActionResult ViewRoom()
        {
            try
            {
                List<Room> room = new List<Room>();

                var roomData = _roomService.GetAll();
                foreach (var roomdata in roomData)
                {
                    var roomObj = _mapper.Map<Room>(roomdata);
                    room.Add(roomObj);
                }
                if (room != null)
                {
                    ViewBag.RoomList = room;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Room Details Not Available');</script>";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.innerEx = ex.InnerException.Message;
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateRoom(int roomId)
        {
            var roomData = _roomService.GetById(roomId);
            return View(roomData);
        }
        [HttpPost]
        public IActionResult UpdateRoom(int roomId, Room room)
        {
            _roomService.Update(roomId, room);
            TempData["msg"] = "<script>alert('Room Updated Succesfully');</script>";
            return RedirectToAction("ViewRoom", "Room");
        }
        public IActionResult DeleteRoom(int roomId)
        {
            _roomService.Delete(roomId);
            TempData["msg"] = "<script>alert('Room Deleted Succesfully');</script>";
            return RedirectToAction("ViewRoom", "Room");
        }
    }
}
