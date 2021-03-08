using System.Threading.Tasks;
using Insight.Application.PostCodesFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Insight.WebApi.Controllers
{

    [ApiController]
    public class PostcodesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPostcodeQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetPostcodeQuery { PostCode = filter.Postcode }));
        }

        [HttpPost]
        public async Task<IActionResult> BulkLookup([FromBody] GetAllPostcodeQuery postcodes)
        {
            return Ok(await Mediator.Send(new GetAllPostcodeQuery { PostCodes = postcodes.PostCodes }));
        }
    }
}
