using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommentCommand
    {
        IApplicationUser _user;
        public EfDeleteCommentCommand(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2022;

        public string Name => "Delete comment";

        public string Description => "Delete comment with all of its subcomments using EF";

        public void Execute(int commentId)
        {
             var comment = Context.Comments.Include(x => x.Votes).FirstOrDefault(c => c.Id == commentId && c.UserId == _user.Id);
            if(comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), commentId);
            }
            var childComments = Context.Comments.Where(x => x.ParentId == commentId);
            if (childComments.Any())
            {
                foreach (var childComment in childComments)
                {
                    if(childComment.Votes.Any())
                    Context.Votes.RemoveRange(childComment.Votes);
                }
            Context.Comments.RemoveRange(childComments);
            }
            if (comment.Votes.Any())
                Context.Votes.RemoveRange(comment.Votes);
            Context.Comments.Remove(comment);
            Context.SaveChanges();
        }
    }
}
