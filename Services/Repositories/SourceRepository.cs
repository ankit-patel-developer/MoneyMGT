using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.Interfaces;

namespace Services.Repositories
{
    public class SourceRepository : ISourceRepository
    {
        public BankTransaction BankInputFromSource(BankTransaction bankTransaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Source> GetAllSources()
        {
            throw new NotImplementedException();
        }
    }
}
