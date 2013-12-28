using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BrandBookModel;
namespace BrandBookDBContext
{
    public class DBLike:DBContext
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
        public DBLike()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBComment public method

        public LikeModel  SaveLike(LikeModel likeModel)
        {
            _spName = "Likes_SaveLike";
            SqlParameter sqlparam = new SqlParameter("@LikID", likeModel.LikeID);
            sqlparam.Direction = ParameterDirection.InputOutput;
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@likedContentID",likeModel.LikedContentID),
                new SqlParameter("@LikedByUSerID",likeModel.LikedByUserID),
                sqlparam,
                new SqlParameter("@LikedContentType",likeModel.LikedContentType),
                new SqlParameter("@CreatedDate",likeModel.CreatedDate)
            };
            int result = ExecuteNoResult(_spName, _spParameters);
            likeModel.LikeID = Convert.ToInt32(sqlparam.Value);
            return likeModel;
        }
        #endregion
    }
}
