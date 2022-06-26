using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class SimpleBlogPostDto : BaseDto
    {
        public string Title { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public string Status { get; set; }
    }
}
