using Microsoft.AspNetCore.Mvc;
using NTCY.Models.Club;
using NTCY.Services.Club;
using NTCY.Utils;
using AutoMapper;
using Azure.Storage.Blobs;
using NTCY.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Globalization;
using System.Diagnostics.Contracts;
using Microsoft.VisualBasic.FileIO;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using AutoMapper.Execution;

namespace NTCY.Controllers.Club
{
    public class ClubMasterController : Controller
    {
        private IClubService _clubService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public ClubMasterController(IClubService clubService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _clubService = clubService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateClub()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateClub(NTCY.Models.Club.Club club)
        {
            club.Logo = "Logo";
            _clubService.Add(club);
            TempData["msg"] = "<script>alert('Club Added Succesfully');</script>";
            return RedirectToAction("ViewClub", "ClubMaster");
        }

        [HttpGet]
        public IActionResult ViewClub()
        {
            try
            {
                List<NTCY.Models.Club.Club> clubMaster = new List<NTCY.Models.Club.Club>();

                var clubMasterData = _clubService.GetAll();
                foreach (var club in clubMasterData)
                {
                    var clubObj = _mapper.Map<Models.Club.Club>(club);
                    clubMaster.Add(clubObj);
                }
                if(clubMaster!=null)
                {
                    ViewBag.ClubList = clubMaster;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Club Details Not Available');</script>";
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
        public IActionResult UpdateClub(int clubId)
        {
            var club = _clubService.GetById(clubId);
            ViewData["DateOfIncorporation"] = Convert.ToDateTime(club.DateOfIncorporation).ToString("yyyy-MM-dd");
            return View(club);
        }

        [HttpPost]
        public IActionResult UpdateClub(int clubId, Models.Club.Club clubMaster)
        {
            clubMaster.Logo = "Logo";
            _clubService.Update(clubId, clubMaster);
            TempData["msg"] = "<script>alert('Club Updated Succesfully');</script>";
            return RedirectToAction("ViewClub", "ClubMaster");
        }

        public IActionResult DeleteClub(int clubId)
        {
            _clubService.Delete(clubId);
            TempData["msg"] = "<script>alert('Club Deleted Succesfully');</script>";
            return RedirectToAction("ViewClub", "ClubMaster");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
