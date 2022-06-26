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
    public class LikeController : ControllerBase
    {
        ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        [HttpPost]
        public void Like(Like like)
        {
            _likeService.ManageLike(like);
        }
        [HttpGet("PostLike/{postId}")]
        public List<User> GetPostLikes(int postId)
        {
            return _likeService.GetPostLikes(postId).Data;
        }
        [HttpGet("UserLike/{userId}")]
        public List<PostDetailDto> GetUserLikes(int userId)
        {
            return _likeService.GetUserLikes(userId).Data;
        }
    }
}
