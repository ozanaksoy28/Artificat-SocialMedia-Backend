using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LikeManager : ILikeService
    {
        IPostService _postService;
        IPostDal _postDal;
        ILikeDal _likeDal;
        IUserDal _userDal;
        public LikeManager(IPostService postService,IPostDal postDal,ILikeDal likeDal,IUserDal userDal)
        {
            _postService = postService;
            _postDal = postDal;
            _likeDal = likeDal;
            _userDal = userDal;
        }

        public IDataResult<bool> CheckLike(Like like)
        {
            if (like == null)
                {
                return new SuccessDataResult<bool>(true);
                }
            return new SuccessDataResult<bool>(false);
        }
        public IResult ManageLike(Like like)
        {
            var unLike = GetLikeById(like.UserId, like.PostId).Data;
            if (CheckLike(unLike).Data)
            {
                LikeToPost(like);
                return new SuccessResult("Gönderi Beğenildi.");
            }
            UnLikeToPost(unLike);
            return new SuccessResult("Gönderinin beğenisi kaldırıldı.");
        }
        public IResult LikeToPost(Like like)
        {

            _likeDal.Add(like);
            var post = _postService.Get(like.PostId).Data;
            post.LikeCount = GetPostLikes(like.PostId).Data.Count;
            _postService.UpdatePost(post);
            return new SuccessResult();

        }

        public IResult UnLikeToPost(Like like)
        {
            _likeDal.Delete(like);
            var post = _postService.Get(like.PostId).Data;
            post.LikeCount = GetPostLikes(like.PostId).Data.Count;
            _postService.UpdatePost(post);
            return new SuccessResult();
        }
        public IResult UnLikeToList(List<Like> likes)
        {
            foreach(var like in likes)
            {
                UnLikeToPost(like);
            }
            return new SuccessResult();
        }
        public IDataResult<List<User>> GetPostLikes(int postId)
        {
            var likes = _likeDal.GetAll(like => like.PostId == postId);
            List<User> users = new List<User>();
            foreach (var like in likes)
            {
                users.Add(_userDal.Get(user => user.UserId == like.UserId));
            }
            return new SuccessDataResult<List<User>>(users);
        }
        public IDataResult<List<PostDetailDto>> GetUserLikes(int userId)
        {
            using (var context = new MSContext())
            {
                var likes = _likeDal.GetAll(like => like.UserId == userId);
                List<PostDetailDto> postsDetailDto = new List<PostDetailDto>();
                foreach (var like in likes)
                {
                    postsDetailDto.Add(_postDal.GetPostDetailsByPostId(like.PostId));
                }
                return new SuccessDataResult<List<PostDetailDto>>(postsDetailDto);
            }
        }
        public IDataResult<Like> GetLikeById(int userId,int postId)
        {
            return new SuccessDataResult<Like>(_likeDal.Get(l => l.PostId == postId && l.UserId == userId));
        }

        public IDataResult<List<Like>> GetLikesByUserId(int userId)
        {
            return new SuccessDataResult<List<Like>>(_likeDal.GetAll(l => l.UserId == userId));
        }
        public IDataResult<List<Like>> GetLikesByPostId(int postId)
        {
            return new SuccessDataResult<List<Like>>(_likeDal.GetAll(l => l.PostId == postId));
        }

    }
}
