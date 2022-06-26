using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfPatchBlogPostCommand : EfUseCase, IPatchBlogPostCommand
    {
        PatchBlogPostValidator _validator;
        private IApplicationUser _user;
        public EfPatchBlogPostCommand(BlogContext context, IApplicationUser user, PatchBlogPostValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 2011;

        public string Name => "Patch Blog Post";

        public string Description => "Patch a specific blog post by changing Title, Content or CoverImg using EF";

        public void Execute(PatchBlogPostDto dto)
        {
            var blogPost = Context.BlogPosts.FirstOrDefault(x => x.Id == dto.Id);
            if (blogPost == null)
            {
                throw new EntityNotFoundException(nameof(BlogPost), dto.Id);
            }
            var isAdmin = Context.Users.Any(x => x.Id == _user.Id && x.Role.Name == "Admin");
            if (blogPost.AuthorId != _user.Id && !isAdmin)
            {
                throw new ForbiddenUseCaseExecutionException(Name, _user.Email);
            }
            if(dto.StatusId != null)
            {
                blogPost.StatusId = dto.StatusId.Value;
                blogPost.StatusUpdatedAt = DateTime.UtcNow;
            }
            if (!string.IsNullOrEmpty(dto.BlogPostContent))
                blogPost.BlogPostContent = dto.BlogPostContent;
            if (!string.IsNullOrEmpty(dto.Title))
                blogPost.Title = dto.Title;
            if (dto.CoverImgId != null && blogPost.BlogPostImages.Any(x => x.ImageId == dto.CoverImgId))
                blogPost.CoverImage = dto.CoverImgId.Value;
            blogPost.UpdatedAt = DateTime.Now;
            Context.Update(blogPost);
            _validator.ValidateAndThrow(blogPost);
            Context.SaveChanges();
        }
    }
}
