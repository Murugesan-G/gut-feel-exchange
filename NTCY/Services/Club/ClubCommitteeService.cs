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
using NTCY.Business;
using NTCY.Models;

namespace NTCY.Services.Club
{
    public class ClubCommitteeService : IClubCommitteeService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IClubService _clubService;

        public ClubCommitteeService(DataContext context,IJwtUtils jwtUtils,IMapper mapper,IClubService clubService)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _clubService = clubService;
        }

        public void Add(ClubCommittee clubComitee)
        {
            if (_context.ClubCommittee.Any(x => x.CommitteeId == clubComitee.CommitteeId))
                throw new AppException("Club '" + clubComitee.CommitteeId + "' is already exists");

            var clubCommitteeDTO = _mapper.Map<ClubCommitteeDTO>(clubComitee);

            _context.ClubCommittee.Add(clubCommitteeDTO);
            _context.SaveChanges();
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

        public void Delete(int comiteeId)
        {
            var clubcommittee = getCommittee(comiteeId);
            _context.ClubCommittee.Remove(clubcommittee);
            _context.SaveChanges();
        }

        public IEnumerable<ClubCommDTO> GetAll()
        {
            var result = (from e in _context.ClubCommittee
                          join mf in _context.Club on e.ClubId equals mf.ClubId
                          select new ClubCommDTO
                          {
                              CommitteeId = e.CommitteeId,
                              ClubId = mf.ClubId,
                              ClubName = mf.ClubName,
                              Chairman = e.Chairman,
                              President = e.President,
                              Secretary = e.Secretary,
                              Treasurer = e.Treasurer,
                              VicePresident = e.VicePresident,
                              CommitteeMembers = e.CommitteeMembers,
                              StartDate = e.StartDate,
                              EndDate = e.EndDate,
                              Status = e.Status
                          }).ToList();

            return result;
        }

        public ClubCommitteeDTO GetById(int comiteeId)
        {
            return getCommittee(comiteeId);
        }

        public void Update(int comiteeId, ClubCommittee clubComitee)
        {
            var comiteeDto = getCommittee(comiteeId);

            _mapper.Map(clubComitee, comiteeDto);
            _context.ClubCommittee.Update(comiteeDto);
            _context.SaveChanges();
        }

        private ClubCommitteeDTO getCommittee(int comiteeId)
        {
            var committee = _context.ClubCommittee.FirstOrDefault(m => m.CommitteeId == comiteeId);
            if (committee == null) throw new KeyNotFoundException("Club not found");
            return committee;   
        }

        public List<ClubDetails> GetClubDetails()
        {
            throw new NotImplementedException();
        }
    }
}
