﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class Repository
    {
        protected readonly ComuseContext db;
        protected readonly IMapper mapper;
        public Repository(ComuseContext comuseContext, IMapper mapper)
        {
            db = comuseContext;
            this.mapper = mapper;
        }
    }
}
