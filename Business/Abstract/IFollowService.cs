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
    public interface IFollowService
    {
        public IResult Follow(Follow follow);
        public IResult UnFollow(Follow follow);
        public IDataResult<int> FollowingCount(int currentUserId);
        public IDataResult<int> FollowerCount(int currentUserId);
        public IDataResult<List<Follow>> GetAll();
        public IDataResult<List<UserFollowDto>> GetAllByUserId(int id);
        public IDataResult<List<UserFollowDto>> GetAllByFollowerId(int id);
        public IResult ManageFollow(Follow follow);
    }
}
