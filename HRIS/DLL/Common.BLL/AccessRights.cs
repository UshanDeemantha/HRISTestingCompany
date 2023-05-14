using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM
{
    public enum AccessRights
    {        
        None = 0,
        ViewOnly = 1,
        AddOnly = 2,
        EditOnly = 3,
        DeleteOnly = 4,
        FullControl = 5
    };
}
