using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Insight.Application.Common.Exceptions;
using Insight.Application.Interfaces;
using Insight.Domain;
using MediatR;

namespace Insight.Application.PostCodesFeatures.Queries
{
    public class GetPostcodeQuery : IRequest<PostcodeDetailsViewModel>
    {
        public string PostCode { get; set; }

        public class GetPostcodeQueryHandler : IRequestHandler<GetPostcodeQuery, PostcodeDetailsViewModel>
        {
            private readonly IPostcodeService _service;
            private readonly IMapper _mapper;
            public GetPostcodeQueryHandler(IPostcodeService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }
            public async Task<PostcodeDetailsViewModel> Handle(GetPostcodeQuery request, CancellationToken cancellationToken)
            {
                if (request.PostCode == null)
                {
                    throw new PostcodeException((int)HttpStatusCode.UnprocessableEntity, Constants.UnprocessableEntity, Constants.ThePostcodeIsMandatory);
                }

                var valid = await _service.ValidateAsync(request.PostCode);

                if (!valid) throw new PostcodeException((int)HttpStatusCode.NotFound, Constants.NotFound, Constants.YouHaveEnteredAnInvalidPostcode);

                var postcode = await _service.LookupByPostCode(request.PostCode);

                return postcode;
            }
        }
    }
}
