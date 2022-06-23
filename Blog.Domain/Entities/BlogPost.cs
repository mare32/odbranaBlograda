using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class BlogPost : Entity
    {
        public string Title { get; set; }
        public string BlogPostContent { get; set; }
        public int AuthorId { get; set; }
        public int CoverImage { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BlogPostCategory> BlogPostCategories { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual User Author { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<BlogPostImage> BlogPostImages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
