using Business.Abstract;
using Core.Utilities.Results;
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
    public class LoginController : ControllerBase
    {
        IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public bool Login(User user)
        {
             return _authService.Login(user.NickName, user.PasswordHashed).Data;
        }
    }
}
