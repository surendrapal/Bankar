using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Common
{
    public class PostDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private readonly IPostService _postService;

        public PostDetailsDynamicNodeProvider()
        {
            _postService = new PostService(new MyDbContext());
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            var returnValue = new List<DynamicNode>();

            foreach (var post in _postService.GetSiteMapData(20))
            {
                var node = new DynamicNode
                {
                    Title = post.Title,
                    Controller = "Post",
                    Action = "Index",
                    Area = "",
                    LastModifiedDate = post.ModifiedDate

                };
                node.RouteValues.Add("id", post.Id);
                node.RouteValues.Add("title", node.Title);
                returnValue.Add(node);
            }

            // Return 
            return returnValue;
        }

        public IList<SiteMapModel> GetSiteMapData(int count)
        {
            return _posts.AsNoTracking().OrderByDescending(post => post.CreatedDate).Take(count).
                          Select(post => new SiteMapModel
                          {
                              Id = post.Id,
                              CreatedDate = post.CreatedDate,
                              ModifiedDate = post.ModifiedDate,
                              Title = post.Title
                          }).ToList();
        }
    }
}
