using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        IUserDal _userDal;
        public AuthManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IDataResult<bool> Login(string nickname, string password)
        {
            try
            {
                using (var context = new MSContext())
                {
                    User userGonnaLogin = _userDal.Get(user => user.NickName == nickname);
                    return VerifyPassword(password, userGonnaLogin.PasswordHashed);
                } 
            }catch
            {
                return new ErrorDataResult<bool>(false,"Giriş Başarısız.");
            }
                 
        }
        public IDataResult<bool> VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i]) { 
                    return new ErrorDataResult<bool>(false, "Şifre Hatalı");
                }
            return new SuccessDataResult<bool>(true);
        }

    }
}
