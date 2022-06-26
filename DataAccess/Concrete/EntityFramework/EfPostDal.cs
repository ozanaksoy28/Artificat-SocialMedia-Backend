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
    public class EfPostDal : EfEntityRepositoryBase<Post,MSContext> , IPostDal
    {
        public List<PostDetailDto> GetPostDetails()
                {
                    using (var context = new MSContext())
                    {
                        var result = from p in context.Posts
                                     join u in context.Users
                                     on p.UserId equals u.UserId
                                     select new PostDetailDto
                                     {
                                         UserId = u.UserId,
                                         NickName = u.NickName,
                                         ProfilePhotoPath = u.ProfilePhotoPath,
                                         PostId = p.PostId,
                                         PostPath = p.PostPath,
                                         PostTitle = p.PostTitle,
                                         PostDescription = p.PostDescription,
                                         LikeCount = p.LikeCount,
                                         DateTime = p.DateTime

                                     };
                        return result.ToList();
                    }
                }
        public List<Like> GetPostLikeDetail()
        {
            using (var context = new MSContext())
            {
                var postLikeDto = from p in context.Posts
                                  join l in context.Likes
                                  on p.PostId equals l.PostId
                                  select new Like
                                  {
                                      LikeId = l.LikeId,
                                      PostId = p.PostId,
                                      UserId = l.UserId
                                  };

                return postLikeDto.ToList();
            }
        }
        public List<UsersCommentDto> GetPostComments()
        {
            using (var context = new MSContext())
            {
                var result = from c in context.Comments
                             join u in context.Users
                             on c.UserId equals u.UserId
                             select new UsersCommentDto
                             {
                                 NickName = u.NickName,
                                 ProfilePhotoPath = u.ProfilePhotoPath,
                                 UserId = u.UserId,
                                 PostId = c.PostId,
                                 CommentMessage = c.CommentMessage,
                                 DateTime = c.DateTime

                             };
                return result.ToList();
            }
        }
        public PostDetailDto GetPostDetailsByPostId(int postId)
        {
            return GetPostDetails().SingleOrDefault(result => result.PostId == postId);
        }
        public List<PostDetailDto> GetPostDetailsByUserId(int userId)
        {
            return GetPostDetails().Where(result => result.UserId == userId).ToList();
        }
        public PostDetailDto GetPostDetailByUserId(int userId)
        {
            return GetPostDetails().FirstOrDefault(result => result.UserId == userId);
        }
        public List<UsersCommentDto> GetPostCommentsByPostId(int postId)
        {
            return GetPostComments().Where(result => result.PostId == postId).ToList();
        }
    }
}
