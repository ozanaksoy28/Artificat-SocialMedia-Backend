using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public bool AddUser([FromBody] User newUser)
        {
            return _userService.Add(newUser).Data;
        }
    }
}
