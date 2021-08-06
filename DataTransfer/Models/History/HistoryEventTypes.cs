using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Models.History
{
    public enum HistoryEventTypes
    {
        GENERAL = 0,

        CATEGORY_CREATED = 1,
        CATEGORY_DELETED = 2,
        CATEGORY_EDITED = 3,

        ROLE_ASSIGNED = 101,
        ROLE_CREATED = 102,
        ROLE_EDITED = 103,

        SPACE_CREATED = 201,
        SPACE_EDITED = 202,
        SPACE_IMAGE_CHANGED = 203,

        TASK_CATEGORY_CHANGED = 301,
        TASK_CREATED = 302,
        TASK_DELETED = 303,
        TASK_EDITED = 304,

        TASK_STATUS_CHANGED = 401,
        TASK_STATUS_CREATED = 402,
        TASK_STATUS_DELETED = 403,
        TASK_STATUS_EDITED = 404,

        USER_JOINED = 501,
        USER_LEFT = 502
    }
}
