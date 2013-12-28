using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public class LikeModel
    {
        public int LikeID { get; set; }
        public int LikedByUserID { get; set; }
        public string LikedByUserFullName { get; set; }
        public int LikedContentID { get; set; }
        public string LikedContentType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
