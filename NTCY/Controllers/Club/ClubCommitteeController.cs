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

namespace NTCY.Controllers.Club
{
    public class ClubCommitteeController : Controller
    {
        private IClubCommitteeService _clubCommitteeService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public ClubCommitteeController(IClubCommitteeService clubCommitteeService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _clubCommitteeService = clubCommitteeService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateCommittee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCommittee(ClubCommittee clubCommittee)
        {
            _clubCommitteeService.Add(clubCommittee);
            TempData["msg"] = "<script>alert('Committee Added Succesfully');</script>";
            return RedirectToAction("ViewCommittee", "ClubCommittee");
        }

        [HttpGet]
        public IActionResult ViewCommittee()
        {
            try
            {
                List<ClubCommittee> clubCommittee = new List<ClubCommittee>();

                var clubCommitteeData = _clubCommitteeService.GetAll();
                foreach (var committee in clubCommitteeData)
                {
                    var committeeObj = _mapper.Map<ClubCommittee>(committee);
                    clubCommittee.Add(committeeObj);
                }
                if (clubCommittee != null)
                {
                    ViewBag.CommitteeList = clubCommittee;
                }
                else
                {
                    TempData["msg"] = "<script>alert('Committee Details Not Available');</script>";
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
        public IActionResult UpdateCommittee(int committeeId)
        {
            var committee = _clubCommitteeService.GetById(committeeId);
            return View(committee);
        }

        [HttpPost]
        public IActionResult UpdateCommittee(int committeeId, ClubCommittee clubCommittee)
        {
            _clubCommitteeService.Update(committeeId, clubCommittee);
            TempData["msg"] = "<script>alert('Committee Updated Succesfully');</script>";
            return RedirectToAction("ViewCommittee", "ClubCommittee");
        }

        public IActionResult DeleteCommittee(int committeeId)
        {
            _clubCommitteeService.Delete(committeeId);
            TempData["msg"] = "<script>alert('Committee Deleted Succesfully');</script>";
            return RedirectToAction("ViewCommittee", "ClubCommittee");
        }
    }
}
