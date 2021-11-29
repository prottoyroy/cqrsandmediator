using CQRS.WebApi.Infrastructure.Context;
using CQRS.WebApi.Interface;
using CQRS.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.WebApi.Service.Implementation
{
    public class UserVM
    {
        public string Name { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
    public class UserService : IUser
    {
        protected readonly IApplicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserService(IApplicationContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<int> SaveAsync(UserVM user)
        {
            string image = UploadImage(user);
            var userModel = new User
            {
                Name = user.Name,
                ProfilePicture =image
            };
            _context.User.Add(userModel);
            var result =await _context.SaveChangesAsync();
            return result;
        }
        private string UploadImage(UserVM model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        //public async Task<int> Update(int id, User user)
        //{
        //    var model = new User();
        //    model.Name = user.Name;
        //    _context.User.Update(model);
        //   var result = await _context.SaveChangesAsync();
        //    return result;

           
        //}

       
    }
}
