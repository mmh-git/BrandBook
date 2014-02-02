using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;

namespace BrandBookRepository
{
    public class UserProfileRepository
    {
        private DBUserProfile _dbUserProfile;


        // default constructor
        public UserProfileRepository()
        {
            _dbUserProfile = new DBUserProfile();
        }

        public UserProfile GetUserProfile(UserModel userModel)
        {
            UserProfile userProfile = new UserProfile();
            StatusUpdateModel _statusUpdateModel;
            CommentModel _commentModel;
            LikeModel _likeModel;
            BrandModel brandModel;
            ProjectModel projectModel;
            List<BrandModel> brands = new List<BrandModel>();
            List<ProjectModel> projects = new List<ProjectModel>();
            List<StatusUpdateModel> statusList = new List<StatusUpdateModel>();
            List<CommentModel> commentList = new List<CommentModel>();
            List<LikeModel> likeList = new List<LikeModel>();
            try
            {
                DataSet dsUserProfile = _dbUserProfile.GetUserProfile(userModel);

                DataTable dtUserDetails = dsUserProfile.Tables["ud"];

                foreach (DataRow dr in dtUserDetails.Rows)
                {
                    userModel.FirstName = dr["FirstName"].ToString();
                    userModel.LastName = dr["LastName"].ToString();
                    userModel.Address = dr["Address"].ToString();
                    userModel.City = dr["City"].ToString();
                    userModel.Country = dr["Country"].ToString();
                    userModel.DateOfBirth = Convert.ToDateTime("DOB");
                    userModel.Gender = dr["Gender"].ToString();
                    userModel.ProfilePicID = dr["proPicId"].ToString();
                    userModel.ProfilePicUrl = dr["ImageUrl"].ToString();
                    userModel.Desination = dr["Designation"].ToString();
                    userModel.Phone = dr["Phone"].ToString();
 
                }
                userProfile.user = userModel;
                DataTable dtUsrStatus = dsUserProfile.Tables["userStatus"];
                foreach (DataRow dr in dtUsrStatus.Rows)
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
                DataTable dtComment = dsUserProfile.Tables["cmt"];
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
                DataTable dtLikes = dsUserProfile.Tables["L"];
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
                userProfile.statusUpdates = statusList;
                DataTable dtBrands = dsUserProfile.Tables["ub"];
                foreach (DataRow dr in dtBrands.Rows)
                {
                    brandModel = new BrandModel() {
                        BrandId = Convert.ToInt32(dr["BrandID"]),
                        BrandName = dr["BrandName"].ToString(),
                        BrandDesc = dr["BrandDesc"].ToString()
                    };
                    brands.Add(brandModel);
                }
                DataTable dtProjects = dsUserProfile.Tables["up"];
                foreach (DataRow dr in dtProjects.Rows)
                {
                    projectModel = new ProjectModel() { 
                    ProjectID=Convert.ToInt32(dr["ProjectID"]),
                    ProjectName=dr["ProjectName"].ToString(),
                    ProjectDesc=dr["ProjectDesc"].ToString()
                    };
                    projects.Add(projectModel);
                }
                userProfile.brands = brands;
                userProfile.projects = projects;
            }
            catch (Exception exception)
            {
                //
            }
            
            
            return userProfile;
        }

        //public StatusUpdateModel SaveStatus(StatusUpdateModel statusUpdateModel)
        //{
            
        //    DataTable dt= _dbStatus.SaveStatus(statusUpdateModel);
        //    DataRow dr = dt.Rows[0];
        //    statusUpdateModel.StatusID = Convert.ToInt32(dr["StatusID"]);
        //        statusUpdateModel.StatusByUserID = Convert.ToInt32(dr["StatusByUserID"]);
        //        statusUpdateModel.StatusContent = dr["StatusContent"].ToString();
        //        statusUpdateModel.StatusType = dr["StatusType"].ToString();
        //        statusUpdateModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
        //        statusUpdateModel.FullName = dr["FullName"].ToString();
        //        statusUpdateModel.PicID = Convert.ToInt32(dr["PicID"]);
        //        statusUpdateModel.ProPicUrl = dr["ProPicUrl"].ToString();
        //        return statusUpdateModel;
           
        //}
       
    }
}
