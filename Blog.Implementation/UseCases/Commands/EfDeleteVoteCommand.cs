using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteVoteCommand : EfUseCase, IDeleteVoteCommand
    {
        IApplicationUser _user;
        public EfDeleteVoteCommand(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2023;

        public string Name => "Delete Vote";

        public string Description => "Delete vote by id using EF";

        public void Execute(int id)
        {
            var vote = Context.Votes.FirstOrDefault(x => x.Id == id && _user.Id == x.UserId);
            if(vote == null)
            {
                throw new EntityNotFoundException(nameof(Vote),id);
            }
            int? postId = vote.PostId;
            if (postId != null)
            {
                var blogPost = Context.BlogPosts.Find(postId.Value);
                if (vote.TypeId == 2)
                {
                    blogPost.Health -= 10;
                    if (blogPost.Health < 0)
                        blogPost.Health = 1;
                    // ovde negde dodati blogPost.Votes.Count() ako je preko odredjenog broja postaje popularan
                    // a ako je u plusu preko odredjenog broja postaje amazing
                }
                else
                {
                    blogPost.Health += 10;
                    if (blogPost.Health > 100)
                    {
                        blogPost.Health = 100;
                    }
                }
            }
            Context.Votes.Remove(vote);
            Context.SaveChanges();
        }
    }
}
