using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;

namespace BrandBookRepository
{
    public class UserRepository
    {
        private DBUser _dbUser;


        // default constructor
        public UserRepository()
        {
            _dbUser = new DBUser();
        }

        public UserModel GetUserDetails(UserModel userModel)
        {
            UserModel _userModel=new UserModel();
            try
            {
                DataRow dr = _dbUser.GetUserDetails(userModel).Rows[0];
               
                    _userModel = new UserModel();
                    
                       _userModel.UserID = dr["UserId"].ToString();
                         _userModel.UserDetailsID = Convert.ToInt32(dr["UserDetailsID"]);
                         _userModel.Address = dr["Address"].ToString();

                         _userModel.Extention = dr["Extension"].ToString();
                         _userModel.FirstName = dr["FirstName"].ToString();
                         _userModel.LastName = dr["LastName"].ToString();
                         _userModel.ProfilePicID = dr["proPicId"].ToString();
                         _userModel.ProfilePicUrl = dr["ImageUrl"].ToString();
                         _userModel.Desination = dr["Designation"].ToString();
                         _userModel.Email = dr["Email"].ToString();
                         _userModel.Gender = dr["Gender"].ToString();
                         _userModel.Mobile = dr["Mobile"].ToString();
                         _userModel.UserName = dr["UserName"].ToString();
                    


            }
            catch (Exception exception)
            {
                //
            }

            return _userModel;
        }

        public UserModel SaveUserDetails(UserModel userModel)
        {
            return _dbUser.SaveUserDetails(userModel);
        }
       
    }
}
