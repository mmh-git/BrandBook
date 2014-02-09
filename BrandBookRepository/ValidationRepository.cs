using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using System.Data;
namespace BrandBookRepository
{
  public  class ValidationRepository
    {
       DBValidation _dbValidation;
        public ValidationRepository()
        {
            _dbValidation = new DBValidation();
        }
        public int ValidateUserName(string username)
        {
            DataTable result=_dbValidation.ValidateUserName(username);
            return Convert.ToInt32(result.Rows[0][0]);
        }
        public int ValidateEmail(string email)
        {
            DataTable result= _dbValidation.ValidateEmail(email);
            return Convert.ToInt32(result.Rows[0][0]);
        }
    }
}
