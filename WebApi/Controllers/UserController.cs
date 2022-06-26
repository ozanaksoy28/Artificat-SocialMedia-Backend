using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IAWSS3Service _aWSS3Service;
        public UserController(IUserService userService,IAWSS3Service aWSS3Service) // (UserManager userService) Bug fixed. "System.InvalidOperationException: Unable to resolve service" exception occured.
        {
            _userService = userService;
            _aWSS3Service = aWSS3Service;
        }
        
        [HttpGet]
        public List<User> GetUser()
        {
            return _userService.GetAll().Data;
        }
        [HttpGet("/Details/{nickName}")]
        public UserProfileDto GetUserProfileById(string nickName)
        {
            return _userService.GetUserProfileByNickName(nickName).Data;
        }
        
        [HttpGet("{nickName}")]
        public User GetByNick(string nickName)
        {
            return _userService.GetByNick(nickName).Data;
        }
        
        [HttpPost("update")]
        public void UpdateUser([FromForm] User updatedUser)
        {
            
            if(Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var folderName = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                    using (var stream = new FileStream(folderName, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        // check it later.
                        updatedUser.ProfilePhotoPath = _aWSS3Service.PostToAWS(stream, updatedUser).Data;
                        System.IO.File.Delete(folderName);
                    }
                }
            }
            
            _userService.Update(updatedUser);
        }
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            _userService.Delete(id);
        }
    }
}
