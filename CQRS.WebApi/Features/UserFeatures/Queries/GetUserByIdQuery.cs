using CQRS.WebApi.Infrastructure.Context;
using CQRS.WebApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebApi.Features.UserFeatures.Queries
{
    public class GetUserByIdQuery:IRequest<User>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IApplicationContext _context;
            public GetUserByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var user = _context.User.Where(a => a.Id == query.Id).FirstOrDefault();
                if (user == null) return null;
                return user;
            }
        }

    }
}
