using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BrandBookDBContext
{
    public class DBValidation:DBContext
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
        public DBValidation()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBImage public method

        public DataTable ValidateUserName(string userName)
        {
            _spName = "ValidateUser";
            _dataTable = new DataTable();
            _spParameters = new SqlParameter[] { new SqlParameter("@userName",userName) };
            DataTable result = ExecuteDataTable(_spName,_spParameters);
            return result;
        }

        public DataTable ValidateEmail(string email)
        {
            _spName = "ValidateUser";
            _dataTable = new DataTable();
            _spParameters = new SqlParameter[] { 
            new SqlParameter("@userName",string.Empty),
            new SqlParameter("@email", email) };
            DataTable result = ExecuteDataTable(_spName, _spParameters);
            return result;
        }
        #endregion DBStatus public method
    }
}
