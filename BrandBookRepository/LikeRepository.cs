using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;
namespace BrandBookRepository
{
   public class LikeRepository
    {
        DBLike _dbLike;
        public LikeRepository()
        {
            _dbLike = new DBLike();
        }
        public LikeModel SaveLike(LikeModel likeModel)
        {
            return _dbLike.SaveLike(likeModel);
        }
    }
}
