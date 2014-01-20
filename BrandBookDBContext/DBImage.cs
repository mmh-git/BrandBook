using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BrandBookModel;
namespace BrandBookDBContext
{
   public class DBImage:DBContext
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
        public DBImage()
        {
            this._dataReader = null;
            this._spParameters = null;
            this._dataTable = null;
            this._dataSet = null;
            this._spName = null;
        }

        #endregion Constructor

        #region DBStatus public method

        public DataSet GetStatusList(ImageModel imageModel)
        {
            _spName = "statusUpdate_getStatusList";
            _dataTable = new DataTable();
            _spParameters = new SqlParameter[]{
						};
            _dataSet = ExecuteDataSet(_spName, _spParameters);
            return _dataSet;
        }

        public DataTable SaveImage(ImageModel imageModel)
        {
            _spName = "sp_SaveDeleteImage";
            _spParameters = new SqlParameter[] 
            {
                new SqlParameter("@ImageID",imageModel.ImageID),
                new SqlParameter("@UserDetailsID",imageModel.UserDetailsID),
                new SqlParameter("@ImageUrl",imageModel.ImageUrl),
                new SqlParameter("@ImgDesc",imageModel.ImgDesc),
                new SqlParameter("@Action",imageModel.Action)
            };
            _dataTable = ExecuteDataTable(_spName, _spParameters);
            return _dataTable;
        }
        #endregion DBStatus public method
    }
}
