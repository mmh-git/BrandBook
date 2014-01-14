using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public  class ImageModel
    {
        public int ImageID { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImgDesc { get; set; }
        public int UserDetailsID { get; set; }
        public string Action { get; set; }
        
    }
}
