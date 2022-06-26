using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        public IDataResult<bool> Login(string nickname, string password);
        public IDataResult<bool> VerifyPassword(string password, string hashedPassword);
    }
}
