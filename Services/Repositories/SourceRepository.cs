using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.Interfaces;

namespace Services.Repositories
{
    public class SourceRepository : ISourceRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public SourceRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Source> GetAllSources()
        {
            return appDbContext.Sources;
        }
    }
}
