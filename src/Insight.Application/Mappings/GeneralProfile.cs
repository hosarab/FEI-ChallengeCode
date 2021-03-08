using AutoMapper;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.Domain.Entities;

namespace Insight.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetPostcodeQuery, GetPostcodeQueryParameter>().ReverseMap();
            CreateMap<PostCode, PostcodeDetailsViewModel>()
                .ForMember(x => x.Coordinates,
                    opt => opt.Ignore()).ReverseMap();
            CreateMap<GetAllPostcodeQuery, PostcodesListViewModel>().ForMember(x => x.Postcodes,
                opt => opt.Ignore());
        }

    }
}
