using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        public IResult AddComment(Comment comment);
        public IDataResult<List<Comment>> GetAllComments();
        public IResult DeleteComment(Comment comment);
        public IResult DeleteCommentList(List<Comment> comments);
        public IDataResult<List<UsersCommentDto>> GetAllCommentsByPostId(int postId);
        public IDataResult<List<Comment>> GetCommentsByUserId(int userId);
        public IDataResult<List<Comment>> GetCommentsByPostId(int postId);
    }
}
