using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CircularDependencyManager:ICircularDependencyService
    {
        IPostDal _postDal;
        ILikeDal _likeDal;
        ICommentDal _commentDal;
        public CircularDependencyManager(IPostDal postDal,ILikeDal likeDal, ICommentDal commentDal)
        {
            _postDal = postDal;
            _likeDal = likeDal;
            _commentDal = commentDal;
        }
        public IResult DeletePost(int postId)
        {
            try
            {
                List<Like> likes = _likeDal.GetAll(l => l.PostId == postId);
                foreach(var like in likes)
                {
                    _likeDal.Delete(like);
                }
                List<Comment> comments = _commentDal.GetAll(c => c.PostId == postId);
                foreach(var comment in comments)
                {
                    _commentDal.Delete(comment);
                }
                var post = _postDal.Get(p=>p.PostId==postId);
                _postDal.Delete(post);
                return new SuccessResult("Gönderi Silindi.");
            }
            catch(Exception e)
            {
                return new ErrorResult("Gönderi Silinemedi : "+e.Message);
            }
            
        }
        
        
    }
}
