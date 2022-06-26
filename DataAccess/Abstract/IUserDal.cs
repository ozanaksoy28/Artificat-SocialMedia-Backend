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
    public interface IUserDal:IEntityRepository<User>
    {
        public List<Like> GetUserLikes(int userId);
        public UserProfileDto GetUserProfile(string nickName);
        public UserProfileDto GetUserProfileByNickName(string nickName);
    }
}
