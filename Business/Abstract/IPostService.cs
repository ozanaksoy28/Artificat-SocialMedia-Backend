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
    public interface IPostService
    {
        public IResult AddPost(Post post);
        public IResult UpdatePost(Post post);
        public IDataResult<List<Post>> GetAll();
        public IDataResult<List<Post>> GetAll(int userId);
        public IDataResult<Post> Get(int id);
        public IResult Delete(Post post);
        public IResult DeleteList(List<Post> posts);
        public IResult DeleteById(int id);
        public IDataResult<List<PostDetailDto>> GetPostDetails();
        public IDataResult<List<PostDetailDto>> GetPostDetailsByUserId(int userId);
        public IDataResult<PostDetailDto> GetPostDetailsByPostId(int postId);
        public IDataResult<List<PostDetailDto>> GetFollowingPosts(int followingId);
        public IDataResult<List<PostDetailDto>> GetTopRatedPosts();
    }
}
