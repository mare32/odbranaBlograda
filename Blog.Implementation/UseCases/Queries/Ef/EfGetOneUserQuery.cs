using Blog.Application.Exceptions;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetOneUserQuery : EfUseCase, IGetOneUserQuery
    {
       
        public EfGetOneUserQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2002;

        public string Name => "View user";

        public string Description => "See details of one user using EF";

        public UserWithRoleDto Execute(int request)
        {
            var user = Context.Users.Include(x => x.Role).FirstOrDefault( x => x.Id == request && x.Active == 1);
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), request);
            }

            return new UserWithRoleDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role.Name
            };
        }
    }
}
