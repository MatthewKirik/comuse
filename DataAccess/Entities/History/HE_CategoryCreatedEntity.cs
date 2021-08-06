﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_CategoryCreatedEntity : HistoryEventEntity
    {
        public int? CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
