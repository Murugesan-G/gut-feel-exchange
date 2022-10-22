using Microsoft.AspNetCore.Mvc;
using NTCY.Models.Club;
using NTCY.Models.Users;
using NTCY.Services.Members;
using NTCY.Utils;
using NTCY.Services.MemberService;
using AutoMapper;
using Azure.Storage.Blobs;
using NTCY.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Globalization;
using System.Diagnostics.Contracts;
using Microsoft.VisualBasic.FileIO;
using static System.Net.WebRequestMethods;
using AutoMapper.Execution;
using System.Data;
using Member = NTCY.Models.Club.Member;


namespace NTCY.Controllers.Club
{
    public class MemberController : Controller
    {
        private IMemberService _memberService;

        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService,IMapper mapper,IOptions<AppSettings> appSettings)
        {
            _memberService = memberService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult CreateMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMember(Member member)
        {
            var membershipNo = _memberService.Add(member);
            TempData["msg"] = "<script>alert('Member Added Succesfully');</script>";
            return RedirectToAction("ViewMembers", "Member");
        }

        [HttpGet]
        public IActionResult UpdateMember(string membershipNo)
        {
            var member = _memberService.GetById(membershipNo);
            ViewData["DOB"] = Convert.ToDateTime(member.DOB).ToString("yyyy-MM-dd");
            ViewData["DOBSpouse"] = Convert.ToDateTime(member.DOBSpouse).ToString("yyyy-MM-dd");
            ViewData["DateOfMarriage"] = Convert.ToDateTime(member.DateOfMarriage).ToString("yyyy-MM-dd");
            ViewData["DOBChild1"] = Convert.ToDateTime(member.DOBChild1).ToString("yyyy-MM-dd");
            ViewData["DOBChild2"] = Convert.ToDateTime(member.DOBChild2).ToString("yyyy-MM-dd");
            ViewData["DOBChild3"] = Convert.ToDateTime(member.DOBChild3).ToString("yyyy-MM-dd");
            ViewData["DateOfInduction"] = Convert.ToDateTime(member.DateOfInduction).ToString("yyyy-MM-dd");
            ViewData["DateOfExpiry"] = Convert.ToDateTime(member.DateOfExpiry).ToString("yyyy-MM-dd");
            ViewData["Gender"] = member.Gender;
            ViewData["MaritalStatus"] = member.MaritalStatus;
            ViewData["MembershipCategory"] = member.MembershipCategory;
            ViewData["MemberShipStatus"] = member.MemberShipStatus;
            ViewData["MemberAlive"] = member.MemberAlive;
            return View(member);
        }

        [HttpPost]
        public IActionResult UpdateMember(string membershipNo, Member member)
        {
            _memberService.Update(membershipNo, member);
            TempData["msg"] = "<script>alert('Member Updated Succesfully');</script>";
            return RedirectToAction("ViewMembers", "Member");
        }

        [HttpGet]        
        public IActionResult ViewMembers()
        {
            try
            {
                List<Member> memberList = new List<Member>();

                var members = _memberService.GetAll();
                foreach (var member in members)
                {
                    var memberobj = _mapper.Map<Member>(member);
                    //memberobj.PhotoExist = _memberService.CheckMemberPhotoExists(member.MembershipNo, FileType.MEMBER_PHOTO);
                    memberList.Add(memberobj);
                }
                if(memberList!=null)
                {
                    ViewBag.MemberList = memberList;
                }
                else
                {
                    ViewBag.message = "Member details Not Available";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.innerEx = ex.InnerException.Message;
            }
            return View();
        }
        public IActionResult DeleteMembers(string membershipNo)
        {
            _memberService.Delete(membershipNo);
            TempData["msg"] = "<script>alert('Member Deleted Succesfully');</script>";
            return RedirectToAction("ViewMembers", "Member");
        }
        [HttpGet]
        public async Task<IActionResult> GetMemberPhotos(string membershipno)
        {
            try
            {
                var fileUrl = new Dictionary<string, string>();

                BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");


                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipno);
                if (containerClient.Exists())
                {
                    var memberBlob = containerClient.GetBlobClient(FileType.MEMBER_PHOTO.ToString() + ".jpeg");
                    if (memberBlob.Exists())
                        fileUrl.Add("memberPhoto", "/Member/GetPhotos/" + membershipno + "/" + FileType.MEMBER_PHOTO.ToString() + ".jpeg");

                    var spouseBlob = containerClient.GetBlobClient(FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
                    if (spouseBlob.Exists())
                        fileUrl.Add("spousePhoto", "/Member/GetPhotos/" + membershipno + "/" + FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
                    var child1Blob = containerClient.GetBlobClient(FileType.CHILD1_PHOTO.ToString() + ".jpeg");
                    if (child1Blob.Exists())
                        fileUrl.Add("child1Photo", "/Member/GetPhotos/" + membershipno + "/" + FileType.CHILD1_PHOTO.ToString() + ".jpeg");
                    var child2Blob = containerClient.GetBlobClient(FileType.CHILD2_PHOTO.ToString() + ".jpeg");
                    if (child2Blob.Exists())
                        fileUrl.Add("child2Photo", "/Member/GetPhotos/" + membershipno + "/" + FileType.CHILD2_PHOTO.ToString() + ".jpeg");
                    var child3Blob = containerClient.GetBlobClient(FileType.CHILD3_PHOTO.ToString() + ".jpeg");
                    if (child3Blob.Exists())
                        fileUrl.Add("child3Photo", "/Member/GetPhotos/" + membershipno + "/" + FileType.CHILD3_PHOTO.ToString() + ".jpeg");
                }
                var result = fileUrl;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult GetMemberPhotoContent(string membershipNo, string doctype)
        {
            var memStream = new MemoryStream();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipNo);

            if (doctype == "memberPhoto")
            {
                var memberBlob = containerClient.GetBlobClient(FileType.MEMBER_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    memberBlob.DownloadTo(memStream);
                return File(memStream.ToArray(), memberBlob.GetProperties().Value.ContentType, FileType.MEMBER_PHOTO.ToString() + ".jpeg");
            }
            else if (doctype == "spousePhoto")
            {
                var memberBlob = containerClient.GetBlobClient(FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    memberBlob.DownloadTo(memStream);
                return File(memStream.ToArray(), memberBlob.GetProperties().Value.ContentType, FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
            }
            else if (doctype == "child1Photo")
            {
                var memberBlob = containerClient.GetBlobClient(FileType.CHILD1_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    memberBlob.DownloadTo(memStream);
                return File(memStream.ToArray(), memberBlob.GetProperties().Value.ContentType, FileType.CHILD1_PHOTO.ToString() + ".jpeg");
            }
            else if (doctype == "child2Photo")
            {
                var memberBlob = containerClient.GetBlobClient(FileType.CHILD2_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    memberBlob.DownloadTo(memStream);
                return File(memStream.ToArray(), memberBlob.GetProperties().Value.ContentType, FileType.CHILD2_PHOTO.ToString() + ".jpeg");
            }
            else if (doctype == "child3Photo")
            {
                var memberBlob = containerClient.GetBlobClient(FileType.CHILD3_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    memberBlob.DownloadTo(memStream);
                return File(memStream.ToArray(), memberBlob.GetProperties().Value.ContentType, FileType.CHILD3_PHOTO.ToString() + ".jpeg");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddMemberPhotos(string membershipNo,IFormFile memberPhoto,IFormFile spousePhoto,IFormFile child1Photo,IFormFile child2Photo,IFormFile child3Photo)
        {
            _memberService.saveFile(membershipNo, FileType.MEMBER_PHOTO, memberPhoto);
            _memberService.saveFile(membershipNo, FileType.SPOUSE_PHOTO, spousePhoto);
            _memberService.saveFile(membershipNo, FileType.CHILD1_PHOTO, child1Photo);
            _memberService.saveFile(membershipNo, FileType.CHILD2_PHOTO, child2Photo);
            _memberService.saveFile(membershipNo, FileType.CHILD3_PHOTO, child3Photo);

            return View(new { message = "Member documents added successfully" });
        }

        public JsonResult GetMembers(string Prefix)
        {
            // Generate Member List
            List<Member> memberList = new List<Member>();
            DataSet ds = _memberService.GetMembers(Prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                memberList.Add(new Member
                {
                    MembershipNo = dr["MemberShipNo"].ToString(),
                    MemberName = dr["MemberName"].ToString()
                });
            }
            return Json(memberList);
        }
    }
}
