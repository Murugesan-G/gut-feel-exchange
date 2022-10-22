using AutoMapper;
using NTCY.Entities;
using NTCY.Models.Users;
using NTCY.Models.Club;
using NTCY.Database;
using NTCY.Services.MemberService;
using NTCY.Utils;
using NTCY.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using NTCY.Utils;
using System.Collections.Generic;
using System.Collections;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace NTCY.Services.Members
{
    public class MemberService : IMemberService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public MemberService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }
        public IEnumerable<MemberDTO> GetAll()
        {
            var members = _context.Members.FromSqlRaw("Select * From Member").OrderBy(m => m.MembershipNo).ToList();
            return members;
        }
        public MemberDTO GetById(string membershipNo)
        {
            return getMember(membershipNo);
        }
        public string Add(Member member)
        {
            if (_context.Members.Any(x => x.MembershipNo == member.MembershipNo))
                throw new AppException("Member '" + member.MembershipNo + "' is already exists");

            member.MemberPhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + "/MEMBER_PHOTO.jpeg";
            member.SpousePhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".A/SPOUSE_PHOTO.jpeg";
            member.Child1PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".B/CHILD1_PHOTO.jpeg";
            member.Child2PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".C/CHILD2_PHOTO.jpeg";
            member.Child3PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".D/CHILD3_PHOTO.jpeg";
            var memberDTO = _mapper.Map<MemberDTO>(member);
            _context.Members.Add(memberDTO);
            _context.SaveChanges();
            UploadPhoto(member.MembershipNo, "MEMBER_PHOTO", member.MemberPhotoPath,"ADD");
            //UploadPhoto(member.MembershipNo, "SPOUSE_PHOTO", member.SpousePhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD1_PHOTO", member.Child1PhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD2_PHOTO", member.Child2PhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD3_PHOTO", member.Child3PhotoPath, "ADD");

            return member.MembershipNo;
        }
        public void Update(string membershipNo, Member member)
        {
            var memberDto = getMember(membershipNo);

            if (memberDto.MembershipNo != member.MembershipNo && _context.Members.Any(x => x.MembershipNo == member.MembershipNo))
                throw new AppException("Member '" + member.MembershipNo + "' is already exists");

            member.MemberPhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + "/MEMBER_PHOTO.jpeg";
            member.SpousePhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".A/SPOUSE_PHOTO.jpeg";
            member.Child1PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".B/CHILD1_PHOTO.jpeg";
            member.Child2PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".C/CHILD2_PHOTO.jpeg";
            member.Child3PhotoPath = "https://ntcynkphoto.blob.core.windows.net/" + member.MembershipNo + ".D/CHILD3_PHOTO.jpeg";

            _mapper.Map(member, memberDto);
            _context.Members.Update(memberDto);
            _context.SaveChanges();

            //UploadPhoto(member.MembershipNo, "MEMBER_PHOTO", member.MemberPhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "SPOUSE_PHOTO", member.SpousePhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD1_PHOTO", member.Child1PhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD2_PHOTO", member.Child2PhotoPath, "ADD");
            //UploadPhoto(member.MembershipNo, "CHILD3_PHOTO", member.Child3PhotoPath, "ADD");

        }

        public void Delete(string membershipNo)
        {
            var member = getMember(membershipNo);
            _context.Members.Remove(member);
            _context.SaveChanges();
            //UploadPhoto(member.MembershipNo, "MEMBER_PHOTO", "", "DEL");
            //UploadPhoto(member.MembershipNo, "SPOUSE_PHOTO", "", "DEL");
            //UploadPhoto(member.MembershipNo, "CHILD1_PHOTO", "", "DEL");
            //UploadPhoto(member.MembershipNo, "CHILD2_PHOTO", "", "DEL");
            //UploadPhoto(member.MembershipNo, "CHILD3_PHOTO", "", "DEL");
        }

        private MemberDTO getMember(string membershipNo)
        {
            var member = _context.Members.FirstOrDefault(m => m.MembershipNo == membershipNo);
            if (member == null) throw new KeyNotFoundException("Member not found");
            return member;
        }
        
        public void saveFile(string membershipNo, FileType photoType, IFormFile inputFile)
        {
            if (inputFile == null)
            {
                return;
            }
            BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");


            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipNo);
            if (!containerClient.Exists())
                containerClient = serviceClient.CreateBlobContainer(membershipNo);

            string path = photoType.ToString() + ".jpeg";
            BlobClient blob = containerClient.GetBlobClient(path);
            if (blob.Exists())
            {
                blob.Delete();
            }
            using (Stream stream = inputFile.OpenReadStream())
            {
                blob.Upload(stream);

            }
        }

        public bool CheckMemberPhotoExists(string membershipNo, FileType fileType)
        {
            try
            {
                var memStream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipNo);
                var memberBlob = containerClient.GetBlobClient(FileType.MEMBER_PHOTO.ToString() + ".jpeg");
                if (memberBlob.Exists())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UploadPhoto(string membershipNo, string photoType, string photoPath, string sMode)
        {
            //BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");
            BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");


            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipNo);
            if (!containerClient.Exists())
                containerClient = serviceClient.CreateBlobContainer(membershipNo,Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            string path;
            BlobClient blob;
            //Uploading Member Photo
            if (photoPath.ToString() != "")
            {
                path = photoType + ".jpeg";
                blob = containerClient.GetBlobClient(path);
                if (blob.Exists())
                {
                    blob.Delete();
                }
                if(sMode == "ADD")
                {
                    using (Stream stream = new MemoryStream(File.ReadAllBytes(photoPath)))
                    {
                        blob.Upload(stream);
                    }
                }
            }
        }

        public Dictionary<string, string> GetPhoto(string membershipNo, string photoType)
        {
            try
            {
                var fileUrl = new Dictionary<string, string>();

                BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=ntcynkphoto;AccountKey=qYr30nnXpc9WmvJSsup6BNrGAyllMJdxTbSaz7Pwfw+Nb7UQQpGhwyzN0inoCKD7qoapA/fQo/3J+ASt70cCbQ==;EndpointSuffix=core.windows.net");


                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(membershipNo);
                if (containerClient.Exists())
                {
                    switch(photoType)
                    {
                        case "MEMBER_PHOTO":
                            var memberBlob = containerClient.GetBlobClient(FileType.MEMBER_PHOTO.ToString() + ".jpeg");
                            if (memberBlob.Exists())
                                fileUrl.Add("memberPhoto", "/Member/GetPhotos/" + membershipNo + "/" + FileType.MEMBER_PHOTO.ToString() + ".jpeg");
                            break;
                        case "SPOUSE_PHOTO":
                            var spouseBlob = containerClient.GetBlobClient(FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
                            if (spouseBlob.Exists())
                                fileUrl.Add("spousePhoto", "/Member/GetPhotos/" + membershipNo + "/" + FileType.SPOUSE_PHOTO.ToString() + ".jpeg");
                            break;
                        case "CHILD1_PHOTO":
                            var child1Blob = containerClient.GetBlobClient(FileType.CHILD1_PHOTO.ToString() + ".jpeg");
                            if (child1Blob.Exists())
                                fileUrl.Add("child1Photo", "/Member/GetPhotos/" + membershipNo + "/" + FileType.CHILD1_PHOTO.ToString() + ".jpeg");
                            break;
                        case "CHILD2_PHOTO":
                            var child2Blob = containerClient.GetBlobClient(FileType.CHILD2_PHOTO.ToString() + ".jpeg");
                            if (child2Blob.Exists())
                                fileUrl.Add("child2Photo", "/Member/GetPhotos/" + membershipNo + "/" + FileType.CHILD2_PHOTO.ToString() + ".jpeg");
                            break;
                        case "CHILD3_PHOTO":
                            var child3Blob = containerClient.GetBlobClient(FileType.CHILD3_PHOTO.ToString() + ".jpeg");
                            if (child3Blob.Exists())
                                fileUrl.Add("child3Photo", "/Member/GetPhotos/" + membershipNo + "/" + FileType.CHILD3_PHOTO.ToString() + ".jpeg");
                            break;
                    }
                }
                return fileUrl;
            }
            catch (Exception ex)
            {
                var fileUrl = new Dictionary<string, string>();
                return fileUrl;
            }
        }

        public DataSet GetMembers(string Prefix)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();

            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetMembers";
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return ds;
            }
        }
    }
}
