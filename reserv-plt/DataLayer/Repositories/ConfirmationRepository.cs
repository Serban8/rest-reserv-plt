﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ConfirmationRepository : RepositoryBase<Confirmation>
    {
        public ConfirmationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
