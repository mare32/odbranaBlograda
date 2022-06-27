using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {

        /// <summary>
        /// Initialize fake data, optionaly delete all databeforehand(if time lets me)
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  POST api/initialData
        ///
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        public IActionResult Post([FromServices]BlogContext context)
        {
            //var context = new BlogContext();
            // Provera da li data ulazi po prvi put

            if (context.Users.Any())
            {
                return StatusCode(409);
            }


            var roles = new List<Role>
            {
                new Role{ Name = "Admin"},
                new Role{ Name = "Normal"}
            };

            // 8 korisnika
            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Tika",
                    LastName = "Tikic",
                    Role = roles.ElementAt(0)
                },
                new User
                {
                    Username = "mika",
                    Email = "mika@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Mitar",
                    LastName = "Mikic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "pera",
                    Email = "pera@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",//lozinka
                    FirstName = "Petar",
                    LastName = "Peric",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "zika",
                    Email = "zika@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Zika",
                    LastName = "Zikic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "mile",
                    Email = "mile@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Milan",
                    LastName = "Milic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "djole",
                    Email = "djole@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Djordje",
                    LastName = "Djordjevic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "tanja",
                    Email = "tanja@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Tatjana",
                    LastName = "Tatjanovic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "nina",
                    Email = "nina@gmail.com",
                    Password = "$2a$11$Fh30e2Ivjz2CRzvlrfYfmewDxr7em8oNgxs55OOH.bzOcQuly09dC",
                    FirstName = "Nikolina",
                    LastName = "Nikolic",
                    Role = roles.ElementAt(1)
                }
                
            };
            // 8 slika
            var images = new List<Image>
            {
                new Image{ Src = @"wwwroot\images\first.jpg", Alt = "first"},
                new Image{ Src = @"wwwroot\images\second.jpg", Alt = "second"},
                new Image{ Src = @"wwwroot\images\third.jpg", Alt = "third"},
                new Image{ Src = @"wwwroot\images\fourth.jpg", Alt = "fourth"},
                new Image{ Src = @"wwwroot\images\fifth.jpg", Alt = "fifth"},
                new Image{ Src = @"wwwroot\images\sixth.jpg", Alt = "sixth"},
                new Image{ Src = @"wwwroot\images\seventh.jpg", Alt = "seventh"},
                new Image{ Src = @"wwwroot\images\eighth.jpg", Alt = "eighth"},
            };
            // 5 kategorija
            var categories = new List<Category>
            {
                new Category { Name = "Travel"},
                new Category { Name = "Food"},
                new Category { Name = "Health"},
                new Category { Name = "Politics"},
                new Category { Name = "Drama"},
                new Category { Name = "Science"},
                new Category { Name = "Event"},
            };

            var votetypes = new List<VoteType>
            {
                new VoteType{ Type = "Attack"},
                new VoteType{ Type = "Defend"}
            };
            // 5 statusa 
            var statuses = new List<Status>
            {
                new Status{ Name = "Alive"},
                new Status{ Name = "Dead"},
                new Status{ Name = "Invisible"},
                new Status{ Name = "Popular"},
                new Status{ Name = "Amazing"}
            };
            // ukupno 32 use case-a
            var anonusecase = new UseCase {Id = 1002, Name = "Register", Description = "Create a new blogpost" };
            var normalusecases = new List<UseCase>
            {
                new UseCase{Id = 2006, Name = "Search Blog Posts", Description = "Delete an existing user"},
                new UseCase{Id = 2007, Name = "View Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2017, Name = "Show Comments", Description = "Delete an existing user"},
                new UseCase{Id = 2002, Name = "View user", Description = "Delete an existing user"},
                new UseCase{Id = 2005, Name = "Create Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2010, Name = "Add images to Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2011, Name = "Patch Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2012, Name = "Create Comment", Description = "Delete an existing user"},
                new UseCase{Id = 2014, Name = "Vote", Description = "Delete an existing user"},
                new UseCase{Id = 2019, Name = "Update Blog Post Categories", Description = "Delete an existing user"},
                new UseCase{Id = 2020, Name = "Update User Profile", Description = "Delete an existing user"},
                new UseCase{Id = 2021, Name = "Delete Image From Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2022, Name = "Delete comment", Description = "Delete an existing user"},
                new UseCase{Id = 2023, Name = "Unvote", Description = "Delete an existing user"},
                new UseCase{Id = 2024, Name = "Delete Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2029, Name = "Search Blog Post Images", Description = "Delete an existing user"},
                new UseCase{Id = 2033, Name = "Search User Votes", Description = "Delete an existing user"},
            };
            var admincases = new List<UseCase>
            {
                new UseCase{Id = 2006, Name = "Search Blog Posts", Description = "Delete an existing user"},
                new UseCase{Id = 2007, Name = "View Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2017, Name = "Show Comments", Description = "Delete an existing user"},
                new UseCase{Id = 2002, Name = "View user", Description = "Delete an existing user"},
                new UseCase{Id = 2005, Name = "Create Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2010, Name = "Add images to Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2011, Name = "Patch Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2012, Name = "Create Comment", Description = "Delete an existing user"},
                new UseCase{Id = 2014, Name = "Vote", Description = "Delete an existing user"},
                new UseCase{Id = 2019, Name = "Update Blog Post Categories", Description = "Delete an existing user"},
                new UseCase{Id = 2020, Name = "Update User Profile", Description = "Delete an existing user"},
                new UseCase{Id = 2021, Name = "Delete Image From Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2022, Name = "Delete comment", Description = "Delete an existing user"},
                new UseCase{Id = 2023, Name = "Unvote", Description = "Delete an existing user"},
                new UseCase{Id = 2024, Name = "Delete Blog Post", Description = "Delete an existing user"},
                new UseCase{Id = 2029, Name = "Search Blog Post Images", Description = "Delete an existing user"},
                new UseCase{Id = 2033, Name = "Search User Votes", Description = "Delete an existing user"},
                new UseCase{Id = 1, Name = "Search Category", Description = "Delete an existing user"},
                new UseCase{Id = 2, Name = "Create Category", Description = "Delete an existing user"},
                new UseCase{Id = 2003, Name = "Search Users", Description = "Delete an existing user"},
                new UseCase{Id = 2004, Name = "Delete Category", Description = "Delete an existing user"},
                new UseCase{Id = 2015, Name = "Search Roles", Description = "Delete an existing user"},
                new UseCase{Id = 2016, Name = "Change Role", Description = "Delete an existing user"},
                new UseCase{Id = 2018, Name = "Update UserUseCases", Description = "Delete an existing user"},
                new UseCase{Id = 2025, Name = "Delete user", Description = "Delete an existing user"},
                new UseCase{Id = 2026, Name = "Search Logs", Description = "Delete an existing user"},
                new UseCase{Id = 2027, Name = "Search UseCases", Description = "Delete an existing user"},
                new UseCase{Id = 2028, Name = "Show Category", Description = "Delete an existing user"},
                new UseCase{Id = 2030, Name = "Search UserUseCases", Description = "Delete an existing user"},
                new UseCase{Id = 2031, Name = "Add UserUseCase", Description = "Delete an existing user"},
                new UseCase{Id = 2032, Name = "Remove UserUseCase", Description = "Delete an existing user"}
            };
            var normaluserusecases = new List<UserUseCase>();
            var adminuserusecases = new List<UserUseCase>();
            foreach(var user in users)
            {
                if(user.Role == roles.ElementAt(1))
                {
                    foreach (var usecase in normalusecases)
                    {
                        normaluserusecases.Add(new UserUseCase { CaseId = usecase.Id,User = user});
                    }
                }
                else
                {
                    foreach (var usecase in admincases)
                    {
                        adminuserusecases.Add(new UserUseCase { CaseId = usecase.Id, User = user });
                    }
                }
            }

            // 5 blogpost-ova
            var blogposts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "Climbing on Himalayas",
                    CoverImage = 1,
                    BlogPostContent = "Climbing up theres was a real challenge but somehow we actualy made it in one piece, gettin down was even harder.",
                    Author = users.First(),
                    Status = statuses.First(),
                    Health = 15
                },
                new BlogPost
                {
                    Title = "Relaxing Honolulu",
                    CoverImage = 2,
                    BlogPostContent = "Climbing up theres was a real challenge but somehow we actualy made it in one piece, gettin down was even harder.",
                    Author = users.First(),
                    Status = statuses.First(),
                    Health = 53,
                    Shield = 45
                },
                new BlogPost
                {
                    Title = "Amber Heard was RIGHT!",
                    CoverImage = 3,
                    BlogPostContent = "Too all Ambers haters, you are wrong, Johnny Depp made so many mistakes, she definitely deserved to win.",
                    Author = users.ElementAt(1),
                    Status = statuses.First(),
                    Health = 95,
                    Shield = 97
                },
                new BlogPost
                {
                    Title = "War on Ukraine is only for money and land",
                    CoverImage = 4,
                    BlogPostContent = "Nothing is going to happen, it won't escalate and you can't prove me wrong.",
                    Author = users.ElementAt(4),
                    Status = statuses.First()
                },
                new BlogPost
                {
                    Title = "New Advent of Code Event is soon",
                    CoverImage = 5,
                    BlogPostContent = "Programmers, better prepare for this year's winter season of coding adventures! Get cozy, warm, and wreck those keyboard buttons!",
                    Author = users.ElementAt(5),
                    Status = statuses.First(),
                    Health = 10
                },
            };

            var blogpostcategories = new List<BlogPostCategory>
            {
                new BlogPostCategory{ BlogPost = blogposts.First(), Category = categories.First() },
                new BlogPostCategory{ BlogPost = blogposts.First(), Category = categories.ElementAt(2) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(1), Category = categories.ElementAt(0) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(1), Category = categories.ElementAt(1) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(2), Category = categories.ElementAt(4) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(3), Category = categories.ElementAt(3) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(3), Category = categories.ElementAt(4) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(4), Category = categories.ElementAt(5) },
                new BlogPostCategory{ BlogPost = blogposts.ElementAt(4), Category = categories.ElementAt(6) },
            };

            var blogpostimages = new List<BlogPostImage>
            {
                new BlogPostImage {BlogPost = blogposts.First(), Image = images.First()},
                new BlogPostImage {BlogPost = blogposts.First(), Image = images.ElementAt(1)},
                new BlogPostImage {BlogPost = blogposts.ElementAt(1), Image = images.ElementAt(2)},
                new BlogPostImage {BlogPost = blogposts.ElementAt(1), Image = images.ElementAt(3)},
                new BlogPostImage {BlogPost = blogposts.ElementAt(2), Image = images.ElementAt(4)},
                new BlogPostImage {BlogPost = blogposts.ElementAt(3), Image = images.ElementAt(5)},
                new BlogPostImage {BlogPost = blogposts.ElementAt(4), Image = images.ElementAt(6)},
            };
            var comments = new List<Comment>
            {
                new Comment { BlogPost = blogposts.First(),CommentText = "Woah that is so crazy!", User = users.ElementAt(1)},
                new Comment { BlogPost = blogposts.ElementAt(1),CommentText = "I wish I was there right now...", User = users.ElementAt(2)}
            };

            context.Roles.AddRange(roles);
            context.Status.AddRange(statuses);
            context.Users.AddRange(users);
            context.Images.AddRange(images);
            context.Categories.AddRange(categories);
            context.VoteTypes.AddRange(votetypes);
            context.UseCases.Add(anonusecase);
            context.UseCases.AddRange(admincases);
            context.UserUseCases.AddRange(normaluserusecases);
            context.UserUseCases.AddRange(adminuserusecases);
            context.BlogPosts.AddRange(blogposts);
            context.BlogPostCategories.AddRange(blogpostcategories);
            context.BlogPostImages.AddRange(blogpostimages);
            context.Comments.AddRange(comments);
            //context.Votes.AddRange(votes);

            context.SaveChanges();
            return StatusCode(201);
        }

    }
}
