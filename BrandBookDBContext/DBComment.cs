using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BrandBookModel;
namespace BrandBookDBContext
{
   public class DBComment:DBContext
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
        public DBComment()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBComment public method

        public DataTable  SaveComment(CommentModel commentModel)
        {
            _spName = "Comments_SaveComment";
            SqlParameter sqlparam = new SqlParameter("@CommentID", commentModel.CommentID);
            sqlparam.Direction = ParameterDirection.InputOutput;
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@StatusID",commentModel.StatusID),
                new SqlParameter("@CommentedByID",commentModel.CommentedByUserID),
                sqlparam,
                new SqlParameter("@CommentType",commentModel.CommentType),
                new SqlParameter("@CommentContent",commentModel.CommentContent),
                new SqlParameter("@CreatedDate",commentModel.CreatedDate)
            };
            DataTable result = ExecuteDataTable(_spName, _spParameters);
            commentModel.CommentID = Convert.ToInt32(sqlparam.Value);
            return result;
        }
        #endregion
    }
}
