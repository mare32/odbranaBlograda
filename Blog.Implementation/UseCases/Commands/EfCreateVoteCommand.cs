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
    public class EfCreateVoteCommand : EfUseCase, ICreateVoteCommand
    {
        CreateVoteValidator _validator;
        private IApplicationUser _user;
        public EfCreateVoteCommand(BlogContext context, IApplicationUser user, CreateVoteValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 2014;

        public string Name => "Create Vote";

        public string Description => "Vote down or up on a comment on a blog post";

        public void Execute(VoteDto dto)
        {
            _validator.ValidateAndThrow(dto);
            int? postId = null;
            int? commId = null;
            int maxHealth = 100;
            int statusDead = 3;
            Vote postojeciPostVote = new Vote();
            bool identicanVoteUradjen = false;
            Vote vote = new Vote();
            if (dto.BlogPostId.HasValue && dto.CommentId.HasValue)
            {
                throw new ValidationConflictException("Ne moze jedan isti glas biti usmeren i na objavu i na komentar.");
            }
            if (dto.BlogPostId.HasValue)
            {
                if(!Context.BlogPosts.Any( x => x.Id == dto.BlogPostId.Value))
                {
                    throw new EntityNotFoundException(nameof(BlogPost), dto.BlogPostId.Value);
                }
                postId = dto.BlogPostId.Value;
                postojeciPostVote = Context.Votes.FirstOrDefault(x => x.UserId == _user.Id && x.PostId == dto.BlogPostId);
                if(postojeciPostVote != null)
                {
                    if (postojeciPostVote.TypeId == dto.VoteType)
                        identicanVoteUradjen = true;
                }
            }
            if(dto.CommentId.HasValue)
            {
                if (!Context.Comments.Any(x => x.Id == dto.CommentId.Value))
                {
                    throw new EntityNotFoundException(nameof(BlogPost), dto.BlogPostId.Value);
                }
                commId = dto.CommentId.Value;
                postojeciPostVote = Context.Votes.FirstOrDefault(x => x.UserId == _user.Id && x.CommentId == dto.CommentId);
            }
            if (postojeciPostVote == null)
            {
                vote = new Vote
                {
                    UserId = _user.Id,
                    PostId = postId,
                    CommentId = commId,
                    TypeId = dto.VoteType
                };
                if(postId != null)
                {
                    var blogPost = Context.BlogPosts.Find(postId.Value);
                    if(vote.TypeId == 2)
                    {
                        blogPost.Health += 10;
                        if (blogPost.Health > maxHealth)
                        {
                            blogPost.Shield += 5;
                            blogPost.Health = maxHealth;
                        }
                    }
                    else
                    {
                        if (blogPost.Shield > 0)
                        {
                            blogPost.Shield -= 5;
                            if (blogPost.Shield < 0)
                                blogPost.Shield = 0;
                            blogPost.Health -= 5;
                        }
                        else
                        {
                            blogPost.Health -= 10;
                        }
                        if (blogPost.Health < 0)
                        {
                            blogPost.Health = 0;
                            blogPost.StatusId = 3;
                        }
                    }
                }
                Context.Votes.Add(vote);
            }
            else
            {
                if(postojeciPostVote.TypeId == 1)
                {
                    if(postojeciPostVote.BlogPost.Shield > 0)
                    {
                        postojeciPostVote.BlogPost.Shield += 5;
                        postojeciPostVote.BlogPost.Health += 5;
                        if(postojeciPostVote.BlogPost.Health > maxHealth)
                        {
                            postojeciPostVote.BlogPost.Health = maxHealth;
                        }
                    }
                    else
                    {
                        postojeciPostVote.BlogPost.Health += 10;
                    }
                }
                else
                {
                    if(postojeciPostVote.BlogPost.Shield > 0 && postojeciPostVote.BlogPost.Health == maxHealth)
                    {
                        postojeciPostVote.BlogPost.Shield -= 5;
                    }
                    else if (postojeciPostVote.BlogPost.Shield > 0)
                    {
                        postojeciPostVote.BlogPost.Shield -= 5;
                        postojeciPostVote.BlogPost.Health -= 5;
                        if (postojeciPostVote.BlogPost.Shield < 0)
                            postojeciPostVote.BlogPost.Shield = 0;
                        if (postojeciPostVote.BlogPost.Health < 0)
                        {
                            postojeciPostVote.BlogPost.Health = 1;
                        }
                    }
                    else
                    {
                        postojeciPostVote.BlogPost.Health -= 10;
                        if (postojeciPostVote.BlogPost.Health < 0)
                        {
                            postojeciPostVote.BlogPost.Health = 1;
                        }
                    }
                }
                Context.Votes.Remove(postojeciPostVote);
                if(!identicanVoteUradjen)
                {
                    vote = new Vote
                    {
                        UserId = _user.Id,
                        PostId = postId,
                        CommentId = commId,
                        TypeId = dto.VoteType
                    };
                    if (postId != null)
                    {
                        var blogPost = Context.BlogPosts.Find(postId.Value);
                        // napraviti reusable funkciju za kod ispod
                        if (vote.TypeId == 2)
                        {
                            if (blogPost.Health >= maxHealth)
                            {
                                blogPost.Shield += 5;
                            }
                            else
                            {
                                blogPost.Health += 10;
                                if (blogPost.Health > maxHealth)
                                {
                                    blogPost.Shield += 5;
                                    blogPost.Health = maxHealth;
                                }
                            }
                        }
                        else
                        {
                            if(blogPost.Shield > 0)
                            {
                                blogPost.Health -= 5;
                                blogPost.Shield -= 5;
                                if (blogPost.Shield < 0)
                                    blogPost.Shield = 0;
                            }
                            else
                            {
                                blogPost.Health -= 10;
                            }
                            if (blogPost.Health < 0)
                            {
                                blogPost.Health = 0;
                                blogPost.StatusId = statusDead;
                            }
                        }
                    }
                    Context.Votes.Add(vote);
                }
            }

            
            Context.SaveChanges();

        }
    }
}
