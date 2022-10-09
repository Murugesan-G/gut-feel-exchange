using AutoMapper;
using NTCY.Entities;
using NTCY.Models.Club;
using NTCY.Models.Foods;
using NTCY.Models.LiquorDetails;
using NTCY.Models.RoomDetails;
using NTCY.Models.Table;
using NTCY.Models.Users;

namespace NTCY.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();

            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<MemberDTO, Member>();
            CreateMap<Member, MemberDTO>();

            CreateMap<ClubDTO, Club>();
            CreateMap<Club, ClubDTO>();

            CreateMap<ClubCommitteeDTO, ClubCommittee>();
            CreateMap<ClubCommittee, ClubCommitteeDTO>();

            CreateMap<ClubCommDTO, ClubCommittee>();
            CreateMap<ClubCommittee, ClubCommDTO>();

            CreateMap<CardTableDTO, CardTable>();
            CreateMap<CardTable, CardTableDTO>();

            CreateMap<FoodDTO, Food>();
            CreateMap<Food, FoodDTO>();

            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();

            CreateMap<LiquorCategoryDTO, LiquorCategory>();
            CreateMap<LiquorCategory, LiquorCategoryDTO>();

            CreateMap<LiquorDTO, Liquor>();
            CreateMap<Liquor, LiquorDTO>();
        }
    }
}
