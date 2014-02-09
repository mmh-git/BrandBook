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

        public BrandModel GetBrandList(int userDetailsId)
        {
            BrandModel _brandModel = new BrandModel();
            
            DataTable dt = _dbUserProfile.getBrandList(userDetailsId);

            foreach (DataRow dr in dt.Rows)
            {
                _brandModel = new BrandModel();
                _brandModel.BrandId = Convert.ToInt16(dr["BrandID"]);
                _brandModel.BrandName = dr["BrandName"].ToString();
                _brandModel.BrandDesc = dr["BrandDesc"].ToString();
               
            }
            return _brandModel;
        }
        public ProjectModel GetProjectList(int userDetailsId)
        {
            ProjectModel _brandModel = new ProjectModel();
            
            DataTable dt = _dbUserProfile.getBrandList(userDetailsId);

            foreach (DataRow dr in dt.Rows)
            {
                _brandModel = new ProjectModel();
                _brandModel.ProjectID = Convert.ToInt16(dr["ProjectID"]);
                _brandModel.ProjectName = dr["ProjectName"].ToString();
                _brandModel.ProjectDesc = dr["ProjectDesc"].ToString();
                
            }
            return _brandModel;
        }

        public UserProfile GetUserProfile(UserModel userModel)
        {
            UserProfile userProfile = new UserProfile();
            StatusUpdateModel _statusUpdateModel;
            CommentModel _commentModel;
            LikeModel _likeModel;
            BrandModel brandModel = new BrandModel();
            ProjectModel projectModel = new ProjectModel(); ;
            UserModel _userModel;
            List<UserModel> users = new List<UserModel>();
            List<BrandModel> brands = new List<BrandModel>();
            List<ProjectModel> projects = new List<ProjectModel>();
            List<StatusUpdateModel> statusList = new List<StatusUpdateModel>();
            List<CommentModel> commentList = new List<CommentModel>();
            List<LikeModel> likeList = new List<LikeModel>();
            try
            {
                DataSet dsUserProfile = _dbUserProfile.GetUserProfile(userModel);

                DataTable dtUserDetails = dsUserProfile.Tables[0];

                foreach (DataRow dr in dtUserDetails.Rows)
                {
                    userModel.FirstName = dr["FirstName"].ToString();
                    userModel.LastName = dr["LastName"].ToString();
                    userModel.Address = dr["Address"].ToString();
                    userModel.UserName = dr["UserName"].ToString();
                    userModel.Extention = dr["Extension"].ToString();
                    userModel.Gender = dr["Gender"].ToString();
                    userModel.ProfilePicID = dr["proPicId"].ToString();
                    userModel.ProfilePicUrl = dr["ImageUrl"].ToString();
                    userModel.Desination = dr["Designation"].ToString();
                    userModel.Mobile = dr["Mobile"].ToString();
                    userModel.Email = dr["Email"].ToString();
 
                }
                userProfile.user = userModel;
                DataTable dtUsrStatus = dsUserProfile.Tables[1];
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
                DataTable dtComment = dsUserProfile.Tables[2];
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
                DataTable dtLikes = dsUserProfile.Tables[3];
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
                DataTable dtBrands = dsUserProfile.Tables[4];
                foreach (DataRow dr in dtBrands.Rows)
                {
                    brandModel = new BrandModel() {
                        BrandId = Convert.ToInt32(dr["BrandID"]),
                        BrandName = dr["BrandName"].ToString(),
                        BrandDesc = dr["BrandDesc"].ToString()
                    };
                    
                }
                DataTable dtProjects = dsUserProfile.Tables[5];
                foreach (DataRow dr in dtProjects.Rows)
                {
                    projectModel = new ProjectModel() { 
                    ProjectID=Convert.ToInt32(dr["ProjectID"]),
                    ProjectName=dr["ProjectName"].ToString(),
                    ProjectDesc=dr["ProjectDesc"].ToString()
                    };
                    
                }
                userProfile.brands = brandModel;
                userProfile.projects = projectModel;
                DataTable dtUsers = dsUserProfile.Tables[6];
                foreach (DataRow dr in dtUsers.Rows)
                {
                    _userModel = new UserModel();
                    _userModel.UserDetailsID = Convert.ToInt16(dr["UserDetailsID"]);
                    _userModel.FirstName = dr["name"].ToString();
                    users.Add(_userModel);
                }
                userProfile.users = users;
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
        public int EditUserProfile(UserModel userModel)
        {
          return  _dbUserProfile.EditUserProfile(userModel);
        }
        public int insertUserBrand(BrandModel brandModel, int userDetailsId)
        {
            return _dbUserProfile.insertUserBrand(brandModel,userDetailsId);
        }
        public int insertUserProject(ProjectModel brandModel, int userDetailsId)
        {
            return _dbUserProfile.insertUserProject(brandModel, userDetailsId);
        }
    }
}
