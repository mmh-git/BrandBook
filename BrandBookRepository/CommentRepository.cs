using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;
using System.Data;
namespace BrandBookRepository
{
    public class CommentRepository
    {
        DBComment _dbComment;
        public CommentRepository()
        {
            _dbComment = new DBComment();
        }
        public CommentModel SaveComment(CommentModel commentModel)
        {
            DataTable dt=_dbComment.SaveComment(commentModel);
            DataRow dr = dt.Rows[0];

            commentModel.CommentID = Convert.ToInt32(dr["CommentID"]);
            commentModel.StatusID = Convert.ToInt32(dr["StatusID"]);
            commentModel.CommentedByUserID = Convert.ToInt32(dr["CommentedByID"]);
            commentModel.CommentType = dr["CommentType"].ToString();
            commentModel.CommentContent = dr["CommentContent"].ToString();
            commentModel.CommentedByUserFullName = dr["CommentedByUserFullName"].ToString();
            commentModel.CommentedByUserProPicID = Convert.ToInt32(dr["ImageID"]);
            commentModel.CommentedByUserProPicUrl = dr["ImageUrl"].ToString();
            commentModel.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
            return commentModel;

        }
    }
}
