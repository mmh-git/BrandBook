using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public class CommentModel
    {
        public int StatusID { get; set; }
        public int CommentID { get; set; }
        public int CommentedByUserID { get; set; }
        public string CommentedByUserFullName { get; set; }
        public int CommentedByUserProPicID { get; set; }
        public string CommentedByUserProPicUrl { get; set; }
        public List<LikeModel> Likes { get; set; }

        public string CommentType { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Action { get;set; }
    }
}
