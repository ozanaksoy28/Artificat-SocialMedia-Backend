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
    public interface IUserService
    {
        IDataResult<bool> Add(User user);
        IResult Update(User user);
        IResult Delete(int id);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByNick(string nickName);
        IDataResult<User> GetById(int id);
        IDataResult<UserProfileDto> GetUserProfileByNickName(string nickName);
    }
}
