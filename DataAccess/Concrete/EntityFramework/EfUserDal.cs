using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal :EfEntityRepositoryBase<User,MSContext> , IUserDal
    {
        IFollowDal _followDal;
        IPostDal _postDal;
        
        public EfUserDal(IFollowDal followDal, IPostDal postDal)
        {
            _followDal = followDal;
            _postDal = postDal;
        }
        public List<Like> GetUserLikes(int userId)
        {
            using (var context = new MSContext())
            {
                var userLikesDto = from l in context.Likes
                                   join u in context.Users
                                   on l.UserId equals u.UserId
                                   select new Like
                                   {
                                       LikeId = l.LikeId,
                                       PostId = l.PostId,
                                       UserId = u.UserId
                                   };
                return userLikesDto.ToList();
            }
        }
        public UserProfileDto GetUserProfile(string nickName)
        {
            var user = Get(u => u.NickName == nickName);
            return new UserProfileDto()
            {
                NickName = user.NickName,
                Description = user.Description,
                FollowerCount = _followDal.GetFollowsByUserId(user.UserId).Count(),
                UserId = user.UserId,
                FollowingCount = _followDal.GetFollowsByFollowerId(user.UserId).Count(),
                Instagram = user.Instagram,
                PostCount = _postDal.GetAll(u => u.UserId == user.UserId).Count(),
                ProfilePhotoPath = user.ProfilePhotoPath,
                Twitter = user.Twitter
            };
        }

        public UserProfileDto GetUserProfileByNickName(string nickName)
        {
            return GetUserProfile(nickName);
        }
    }
}
