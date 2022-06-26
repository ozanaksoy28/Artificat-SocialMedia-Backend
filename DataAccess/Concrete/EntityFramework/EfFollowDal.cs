using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFollowDal :EfEntityRepositoryBase<Follow,MSContext>, IFollowDal
    {
        public List<UserFollowDto> GetFollower()
        {
            using (var context = new MSContext())
            {
                var result = from f in context.Follows
                             join u in context.Users
                             on f.FollowerId equals u.UserId
                             select new UserFollowDto
                             {
                                 UserId = f.UserId,
                                 FollowerId = f.FollowerId,
                                 NickName = u.NickName,
                                 ProfilePhotoPath = u.ProfilePhotoPath
                             };
                return result.ToList();
            }
        }
        public List<UserFollowDto> GetFollowing()
        {
            using (var context = new MSContext())
            {
                var result = from f in context.Follows
                             join u in context.Users
                             on f.UserId equals u.UserId
                             select new UserFollowDto
                             {
                                 UserId = f.UserId,
                                 FollowerId = f.FollowerId,
                                 NickName = u.NickName,
                                 ProfilePhotoPath = u.ProfilePhotoPath
                             };
                return result.ToList();
            }
        }
        public List<UserFollowDto> GetFollowsByUserId(int id)
        {
            return GetFollower().Where(f => f.UserId == id).ToList();
        }
        public List<UserFollowDto> GetFollowsByFollowerId(int id)
        {
            return GetFollowing().Where(f => f.FollowerId == id).ToList();
        }
    }
}
