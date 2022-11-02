using Microsoft.VisualBasic.FileIO;
using NTCY.Entities;
using NTCY.Models.Club;
using NTCY.Models.Users;
using System.Collections.Generic;
using System.Data;

namespace NTCY.Services.MemberService
{
    public enum FileType
    {
        MEMBER_PHOTO,
        SPOUSE_PHOTO,
        CHILD1_PHOTO,
        CHILD2_PHOTO,
        CHILD3_PHOTO
    }
    public interface IMemberService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<MemberDTO> GetAll();
        MemberDTO GetById(string MembershipNo);
        public string Add(Member member, IFormFile memberformFile, IFormFile spouseformFile, IFormFile child1formFile, IFormFile child2formFile, IFormFile child3formFile);
        void Update(string MembershipNo, Member member, IFormFile memberformFile, IFormFile spouseformFile, IFormFile child1formFile, IFormFile child2formFile, IFormFile child3formFile);
        void Delete(string MembershipNo);
        public void UploadPhoto(string membershipNo, string photoType, string photoPath);
        public Dictionary<string, string> GetPhoto(string membershipNo, string photoType);
        void saveFile(string membershipNo, FileType photoType, IFormFile inputFile);
        bool CheckMemberPhotoExists(string membershipNo, FileType fileType);
        public DataSet GetMembers(string Prefix);

    }
}
