using System;
using System.Collections.Generic;
using System.Data;
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
        public List<LikeModel> SaveLike(LikeModel likeModel)
        {
            DataTable dt= _dbLike.SaveLike(likeModel);

            List<LikeModel> likes = new List<LikeModel>();
            LikeModel like;
            foreach (DataRow dr in dt.Rows)
            {
                like = new LikeModel()
                {
                    LikedContentType = dr["LikedContentType"].ToString(),
                    LikedContentID = Convert.ToInt32(dr["LikedContentId"]),
                    LikedByUserID = Convert.ToInt32(dr["LikedByUserId"]),
                    LikeID = Convert.ToInt32(dr["LikeID"]),
                    CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                    LikedByUserFullName = dr["FirstName"].ToString()
                };
                likes.Add(like);
            }
            return likes;
        }

        public List<UserModel> GetLikedUserCollection(int LikedContentId)
        {
            DataTable dt = _dbLike.GetLikedUserCollection(LikedContentId);
            List<UserModel> likedUsers = new List<UserModel>();
            UserModel user;
            foreach (DataRow dr in dt.Rows)
            {
                user = new UserModel();
                user.FirstName=dr["FirstName"].ToString();
                user.LastName=dr["LastName"].ToString();
                user.ProfilePicID = dr["ImageID"].ToString();
                user.ProfilePicUrl = dr["ImageUrl"].ToString();
                likedUsers.Add(user);
            }
            return likedUsers;
        }
    }
}
