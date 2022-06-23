using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class BlogPostDto : BaseDto
    {
        public string Title { get; set; }
        public string BlogPostContent { get; set; }
        public UserDto Author { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<int> BlogPostImageIds { get; set; }
        public int CoverImageId { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int VoteScore { get; set; }
        public int TotalVotes { get; set; }
        public string Status { get; set; }
        public int Health { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
