using CQRS.WebApi.Domain.Models;
using CQRS.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CQRS.WebApi.Infrastructure.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync();
    }
}