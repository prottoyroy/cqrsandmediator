using CQRS.WebApi.Infrastructure.Context;
using CQRS.WebApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebApi.Features.UserFeatures.Commands
{
    public class CreateUserCommand:IRequest<int>
    {
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationContext _context;
            public CreateUserCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    Name=command.Name,
                    ProfilePicture=command.ProfilePicture
                };
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return user.Id;
            }
        }
    }
}
