using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrandBookDBContext;
using BrandBookModel;
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
            return _dbComment.SaveComment(commentModel);
        }
    }
}
