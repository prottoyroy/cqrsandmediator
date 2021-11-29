using CQRS.WebApi.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebApi.Features.UserFeatures.Commands
{
    public class UpdateUserCommand:IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePiture { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateUserCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                var user = _context.User.Where(a => a.Id == command.Id).FirstOrDefault();
                if (user == null)
                {
                    return default;
                }
                else
                {
                    user.Name = command.Name;
                    
                    await _context.SaveChangesAsync();
                    return user.Id;
                }
            }
        }
    }
}
