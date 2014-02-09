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
       public BrandModel brands { get; set; }
       public ProjectModel projects { get; set; }
       public List<UserModel> users { get; set; }
    }
}
