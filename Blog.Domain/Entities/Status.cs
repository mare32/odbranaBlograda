using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Status : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
