using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PostController : ControllerBase
    {
        IPostService _postService;
        IAWSS3Service _aWSS3Service;
        public PostController(IPostService postService,IAWSS3Service aWSS3Service)
        {
            _postService = postService;
            _aWSS3Service = aWSS3Service;
        }
        //All posts
        [HttpGet]
        public List<PostDetailDto> GetPosts()
        {
            return _postService.GetPostDetails().Data;
        }
        //User's posts that you are following
        [HttpGet("Following/{currentUserId}")]
        public List<PostDetailDto> GetFollowingPosts(int currentUserId)
        {
            return _postService.GetFollowingPosts(currentUserId).Data;
        }
        [HttpGet("Top-rated")]
        public List<PostDetailDto> GetTopRatedPosts()
        {
            return _postService.GetTopRatedPosts().Data;
        }
        [HttpGet("{id}")]
        public PostDetailDto GetPostDetails(int id)
        {
            return _postService.GetPostDetailsByPostId(id).Data;
        }
        [HttpGet("Users/{id}")]
        public List<PostDetailDto> GetUserPosts(int id)
        {
            return _postService.GetPostDetailsByUserId(id).Data;
        }
        [HttpPost]
        public void AddPost([FromForm]Post post)
        {
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var folderName = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                using(var stream = new FileStream(folderName, FileMode.Create))
                {
                    file.CopyTo(stream);
                    //check later.
                    post.PostPath=_aWSS3Service.PostToAWS(stream, post).Data;
                    System.IO.File.Delete(folderName);
                }
            }
            _postService.AddPost(post);
        }
        [HttpDelete]
        public void DeletePostById(int id)
        {
            _postService.DeleteById(id);
        }
    }
}
