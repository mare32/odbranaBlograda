using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class SearchBlogPostsDto : BasePagedSearch
    {
        public bool LoggedUsersPosts { get; set; }
        public int? AuthorId { get; set; }
    }
}
