using CQRS.WebApi.Infrastructure.Context;
using CQRS.WebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebApi.Features.UserFeatures.Queries
{
    public class GetAllUsersQuery:IRequest<IEnumerable<User>>
    {
        public class GetAllUsersQueryHandler :IRequestHandler<GetAllUsersQuery,IEnumerable<User>>
        {
            private readonly IApplicationContext _context;
            public GetAllUsersQueryHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
            {
                var userList = await _context.User.ToListAsync();
                if (userList == null)
                {
                    return null;
                }
                return userList.AsReadOnly();
            }
        }
    }
}
