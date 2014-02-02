using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
   public class UserProfile
    {
       public UserModel user { get; set; }
       public List<StatusUpdateModel> statusUpdates { get; set; }
       public List<BrandModel> brands { get; set; }
       public List<ProjectModel> projects { get; set; }
    }
}
