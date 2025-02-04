using System;
using System.Collections.Generic;
using System.Text;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Repositories
{
    public class EntityMonitorRepository : IEntityMonitorRepository
    {
        public IEnumerable<AccountMonthly> MonitorAccountMonthly(AccountMonthlyRequest accountMonthlyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
