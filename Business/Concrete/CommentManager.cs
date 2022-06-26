using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager:ICommentService
    {
        ICommentDal _commentDal;
        IPostDal _postDal;
        public CommentManager(ICommentDal commentDal,IPostDal postDal)
        {
            _commentDal = commentDal;
            _postDal = postDal;
        }
        public IResult AddComment(Comment comment)
        {
            _commentDal.Add(comment);
            return new SuccessResult("Yorum eklendi.");
        }

        public IResult DeleteComment(Comment comment)
        {
            _commentDal.Delete(comment);
            return new SuccessResult("Yorum silindi.");
        }
        public IResult DeleteCommentList(List<Comment> comments)
        {
            foreach(var comment in comments)
            {
                DeleteComment(comment);
            }
            return new SuccessResult("Yorumlar Silindi.");
        }
        public IDataResult<List<Comment>> GetAllComments()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll());
        }
        public IDataResult<List<UsersCommentDto>> GetAllCommentsByPostId(int postId)
        {
            return new SuccessDataResult<List<UsersCommentDto>>(_postDal.GetPostCommentsByPostId(postId));
        }

        public IDataResult<List<Comment>> GetCommentsByPostId(int postId)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(c => c.PostId == postId));
        }

        public IDataResult<List<Comment>> GetCommentsByUserId(int userId)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(c => c.UserId == userId));
        }
    }
}
