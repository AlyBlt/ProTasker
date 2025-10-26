using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Domain.Enums
{
    public enum TaskActionType
    {
        Created = 1,
        Updated = 2,
        Assigned = 3,
        StatusChanged = 4,
        Completed = 5,
        Commented = 6
    }
}
