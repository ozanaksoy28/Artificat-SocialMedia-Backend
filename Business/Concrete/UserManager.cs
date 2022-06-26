using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IPostService _postService;
        ILikeService _likeService;
        ICommentService _commentService;
        public UserManager(IUserDal userDal, IPostService postService, ILikeService likeService, ICommentService commentService)
        {
            _userDal = userDal;
            _postService = postService;
            _likeService = likeService;
            _commentService = commentService;
        }
        public IDataResult<bool> Add(User user)
        {
            HashPassword(user);
            try
            {
                _userDal.Add(user);
                return new SuccessDataResult<bool>(true);
            }
            catch
            {
                return new ErrorDataResult<bool>(false);
            }
        }
        public IResult Delete(int userId)
        {
            
            List<Post> userPosts = _postService.GetAll(userId).Data;
            List<Like> userLike = _likeService.GetLikesByUserId(userId).Data;
            List<Comment> userComments = _commentService.GetCommentsByUserId(userId).Data;
            _commentService.DeleteCommentList(userComments);
            _likeService.UnLikeToList(userLike);
            _postService.DeleteList(userPosts);
            User user = GetById(userId).Data;
            _userDal.Delete(user);
            return new SuccessDataResult<bool>(true);
        }
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        public IResult Update(User user)
        {

            User tempUser = GetById(user.UserId).Data;
            if (user.PasswordHashed == null)
            {
                user.PasswordHashed = tempUser.PasswordHashed;
            }
            if(user.Email == null)
            {
                user.Email = tempUser.Email;
            }
            _userDal.Update(user);
            return new SuccessResult();
        }
        public IDataResult<User> GetByNick(string nickName)
        {
            return new SuccessDataResult<User>(_userDal.Get(user => user.NickName == nickName));
        }
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(user => user.UserId == id));
        }
        public static IResult HashPassword(User user)
        {
            //Create salt value with a cryprographic PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //Create the Rfc2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(user.PasswordHashed, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            //Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            //Turn the combine salt+hash into string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            user.PasswordHashed = savedPasswordHash;
            return new SuccessResult("Parola Şifrelendi.");
            
        }

        public IDataResult<UserProfileDto> GetUserProfileByNickName(string nickName)
        {
            return new SuccessDataResult<UserProfileDto>(_userDal.GetUserProfileByNickName(nickName));
        }
    }
}
