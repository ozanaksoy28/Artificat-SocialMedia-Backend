using Business.Abstract;
using Entities.Concrete;
using System;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class FollowManager : IFollowService
    {
        IFollowDal _followDal;
        public FollowManager(IFollowDal followDal)
        {
            _followDal = followDal;
        }
        public IResult Follow(Follow follow)
        {
            _followDal.Add(follow);
            return new SuccessResult("Takip edildi.");
        }
        public IDataResult<int> FollowerCount(int currentUserId)
        {
            return new SuccessDataResult<int>(_followDal.GetAll(f => f.UserId == currentUserId).Count());
        }
        public IDataResult<int> FollowingCount(int currentUserId)
        {
            return new SuccessDataResult<int>(_followDal.GetAll(f => f.FollowerId == currentUserId).Count());
        }

        public IResult UnFollow(Follow follow)
        {
            _followDal.Delete(follow);
            return new SuccessResult("Takipten çıkarıldı.");
        }
        public IDataResult<Follow> GetFollowById(int followerId, int userId)
        {
            return new SuccessDataResult<Follow>(_followDal.Get(f => f.FollowerId == followerId && f.UserId == userId));
        }
        public IDataResult<bool> CheckFollow(Follow follow)
        {
            if(follow == null)
            {
                return new SuccessDataResult<bool>(true);
            }
            return new ErrorDataResult<bool>(false);
        }
        public IResult ManageFollow(Follow follow)
        {
            var unFollow = GetFollowById(follow.FollowerId, follow.UserId).Data;
            if (CheckFollow(unFollow).Data)
            {
                Follow(follow);
                return new SuccessResult();
            }
            UnFollow(unFollow);
            return new SuccessResult();
        }

        public IDataResult<List<Follow>> GetAll()
        {
            return new SuccessDataResult<List<Follow>>(_followDal.GetAll());
        }

        public IDataResult<List<UserFollowDto>> GetAllByUserId(int id)
        {
            return new SuccessDataResult<List<UserFollowDto>>(_followDal.GetFollowsByUserId(id));
        }

        public IDataResult<List<UserFollowDto>> GetAllByFollowerId(int id)
        {
            return new SuccessDataResult<List<UserFollowDto>>(_followDal.GetFollowsByFollowerId(id));
        }
    }
}
