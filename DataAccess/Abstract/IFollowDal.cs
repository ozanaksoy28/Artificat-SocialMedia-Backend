using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFollowDal:IEntityRepository<Follow>
    {
        public List<UserFollowDto> GetFollower();
        public List<UserFollowDto> GetFollowing();
        public List<UserFollowDto> GetFollowsByUserId(int id);
        public List<UserFollowDto> GetFollowsByFollowerId(int id);
    }
}
