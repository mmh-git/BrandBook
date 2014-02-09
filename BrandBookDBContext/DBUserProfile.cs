using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BrandBookModel;

namespace BrandBookDBContext
{
    public class DBUserProfile:DBContext
    {
         #region private property

        private SqlDataReader _dataReader;
        private SqlParameter[] _spParameters;
        private DataTable _dataTable;
        private DataSet _dataSet;
        private string _spName;

        #endregion private property

        #region Constructor

        // default constructor
        public DBUserProfile()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBUserProfile public method

        public DataSet GetUserProfile(UserModel userModel)
        {
            _spName = "sp_getUserProfile";
            _dataSet = new DataSet();
            _spParameters = new SqlParameter[]{
                new SqlParameter("@UserDetailsID",userModel.UserDetailsID)
						};
            _dataSet = ExecuteDataSet(_spName, _spParameters);
            return _dataSet;
        }

        public int EditUserProfile(UserModel userModel)
        {
            _spName = "sp_editUserInfo";
            SqlParameter sqlparam = new SqlParameter("@UserDetailsID", userModel.UserDetailsID);
            _spParameters = new SqlParameter[] 
            {
                sqlparam,
                new SqlParameter("@FirstName",userModel.FirstName),
                new SqlParameter("@LastName",userModel.LastName),
                new SqlParameter("@Address",userModel.Address),
                
                new SqlParameter("@Designation",userModel.Desination),
                new SqlParameter("@Mobile",userModel.Mobile),
                new SqlParameter("@Extension",userModel.Extention)
            };
            int result = ExecuteNoResult(_spName, _spParameters);

            return result;
        }

        public DataTable getBrandList(int userDetailsId)
        {
            _spName = "sp_getBrandList";
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@UserDetailsID",userDetailsId)
            };
            _dataTable = ExecuteDataTable(_spName, _spParameters);
            return _dataTable;
        }
        public DataTable getProjectList(int userDetailsId)
        {
            _spName = "sp_getProjectList";
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@UserDetailsID",userDetailsId)
            };
            _dataTable = ExecuteDataTable(_spName, _spParameters);
            return _dataTable;
        }
        public int insertUserBrand(BrandModel brandModel,int userDetailsId)
        {
            _spName = "sp_insertBrands";
            SqlParameter sqlparam = new SqlParameter("@UserDetailsID", userDetailsId);
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@brandid", brandModel.BrandId),
                sqlparam,
                new SqlParameter("@brandName",brandModel.BrandName),
                new SqlParameter("@brandDesc",brandModel.BrandDesc)
                
            };
            int result = ExecuteNoResult(_spName, _spParameters);

            return result;
        }
        public int insertUserProject(ProjectModel brandModel, int userDetailsId)
        {
            _spName = "sp_insertProjects";
            SqlParameter sqlparam = new SqlParameter("@UserDetailsID", userDetailsId);
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@projectid", brandModel.ProjectID),
                sqlparam,
                new SqlParameter("@projectName",brandModel.ProjectName),
                new SqlParameter("@projectDesc",brandModel.ProjectDesc)
                
            };
            int result = ExecuteNoResult(_spName, _spParameters);

            return result;
        }
        #endregion DBUser public method
    }
}
