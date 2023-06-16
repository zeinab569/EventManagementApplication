﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Interfaces.IGenericRepo;

namespace Core.Interfaces
{
    public interface ITicketPurchasesRepo:IGenericRepo<TicketPurchase>
    {
    }
}
