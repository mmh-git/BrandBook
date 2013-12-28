using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BrandBookModel;
namespace BrandBookDBContext
{
   public class DBStatus:DBContext
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
        public DBStatus()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBStatus public method

        public DataSet GetStatusList(StatusUpdateModel statusUpdateModel)
        {
            _spName = "statusUpdate_getStatusList";
            _dataTable = new DataTable();
            _spParameters = new SqlParameter[]{
						};
            _dataSet = ExecuteDataSet(_spName, _spParameters);
            return _dataSet;
        }

        public DataTable SaveStatus(StatusUpdateModel statusUpdateModel)
        {
            _spName = "StatusUPdate_SaveStatus";
            SqlParameter sqlparam = new SqlParameter("@StatusID", statusUpdateModel.StatusID);
            sqlparam.Direction = ParameterDirection.InputOutput;
            _spParameters = new SqlParameter[] 
            {
                sqlparam,
                new SqlParameter("@UserID",statusUpdateModel.UserID),
                new SqlParameter("@UserDetailsID",statusUpdateModel.StatusByUserID),
                new SqlParameter("@StatusType",statusUpdateModel.StatusType),
                new SqlParameter("@StatusContent",statusUpdateModel.StatusContent),
                new SqlParameter("@CreatedDate",DateTime.Now)
            };
            _dataTable = ExecuteDataTable(_spName, _spParameters);
            statusUpdateModel.StatusID = Convert.ToInt32(sqlparam.Value);
            return _dataTable;
        }
        #endregion DBStatus public method
    }
}
