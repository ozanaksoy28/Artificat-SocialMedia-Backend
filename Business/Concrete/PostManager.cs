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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PostManager : IPostService
    {
        IPostDal _postDal;
        IFollowDal _followDal;
        ICircularDependencyService _circularDependencyService;
        public PostManager(IPostDal postDal, ICircularDependencyService circularDependencyService,IFollowDal followDal)
        {
            _postDal = postDal;
            _circularDependencyService = circularDependencyService;
            _followDal = followDal;
        }
        public IResult AddPost(Post post)
        {
            TimeZoneInfo Turkey_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime dateTime_Turkey = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Turkey_Standard_Time);
            post.DateTime = dateTime_Turkey;
            _postDal.Add(post);
            return new SuccessResult();
        }
        public IResult UpdatePost(Post post)
        {
            _postDal.Update(post);
            return new SuccessResult();
        }
        public IDataResult<List<Post>> GetAll()
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll());
        }
        public IDataResult<List<Post>> GetAll(int userId)
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(p => p.UserId == userId));
        }
        public IDataResult<Post> Get(int id)
        {
            return new SuccessDataResult<Post>(_postDal.Get(p => p.PostId == id));
        }
        public IDataResult<List<PostDetailDto>> GetPostDetails()
        {
            return new SuccessDataResult<List<PostDetailDto>>(_postDal.GetPostDetails());
        }
        public IDataResult<PostDetailDto> GetPostDetailsByPostId(int postId)
        {
            return new SuccessDataResult<PostDetailDto>(_postDal.GetPostDetailsByPostId(postId));
        }
        public IDataResult<List<PostDetailDto>> GetPostDetailsByUserId(int userId)
        {
            return new SuccessDataResult<List<PostDetailDto>>(_postDal.GetPostDetailsByUserId(userId));
        }
        public IResult Delete(Post post)
        {
            _postDal.Delete(post);
            return new SuccessResult();
        }
        public IResult DeleteById(int id)
        {
            _circularDependencyService.DeletePost(id);
            return new SuccessResult();
        }
        public IResult DeleteList(List<Post> posts)
        {
            foreach(var post in posts)
            {
                Delete(post);
            }
            return new SuccessResult();
        }

        public IDataResult<List<PostDetailDto>> GetFollowingPosts(int currentUserId)
        {
            List<PostDetailDto> postDetailDtos = new List<PostDetailDto>();
            List<UserFollowDto> userFollowDtos = _followDal.GetFollowsByFollowerId(currentUserId);
            foreach(var userFollowDto in userFollowDtos)
            {
                var postsDetail = _postDal.GetPostDetailsByUserId(userFollowDto.UserId);
                foreach(var posts in postsDetail)
                {
                    postDetailDtos.Add(posts);
                }
                //var postDetail = _postDal.GetPostDetailByUserId(userFollowDto.UserId);
                //postDetailDtos.Add(postDetail);
            }
            postDetailDtos.OrderBy(p => p.DateTime).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(postDetailDtos);
        }
        public IDataResult<List<PostDetailDto>> GetTopRatedPosts()
        {
            List<PostDetailDto> sortedList = _postDal.GetPostDetails().OrderBy(p => p.LikeCount).ToList();
            sortedList.Reverse();
            return new SuccessDataResult<List<PostDetailDto>>(sortedList);
        }
    }
}
