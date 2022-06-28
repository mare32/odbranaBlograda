using Blog.Api.Core;
using Blog.Api.Core.DTO;
using Blog.Api.Core.ImageHelpers;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostImagesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        private BlogContext _context;
        public BlogPostImagesController(UseCaseHandler handler, IApplicationUser user, BlogContext context)
        {
            _handler = handler;
            _user = user;
            _context = context;
        }

        /// <summary>
        /// Adds an image to a blog post.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="uploader"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Categories
        ///     {
        ///        "image": "fileUpload",
        ///        "blogPostId": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] ImageDto dto, 
                                  [FromServices]IAddImageToBlogPostCommand command,
                                  [FromServices]IFileUploader uploader)
        {
            _handler.HandleCommand(command, dto);
            var fileName = uploader.Upload(FileLocations.Images,dto.Image);
            var image = new Image
            {
                Src = fileName,
                Alt = fileName
            };
            _context.Images.Add(image);
            _context.SaveChanges();
            var imageId = _context.Images.FirstOrDefault(y => y.Src == fileName);
            var blogPostImage = new BlogPostImage { ImageId = imageId.Id, PostId = dto.BlogPostId };
            _context.BlogPostImages.Add(blogPostImage);
            _context.SaveChanges();
            return StatusCode(201);
        }

        /// <summary>
        /// Search blog posts images
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>BlogPostImages</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/blogPostImages
        ///     QueryString
        ///        "keyword": "im",
        ///        "perPage": 2,
        ///        "page": 2
        ///     
        ///
        /// </remarks>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices]ISearchBlogPostImagesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }
    }
}
