using System.Collections.Generic;
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
    public class GetAllPostcodeQuery : IRequest<PostcodesListViewModel>
    {
        public IEnumerable<string> PostCodes { get; set; }

        public class GetAllPostcodeQueryHandler : IRequestHandler<GetAllPostcodeQuery, PostcodesListViewModel>
        {
            private readonly IPostcodeService _service;
            private readonly IMapper _mapper;

            public GetAllPostcodeQueryHandler(IPostcodeService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            public async Task<PostcodesListViewModel> Handle(GetAllPostcodeQuery request, CancellationToken cancellationToken)
            {
                if (request == null || request.PostCodes == null)
                {
                    throw new PostcodeException((int)HttpStatusCode.UnprocessableEntity, Constants.UnprocessableEntity,
                        Constants.AListWithAtLeastOnePostcodeIsMandatory);

                }
                var postcodes = await _service.BulkLookup(request.PostCodes);

                return postcodes;
            }
        }
    }
}