using Blog.Api.Core;
using Blog.Api.Core.DTO;
using Blog.Application;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostsController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        private BlogContext _context;
        //public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpg",".png",".jpeg",".gif" };
        public BlogPostsController(UseCaseHandler handler, IApplicationUser user, BlogContext context)
        {
            _context = context;
            _handler = handler;
            _user = user;
        }
        /// <summary>
        /// Search blog posts.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>Array of posts</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/blogposts
        ///   QueryString
        ///  "perPage": 5,
        ///  "page": 1,
        ///  "keyword": "p"
        ///
        /// </remarks>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] SearchBlogPostsDto search, [FromServices] ISearchBlogPostsQuery query)
        {

            return Ok(_handler.HandleQuery(query,search));
        }

        /// <summary>
        /// Shows a blog post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns>Blog post</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET api/blogposts/5
        ///
        /// </remarks>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOneBlogPostQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        /// <summary>
        /// Creates new blog post.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="uploader"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/blogposts
        ///     {
        ///        "image": "*fileUploadField*",
        ///        "title" : "Example",
        ///        "blogPostContent" : "Example gratia",
        ///        "categoryIds" : [ 2, 5 ,7],
        ///        "imageAlt" : "Example alternative"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfull creation.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm]CreateBlogPostWithImageDto dto,
                                  [FromServices]ICreateBlogPostCommand command, 
                                  [FromServices]IFileUploader uploader)
        {
            if (dto.CategoryIds == null || dto.CategoryIds.Count() < 1)
            {
                return UnprocessableEntity(new { error = "Izaberite makar jednu kategoriju." });
            }
            //uploader.File = dto.Image;
            var filename = uploader.Upload(FileLocations.Images, dto.Image);
            CreateBlogPostDto noviDto = new CreateBlogPostDto
            {
                Title = dto.Title,
                BlogPostContent = dto.BlogPostContent,
                AuthorId = _user.Id,
                CategoryIds = dto.CategoryIds,
                ImageAlt = filename,
                ImageSrc = filename
            };
            _handler.HandleCommand(command, noviDto);

            return StatusCode(201);
        }

        /// <summary>
        /// Deletes a blog post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="deleter"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/blogposts/5
        ///
        /// </remarks>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id, [FromServices]IDeleteBlogPostCommand command, 
                                            [FromServices] IFileDeleter deleter)
        {
            var blogPostImages = _context.BlogPostImages.Include(x => x.Image).Where(x => x.PostId == id).ToList();
            foreach(var img in blogPostImages)
            {
                deleter.DeleteFile(img.Image.Src);
            }
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        /// <summary>
        /// Update blog post
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PATCH /api/blogposts
        ///  {
        ///  "id":2
        ///  "title": "New title",
        ///  "blogPostContent": "New content",
        ///  "coverImgId" : 2005
        ///  }
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPatch]
        public IActionResult Patch([FromBody] PatchBlogPostDto dto, [FromServices]IPatchBlogPostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
