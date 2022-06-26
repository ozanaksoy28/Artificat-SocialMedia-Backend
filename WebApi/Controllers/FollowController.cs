using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        IFollowService _followService;
        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }
        [HttpPost]
        public void Follow(Follow follow)
        {
            _followService.ManageFollow(follow);
        }
        [HttpGet]
        public List<Follow> GetFollows()
        {
            return _followService.GetAll().Data;
        }
        [HttpGet("/Follower/{currentUserId}")]
        public List<UserFollowDto> GetFollowsByUserId(int currentUserId)
        {
            return _followService.GetAllByUserId(currentUserId).Data;
        }
        [HttpGet("/Following/{currentUserId}")]
        public List<UserFollowDto> GetFollowsByFollowerId(int currentUserId)
        {
            return _followService.GetAllByFollowerId(currentUserId).Data;
        }
    }
}
