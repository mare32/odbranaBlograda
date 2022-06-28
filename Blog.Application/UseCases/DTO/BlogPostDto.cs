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
        public CoverImageDto Image { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int VoteScore { get; set; }
        public int TotalVotes { get; set; }
        public string Status { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
    public class CoverImageDto : BaseDto
    {
        public string src { get; set; }
        public string alt { get; set; }
    }
}
