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
    public interface IPostDal:IEntityRepository<Post>
    {
        public List<UsersCommentDto> GetPostComments();
        public List<UsersCommentDto> GetPostCommentsByPostId(int postId);
        public List<PostDetailDto> GetPostDetails();
        public PostDetailDto GetPostDetailsByPostId(int postId);
        public List<PostDetailDto> GetPostDetailsByUserId(int userId);
        public PostDetailDto GetPostDetailByUserId(int userId);
        public List<Like> GetPostLikeDetail();
    }
}
