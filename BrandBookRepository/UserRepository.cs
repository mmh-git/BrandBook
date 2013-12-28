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
            //try
            //{
                DataRow dr = _dbUser.GetUserDetails(userModel).Rows[0];
               
                    _userModel = new UserModel()
                    {
                        UserID = dr["UserId"].ToString(),
                        UserDetailsID = Convert.ToInt32(dr["UserDetailsID"]),
                        Address = dr["Address"].ToString(),
                        City = dr["City"].ToString(),
                        Country = dr["Country"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        ProfilePicID = dr["proPicId"].ToString(),
                        ProfilePicUrl = dr["ImageUrl"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DOB"]),
                        Desination = dr["Designation"].ToString(),
                        Email = dr["Email"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        UserName = dr["UserName"].ToString()
                    };
                    
                
            //}
            //catch (Exception exception)
            //{
            //    //
            //}

            return _userModel;
        }

        public UserModel SaveUserDetails(UserModel userModel)
        {
            return _dbUser.SaveUserDetails(userModel);
        }
       
    }
}
