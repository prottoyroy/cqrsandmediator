using CQRS.WebApi.Models;
using CQRS.WebApi.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.WebApi.Interface
{
    public interface IUser
    {
      //  Task<int> Update(int id , UserVM user);
        Task<int> SaveAsync(UserVM user);
    }
}
