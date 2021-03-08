using System;
using System.Collections.Generic;

namespace Insight.Application.PostCodesFeatures.Queries
{
    [Serializable]
    public class PostcodesListViewModel
    {
        public IList<PostcodeDetailsViewModel> Postcodes { get; set; }
    }

}