using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;

namespace BrandBookRepository
{
    public class StatusRepository
    {
        private DBStatus _dbStatus;


        // default constructor
        public StatusRepository()
        {
            _dbStatus = new DBStatus();
        }

        public List<StatusUpdateModel> GetStatusList(StatusUpdateModel statusUpdateModel)
        {
            StatusUpdateModel _statusUpdateModel;
            CommentModel _commentModel;
            LikeModel _likeModel;
            List<StatusUpdateModel> statusList = new List<StatusUpdateModel>();
            List<CommentModel> commentList = new List<CommentModel>();
            List<LikeModel> likeList = new List<LikeModel>();
            try
            {
                DataSet dsStatus = _dbStatus.GetStatusList(statusUpdateModel);
                DataTable dtStatus = dsStatus.Tables[0];
                foreach (DataRow dr in dtStatus.Rows)
                {
                    _statusUpdateModel = new StatusUpdateModel()
                    {
                        StatusID = Convert.ToInt32(dr["StatusID"]),
                        StatusByUserID = Convert.ToInt32(dr["StatusByUserID"]),
                        StatusContent = dr["StatusContent"].ToString(),
                        fileDesc=dr["fileDesc"].ToString(),
                        StatusType = dr["StatusType"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        FullName = dr["FullName"].ToString(),
                        PicID = Convert.ToInt32(dr["PicID"]),
                        ProPicUrl=dr["ProPicUrl"].ToString()
                    };
                    statusList.Add(_statusUpdateModel);
                }
                DataTable dtComment = dsStatus.Tables[1];
                foreach (DataRow dr in dtComment.Rows)
                {
                    _commentModel = new CommentModel() 
                    {
                        StatusID=Convert.ToInt32(dr["StatusID"]),
                        CommentedByUserFullName = dr["CommentedByUserFullName"].ToString(),
                        CommentedByUserID = Convert.ToInt32(dr["CommentedByUserID"]),
                        CommentedByUserProPicID = Convert.ToInt32(dr["CommentedByUserProPicID"]),
                        CommentedByUserProPicUrl = dr["CommentedByUserProPicUrl"].ToString(),
                        CommentID = Convert.ToInt32(dr["CommentID"]),
                        CommentContent = dr["CommentContent"].ToString(),
                        CommentType = dr["CommentType"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                    };
                    commentList.Add(_commentModel);
                }
                DataTable dtLikes = dsStatus.Tables[2];
                foreach (DataRow dr in dtLikes.Rows)
                {
                    _likeModel = new LikeModel() {
                        LikedByUserFullName = dr["LikedByUserFullName"].ToString(),
                        LikedByUserID = Convert.ToInt32(dr["LikedByUserID"]),
                        LikedContentID = Convert.ToInt32(dr["LikedContentID"]),
                        LikedContentType = dr["LikedContentType"].ToString(),
                        CreatedDate=Convert.ToDateTime(dr["CreatedDate"]),
                        LikeID=Convert.ToInt32(dr["LikeID"])
                    };
                    likeList.Add(_likeModel);
                }

                List<LikeModel> likesOnComment=likeList.Where(l=>l.LikedContentType=="C").ToList();
                foreach (CommentModel cmt in commentList)
                {
                    cmt.Likes = likesOnComment.Where(l => l.LikedContentID == cmt.CommentID).ToList();
                }
                List<LikeModel> likesOnStatus = likeList.Where(l => l.LikedContentType == "S").ToList();
                foreach (StatusUpdateModel st in statusList)
                {
                    st.Comments = commentList.Where(cmt=>cmt.StatusID==st.StatusID).ToList();
                    st.Likes = likesOnStatus.Where(l => l.LikedContentID == st.StatusID).ToList();
                }

            }
            catch (Exception exception)
            {
                //
            }

            return statusList;
        }

        public StatusUpdateModel SaveStatus(StatusUpdateModel statusUpdateModel)
        {
            
            DataTable dt= _dbStatus.SaveStatus(statusUpdateModel);
            DataRow dr = dt.Rows[0];
            statusUpdateModel.StatusID = Convert.ToInt32(dr["StatusID"]);
                statusUpdateModel.StatusByUserID = Convert.ToInt32(dr["StatusByUserID"]);
                statusUpdateModel.StatusContent = dr["StatusContent"].ToString();
                statusUpdateModel.StatusType = dr["StatusType"].ToString();
                statusUpdateModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                statusUpdateModel.FullName = dr["FullName"].ToString();
                statusUpdateModel.PicID = Convert.ToInt32(dr["PicID"]);
                statusUpdateModel.ProPicUrl = dr["ProPicUrl"].ToString();
                return statusUpdateModel;
           
        }
       
    }
}
