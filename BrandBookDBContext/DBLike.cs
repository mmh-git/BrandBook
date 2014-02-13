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

        public DataTable  SaveLike(LikeModel likeModel)
        {
            _spName = "sp_SaveDeleteLike";
           
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@LikedContentId",likeModel.LikedContentID),
                new SqlParameter("@UserID",likeModel.LikedByUserID),
                new SqlParameter("@LikedContentType",likeModel.LikedContentType)
            };
            DataTable dt = ExecuteDataTable(_spName, _spParameters);
            
            return dt;
        }

        public DataTable GetLikedUserCollection(int LikedContentId)
        {
            _spName = "sp_getLikesListByContentId";

            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@LikedContentId",LikedContentId)
                
            };
            DataTable dt = ExecuteDataTable(_spName, _spParameters);

            return dt;
        }
        #endregion
    }
}
