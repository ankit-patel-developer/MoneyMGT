using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IEntityMonitorRepository
    {
        IEnumerable<AccountMonthly> MonitorAccountMonthly(AccountMonthlyRequest accountMonthlyRequest);
    }
}
