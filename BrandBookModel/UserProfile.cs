using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    class UserProfile
    {
        UserModel user { get; set; }
        List<StatusUpdateModel> statusUpdates { get; set; }
    }
}
