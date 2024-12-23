﻿using SimpleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService.Interfaces
{
    public interface IEmailSenderRepository : IRepository<EmailSubscription>
    {
    }
}
