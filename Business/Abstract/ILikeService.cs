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
    public interface ILikeService
    {
        public IDataResult<bool> CheckLike(Like like);
        public IResult LikeToPost(Like like);
        public IResult UnLikeToPost(Like like);
        public IResult UnLikeToList(List<Like> likes);
        public IDataResult<List<Like>> GetLikesByUserId(int userId);
        public IDataResult<List<Like>> GetLikesByPostId(int postId);
        public IDataResult<List<User>> GetPostLikes(int postId);
        public IDataResult<Like> GetLikeById(int userId,int postId);
        public IDataResult<List<PostDetailDto>> GetUserLikes(int userId);
        public IResult ManageLike(Like like);
    }
}
