using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public  class StatusUpdateModel
    {
        public int StatusID { get; set; }
        public string StatusType { get; set; }
        public string StatusContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserID { get; set; }
        public int StatusByUserID { get; set; }
        public string FullName { get; set; }
        public int PicID { get; set; }
        public string ProPicUrl { get; set; }
        public int fileID { get; set; }
        public string fileDesc { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<LikeModel> Likes { get; set; }
    }
}
