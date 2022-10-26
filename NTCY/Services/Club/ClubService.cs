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
using NTCY.Models;

namespace NTCY.Services.Club
{
    public class ClubService : IClubService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public ClubService(DataContext context,IJwtUtils jwtUtils,IMapper mapper)
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

        void IClubService.Add(Models.Club.Club club)
        {
            if (_context.Club.Any(x => x.ClubName == club.ClubName))
                throw new AppException("Club '" + club.ClubName + "' is already exists");

            var clubDTO = _mapper.Map<ClubDTO>(club);

            _context.Club.Add(clubDTO);
            _context.SaveChanges();
        }

        AuthenticateResponse IClubService.Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        void IClubService.Delete(int clubId)
        {
            var club = getClub(clubId);
            _context.Club.Remove(club);
            _context.SaveChanges();
        }

        public IEnumerable<ClubDTO> GetAll()
        {
            return _context.Club;
        }

        ClubDTO IClubService.GetById(int clubId)
        {
            return getClub(clubId);
        }

        void IClubService.Update(int clubId, Models.Club.Club club)
        {
            var clubDto = getClub(clubId);

            if (club.ClubName != club.ClubName && _context.Club.Any(x => x.ClubName == club.ClubName))
                throw new AppException("Club '" + club.ClubName + "' is already exists");

            _mapper.Map(club, clubDto);
            _context.Club.Update(clubDto);
            _context.SaveChanges();
        }

        private ClubDTO getClub(int clubId)
        {
            var club = _context.Club.FirstOrDefault(m => m.ClubId == clubId);
            if (club == null) throw new KeyNotFoundException("Club not found");
            return club;
        }
    }
}
