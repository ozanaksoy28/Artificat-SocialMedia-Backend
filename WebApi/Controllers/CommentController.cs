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
    public class CommentController : ControllerBase
    {
        ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public void CommentToPost([FromBody]Comment comment)
        {
            _commentService.AddComment(comment);
        }
        [HttpGet]
        public List<Comment> GetAllComments()
        {
            return _commentService.GetAllComments().Data;
        }
        [HttpGet("Post/{postId}")]
        public List<UsersCommentDto> GetAllCommentsByPostId(int postId)
        {
            return _commentService.GetAllCommentsByPostId(postId).Data;
        }
    }
}
