using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.Queries;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        // /profile da prikaze sve podatke o ulogovanom korisniku
        private IApplicationUser _user;
        private UseCaseHandler _handler;
        public ProfileController(IApplicationUser user, UseCaseHandler handler)
        {
            _user = user;
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices]IApplicationUser user,[FromServices]IGetOneUserQuery query)
        {
            var id = user.Id;
            return Ok(_handler.HandleQuery(query, id));
        }
    }
}
